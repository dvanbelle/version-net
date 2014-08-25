using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TopMachine.Web.WCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDeleteServices" in both code and config file together.
    [ServiceContract]
    public interface IDeleteServices
    {
        [OperationContract]
        bool DeleteGameXML(string fn);
    }
}
