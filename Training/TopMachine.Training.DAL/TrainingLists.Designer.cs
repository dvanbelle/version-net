﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace TopMachine.Training.DAL
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class TrainingEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new TrainingEntities object using the connection string found in the 'TrainingEntities' section of the application configuration file.
        /// </summary>
        public TrainingEntities() : base("name=TrainingEntities", "TrainingEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TrainingEntities object.
        /// </summary>
        public TrainingEntities(string connectionString) : base(connectionString, "TrainingEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TrainingEntities object.
        /// </summary>
        public TrainingEntities(EntityConnection connection) : base(connection, "TrainingEntities")
        {
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Config> Configs
        {
            get
            {
                if ((_Configs == null))
                {
                    _Configs = base.CreateObjectSet<Config>("Configs");
                }
                return _Configs;
            }
        }
        private ObjectSet<Config> _Configs;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Session> Sessions
        {
            get
            {
                if ((_Sessions == null))
                {
                    _Sessions = base.CreateObjectSet<Session>("Sessions");
                }
                return _Sessions;
            }
        }
        private ObjectSet<Session> _Sessions;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Word> Words
        {
            get
            {
                if ((_Words == null))
                {
                    _Words = base.CreateObjectSet<Word>("Words");
                }
                return _Words;
            }
        }
        private ObjectSet<Word> _Words;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Configs EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToConfigs(Config config)
        {
            base.AddObject("Configs", config);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Sessions EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToSessions(Session session)
        {
            base.AddObject("Sessions", session);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Words EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToWords(Word word)
        {
            base.AddObject("Words", word);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TrainingModel", Name="Config")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Config : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Config object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="xMLConfig">Initial value of the XMLConfig property.</param>
        /// <param name="xMLPlay">Initial value of the XMLPlay property.</param>
        public static Config CreateConfig(global::System.Int64 id, global::System.String xMLConfig, global::System.String xMLPlay)
        {
            Config config = new Config();
            config.ID = id;
            config.XMLConfig = xMLConfig;
            config.XMLPlay = xMLPlay;
            return config;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int64 _ID;
        partial void OnIDChanging(global::System.Int64 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String XMLConfig
        {
            get
            {
                return _XMLConfig;
            }
            set
            {
                OnXMLConfigChanging(value);
                ReportPropertyChanging("XMLConfig");
                _XMLConfig = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("XMLConfig");
                OnXMLConfigChanged();
            }
        }
        private global::System.String _XMLConfig;
        partial void OnXMLConfigChanging(global::System.String value);
        partial void OnXMLConfigChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String XMLPlay
        {
            get
            {
                return _XMLPlay;
            }
            set
            {
                OnXMLPlayChanging(value);
                ReportPropertyChanging("XMLPlay");
                _XMLPlay = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("XMLPlay");
                OnXMLPlayChanged();
            }
        }
        private global::System.String _XMLPlay;
        partial void OnXMLPlayChanging(global::System.String value);
        partial void OnXMLPlayChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TrainingModel", Name="Session")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Session : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Session object.
        /// </summary>
        /// <param name="dateTime">Initial value of the DateTime property.</param>
        /// <param name="roundsFound">Initial value of the RoundsFound property.</param>
        /// <param name="roundsTotal">Initial value of the RoundsTotal property.</param>
        /// <param name="wordsFound">Initial value of the WordsFound property.</param>
        /// <param name="wordsTotal">Initial value of the WordsTotal property.</param>
        /// <param name="totalTime">Initial value of the TotalTime property.</param>
        public static Session CreateSession(global::System.Guid dateTime, global::System.Int64 roundsFound, global::System.Int64 roundsTotal, global::System.Int64 wordsFound, global::System.Int64 wordsTotal, global::System.Int64 totalTime)
        {
            Session session = new Session();
            session.DateTime = dateTime;
            session.RoundsFound = roundsFound;
            session.RoundsTotal = roundsTotal;
            session.WordsFound = wordsFound;
            session.WordsTotal = wordsTotal;
            session.TotalTime = totalTime;
            return session;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid DateTime
        {
            get
            {
                return _DateTime;
            }
            set
            {
                if (_DateTime != value)
                {
                    OnDateTimeChanging(value);
                    ReportPropertyChanging("DateTime");
                    _DateTime = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("DateTime");
                    OnDateTimeChanged();
                }
            }
        }
        private global::System.Guid _DateTime;
        partial void OnDateTimeChanging(global::System.Guid value);
        partial void OnDateTimeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 RoundsFound
        {
            get
            {
                return _RoundsFound;
            }
            set
            {
                OnRoundsFoundChanging(value);
                ReportPropertyChanging("RoundsFound");
                _RoundsFound = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RoundsFound");
                OnRoundsFoundChanged();
            }
        }
        private global::System.Int64 _RoundsFound;
        partial void OnRoundsFoundChanging(global::System.Int64 value);
        partial void OnRoundsFoundChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 RoundsTotal
        {
            get
            {
                return _RoundsTotal;
            }
            set
            {
                OnRoundsTotalChanging(value);
                ReportPropertyChanging("RoundsTotal");
                _RoundsTotal = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RoundsTotal");
                OnRoundsTotalChanged();
            }
        }
        private global::System.Int64 _RoundsTotal;
        partial void OnRoundsTotalChanging(global::System.Int64 value);
        partial void OnRoundsTotalChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 WordsFound
        {
            get
            {
                return _WordsFound;
            }
            set
            {
                OnWordsFoundChanging(value);
                ReportPropertyChanging("WordsFound");
                _WordsFound = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("WordsFound");
                OnWordsFoundChanged();
            }
        }
        private global::System.Int64 _WordsFound;
        partial void OnWordsFoundChanging(global::System.Int64 value);
        partial void OnWordsFoundChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 WordsTotal
        {
            get
            {
                return _WordsTotal;
            }
            set
            {
                OnWordsTotalChanging(value);
                ReportPropertyChanging("WordsTotal");
                _WordsTotal = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("WordsTotal");
                OnWordsTotalChanged();
            }
        }
        private global::System.Int64 _WordsTotal;
        partial void OnWordsTotalChanging(global::System.Int64 value);
        partial void OnWordsTotalChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 TotalTime
        {
            get
            {
                return _TotalTime;
            }
            set
            {
                OnTotalTimeChanging(value);
                ReportPropertyChanging("TotalTime");
                _TotalTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TotalTime");
                OnTotalTimeChanged();
            }
        }
        private global::System.Int64 _TotalTime;
        partial void OnTotalTimeChanging(global::System.Int64 value);
        partial void OnTotalTimeChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TrainingModel", Name="Word")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Word : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Word object.
        /// </summary>
        /// <param name="rack">Initial value of the Rack property.</param>
        /// <param name="lost">Initial value of the Lost property.</param>
        /// <param name="succeeded">Initial value of the Succeeded property.</param>
        /// <param name="total">Initial value of the Total property.</param>
        /// <param name="status">Initial value of the Status property.</param>
        /// <param name="wordsXML">Initial value of the WordsXML property.</param>
        public static Word CreateWord(global::System.String rack, global::System.Int32 lost, global::System.Int32 succeeded, global::System.Int32 total, global::System.Int32 status, global::System.String wordsXML)
        {
            Word word = new Word();
            word.Rack = rack;
            word.Lost = lost;
            word.Succeeded = succeeded;
            word.Total = total;
            word.Status = status;
            word.WordsXML = wordsXML;
            return word;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Rack
        {
            get
            {
                return _Rack;
            }
            set
            {
                if (_Rack != value)
                {
                    OnRackChanging(value);
                    ReportPropertyChanging("Rack");
                    _Rack = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("Rack");
                    OnRackChanged();
                }
            }
        }
        private global::System.String _Rack;
        partial void OnRackChanging(global::System.String value);
        partial void OnRackChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Lost
        {
            get
            {
                return _Lost;
            }
            set
            {
                OnLostChanging(value);
                ReportPropertyChanging("Lost");
                _Lost = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Lost");
                OnLostChanged();
            }
        }
        private global::System.Int32 _Lost;
        partial void OnLostChanging(global::System.Int32 value);
        partial void OnLostChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Succeeded
        {
            get
            {
                return _Succeeded;
            }
            set
            {
                OnSucceededChanging(value);
                ReportPropertyChanging("Succeeded");
                _Succeeded = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Succeeded");
                OnSucceededChanged();
            }
        }
        private global::System.Int32 _Succeeded;
        partial void OnSucceededChanging(global::System.Int32 value);
        partial void OnSucceededChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Total
        {
            get
            {
                return _Total;
            }
            set
            {
                OnTotalChanging(value);
                ReportPropertyChanging("Total");
                _Total = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Total");
                OnTotalChanged();
            }
        }
        private global::System.Int32 _Total;
        partial void OnTotalChanging(global::System.Int32 value);
        partial void OnTotalChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Status
        {
            get
            {
                return _Status;
            }
            set
            {
                OnStatusChanging(value);
                ReportPropertyChanging("Status");
                _Status = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Status");
                OnStatusChanged();
            }
        }
        private global::System.Int32 _Status;
        partial void OnStatusChanging(global::System.Int32 value);
        partial void OnStatusChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String WordsXML
        {
            get
            {
                return _WordsXML;
            }
            set
            {
                OnWordsXMLChanging(value);
                ReportPropertyChanging("WordsXML");
                _WordsXML = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("WordsXML");
                OnWordsXMLChanged();
            }
        }
        private global::System.String _WordsXML;
        partial void OnWordsXMLChanging(global::System.String value);
        partial void OnWordsXMLChanged();

        #endregion
    
    }

    #endregion
    
}
