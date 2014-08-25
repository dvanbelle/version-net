
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ServiceProcess;
using System.Net;
using System.Security.Principal; 


namespace TopMachine.Web.WCF.admin
{
    public partial class Service : System.Web.UI.Page
    {
        ServiceController sc;
        protected void Page_Load(object sender, EventArgs e)
        {

            sc = new ServiceController();
            sc.ServiceName = "CMWA.TopMachine.Services";
            LblMsgError.Text =  "The Alerter service " + sc.ServiceName  + " status is currently set to " + sc.Status.ToString();
        }

        protected void btnstart_Click(object sender, EventArgs e)
        {

            // Check whether the Alerter service is started.

          

            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                // Start the service if the current status is stopped.
                LblMsgError.Text = "Starting the Alerter service...";
                try
                {
                    // Start the service, and wait until its status is "Running".
                    sc.Start();
                   TimeSpan timeout = TimeSpan.FromMilliseconds(100);
                   sc.WaitForStatus(ServiceControllerStatus.Running,timeout);

                    // Display the current service status.
                    LblMsgError.Text = "The Alerter service status is now set to " + sc.Status.ToString();
                }
                catch (InvalidOperationException ex)
                {
                    LblMsgError.Text = ex.Message;   
                }
            }
                      
          
        }

        protected void btnStop_Click(object sender, EventArgs e)
        {
            // Check whether the Alerter service is started.
                      
            if (sc.Status != ServiceControllerStatus.Stopped)
            {
                // Start the service if the current status is stopped.
                LblMsgError.Text = "Stoping the Alerter service...";
                try
                {
                    // Start the service, and wait until its status is "Running".
                    sc.Stop(); 
                    TimeSpan timeout = TimeSpan.FromMilliseconds(100);
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    
                    // Display the current service status.
                    LblMsgError.Text = "The Alerter service status is now set to " + sc.Status.ToString();
                }
                catch (InvalidOperationException ex)
                {
                    LblMsgError.Text = ex.Message;
                }
            }

        }
              
    }
}