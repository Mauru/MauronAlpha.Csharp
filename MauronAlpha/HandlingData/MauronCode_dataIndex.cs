using System;
using System.Collections.Generic;

namespace MauronAlpha.HandlingData {

	//A data index is a numerical index of generics - it does not reindex its element like dataList
	public class MauronCode_dataIndex<T>:MauronCode_dataObject,IDictionary<long,T>,ICollection<T> {
		
		//constructor
		public MauronCode_dataIndex():base(DataType_dataIndex.Instance) {}

		//The data
		private Dictionary<long,T> DIC_data;
		public Dictionary<long,T> Data {
			get {
				if(DIC_data==null) {
					SetData(new Dictionary<long,T>());
				}
				return DIC_data;
			}
		}
		public MauronCode_dataIndex<T> SetData(Dictionary<long,T> data){
			DIC_data=data;
			return this;
		}

		#region Custom DataObject functions

		//Set a value
		public ICollection<T> Values {
			get {
				return Data.Values;
			}
		}
		public T Value(long key) {
			return Data[key];
		}
		public MauronCode_dataIndex<T> SetValue(long key, T value){
			Data.Add(key,value);
			return this;
		}

		//is the index ReadOnly
		public bool IsReadOnly { get { return false; } }

		//Does data contain a key
		public bool ContainsKey(long key){
			return Data.ContainsKey(key);
		}

		//Does data contain the specified object
		public bool ContainsValue(T item){
			return Data.ContainsValue(item);
		}

		//The keys of Data
		public ICollection<long> Keys { get {
			return Data.Keys;
		} }

		//The number of T in collection
		public int Count {
			get {
				return Data.Values.Count;
			}
		}

		//Remove a value by Key
		public MauronCode_dataIndex<T> RemoveByKey(long key){
			Data.Remove(key);
			return this;
		}

		//Clear
		public MauronCode_dataIndex<T> Clear() {
			SetData(new Dictionary<long,T>());
			return this;
		}

		#region Specific to this class

		public long NextIndex { 
			get {
				List<long> indexes = new List<long>(Data.Keys);
				if( indexes.Count>0 ) {
					return indexes[indexes.Count-1]+1;
				}
				return 0;
			}		
		}
		public long LastIndex {
			get{
				List<long> indexes = new List<long>(Keys);
				if(indexes.Count>0) {
					return indexes[indexes.Count-1];
				}
				return 0;
			}
		}
		public long FirstIndex {
			get {
				List<long> indexes = new List<long>(Keys);
				if(indexes.Count>0) {
					return indexes[0];
				}
				return 0;
			}
		}
		public long IndexOf(T item){
			long index=-1;
			foreach(KeyValuePair<long,T> d in Data) {
				if(d.Value.Equals(item)){
					return d.Key;
				}
			}
			Error("Item is not in index",this);
			return index;
		}

		#endregion

		#endregion

		#region IDictionary<long,T>
		void IDictionary<long, T>.Add (long key, T value) {
			SetValue(key, value);
		}

		bool IDictionary<long, T>.ContainsKey (long key) {
			return ContainsKey(key);
		}

		ICollection<long> IDictionary<long, T>.Keys {
			get { return Keys; }
		}

		bool IDictionary<long, T>.Remove (long key) {
			if(!ContainsKey(key)){
				return false;
			}
			RemoveByKey(key);
			return true;
		}

		bool IDictionary<long, T>.TryGetValue (long key, out T value) {
			if(ContainsKey(key)){
				value=Value(key);
				return true;
			}
			value=default(T);
			return false;
		}

		ICollection<T> IDictionary<long, T>.Values {
			get { return Values; }
		}

		T IDictionary<long, T>.this[long key] {
			get {
				return Value(key);
			}
			set {
				SetValue(key,value);
			}
		}

		void ICollection<KeyValuePair<long, T>>.Add (KeyValuePair<long, T> item) {
			SetValue(item.Key,item.Value);
		}

		void ICollection<KeyValuePair<long, T>>.Clear ( ) {
			Clear();
		}

		bool ICollection<KeyValuePair<long, T>>.Contains (KeyValuePair<long, T> item) {
			return ContainsKey(item.Key);
		}

		void ICollection<KeyValuePair<long, T>>.CopyTo (KeyValuePair<long, T>[] array, int arrayIndex) {
			long index=arrayIndex;
			foreach(KeyValuePair<long,T> d in Data){
				array[index]=d;
				index++;
			}
		}

		int ICollection<KeyValuePair<long, T>>.Count {
			get { return Count; }
		}

		bool ICollection<KeyValuePair<long, T>>.IsReadOnly {
			get { return IsReadOnly; }
		}

		bool ICollection<KeyValuePair<long, T>>.Remove (KeyValuePair<long, T> item) {
			if(!ContainsKey(item.Key)){
				return false;
			}
			RemoveByKey(item.Key);
			return true;
		}

		IEnumerator<KeyValuePair<long, T>> IEnumerable<KeyValuePair<long, T>>.GetEnumerator ( ) {
			return Data.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) {
			return Data.GetEnumerator();
		}
		#endregion
	
		#region ICollection<T>

		void ICollection<T>.Add (T item) {
			SetValue(NextIndex,item);
		}

		void ICollection<T>.Clear ( ) {
			Clear();
		}

		bool ICollection<T>.Contains (T item) {
			return ContainsValue(item);
		}

		void ICollection<T>.CopyTo (T[] array, int arrayIndex) {
			int index=arrayIndex;
			foreach(T obj in Values){
				array[index]=obj;
				index++;
			}
		}

		int ICollection<T>.Count {
			get { return Count; }
		}

		bool ICollection<T>.IsReadOnly {
			get { return IsReadOnly; }
		}

		bool ICollection<T>.Remove (T item) {
			if(!ContainsValue(item)){
				return false;
			}
			RemoveByKey(IndexOf(item));
			return true;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator ( ) {
			return Values.GetEnumerator();
		}

		#endregion
	
	}

	//A description of the dataType
	public sealed class DataType_dataIndex:DataType {
		#region singleton
		private static volatile DataType_dataIndex instance=new DataType_dataIndex();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_dataIndex ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dataIndex();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataIndex"; } }

	}

}