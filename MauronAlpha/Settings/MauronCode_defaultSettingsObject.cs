using System;
using System.Collections.Generic;

using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

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
		private MauronCode_dataDictionary DS_data;
		internal MauronCode_dataDictionary Data { 
			get { 
				if(DS_data==null) {
					DS_data=new MauronCode_dataDictionary(Name);
				}
				return DS_data;
			}
		}
		
		public virtual object GetDefault(string key, object obj) {
			if(!Data.ContainsKey(key)){
				throw Error("Invalid Default Key!,{ string "+key+" },(GetDefault)",this,ErrorType_index.Instance);
			}
			return Data.Value(key);
		}
		public virtual MauronCode_defaultSettingsObject SetDefault(string key, object obj) {
			Data.SetValue(key,obj);
			return this;
		}
		public virtual MauronCode_defaultSettingsObject SetDefaults(ICollection<KeyValuePair<string,object>> values){
			Data.SetValue(values);
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
