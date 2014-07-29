using System;
using System.Collections.Generic;

using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Settings {
	
	//A dataset that potentially holds defaultSettings
	public abstract class MauronCode_defaultSettingsObject:MauronCode,I_Singleton, I_dataObject {
		
		public MauronCode_defaultSettingsObject():base(CodeType_defaultSettingsObject.Instance){}
		
		//has this object been initialized?
		private bool B_initialized=false;
		public bool Initialized {
			get { return B_initialized; }
		}
		public MauronCode_defaultSettingsObject SetInitialized(bool status){
			B_initialized = status;
			return this;
		}
		
		#region Default Settings
		internal MauronCode_dataSet Data=new MauronCode_dataSet();
		public virtual object GetDefault(string key, object obj) {
			if(!Data.HasKey(key)){
				throw new MauronCode_error("Invalid Default Key { string "+key+" }",this);
			}
			return Data[key];
		}
		public virtual MauronCode_defaultSettingsObject SetDefault(string key, object obj) {
			Data.Store(key,obj);
			return this;
		}
		public virtual MauronCode_defaultSettingsObject SetDefaults(KeyValuePair<string,object>[] values){
			Data.Store(values);
			return this;
		}
		#endregion

		//the name
		private string STR_name;
		public string Name { get { return STR_name; } }
		public MauronCode_defaultSettingsObject SetName(string name) {
			STR_name=name;
			return this;
		}

		#region I_dataObject
		public abstract string[] PropertyKeys { get; }
		#endregion

	}

	//Code Description
	public sealed class CodeType_defaultSettingsObject : CodeType {
		#region singleton
		private static volatile CodeType_defaultSettingsObject instance=new CodeType_defaultSettingsObject();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_defaultSettingsObject ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_defaultSettingsObject();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "defaultSettingsObject"; } }

	}


}
