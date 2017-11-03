using System;
using System.Collections.Generic;

namespace DatabaseRepoLib.Interfaces
{
    public interface IDatabaseRepo
    {
        object Save(object model);

        object GetObject(string searchString, object model);

        ExecuteCodes Update(object model);

        List<object> GetAll(object model);
    }
    public enum ExecuteCodes
    {
        FailedToExecute,
        SuccessToExecute
    }
}
