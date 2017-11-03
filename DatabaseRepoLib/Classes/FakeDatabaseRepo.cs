using DatabaseRepoLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseRepoLib.Classes
{
    public class FakeDatabaseRepo : IDatabaseRepo
    {
        public List<object> GetAll(object model)
        {
            throw new NotImplementedException();
        }

        public object GetObject(string searchString, object model)
        {
            throw new NotImplementedException();
        }

        public object Save(object model)
        {
            throw new NotImplementedException();
        }

        public ExecuteCodes Update(object model)
        {
            throw new NotImplementedException();
        }
    }
}
