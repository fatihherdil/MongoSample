using System;
using System.Collections.Generic;
using System.Text;

namespace MongoSample.Application.Interfaces
{
    public interface IResponse
    {
        string Status { get; set; }

        int ResponseCode { get; set; }

        object Response { get; set; }
    }

}
