using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.HandlingData {

	//A data array that maps a string to a generic
	public class MauronCode_dataMap<T>:MauronCode_dataObject,IDictionary<string,T> {

		public MauronCode_dataMap():base(DataType_dataDictionary.Instance){}

		//Data
		private Dictionary<string,T> DIC_data=new Dictionary<string,T>();
		public Dictionary<string,T> Data {
			get { return DIC_data; }
		}
		public MauronCode_dataMap<T> SetData(Dictionary<string,T> data) {
			DIC_data=data;
			return this;
		}

		//Count
		public int Count { get { return Data.Count; } }

		//values
		public T Value(string key) {
			return Data[key];
		}
		public ICollection<T> Values {
			get {
				return Data.Values;
			}
		}		
		public MauronCode_dataMap<T> SetValue(string key, T value){
			Data[key]=value;
			return this;
		}

		//Get all keys
		public ICollection<string> Keys {
			get {
				return Data.Keys;
			}
		}

		//read Only (false)
		private bool B_isReadOnly=false;
		public bool IsReadOnly { get { return B_isReadOnly; } }

		//Remove
		public MauronCode_dataMap<T> RemoveByKey (string key) {
			Data.Remove(key);
			return this;
		}
		public MauronCode_dataMap<T> Clear ( ) {
			SetData(new Dictionary<string, T>());
			return this;
		}

		//Contains
		public bool ContainsKey (string key) {
			return Data.ContainsKey(key);
		}
		public bool ContainsValue (T item) {
			foreach( KeyValuePair<string, T> d in Data ) {
				if( d.Value.Equals(item) ) {
					return true;
				}
			}
			return false;
		}

		#region IDictionary

		#region ICollection
		ICollection<string> IDictionary<string, T>.Keys {
			get { return Keys; }
		}

		ICollection<T> IDictionary<string, T>.Values {
			get { return Values; }
		}

		void ICollection<KeyValuePair<string, T>>.Add (KeyValuePair<string, T> item) {
			SetValue(item.Key,item.Value);
		}

		void ICollection<KeyValuePair<string, T>>.Clear ( ) {
			Clear();
		}

		bool ICollection<KeyValuePair<string, T>>.Contains (KeyValuePair<string, T> item) {
			//remember this is !notprecise
			return ContainsKey(item.Key);
		}

		void ICollection<KeyValuePair<string, T>>.CopyTo (KeyValuePair<string, T>[] array, int arrayIndex) {
			int index=0;
			foreach(KeyValuePair<string,T> d in Data){
				array[index]=d;
				index++;
			}	
		}

		int ICollection<KeyValuePair<string, T>>.Count {
			get { return Count; }
		}

		bool ICollection<KeyValuePair<string, T>>.IsReadOnly {
			get { return IsReadOnly; }
		}

		bool ICollection<KeyValuePair<string, T>>.Remove (KeyValuePair<string, T> item) {
			if(!ContainsKey(item.Key)){
				return false;
			}
			RemoveByKey(item.Key);
			return true;
		}
		#endregion

		void IDictionary<string, T>.Add (string key, T value) {
			SetValue(key,value);
		}
		bool IDictionary<string, T>.ContainsKey (string key) {
			return ContainsKey(key);
		}
		bool IDictionary<string, T>.Remove (string key) {
			if(!ContainsKey(key)){
				return false;
			}
			RemoveByKey(key);
			return true;
		}
		bool IDictionary<string, T>.TryGetValue (string key, out T value) {
			return Data.TryGetValue(key, out value);
		}
		T IDictionary<string, T>.this[string key] {
			get {
				return Value(key);
			}
			set {
				SetValue(key,value);
			}
		}
		IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator ( ) {
			return Data.GetEnumerator();
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) {
			return Data.GetEnumerator();
		}
		#endregion


	}	
}
