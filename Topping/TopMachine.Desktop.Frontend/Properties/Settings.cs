namespace TopMachine.Topping.Frontend.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("0, 0")]
        public Point GridWindowLocation
        {
            get
            {
                return (Point) this["GridWindowLocation"];
            }
            set
            {
                this["GridWindowLocation"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("800, 600")]
        public Size GridWindowSize
        {
            get
            {
                return (Size) this["GridWindowSize"];
            }
            set
            {
                this["GridWindowSize"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string LocalUser
        {
            get
            {
                return (string) this["LocalUser"];
            }
            set
            {
                this["LocalUser"] = value;
            }
        }

        [UserScopedSetting, DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" />"), DebuggerNonUserCode]
        public StringCollection MyFriends
        {
            get
            {
                return (StringCollection) this["MyFriends"];
            }
            set
            {
                this["MyFriends"] = value;
            }
        }

        [DebuggerNonUserCode, DefaultSettingValue(""), UserScopedSetting]
        public string OnlinePassword
        {
            get
            {
                return (string) this["OnlinePassword"];
            }
            set
            {
                this["OnlinePassword"] = value;
            }
        }

        [DefaultSettingValue(""), DebuggerNonUserCode, UserScopedSetting]
        public string OnlineUser
        {
            get
            {
                return (string) this["OnlineUser"];
            }
            set
            {
                this["OnlineUser"] = value;
            }
        }

        [DefaultSettingValue("501"), DebuggerNonUserCode, UserScopedSetting]
        public int WidthOfGrid
        {
            get
            {
                return (int) this["WidthOfGrid"];
            }
            set
            {
                this["WidthOfGrid"] = value;
            }
        }
    }
}

