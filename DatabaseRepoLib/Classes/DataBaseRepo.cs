using DatabaseRepoLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace DatabaseRepoLib.Classes
{
    public class DataBaseRepo : IDatabaseRepo
    {
        public enum MethodType
        {
            Save,
            GetOne,
            GetAll,
            Update
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;" +
                                    "Initial Catalog=OOAD;" +
                                    "Integrated Security=True;" +
                                    "Connect Timeout=30;" +
                                    "Encrypt=False;" +
                                    "TrustServerCertificate=True;" +
                                    "ApplicationIntent=ReadWrite;" +
                                    "MultiSubnetFailover=False");
        }

        private SqlCommand CreateCommand()
        {
            return new SqlCommand();
        }

        private DatabaseHolder DataBaseHandle(object model, MethodType type, string search = null, string[] updateValues = null)
        {

            var properties = "";
            var propertiesName = "";
            var values = "";
            var modelId = "";
            var counter = model.GetType().GetProperties().Length;
            var loopcounter = 0;
            var databaseHolder = new DatabaseHolder { ExecuteCodes = ExecuteCodes.SuccessToExecute };

            var con = CreateConnection();
            con.Open();

            var command = CreateCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;

            foreach (var prop in model.GetType().GetProperties())
            {
                if (loopcounter < counter - 1 && !prop.Name.ToLower().Contains($"{model.GetType().Name.ToLower()}id"))
                {
                    propertiesName += $"@{prop.Name},";
                    properties += $"{prop.Name},";
                    if (type != MethodType.GetOne && type != MethodType.GetAll)
                    {
                        object p = prop.GetValue(model).ToString();
                        values += p.ToString() + ",";
                    }
                }
                if (loopcounter == counter - 1 && !prop.Name.ToLower().Contains($"{model.GetType().Name.ToLower()}id"))
                {
                    propertiesName += $"@{prop.Name}";
                    properties += $"{prop.Name}";
                    if (type != MethodType.GetOne && type != MethodType.GetAll)
                    {
                        object p = prop.GetValue(model).ToString();
                        values += p.ToString();
                    }
                }
                else
                {
                    if (type != MethodType.GetOne && type != MethodType.GetAll)
                    {
                        modelId = prop.GetValue(model).ToString();
                    }
                    if (loopcounter == 0 && (type == MethodType.GetAll || type == MethodType.GetOne))
                    {
                        properties += $"{prop.Name},";
                    }


                }
                ++loopcounter;
            }
            var result = CreateSqlSyntax(type, model.GetType().Name, properties, propertiesName, search, values, modelId, command);
            command = result.Command;
            command.CommandText = result.Result;

            if (type == MethodType.Save || type == MethodType.Update)
            {
                try
                {
                    var command2 = CreateCommand();
                    command2.Connection = con;
                    command2.CommandType = CommandType.Text;
                    command2.CommandText = "select @@Identity";
                    command.ExecuteNonQuery();
                    databaseHolder.PrimaryKey = Convert.ToInt32(command2.ExecuteScalar());
                }
                catch (Exception e)
                {
                    var a = e;
                    databaseHolder.ExecuteCodes = ExecuteCodes.FailedToExecute;
                }
                finally
                {
                    con.Close();
                }
            }
            else if (type == MethodType.GetOne)
            {
                try
                {
                    command.ExecuteNonQuery();

                    var reader = command.ExecuteReader();

                    var list = model.GetType().GetProperties();

                    while (reader.Read())
                    {
                        for (int i = 0; i < list.Length; i++)
                        {
                            list[i].SetValue(model, reader[reader.GetName(i)]);
                        }
                    }
                    databaseHolder.Model = model;
                    reader.Close();
                }
                catch (Exception e)
                {
                    var a = e;
                    databaseHolder.ExecuteCodes = ExecuteCodes.FailedToExecute;
                }
                finally
                {
                    con.Close();
                }
            }
            else if(type == MethodType.GetAll)
            {
                try
                {
                    //TODO Bygg upp en lista av object och retunera.
                    command.ExecuteNonQuery();

                    var reader = command.ExecuteReader();

                    var list = model.GetType().GetProperties();
                    while (reader.Read())
                    {
                        var newModel = Activator.CreateInstance(model.GetType());
                        for (int i = 0; i < list.Length; i++)
                        {
                            object value = reader[reader.GetName(i)];
                            list[i].SetValue(newModel, value);
                        }
                        databaseHolder.List.Add(newModel);
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    var a = e;
                    databaseHolder.ExecuteCodes = ExecuteCodes.FailedToExecute;
                }
                finally
                {
                    con.Close();
                }
            }

            return databaseHolder;
        }

        private SqlSyntax CreateSqlSyntax(MethodType type, string modelName, string props, string propNames, string search = null, string values = null, string modelId = null, SqlCommand command = null)
        {
            var result = new SqlSyntax();
            var propSplit = props.Split(',');
            switch (type)
            {
                case MethodType.Save:
                    result.Result = $"Insert into [dbo].[{modelName}] ({props}) values ({propNames})";
                    var splitPropName = propNames.Split(',');
                    var splitValues = values.ToString().Split(',');
                    for (int i = 0; i < splitPropName.Length; i++)
                    {
                        command.Parameters.AddWithValue(splitPropName[i], splitValues[i]);
                    }
                    result.Command = command;
                    break;

                case MethodType.GetOne:
                    result.Result = $"Select";
                    for (int i = 0; i < propSplit.Length; i++)
                    {
                        if (i == 0)
                        {
                            result.Result += $" {modelName}Id,";
                        }
                        else if (i <= propSplit.Length - 2)
                        {
                            result.Result += $" {propSplit[i]},";
                        }
                        else
                        {
                            result.Result += $" {propSplit[i]}";
                        }
                    }
                    result.Result += $" from [dbo].[{ modelName}] where { modelName}Id = { search}";
                    result.Command = command;
                    break;

                case MethodType.GetAll:
                    result.Result = $"Select * from [dbo].[{modelName}]";
                    result.Command = command;
                    break;

                case MethodType.Update:
                    var valueSplit = values.Split(',');

                    result.Result = $"Update [dbo].[{modelName}] Set ";

                    for (int i = 0; i < valueSplit.Length; i++)
                    {
                        if (i != valueSplit.Length - 1)
                        {
                            result.Result += $"{propSplit[i]} = {valueSplit[i]},";
                        }
                        else
                        {
                            result.Result += $"{propSplit[i]} = {valueSplit[i]} ";
                        }
                    }
                    result.Result += $"Where {modelName}Id = {modelId}";
                    result.Command = command;
                    break;

                default:
                    break;
            }

            return result;
        }

        public object Save(object model)
        {
            var result = DataBaseHandle(model, MethodType.Save);

            return result;
        }

        //TODO denna ska ju vara i databasen så, lägga till id från klassen.
        public ExecuteCodes Update(object model)
        {
            var result = DataBaseHandle(model, MethodType.Update);

            return result.ExecuteCodes;
        }

        //TODO Lägg till ett sök ord som ska följa med
        public object GetObject(string searchString, object model)
        {

            var result = DataBaseHandle(model, MethodType.GetOne, searchString);

            return result.Model;
        }

        public List<object> GetAll(object model)
        {
            var result = DataBaseHandle(model, MethodType.GetAll);

            return result.List;
        }

        public class DatabaseHolder
        {
            public List<object> List { get; set; } = new List<object>();
            public object Model { get; set; }
            public ExecuteCodes ExecuteCodes { get; set; }
            public int PrimaryKey { get; set; }
        }

        internal class SqlSyntax
        {
            public string Result { get; set; }
            public SqlCommand Command { get; set; }
        }
    }
}
