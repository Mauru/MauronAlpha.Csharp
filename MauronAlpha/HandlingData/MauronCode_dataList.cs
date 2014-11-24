using System.Collections.Generic;
using System;

using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//A list of numerically sorted data
	public class MauronCode_dataList<T>:MauronCode_dataObject,ICollection<T>,IEnumerable<T> {

		#region Constructors
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
		#endregion
		#region Instance of this list (does not instance the objects)
		public MauronCode_dataList<T> Instance {
			get {
				MauronCode_dataList<T> ret=new MauronCode_dataList<T>();
				foreach( T obj in Data ) {
					ret.AddValue(obj);
				}
				return this;
			}
		}
		#endregion

		#region The Data
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
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(SetData)", this, ErrorType_protected.Instance);
			}
			#endregion
			L_data=data;
			return this;
		}
		#endregion

		#region ReadOnly
		internal bool B_isReadOnly=false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public MauronCode_dataList<T> SetIsReadOnly(bool status) {
			B_isReadOnly=status;
			return this;
		}
		#endregion

		#region Count
		public int Count {
			get { return Data.Count; }
		}
		#endregion

		#region Check if Values are in Data
		public bool ContainsValue(T obj) {
			return Data.Contains(obj);
		}
		public bool ContainsKey(int key) {
			return key>=0 && Data.Count>0 && key<Data.Count;
		}
		//Is a List empty?
		public bool IsEmpty {
			get {
				return Data.Count==0;
			}
		} 
		#endregion
		
		#region Perform an action on each element in Data
		public delegate void Delegate_performEach(T obj);
		public MauronCode_dataList<T> Each(Delegate_performEach doStuff){
			foreach(T obj in L_data){
				doStuff(obj);
			}
			return this;
		}
		#endregion

		#region Add Values to Data
		public MauronCode_dataList<T> Add(T obj){
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(Add)", this, ErrorType_protected.Instance);
			}
			#endregion
			return AddValue(obj);
		}
		public MauronCode_dataList<T> AddValue(T obj) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(AddValue)", this, ErrorType_protected.Instance);
			}
			#endregion
			return SetValue(NextIndex, obj);
		}
		public MauronCode_dataList<T> SetValue(int key, T obj) {
			#region Avoid Invalid Key
			if( key>=Count||(key==0&&Count==0) ) { 
				Data.Add(obj);
				return this;
			}
			#endregion
			#region Error Check
			if(!ContainsKey(key)){				
				throw Error("Invalid Index! {"+key+"},(SetValue)",this, ErrorType_index.Instance);

			}
			#endregion
			Data[key]=obj;
			return this;
		}
		public MauronCode_dataList<T> AddValuesFrom(ICollection<T> collection){
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(AddValuesFrom)", this, ErrorType_protected.Instance);
			}
			#endregion
			foreach(T obj in collection) {
				AddValue(obj);
			}
			return this;
		}

		//Add a value at the specified index, shift all other indexes
		public MauronCode_dataList<T> InsertValueAt(int key, T obj){
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(InsertValueAt)", this, ErrorType_protected.Instance);
			}
			#endregion
			Data.InsertRange(key, new T[1]{obj});
			return this;
		}
		#endregion
		#region Remove Values from Data
		public MauronCode_dataList<T> RemoveByValue (T obj) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(RemoveByValue)", this, ErrorType_protected.Instance);
			}
			#endregion
			Data.Remove(obj);
			return this;
		}
		public MauronCode_dataList<T> RemoveByKey (int key) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(RemoveByKey)", this, ErrorType_protected.Instance);
			}
			#endregion
			#region Error Check
			if( !ContainsKey(key) ) {
				throw Error("Index out of bounds!,{"+key+"},(RemoveByKey)", this, ErrorType_index.Instance);
			}
			#endregion
			Data.RemoveAt(key);
			return this;
		}
		public MauronCode_dataList<T> RemoveByRange (int start, int end) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(RemoveByRange)", this, ErrorType_protected.Instance);
			}
			#endregion
			#region Error Check
			if( start<0||start>=Count ) {
				throw Error("Range start out of bounds! {"+start+"},(RemoveByRange)", this, ErrorType_index.Instance);
			}
			if( end<0||end>=Count||end<start ) {
				throw Error("Range end out of bounds! {"+end+"},(RemoveByRange)", this, ErrorType_index.Instance);
			}
			#endregion
			for( int n=start; n<=end; n++ ) {
				RemoveByKey(start);
			}
			return this;
		}
		public MauronCode_dataList<T> RemoveLastElement ( ) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(RemoveByValue)", this, ErrorType_protected.Instance);
			}
			#endregion
			#region Error Check
			if( Count==0 ) {
				throw Error("Data is empty!,(RemovelastElement)", this, ErrorType_index.Instance);
			}
			#endregion
			return RemoveByKey(Count-1);
		}

		//Clear
		public MauronCode_dataList<T> Clear ( ) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(Clear)", this, ErrorType_protected.Instance);
			}
			#endregion
			L_data=new List<T>();
			return this;
		}
		#endregion

		#region Get Elements from Data
		//Get a range of results
		public MauronCode_dataList<T> Range (int start, int end) {
			if( start<0||start>=Count ) {
				throw Error("Range start out of bounds! {"+start+"}", this, ErrorType_index.Instance);
			}
			if( end<0||end>=Count||end<start ) {
				throw Error("Range end out of bounds! {"+end+"}", this, ErrorType_index.Instance);
			}
			MauronCode_dataList<T> result=new MauronCode_dataList<T>();
			for( int n=start; n<=end; n++ ) {
				T obj=Value(n);
				result.AddValue(obj);
			}
			return result;
		}
		public MauronCode_dataList<T> Range (int start) {
			return Range(start, LastIndex);
		}

		public T Value(int key){
			#region Error Check
			if(!ContainsKey(key)){
				throw Error("Invalid key {"+key+"},(Value)",this,ErrorType_index.Instance);
			}
			#endregion
			return Data[key];
		}
		public ICollection<T> Values {
			get {
				return Data;
			}
		}
		public MauronCode_dataList<T> SetValues(ICollection<T> values){
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(SetValues)", this, ErrorType_protected.Instance);
			}
			#endregion
			SetData(new List<T>(values));
			return this;
		}

		//Get the first element
		public T FirstElement {
			get {
				#region Error Check
				if( Data.Count<1 ) {
					MauronCode.Error("Data is empty!,(FirstElement)", this, ErrorType_index.Instance);
				}
				#endregion
				return Data[FirstIndex];
			}
		}
		//Get the last element
		public T LastElement {
			get {
				#region Error Check
				if( Data.Count<1 ) {
					MauronCode.Error("Data is empty!,(LastElement)", this, ErrorType_index.Instance);
				}
				#endregion
				return Data[LastIndex];
			}
		}

		//Indexes (keys)
		public int NextIndex {
			get {
				if( Count==0 ) {
					return 0;
				}
				return Count;
			}
		}
		public int LastIndex {
			get {
				if( Count>0 ) {
					return Count-1;
				}
				return 0;
			}
		}
		public int FirstIndex {
			get { return 0; }
		}
		#endregion

		public MauronCode_dataList<T> CopyTo(T[] array, int arrayIndex) {
			int index=arrayIndex;
			foreach( T obj in Values ) {
				array[index] = obj;
				index++;
			}
			return this;
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
			CopyTo(array,arrayIndex);
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

		public bool Equals(MauronCode_dataList<T> other){
			long count = Count;
			if(count!=other.Count)
				return false;
			for(int index=0; index<count; index++) {
				T source = Value(index);
				T candidate = other.Value(index);
				if(!source.Equals(candidate))
					return false;
			}
			return true;
		}

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

		public override bool IsProtectable { get { return true; }}
		public override string Name { get { return "dataList"; } }

	}
}