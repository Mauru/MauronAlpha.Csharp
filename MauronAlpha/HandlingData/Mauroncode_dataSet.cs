using System;
using System.Collections.Generic;

namespace MauronAlpha.HandlingData {

	//Base class for variable data storrage (i.e. storing object by string keys, retrieving by type)
	public class MauronCode_dataSet : MauronCode, I_dataSet {
		public MauronCode_dataSet ( ) : base(CodeType_dataset.Instance) { }
		public string Name;

		//stores data and type
		private Dictionary<string, object> Data=new Dictionary<string, object>();
		private Dictionary<string, Type> TypeKey=new Dictionary<string, Type>();

		//Length
		public int Length { get { return Data.Count; } }

		//Remove an item from the dataset
		public object Remove(string s){
			if(!Data.ContainsKey(s)){
				return null;
			}
			object o=Data[s];
			Data.Remove(s);
			TypeKey.Remove(s);
			return o;
		}

		//return the Data ordered by String
		public List<KeyValuePair<string,object>> OrderByString {
			get {
				List<KeyValuePair<string,object>>data=AsList;
				data.Sort(
					delegate(KeyValuePair<string, object> firstPair,
					KeyValuePair<string, object> nextPair) {
						return firstPair.Key.CompareTo(nextPair.Key);
					}
				);
				return data;
			}
		}
		public List<KeyValuePair<string,object>> AsList { get {
			List<KeyValuePair<string,object>> ret = new List<KeyValuePair<string,object>>();
			foreach(string key in Data.Keys){
				ret.Add(new KeyValuePair<string,object>(key,Data[key]));
			}
			return ret;
		} }

		//data[s]
		public object this[string s] {
			get {
				if( !Data.ContainsKey(s) ) {
					return null;
				}
				return s;
			}
			set {
				TypeKey[s]=value.GetType();
				Data[s]=value;
			}
		}
		public object Get(string s) {
			return Data[s];
		}
		public MauronCode_dataSet Store(string s, object o){
			this[s]=o;
			return this;
		}
		public MauronCode_dataSet Store(KeyValuePair<string,object>[] values){
			foreach(KeyValuePair<string,object> val in values){
				Store(val.Key,val.Value);
			}
			return this;
		}

#region things involving Type
		//TypeKey[s]
		public Type TypeOf (string s) {
			return TypeKey[s];
		}
		//TypeKey[s]
		public Type GetType (string s) {
			return TypeKey[s];
		}
		//data[s],null if typeKey[s]=t
		public object this[Type t, string s] {
			get {
				if( TypeKey[s]!=t ) {
					return null;
				}
				return this[s];
			}
			set {
				TypeKey[s]=value.GetType();
				this[s]=value;
			}
		}
		//Get by Type
		public Stack<object> ByType (Type t) {
			Stack<object> r=new Stack<object>();
			foreach( string s in TypeKey.Keys ) {
				if( TypeKey[s]==t ) {
					r.Push(Data[s]);
				}
			}
			return r;
		}
#endregion

#region boolean checks
		//Has [s]
		public bool HasKey (string s) {
			return Data.ContainsKey(s);
		}

		//Has data[s]
		public bool HasValue (object o) {
			return Data.ContainsValue(o);
		}

		//Has type t
		public bool HasType (Type t) {
			return TypeKey.ContainsValue(t);
		}
#endregion

	}

	//A class that offers exclusively static functions
	public sealed class CodeType_dataset : CodeType {
		#region singleton
		private static volatile CodeType_dataset instance=new CodeType_dataset();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_dataset ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_dataset();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataset"; } }

	}

}
