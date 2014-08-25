using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Topping.Dto;
using Topping.Core.Data;

  

namespace Topping.Web.Security.Tools
{
    public partial class ConvertDB : Form
    {
        private ToppingAccessor _taSqlLite;
        private Core.Data.Db4o.ToppingAccessor _tadb4o;

        public ConvertDB()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            LoadFileSqlLite(txtPathSqlLite.Text);
            LoadFileDb4o(txtPathSqlLite.Text);
            SaveConfiguration();
            SaveUser(); 
            _tadb4o.Dispose();
            _taSqlLite.Dispose();  
        }

        private void SaveUser()
        {
            List<user> lst = _taSqlLite.GetUsers();

            db4oProviders.User u;
          
            foreach (var item in lst)
            {
                u = new db4oProviders.User();

                u.ApplicationName = item.ApplicationName;
                u.Comment = item.Comment;
                u.CreationDate = item.CreationDate.Value;
                u.Email = item.Email;
                u.LastLoginDate = item.LastLoginDate.Value;
                u.Username = item.Username;
                u.PKID = item.PKID;
                u.Password = item.Password;
                u.IsApproved = true;
                u.IsLockedOut = false;
                u.IsOnLine = false; 
 
                _tadb4o.UpdateUsers(u,null,null);
            }
        }
        private void SaveConfiguration() 
        {

            List<Topping.Core.Data.GameConfig> lst = _taSqlLite.GetConfigurationsPrimary();

            Topping.Core.Data.Db4o.GameConfig gc;
     
            foreach (var item in lst)
            {
                gc = new Core.Data.Db4o.GameConfig();
                gc.Id = item.Id;
                gc.XmlConfig = item.XmlConfig; 

                _tadb4o.AddConfiguration(gc);  
            }
             
        }

        private void LoadFileDb4o(string path)
        {
            path = path + "o";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);    
            }    
            _tadb4o = new Core.Data.Db4o.ToppingAccessor(path);
             
            //List<ConfigGameDto> lst = _tadb4o.GetConfigurations();
            //List<db4oProviders.User> lstu = _tadb4o.GetUsers();

        }
        private void LoadFileSqlLite(string path) 
        {
            _taSqlLite = new Core.Data.ToppingAccessor(path);
            List<Topping.Core.Data.GameConfig> lst = _taSqlLite.GetConfigurationsPrimary();
            List<Core.Data.user> lstUser = _taSqlLite.GetUsers();  

            
        
        }
        private void btnPath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult dr = OFDPathDB.ShowDialog() ;   
            if ( dr == System.Windows.Forms.DialogResult.OK) 
            {
                txtPathSqlLite.Text = OFDPathDB.FileName;
                          
            }
   

        }
    }
}
