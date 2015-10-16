﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChimetDataAnalysis.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="WeatherData")]
	public partial class WeatherDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertWeatherStation(WeatherStation instance);
    partial void UpdateWeatherStation(WeatherStation instance);
    partial void DeleteWeatherStation(WeatherStation instance);
    partial void InsertWeatherEntry(WeatherEntry instance);
    partial void UpdateWeatherEntry(WeatherEntry instance);
    partial void DeleteWeatherEntry(WeatherEntry instance);
    #endregion
		
		public WeatherDataDataContext() : 
				base(global::ChimetDataAnalysis.Properties.Settings.Default.WeatherDataConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public WeatherDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WeatherDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WeatherDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WeatherDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<WeatherStation> WeatherStations
		{
			get
			{
				return this.GetTable<WeatherStation>();
			}
		}
		
		public System.Data.Linq.Table<WeatherEntry> WeatherEntries
		{
			get
			{
				return this.GetTable<WeatherEntry>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.WeatherStation")]
	public partial class WeatherStation : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _OID;
		
		private string _Name;
		
		private EntityRef<WeatherEntry> _WeatherEntry;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOIDChanging(int value);
    partial void OnOIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public WeatherStation()
		{
			this._WeatherEntry = default(EntityRef<WeatherEntry>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int OID
		{
			get
			{
				return this._OID;
			}
			set
			{
				if ((this._OID != value))
				{
					this.OnOIDChanging(value);
					this.SendPropertyChanging();
					this._OID = value;
					this.SendPropertyChanged("OID");
					this.OnOIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NChar(10)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="WeatherStation_WeatherEntry", Storage="_WeatherEntry", ThisKey="OID", OtherKey="OID", IsUnique=true, IsForeignKey=false)]
		public WeatherEntry WeatherEntry
		{
			get
			{
				return this._WeatherEntry.Entity;
			}
			set
			{
				WeatherEntry previousValue = this._WeatherEntry.Entity;
				if (((previousValue != value) 
							|| (this._WeatherEntry.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._WeatherEntry.Entity = null;
						previousValue.WeatherStation1 = null;
					}
					this._WeatherEntry.Entity = value;
					if ((value != null))
					{
						value.WeatherStation1 = this;
					}
					this.SendPropertyChanged("WeatherEntry");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.WeatherEntry")]
	public partial class WeatherEntry : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _OID;
		
		private System.Nullable<double> _WindSpeed;
		
		private System.Nullable<int> _WindBearing;
		
		private System.Nullable<double> _WindGust;
		
		private System.Nullable<double> _AirTemperature;
		
		private System.Nullable<double> _WaterTemperature;
		
		private System.Nullable<double> _AirPressure;
		
		private int _WeatherStation;
		
		private System.DateTime _Timestamp;
		
		private EntityRef<WeatherStation> _WeatherStation1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOIDChanging(int value);
    partial void OnOIDChanged();
    partial void OnWindSpeedChanging(System.Nullable<double> value);
    partial void OnWindSpeedChanged();
    partial void OnWindBearingChanging(System.Nullable<int> value);
    partial void OnWindBearingChanged();
    partial void OnWindGustChanging(System.Nullable<double> value);
    partial void OnWindGustChanged();
    partial void OnAirTemperatureChanging(System.Nullable<double> value);
    partial void OnAirTemperatureChanged();
    partial void OnWaterTemperatureChanging(System.Nullable<double> value);
    partial void OnWaterTemperatureChanged();
    partial void OnAirPressureChanging(System.Nullable<double> value);
    partial void OnAirPressureChanged();
    partial void OnWeatherStationChanging(int value);
    partial void OnWeatherStationChanged();
    partial void OnTimestampChanging(System.DateTime value);
    partial void OnTimestampChanged();
    #endregion
		
		public WeatherEntry()
		{
			this._WeatherStation1 = default(EntityRef<WeatherStation>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int OID
		{
			get
			{
				return this._OID;
			}
			set
			{
				if ((this._OID != value))
				{
					if (this._WeatherStation1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnOIDChanging(value);
					this.SendPropertyChanging();
					this._OID = value;
					this.SendPropertyChanged("OID");
					this.OnOIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WindSpeed", DbType="Float")]
		public System.Nullable<double> WindSpeed
		{
			get
			{
				return this._WindSpeed;
			}
			set
			{
				if ((this._WindSpeed != value))
				{
					this.OnWindSpeedChanging(value);
					this.SendPropertyChanging();
					this._WindSpeed = value;
					this.SendPropertyChanged("WindSpeed");
					this.OnWindSpeedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WindBearing", DbType="Int")]
		public System.Nullable<int> WindBearing
		{
			get
			{
				return this._WindBearing;
			}
			set
			{
				if ((this._WindBearing != value))
				{
					this.OnWindBearingChanging(value);
					this.SendPropertyChanging();
					this._WindBearing = value;
					this.SendPropertyChanged("WindBearing");
					this.OnWindBearingChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WindGust", DbType="Float")]
		public System.Nullable<double> WindGust
		{
			get
			{
				return this._WindGust;
			}
			set
			{
				if ((this._WindGust != value))
				{
					this.OnWindGustChanging(value);
					this.SendPropertyChanging();
					this._WindGust = value;
					this.SendPropertyChanged("WindGust");
					this.OnWindGustChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AirTemperature", DbType="Float")]
		public System.Nullable<double> AirTemperature
		{
			get
			{
				return this._AirTemperature;
			}
			set
			{
				if ((this._AirTemperature != value))
				{
					this.OnAirTemperatureChanging(value);
					this.SendPropertyChanging();
					this._AirTemperature = value;
					this.SendPropertyChanged("AirTemperature");
					this.OnAirTemperatureChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WaterTemperature", DbType="Float")]
		public System.Nullable<double> WaterTemperature
		{
			get
			{
				return this._WaterTemperature;
			}
			set
			{
				if ((this._WaterTemperature != value))
				{
					this.OnWaterTemperatureChanging(value);
					this.SendPropertyChanging();
					this._WaterTemperature = value;
					this.SendPropertyChanged("WaterTemperature");
					this.OnWaterTemperatureChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AirPressure", DbType="Float")]
		public System.Nullable<double> AirPressure
		{
			get
			{
				return this._AirPressure;
			}
			set
			{
				if ((this._AirPressure != value))
				{
					this.OnAirPressureChanging(value);
					this.SendPropertyChanging();
					this._AirPressure = value;
					this.SendPropertyChanged("AirPressure");
					this.OnAirPressureChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WeatherStation", DbType="Int NOT NULL")]
		public int WeatherStation
		{
			get
			{
				return this._WeatherStation;
			}
			set
			{
				if ((this._WeatherStation != value))
				{
					this.OnWeatherStationChanging(value);
					this.SendPropertyChanging();
					this._WeatherStation = value;
					this.SendPropertyChanged("WeatherStation");
					this.OnWeatherStationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Timestamp", DbType="DateTime NOT NULL")]
		public System.DateTime Timestamp
		{
			get
			{
				return this._Timestamp;
			}
			set
			{
				if ((this._Timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._Timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="WeatherStation_WeatherEntry", Storage="_WeatherStation1", ThisKey="OID", OtherKey="OID", IsForeignKey=true)]
		public WeatherStation WeatherStation1
		{
			get
			{
				return this._WeatherStation1.Entity;
			}
			set
			{
				WeatherStation previousValue = this._WeatherStation1.Entity;
				if (((previousValue != value) 
							|| (this._WeatherStation1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._WeatherStation1.Entity = null;
						previousValue.WeatherEntry = null;
					}
					this._WeatherStation1.Entity = value;
					if ((value != null))
					{
						value.WeatherEntry = this;
						this._OID = value.OID;
					}
					else
					{
						this._OID = default(int);
					}
					this.SendPropertyChanged("WeatherStation1");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
