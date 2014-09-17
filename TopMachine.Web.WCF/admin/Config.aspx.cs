using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Topping.Core.Data.Db4o;
using TopMachine.Topping.Dto;
using System.Reflection;

namespace TopMachine.Web.WCF
{
    public partial class Config : System.Web.UI.Page
    {
        List<ConfigGameDto> lists;
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ToppingAccessor ta = new ToppingAccessor())
            {
                lists = ta.GetConfigurations();
                lists.Add(new ConfigGameDto());
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            GridView1.DataKeyNames = new string[] {"Id"}; 
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
            ConfigGameDto dto = new ConfigGameDto();
            Type dtoType = dto.GetType();

           
            foreach (string k in e.NewValues.Keys)
            {
                if (e.NewValues[k] != null)
                {
                    if (k != "Id")
                    {
                        PropertyInfo pi = dtoType.GetProperty(k);
                        object o = System.Convert.ChangeType(e.NewValues[k], pi.PropertyType);
                        pi.SetValue(dto, o, null);
                    }
                }
            }

            dto.Id = new Guid(e.Keys[0].ToString()); 
            using (ToppingAccessor ta = new ToppingAccessor())
            {
                ta.UpdateConfiguration(dto); 
                lists = ta.GetConfigurations();
                lists.Add(new ConfigGameDto()); 
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

                ConfigGameDto dto = new ConfigGameDto();
                dto = lists[e.RowIndex];

                if (dto != null) 
                {
                    ta.DeleteConfiguration(dto);
                    lists.Remove(dto);
                 
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