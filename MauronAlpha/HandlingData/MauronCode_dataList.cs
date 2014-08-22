using System.Collections.Generic;
using System;

using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//A list of numerically sorted data
	public class MauronCode_dataList<T>:MauronCode_dataObject,ICollection<T>,IEnumerable<T> {

		//Constructor
		public MauronCode_dataList():base(DataType_dataList.Instance) {}
		public MauronCode_dataList ( T obj)
			: this() {
			AddValue(obj);
		}
		public MauronCode_dataList(T[] obj):this() {
			foreach (T o in obj) {
				AddValue (o);
			}
		}
		//Data
		private List<T> L_data;
		public List<T> Data {
			get { 
				if (L_data == null) {
					SetData(new List<T>());
				}
				return L_data;
			}
		}
		public T[] AsArray { 
			get {
				return L_data.ToArray();
			}
		}
		public MauronCode_dataList<T> SetData(List<T> data) {
			L_data=data;
			return this;
		}

		//readonly
		internal bool B_isReadOnly=false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		//Remove
		public MauronCode_dataList<T> RemoveByValue(T obj){
			Data.Remove(obj);
			return this;
		}
		public MauronCode_dataList<T> RemoveByKey(int key){
			//!silent return
			if(!ContainsKey(key)){
				return this;
			}
			//actual removal
			Data.RemoveAt(key);
			return this;
		}
		public MauronCode_dataList<T> RemoveByRange(int start, int end){
			if( start<0||start>=Count ) {
				Error("Range start out of bounds! {"+start+"}", this,ErrorType_index.Instance);
			}
			if( end<0||end>=Count||end<start ) {
				Error("Range end out of bounds! {"+end+"}", this,ErrorType_index.Instance);
			}
			for( int n=start; n<=end; n++ ) {
				RemoveByKey(start);
			}
			return this;
		}

		//Get a range of results
		public MauronCode_dataList<T> Range(int start, int end){
			if(start<0||start>=Count){
				Error("Range start out of bounds! {"+start+"}", this, ErrorType_index.Instance);
			}
			if(end<0||end>=Count||end<start){
				Error("Range end out of bounds! {"+end+"}", this, ErrorType_index.Instance);
			}
			MauronCode_dataList<T> result = new MauronCode_dataList<T>();
			for(int n=start;n<=end;n++){
				T obj = Value(n);
				result.AddValue(obj);
			}
			return result;
		}

		//Count
		public int Count {
			get { return Data.Count; }
		}

		//Contains
		public bool ContainsValue(T obj) {
			return Data.Contains(obj);
		}
		public bool ContainsKey(int i) {
			return i>0 && Data.Count>0;
		}
		
		//Perform an action on each element
		public delegate void Delegate_performEach(T obj);
		public MauronCode_dataList<T> Each(Delegate_performEach doStuff){
			foreach(T obj in L_data){
				doStuff(obj);
			}
			return this;
		}

		//Add a value
		public MauronCode_dataList<T> Add(T obj){
			return AddValue(obj);
		}
		public MauronCode_dataList<T> AddValue(T obj) {
			return SetValue(NextIndex, obj);
		}
		public MauronCode_dataList<T> SetValue(int key, T obj){
			if(!ContainsKey(key)){
				if(key==0||key==(Count-1)){
					Data.Add(obj);
					return this;
				}
				Error("Invalid Index! {"+key+"}",this, ErrorType_index.Instance);
			}
			Data[key]=obj;
			return this;
		}
		public MauronCode_dataList<T> AddValuesFrom(ICollection<T> collection){
			foreach(T obj in collection) {
				AddValue(obj);
			}
			return this;
		}

		//Add a value at the specified index, shift all other indexes
		public MauronCode_dataList<T> InsertValueAt(int key, T obj){
			Data.InsertRange(key, new T[1]{obj});
			return this;
		}

		//Get a value
		public T Value(int key){
			if(!ContainsKey(key)){
				Error("Invalid key {"+key+"}",this,ErrorType_index.Instance);
			}
			return Data[key];
		}
		public ICollection<T> Values {
			get {
				return Data;
			}
		}
		public MauronCode_dataList<T> SetValues(ICollection<T> values){
			SetData(new List<T>(values));
			return this;
		}

		//Clear
		public MauronCode_dataList<T> Clear ( ) {
			L_data=new List<T>();
			return this;
		}

		//return an instance of this list, do not instance the objects
		public MauronCode_dataList<T> Instance { get {
			MauronCode_dataList<T> ret = new MauronCode_dataList<T>();
			foreach(T obj in Data) {
				ret.AddValue(obj);
			}
			return this;
		}}

		//Indexes
		public int NextIndex { get {
			if (Count == 0) {
				return 0;
			}
			return Count;
		} }
		public int LastIndex { 
			get {
				if(Count>0){
					return Count-1;
				}
				return 0;
			}
		}
		public static int FirstIndex {
			get { return 0; }
		}
		
		//Get the first element
		public T FirstElement {
			get {
				if (Data.Count < 1) {
					MauronCode.Error ("Data is empty!", this,ErrorType_index.Instance);
				}
				return Data [FirstIndex];
			}
		}

		//Get the last element
		public T LastElement {
			get {
				if (Data.Count < 1) {
					MauronCode.Error("Data is empty", this, ErrorType_index.Instance);
				}
				return Data [LastIndex];
			}
		}
		
		//Is a List empty?
		public bool IsEmpty {
			get {
				return Data.Count == 0; 
			}
		} 

		#region ICollection

		//Add
		void ICollection<T>.Add (T item) {
			AddValue(item);
		}

		void ICollection<T>.Clear ( ) {
			Clear();
		}

		//copy to array
		void ICollection<T>.CopyTo (T[] array, int arrayIndex) {
			int index=arrayIndex;
			foreach(T obj in Values) {
				array.CopyTo(array, index);
				index++;
			}
		}
		
		bool ICollection<T>.Remove (T item) {
			if(!ContainsValue(item)){ return false; }
			RemoveByValue(item);
			return true;
		}

		bool ICollection<T>.Contains (T item) {
			return ContainsValue(item);
		}

		int ICollection<T>.Count {
			get { return Count; }
		}

		bool ICollection<T>.IsReadOnly {
			get { return IsReadOnly; }
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator ( ) {
			return Data.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) {
			return Data.GetEnumerator();
		}

		#endregion

	}

	//A Description of the DataType
	public sealed class DataType_dataList:DataType {
		#region singleton
		private static volatile DataType_dataList instance=new DataType_dataList();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_dataList ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dataList();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataList"; } }

	}
}