//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TopMachine.Training.DAL.fdbo
{
    public partial class Word
    {
        #region Primitive Properties
    
        public virtual string Rack
        {
            get;
            set;
        }
    
        public virtual int Lost
        {
            get;
            set;
        }
    
        public virtual int Succeeded
        {
            get;
            set;
        }
    
        public virtual int Total
        {
            get;
            set;
        }
    
        public virtual int Status
        {
            get;
            set;
        }
    
        public virtual string WordsXML
        {
            get;
            set;
        }

        #endregion
    }
}