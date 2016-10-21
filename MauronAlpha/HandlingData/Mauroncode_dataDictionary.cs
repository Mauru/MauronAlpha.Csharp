using System;
using System.Collections.Generic;

using MauronAlpha.ExplainingCode;

using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//Base class for variable data storrage (i.e. storing object by string keys, retrieving by type)
	public class MauronCode_dataDictionary : MauronCode_dataObject, I_dataDictionary,I_dataObject {

		//constructor
		public MauronCode_dataDictionary (string name): base() { 
			SetName(name);
		}
		
		//name 
		private string STR_name;
		public string Name { get { return STR_name; } }
		public MauronCode_dataDictionary SetName (string name) {
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
		public MauronCode_dataDictionary SetData (Dictionary<string, KeyValuePair<object, Type>> data) {
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
				throw Error("Key not found!,{"+key+"}!",this,ErrorType_index.Instance);
			}
			return Data[key].Key;
		}
		public T Value<T>(string key){
			if(!Data.ContainsKey(key)){
				throw Error("Key not found!,{"+key+"}!",this,ErrorType_index.Instance);
			}
			return (T) Data[key].Key;
		}

		//Set the value of a dataSet by Key
		public MauronCode_dataDictionary SetValue<T>(string key, T value) {
			Add(
				new KeyValuePair<string,KeyValuePair<object,Type>>(
					key, 
					new KeyValuePair<object,Type>(value, typeof(T))
				)
			);
			return this;
		}
		public MauronCode_dataDictionary SetValue(string key, object value) {
			Add(
				new KeyValuePair<string, KeyValuePair<object, Type>>(
					key,
					new KeyValuePair<object, Type>(value, value.GetType())
				)
			);
			return this;
		}
		public MauronCode_dataDictionary SetValue(ICollection<string> keys, ICollection<object> values) {
			if(keys.Count!=values.Count) {
				throw Error("Keys and Values need Same Length!,(SetValue),{"+keys.Count+","+values.Count+"}",this,ErrorType_outOfSynch.Instance);
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
		public MauronCode_dataDictionary SetValue(string key, KeyValuePair<object, Type> value){
			Add(
				new KeyValuePair<string,KeyValuePair<object,Type>>(key,value)
			);
			return this;
		}
		public MauronCode_dataDictionary SetValue(KeyValuePair<string,KeyValuePair<object,Type>> o){
			Add(o);
			return this;
		}
		public MauronCode_dataDictionary SetValue(ICollection<KeyValuePair<string,object>> values) {
			foreach(KeyValuePair<string,object> d in values){
				SetValue(d.Key,d.Value);
			}
			return this;
		}

		//Find any instances of o in Data and remove them
		public MauronCode_dataDictionary RemoveValue(object o){
			foreach(KeyValuePair<string,KeyValuePair<object,Type>> d in Data){
				if(d.Value.Key==o){
					Remove(d.Key);
				}
			}
			return this;
		}
		public MauronCode_dataDictionary RemoveValue(ICollection<object> o){
			foreach(object obj in o) {
				RemoveValue(obj);
			}
			return this;
		}
		#endregion

		//Add to the dictionary
		MauronCode_dataDictionary Add (KeyValuePair<string, KeyValuePair<object, Type>> value) {
			Data.Add(value.Key,value.Value);
			return this;
		}
		MauronCode_dataDictionary Add<T> (string key, T value) {
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
		public MauronCode_dataDictionary Remove (string key) {
			Data.Remove(key);
			return this;
		}
		public MauronCode_dataDictionary Remove (ICollection<string> keys) {
			foreach(string key in keys) {
				Data.Remove(key);
			}
			return this;
		}
		bool IDictionary<string, KeyValuePair<object, Type>>.Remove (string key) {
			return Data.Remove(key);
		}
		bool ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.Remove (KeyValuePair<string, KeyValuePair<object, Type>> item) {
			return Data.Remove(item.Key);
		}

		//Try and set IN value to value with key , return true if successful
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
		public MauronCode_dataDictionary Clear ( ) {
			SetData(new Dictionary<string, KeyValuePair<object, Type>>());
			return this;
		}
		void ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.Clear ( ) {
			Clear();
		}

		//Check if data Contains an object
		public bool Contains(object o) {
			foreach(KeyValuePair<string,KeyValuePair<object,Type>> d in Data) {
				if(o==d.Value.Key){
					return true;
				}
			}
			return false;
		}
		bool ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.Contains (KeyValuePair<string, KeyValuePair<object, Type>> item) {
			//fast check
			return Data.ContainsKey(item.Key);
			//more thorough check
			//return Data[item.Key].Key==item.Value.Key;
		}

		//Copy the data to another array, overwriting the dataset if necessary
		MauronCode_dataDictionary CopyTo (MauronCode_dataDictionary dataSet) {
			foreach(KeyValuePair<string, KeyValuePair<object, Type>> d in Data) {
				dataSet.Add(d);
			}
			return this;
		}
		//copy the data to another dataset, but keep a history of any conflicts when merging
		MauronCode_dataDictionary MergeTo (MauronCode_dataDictionary dataSet, ICollection<MergeConflict> conflicts) {
			foreach(KeyValuePair<string, KeyValuePair<object, Type>> d in Data){
				if(dataSet.ContainsKey(d.Key)){
					MergeConflict conflict = new MergeConflict(this,dataSet, d.Key ,dataSet.Value(d.Key));
					conflicts.Add(conflict);
				}
				CopyTo(dataSet);
			}
			return this;
		}


		//Copy FROM another dataset, overwriting Data if necessary
		MauronCode_dataDictionary CopyFrom (MauronCode_dataDictionary dataSet) {
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

		//How many items in data
		public int Count { get { return Data.Count; } }
		int ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.Count {
			get { return Data.Count; }
		}

		//Is data readonly (currently no)
		private bool B_isReadOnly=false;
		public bool IsReadOnly { get { return B_isReadOnly; } } 
		bool ICollection<KeyValuePair<string, KeyValuePair<object, Type>>>.IsReadOnly {
			get { return IsReadOnly; }
		}

		//The Enumerator for data (foreach)
		IEnumerator<KeyValuePair<string, KeyValuePair<object, Type>>> IEnumerable<KeyValuePair<string, KeyValuePair<object, Type>>>.GetEnumerator ( ) {
			return Data.GetEnumerator();
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) {
			return Data.GetEnumerator();
		}
	
	}

	//A class that offers exclusively static functions
	public sealed class DataType_dataDictionary : DataType {
		#region singleton
		private static volatile DataType_dataDictionary instance=new DataType_dataDictionary();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_dataDictionary ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dataDictionary();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataDictionary"; } }

	}

}
