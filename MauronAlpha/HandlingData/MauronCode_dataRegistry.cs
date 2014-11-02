using System;
using System.Collections.Generic;


namespace MauronAlpha.HandlingData {

	//Supposed to become a MauronCode_dataMap<MauronCode_dataList<T>>
	public class MauronCode_dataRegistry<T>:MauronCode_dataObject, ICollection<T> {

		//constructor
		public MauronCode_dataRegistry():base(DataType_dataRegistry.Instance) {}

		public MauronCode_dataRegistry<T> Instance { 
			get {
				MauronCode_dataRegistry<T> instance = new MauronCode_dataRegistry<T>();
				instance.SetDefaultKey(STR_defaultKey);
				foreach(string key in DATA_registry.Keys) {
					MauronCode_dataList<T> values = DATA_registry.Value(key);
					instance.SetValue(key,values);
				}
				return instance;
			}
		}

		//The Data
		private MauronCode_dataMap<MauronCode_dataList<T>> DATA_registry;
		public MauronCode_dataMap<MauronCode_dataList<T>> Data { 
			get {
				if(DATA_registry==null){
					SetData(new MauronCode_dataMap<MauronCode_dataList<T>>());
				}
				return DATA_registry;
			}
		}
		
		public MauronCode_dataRegistry<T> SetData(MauronCode_dataMap<MauronCode_dataList<T>> data){
			DATA_registry=data;
			return this;
		}
	
		private string STR_defaultKey="default";
		public string DefaultKey { get { return STR_defaultKey; }}
		public MauronCode_dataRegistry<T> SetDefaultKey(string key){
			STR_defaultKey=key;
			return this;
		}

		//Basic accessor functions
		public MauronCode_dataRegistry<T> AddValue(string key, T obj){
			if(!Data.ContainsKey(key)){
				Data.SetValue(key, new MauronCode_dataList<T>());
			}
			MauronCode_dataList<T> list = Data.Value(key);
			list.AddValue(obj);
			return SetValue(key, list);
		}
		public MauronCode_dataRegistry<T> SetValue(string key, MauronCode_dataList<T> obj){
			Data.SetValue(key, obj);
			return this;
		}
		public ICollection<T> Values { get {
			MauronCode_dataList<T> ret=new MauronCode_dataList<T>();
			foreach(KeyValuePair<string,MauronCode_dataList<T>>d in Data){
				ret.AddValuesFrom(d.Value.Values);
			}
			return ret;
		} }

		//Count
		public int Count { get {
			return Values.Count;
		} }

		//Readonly (false)
		private bool B_isReadOnly=false;
		public bool IsReadOnly { get { return B_isReadOnly; } } 

		//Remove a value
		public MauronCode_dataRegistry<T> RemoveByKey(string key){
			Data.RemoveByKey(key);
			return this;
		}
		public MauronCode_dataRegistry<T> RemoveByValue(T obj){
			foreach(KeyValuePair<string,MauronCode_dataList<T>> d in Data ){
				d.Value.RemoveByValue(obj);
			}
			return this;
		}
		public MauronCode_dataRegistry<T> Clear() {
			SetData(new MauronCode_dataMap<MauronCode_dataList<T>>());
			return this;
		}

		//Contains
		public bool ContainsValue(T item){
			foreach( KeyValuePair<string,MauronCode_dataList<T>> d in Data ) {
				if( d.Value.ContainsValue(item)) {
					return true;
				}
			}
			return false;
		}

		#region ICollection
		void ICollection<T>.Add (T item) {
			AddValue(DefaultKey, item);
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
			get {return IsReadOnly; }
		}

		bool ICollection<T>.Remove (T item) {
			if(!ContainsValue(item)) {
				return false;
			}
			RemoveByValue(item);
			return true;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator ( ) {
			return Values.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) {
			return Values.GetEnumerator();
		}
		#endregion
	}


	//A description of the dataType
	public sealed class DataType_dataRegistry : DataType {
		#region singleton
		private static volatile DataType_dataRegistry instance=new DataType_dataRegistry();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_dataRegistry ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dataRegistry();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataRegistry"; } }

	}
}
