using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TopMachine.Web.WCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DeleteServices" in code, svc and config file together.
    public class DeleteServices : IDeleteServices
    {
        public bool DeleteGameXML(string fn)
        {
            try
            {
                string nfn = System.Configuration.ConfigurationManager.AppSettings["SerializedGamesPath"] + "\\" + fn;
                System.IO.File.Delete(nfn);
            }
            catch (Exception)
            {
                ;
            }

            return true;

        }
    }
}
