using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TopMachine.Topping.Frontend.Properties
{
   

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), CompilerGenerated, DebuggerNonUserCode]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static Bitmap application_add
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("application_add", resourceCulture);
            }
        }

        internal static Bitmap application_delete
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("application_delete", resourceCulture);
            }
        }

        internal static Bitmap application_edit
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("application_edit", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static Bitmap database_refresh
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("database_refresh", resourceCulture);
            }
        }

        internal static string DefaultHTML
        {
            get
            {
                return ResourceManager.GetString("DefaultHTML", resourceCulture);
            }
        }

        internal static Bitmap lock_edit
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("lock_edit", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("TopMachine.Topping.Frontend.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap user_add
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("user_add", resourceCulture);
            }
        }

        internal static Bitmap user_delete
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("user_delete", resourceCulture);
            }
        }

        internal static Bitmap user_edit
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("user_edit", resourceCulture);
            }
        }
    }
}

