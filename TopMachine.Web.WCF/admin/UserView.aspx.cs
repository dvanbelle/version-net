using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Topping.Core.Data.Db4o ;
using TopMachine.Topping.Dto;
using System.Reflection;
using db4oProviders;
using Topping.Web.Security.db4o;
   
       

namespace TopMachine.Web.WCF
{
    public partial class UserView : System.Web.UI.Page
    {
        List<User> lists;
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ToppingAccessor ta = new ToppingAccessor())
            {
                lists = ta.GetUsers();
                lists.Add(new User());
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            GridView1.DataKeyNames = new string[] {"PKid"}; 
            GridView1.DataSource = lists;
            GridView1.DataBind();


            base.OnPreRender(e);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = false; 
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
           
        

            using (ToppingAccessor ta = new ToppingAccessor())
            {
                string str = e.Keys[0].ToString() ;
                Guid g = new Guid(str); 
                User dto = ta.GetUser(g);


                    Type dtoType = dto.GetType();
            foreach (string k in e.NewValues.Keys)
            {
                if (e.NewValues[k] != null)
                {
                    if (k != "PKid")
                    {
                        PropertyInfo pi = dtoType.GetProperty(k);
                        object o = System.Convert.ChangeType(e.NewValues[k], pi.PropertyType);
                        pi.SetValue(dto, o, null);
                    }
                }
            }

            //dto.PKID = new Guid(e.Keys[0].ToString()); 
                dto.Password = db4oMembershipProvider.GetEncryption(dto.Password);  
                ta.UpdateUsers(dto); 
                lists = ta.GetUsers();
                lists.Add(new User()); 
            }

        }

        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            e.KeepInEditMode = false; 
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (ToppingAccessor ta = new ToppingAccessor())
            {

                User u = new User();
                u = lists[e.RowIndex];

                if (u != null)
                {
                    ta.DeleteUsers(u);
                    lists.Remove(u);

                }
            }
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
          //  GridView1.Columns[2].Visible = false; 
        }
    }
}