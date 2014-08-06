using System;
using System.Collections.Generic;

using MauronAlpha.ExplainingCode;

namespace MauronAlpha.HandlingData {

	//Base class for variable data storrage (i.e. storing object by string keys, retrieving by type)
	public class MauronCode_dataSet : MauronCode_dataObject, I_dataSet,I_dataObject {

		//constructor
		public MauronCode_dataSet (string name) : base(DataType_dataSet.Instance) { 
			SetName(name);
		}
		
		//name 
		private string STR_name;
		public string Name { get { return STR_name; } }
		public MauronCode_dataSet SetName(string name) {
			STR_name=name;
			return this;
		}

		//the data
		private Dictionary<string, KeyValuePair<object,Type>> DIC_data;
		public Dictionary<string, KeyValuePair<object,Type>> Data { 
			get { 
				//initialize on demand
				if(DIC_data==null) {
					DIC_data=new Dictionary<string,KeyValuePair<object,Type>>();
				}
				return DIC_data;
			}
		}
		public MauronCode_dataSet SetData(Dictionary<string, KeyValuePair<object,Type>> data) {
			DIC_data=data;
			return this;
		}

		#region custom methods

		//Count by Type
		public int CountType(Type t) {
			int count=0;
			foreach(KeyValuePair<object,Type> d in Data.Values ){
				if(d.Value==t){
					count++;
				}
			}
			return count;
		}
		public int CountType(ICollection<Type> t) {
			int count=0;
			foreach( KeyValuePair<object, Type> d in Data.Values ) {
				if( t.Contains(d.Value) ) {
					count++;
				}
			}
			return count;
		}

		//Get all values with the designated Type and cast them as T
		public ICollection<T> ValuesByType<T>(Type t) {
			List<T> result= new List<T>();
			foreach(KeyValuePair<object,Type> d in Data.Values) {
				if(d.Value==t) {
					T obj = (T) d.Key;
					result.Add(obj);
				}
			}
			return result;
		}
		public ICollection<T> ValuesByType<T> (ICollection<Type> t) {
			List<T> result=new List<T>();
			foreach( KeyValuePair<object, Type> d in Data.Values ) {
				if( t.Contains(d.Value) ) {
					T obj=(T) d.Key;
					result.Add(obj);
				}
			}
			return result;
		}

		//get the value of a dataset by key
		public object Value(string key) {
			if(!Data.ContainsKey(key)){
				Error("Key not found {"+key+"}!",this);
			}
			return Data[key].Key;
		}
		public T Value<T>(string key){
			if(!Data.ContainsKey(key)){
				Error("Key not found {"+key+"}!",this);
			}
			return (T) Data[key].Key;
		}

		//Set the value of a dataSet by Key
		public MauronCode_dataSet SetValue<T>(string key, T value) {
			Add(
				new KeyValuePair<string,KeyValuePair<object,Type>>(
					key, 
					new KeyValuePair<object,Type>(value, typeof(T))
				)
			);
			return this;
		}
		public MauronCode_dataSet SetValue(string key, object value) {
			Add(
				new KeyValuePair<string, KeyValuePair<object, Type>>(
					key,
					new KeyValuePair<object, Type>(value, value.GetType())
				)
			);
			return this;
		}
		public MauronCode_dataSet SetValue(ICollection<string> keys, ICollection<object> values) {
			if(keys.Count!=values.Count) {
				Error("Keys and Values need Same Length! {"+keys.Count+","+values.Count+"}",this);
			}
			object[] v=new object[values.Count];
			string[] k=new string[keys.Count];
			keys.CopyTo(k,0);
			values.CopyTo(v,0);
			for(int i=0; i<k.Length;i++) {
				Add(
					new KeyValuePair<string, KeyValuePair<object, Type>>(
						k[i],
						new KeyValuePair<object, Type>(
							v[i],
							v[i].GetType()
						)
					)
				);
			}
			return this;
		}
		public MauronCode_dataSet SetValue(string key, KeyValuePair<object, Type> value){
			Add(
				new KeyValuePair<string,KeyValuePair<object,Type>>(key,value)
			);
			return this;
		}
		public MauronCode_dataSet SetValue(KeyValuePair<string,KeyValuePair<object,Type>> o){
			Add(o);
			return this;
		}
		
		//Remove the value of a dataSet by Value
		public MauronCode_dataSet RemoveValue(object o){
		
		}
		#endregion


		/*
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
		public MauronCode_dataSet Store(KeyValuePair<string,object> o){
			return Store(o.Key,o.Value);
		}
		public MauronCode_dataSet Store(KeyValuePair<string,object>[] values){
			foreach(KeyValuePair<string,object> val in values){
				Store(val);
			}
			return this;
		}
		public T Get<T>(string s) {
			Type t = typeof(T);
			if(typeof(T)!=TypeOf(s)){
				Error("Invalid Type Cast for key {"+s+"}",this);
			}
			return (T) Get(s);
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
		*/

		//Add to the dictionary
		MauronCode_dataSet Add(KeyValuePair<string,KeyValuePair<object,Type>> value){
			Data.Add(value.Key,value.Value);
			return this;
		}
		MauronCode_dataSet Add<T>(string key, T value){
			Data.Add(
				key,
				new KeyValuePair<object,Type>(value,typeof(T))
			);
			return this;
		}
		void IDictionary<string, KeyValuePair<object, Type>>.Add (string key, KeyValuePair<object, Type> value) {
			Data.Add(key,value);
		}
		void ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.Add (KeyValuePair<string, KeyValuePair<object, Type>> item) {
			Data.Add(item.Key, item.Value);
		}

		//Does the dictionary contain a key
		public bool ContainsKey(string key) {
			return Data.ContainsKey(key);
		}
		bool IDictionary<string, KeyValuePair<object, Type>>.ContainsKey (string key) {
			return ContainsKey(key);
		}

		//Get all the keys of Data
		public ICollection<string> Keys { get { return Data.Keys; } }
		ICollection<string> IDictionary<string, KeyValuePair<object, Type>>.Keys {
			get { return Data.Keys; }
		}

		//Remove an object from the DataSet By Key
		public MauronCode_dataSet Remove(string key) {
			Data.Remove(key);
			return this;
		}
		public MauronCode_dataSet Remove(ICollection<string> keys) {
			foreach(string key in keys) {
				Data.Remove(key);
			}
			return this;
		}
		bool IDictionary<string, KeyValuePair<object, Type>>.Remove (string key) {
			return Data.Remove(key);
		}

		//Try and set a value, return if successful
		public bool TryGet<T>(string key, out T value) {
			KeyValuePair<object,Type> result;
			bool success=Data.TryGetValue(key, out result);
			if(success){
				value = (T) result.Key;
				return true;
			}
			value=default(T);
			return false;
		}
		bool IDictionary<string, KeyValuePair<object, Type>>.TryGetValue (string key, out KeyValuePair<object, Type> value) {
			return Data.TryGetValue(key,out value);
		}

		//Get a list of all Values
		public ICollection<object> Values { get {
			List<object> result = new List<object>();
			foreach(KeyValuePair<object,Type> d in Data.Values){
				result.Add(d.Key);
			}
			return result;
		} }
		ICollection<KeyValuePair<object, Type>> IDictionary<string, KeyValuePair<object, Type>>.Values {
			get { return Data.Values; }
		}

		//Get a object by string
		KeyValuePair<object, Type> IDictionary<string, KeyValuePair<object, Type>>.this[string key] {
			get {
				return Data[key];
			}
			set {
				Data[key]=value;
			}
		}

		//Clear data
		public MauronCode_dataSet Clear() {
			SetData(new Dictionary<string, KeyValuePair<object, Type>>());
			return this;
		}
		void ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.Clear ( ) {
			Clear();
		}

		//Check if data Contains an object
		bool ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.Contains (KeyValuePair<string, KeyValuePair<object, Type>> item) {
			//fast check
			return Data.ContainsKey(item.Key);
			//more thorough check
			//return Data[item.Key].Key==item.Value.Key;
		}

		//Copy the data to another array, overwriting the dataset if necessary
		MauronCode_dataSet CopyTo(MauronCode_dataSet dataSet) {
			foreach(KeyValuePair<string, KeyValuePair<object, Type>> d in Data) {
				dataSet.Add(d);
			}
			return this;
		}
		//copy the data to another dataset, but keep a history of any conflicts when merging
		MauronCode_dataSet MergeTo(MauronCode_dataSet dataSet, ICollection<MergeConflict> conflicts){
			foreach(KeyValuePair<string, KeyValuePair<object, Type>> d in Data){
				if(dataSet.ContainsKey(d.Key)){
					MergeConflict conflict = new MergeConflict(this,dataSet, d.Key ,dataSet.Value(d.Key));
				}
			}
		}


		//Copy FROM another dataset, overwriting Data if necessary
		MauronCode_dataSet CopyFrom(MauronCode_dataSet dataSet) {
			foreach( KeyValuePair<string, KeyValuePair<object, Type>> d in dataSet.Data ) {
				Add(d);
			}
			return this;
		}
		void ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.CopyTo (KeyValuePair<string, KeyValuePair<object, Type>>[] array, int arrayIndex) {
			int index=arrayIndex;
			foreach(KeyValuePair<string, KeyValuePair<object, Type>> d in Data){
				array[index]=d;
				index++;
			}
		}

		int ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.Count {
			get { throw new NotImplementedException(); }
		}

		bool ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.IsReadOnly {
			get { throw new NotImplementedException(); }
		}

		bool ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.Remove (KeyValuePair<string, KeyValuePair<object, Type>> item) {
			throw new NotImplementedException();
		}

		IEnumerator<KeyValuePair<string, KeyValuePair<object, Type>>> IEnumerable<KeyValuePair<string, KeyValuePair<object, Type>>>.GetEnumerator ( ) {
			throw new NotImplementedException();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) {
			throw new NotImplementedException();
		}
	}

	//A class that offers exclusively static functions
	public sealed class DataType_dataSet : DataType {
		#region singleton
		private static volatile DataType_dataSet instance=new DataType_dataSet();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_dataSet ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dataSet();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataSet"; } }

	}

}
