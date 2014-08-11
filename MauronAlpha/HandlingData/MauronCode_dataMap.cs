using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.HandlingData {

	//A data array that maps a string to a generic
	public class MauronCode_dataMap<T>:MauronCode_dataObject,IDictionary<string,T> {

		public MauronCode_dataMap():base(DataType_dataSet.Instance){}

		//Data
		private Dictionary<string,T> DIC_data=new Dictionary<string,T>();
		public Dictionary<string,T> Data {
			get { return DIC_data; }
		}
		public MauronCode_dataMap<T> SetData(Dictionary<string,T> data) {
			DIC_data=data;
			return this;
		}


		//values
		public T Value(string key) {
			return Data[key];
		}
		public MauronCode_dataMap<T> SetValue(string key, T value){
			Data[key]=value;
			return this;
		}

		#region IDictionary

		//Add 
		public MauronCode_dataMap<T> Add(string key, T value) {
			SetValue(key, value);
			return this;
		}
		void IDictionary<string, T>.Add (string key, T value) {
			SetValue(key,value);
		}
		void ICollection<KeyValuePair<string, T>>.Add (KeyValuePair<string, T> item) {
			SetValue(item.Key, item.Value);
		}

		//ContainsKey
		public bool ContainsKey(string key){
			return Data.ContainsKey(key);
		}
		bool IDictionary<string, T>.ContainsKey (string key) {
			return ContainsKey(key);
		}

		//Get all keys
		public ICollection<string> Keys { get {
			return Data.Keys;
		} }
		ICollection<string> IDictionary<string, T>.Keys {
			get { return Keys; }
		}

		//Remove by Key
		public MauronCode_dataMap<T> Remove(string key) {
			Data.Remove(key);
			return this;
		}
		bool IDictionary<string, T>.Remove (string key) {
			return Data.Remove(key);
		}
		bool ICollection<KeyValuePair<string, T>>.Remove (KeyValuePair<string, T> item) {
			return Data.Remove(item.Key);
		}

		//Try and set value, return bool is availiable
		bool IDictionary<string, T>.TryGetValue (string key, out T value) {
			return Data.TryGetValue(key, out value);
		}

		//Get all values
		public ICollection<T> Values {
			get {
				return Data.Values;
			}
		}
		ICollection<T> IDictionary<string, T>.Values {
			get { return Values; }
		}

		//Set a value
		T IDictionary<string, T>.this[string key] {
			get {
				return Value(key);
			}
			set {
				SetValue(key,value);
			}
		}

		//Clear the array
		public MauronCode_dataMap<T> Clear() {
			SetData(new Dictionary<string,T>());
			return this;
		}
		void ICollection<KeyValuePair<string, T>>.Clear ( ) {
			Clear();
		}

		//Does the data contain an entry
		public bool Contains(T item){
			foreach(KeyValuePair<string,T> d in Data) {
				if(d.Value.Equals(item)){
					return true;
				}
			}
			return false;
		}
		bool ICollection<KeyValuePair<string, T>>.Contains (KeyValuePair<string, T> item) {
			return ContainsKey(item.Key);
		}

		//Copy to array
		void ICollection<KeyValuePair<string, T>>.CopyTo (KeyValuePair<string, T>[] array, int arrayIndex) {
			int index=arrayIndex;
			foreach( KeyValuePair<string, T> d in Data ) {
				array[index]=d;
				index++;
			}
		}

		//Count
		public int Count { get { return Data.Count; } }
		int ICollection<KeyValuePair<string, T>>.Count {
			get { throw new NotImplementedException(); }
		}

		//read Only (false)
		private bool B_isReadOnly=false;
		public bool IsReadOnly { get { return B_isReadOnly; } }
		bool ICollection<KeyValuePair<string, T>>.IsReadOnly {
			get { return IsReadOnly; }
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
