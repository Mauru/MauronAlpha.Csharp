using System.Collections.Generic;
using System;

using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData.Interfaces;
using MauronAlpha.HandlingData.Sorting;

namespace MauronAlpha.HandlingData {

	//A list of numerically sorted data
	public class MauronCode_dataList<T> : MauronCode_dataObject, 
	ICollection<T>,
	IEnumerable<T>,
	I_dataCollection<int,T>,
	IList<T> {

		//Constructors
		public MauronCode_dataList() :base(DataType_dataList.Instance) {}
		// Interpretative constructors
		public MauronCode_dataList ( T obj)	:this() {
			AddValue(obj);
		}
		public MauronCode_dataList( ICollection<T> data ):this() {
			foreach( T o in data ) {
				AddValue( o );
			}
		}

		//The Data
		private List<T> L_data;
		public List<T> Data {
			get { 
				if (L_data == null)
					L_data = new List<T>();
				return L_data;
			}
		}

		public T[] AsArray { 
			get {
				return L_data.ToArray();
			}
		}
		//Indexes: KeyValuePair
		public ICollection<KeyValuePair<int, T>> KeyValuePairs {
			get {
				KeyValuePair<int, T>[] result=new KeyValuePair<int, T>[Count];
				foreach( int n in Keys ) {
					KeyValuePair<int, T> kvp=new KeyValuePair<int, T>(n, Data[n]);
					result[n]=kvp;
				}
				return result;
			}
		}
		//Indexes: Keys
		public ICollection<int> Keys {
			get {
				int[] result=new int[Count];
				for( int n=0; n<Count; n++ ) {
					result[n]=n;
				}
				return result;
			}
		}

		//Booleans
		public bool Equals (MauronCode_dataList<T> other) {
			long count=Count;
			if( count!=other.Count )
				return false;
			for( int index=0; index<count; index++ ) {
				T source=Value(index);
				T candidate=other.Value(index);
				if( !source.Equals(candidate) )
					return false;
			}
			return true;
		}
		public bool Equals_unsorted(MauronCode_dataList<T> other) {
			long count = Count;
			if (count != other.Count)
				return false;
			foreach (T val in Data)
				if(!other.ContainsValue(val))
					return false;
			return true;
		}
		internal bool B_isReadOnly=false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public bool IsEmpty {
			get {
				if (L_data == null)
					return true;
				if (L_data.Count > 0)
					return false;
				return true;
			}
		} 

		public bool ContainsValue (T obj) {
			return Data.Contains(obj);
		}
		public bool ContainsKey (int key) {
			return key>=0&&Data.Count>0&&key<Data.Count;
		}

		//As String
		public string AsString {
			get {
				string result="";
				foreach( T obj in Data ) {
					result+=obj.ToString()+',';
				}
				return result;
			}
		}
		
		//Methods
		public MauronCode_dataList<T> Instance {
			get {
				MauronCode_dataList<T> ret=new MauronCode_dataList<T>();
				foreach( T obj in Data ) {
					ret.AddValue(obj);
				}
				return this;
			}
		}		
		public MauronCode_dataList<T> SetIsReadOnly(bool status) {
			B_isReadOnly=status;
			return this;
		}
		public MauronCode_dataList<T> CopyTo (T[] array, int arrayIndex) {
			int index=arrayIndex;
			foreach( T obj in Values ) {
				array[index]=obj;
				index++;
			}
			return this;
		}

		public MauronCode_dataStack<T> AsReversedStack {
			get {
				MauronCode_dataStack<T> result = new MauronCode_dataStack<T>();
				int index = Count - 1;
				if (index < 0)
					return result;
				for (int n = index; n >= 0; n--)
					result.Add(Data[n]);
				return result;
			}
		}
		public MauronCode_dataStack<T> AsStack {
			get {
				MauronCode_dataStack<T> result = new MauronCode_dataStack<T>();
				foreach (T child in Data)
					result.Add(child);
				return result;
			}
		}

		//Sorting
		public MauronCode_dataList<T> SortWith (Sort_quickSort<T>.Comparer comparer) {
			Sort_quickSort<T> sorter=new Sort_quickSort<T>();
			sorter.Sort(this, comparer);
			return this;
		}
		public MauronCode_dataList<T> Reverse() {
			if (IsReadOnly)
				throw Error("Is protected!,(Reversed)", this, ErrorType_protected.Instance);
			Data.Reverse();
			return this;
		}

		//Modifiers: Each
		public MauronCode_dataList<T> Each (Delegate_performEach doStuff) {
			foreach( T obj in L_data ) {
				doStuff(obj);
			}
			return this;
		}

		//Modifiers: Add
		public MauronCode_dataList<T> Add (T obj) {
			return AddValue(obj);
		}
		public MauronCode_dataList<T> AddValue (T obj) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(AddValue)", this, ErrorType_protected.Instance);
			}
			#endregion
			return SetValue(NextIndex, obj);
		}
		public MauronCode_dataList<T> SetValue (int key, T obj) {
			#region Avoid Invalid Key
			if( key>=Count||(key==0&&Count==0) ) {
				Data.Add(obj);
				return this;
			}
			#endregion
			#region Error Check
			if( !ContainsKey(key) ) {
				throw Error("Invalid Index! {"+key+"},(SetValue)", this, ErrorType_index.Instance);

			}
			#endregion
			Data[key]=obj;
			return this;
		}
		public MauronCode_dataList<T> SetData (List<T> data) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(SetData)", this, ErrorType_protected.Instance);
			}
			#endregion
			L_data=data;
			return this;
		}
		public MauronCode_dataList<T> SetValues (ICollection<T> values) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(SetValues)", this, ErrorType_protected.Instance);
			}
			#endregion
			SetData(new List<T>(values));
			return this;
		}
		public MauronCode_dataList<T> AddValuesFrom (ICollection<T> collection) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(AddValuesFrom)", this, ErrorType_protected.Instance);
			}
			#endregion
			foreach( T obj in collection ) {
				AddValue(obj);
			}
			return this;
		}
		public MauronCode_dataList<T> AddValuesFrom(MauronCode_dataList<T> other) {
			if (IsReadOnly)
				throw Error("Is protected!,(AddValuesFrom)", this, ErrorType_protected.Instance);
			foreach (T item in other)
				Add(item);
			return this;
		}
		public MauronCode_dataList<T> InsertValueAt (int key, T obj) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(InsertValueAt)", this, ErrorType_protected.Instance);
			}
			#endregion
			Data.InsertRange(key, new T[1] { obj });
			return this;
		}
		public MauronCode_dataList<T> InsertValuesAt(int key, IEnumerable<T> values) {
			#region ReadOnly Check
			if (IsReadOnly) {
				throw Error("ReadOnly!,(InsertValueAt)", this, ErrorType_protected.Instance);
			}
			#endregion
			Data.InsertRange(key, values);
			return this;
		}
		public MauronCode_dataList<T> Join(MauronCode_dataList<T> other) {
			if(IsReadOnly)
				throw Error("Is protected!,(Join)",this,ErrorType_protected.Instance);
			foreach(T obj in other)
				AddValue(obj);
			return this;
		}

		//Modifiers: Remove
		public MauronCode_dataList<T> Clear ( ) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(Clear)", this, ErrorType_protected.Instance);
			}
			#endregion
			L_data=new List<T>();
			return this;
		}
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

			int count = Count;

			if (start < 0 || start >= count)
				throw Error("Range start out of bounds! {"+start+"},(RemoveByRange)", this, ErrorType_index.Instance);

			if (end < 0 || end > count || end < start)
				throw Error("Range end out of bounds! {"+end+"},(RemoveByRange)", this, ErrorType_index.Instance);

			#endregion
			for (int n = start; n < end; n++)
				RemoveByKey(start);

			return this;
		}
		public MauronCode_dataList<T> RemoveByRange(int start) {
			#region ReadOnly Check
			if (IsReadOnly) {
				throw Error("ReadOnly!,(RemoveByRange)", this, ErrorType_protected.Instance);
			}
			#endregion

			int count = Count;
			#region Error Check
			if (start < 0 || start >= count)
				throw Error("Range start out of bounds! {" + start + "},(RemoveByRange)", this, ErrorType_index.Instance);

			#endregion
			for (int n = start; n < count; n++)
				RemoveByKey(start);

			return this;
		}
		public MauronCode_dataList<T> Split(int start) {
			#region ReadOnly Check
			if (IsReadOnly) {
				throw Error("ReadOnly!,(RemoveByRange)", this, ErrorType_protected.Instance);
			}
			#endregion
			int count = Count;
			#region Error Check
			if (start < 0 || start >= count) {
				throw Error("Range start out of bounds! {" + start + "},(RemoveByRange)", this, ErrorType_index.Instance);
			}

			MauronCode_dataList<T> result = new MauronCode_dataList<T>();

			#endregion
			for (int n = start; n <= count; n++) {
				result.Add(Value(start));
				RemoveByKey(start);
			}
			return result;
		}
		public MauronCode_dataList<T> Extract(int start, int end) {
			#region ReadOnly Check
			if (IsReadOnly) {
				throw Error("ReadOnly!,(RemoveByRange)", this, ErrorType_protected.Instance);
			}
			#endregion
			#region Error Check

			int count = Count;

			if (start < 0 || start >= count)
				throw Error("Range start out of bounds! {" + start + "},(Extract)", this, ErrorType_index.Instance);

			if (end < 0 || end > count || end < start)
				throw Error("Range end out of bounds! {" + end + "},(Extract)", this, ErrorType_index.Instance);

			MauronCode_dataList<T> result = new MauronCode_dataList<T>();

			#endregion
			for (int n = start; n < end; n++) {
				result.Add(Value(start));
				RemoveByKey(start);
			}
			return result;
		}
		public MauronCode_dataList<T> ExtractRange(int start) {
			int count = Count;
			return Extract(start, count);
		}
		public MauronCode_dataList<T> RemoveLastElement ( ) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("ReadOnly!,(RemoveByValue)", this, ErrorType_protected.Instance);
			}
			#endregion
			#region Error Check
			if( Count==0 )
				throw Error("Data is empty!,(RemovelastElement)", this, ErrorType_index.Instance);
			
			#endregion
			return RemoveByKey(Count-1);
		}
		
		//Queries: List
		public MauronCode_dataList<T> Range (int start, int end) {
			MauronCode_dataList<T> result = new MauronCode_dataList<T>();
			if (start < 0)
				start =0;
			int count = Count;
			if (count == 0)
				return result;
			if (start >= count)
				return result;
			if( end < 0 )
				return result;
			if (end >= count)
				end = count-1;
			for( int n=start; n<=end; n++ ) {
				T obj=Value(n);
				result.AddValue(obj);
			}
			return result;
		}
		public MauronCode_dataList<T> Range (int start) {
			return Range(start, LastIndex);
		}

		public bool TryIndex(int index, ref T result) {
			if(index < 0)
				return false;
			int n = Count;
			if(index >= n)
				return false;
			result = L_data[index];
			return true;
			
		}

		//Queries: Single
		public T Value (int key) {
			#region Error Check
			if( !ContainsKey(key) ) {
				throw Error("Invalid key {"+key+"},(Value)", this, ErrorType_index.Instance);
			}
			#endregion
			return Data[key];
		}
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
		public bool TryFirstElement(ref T result) {
			return TryIndex(0, ref result);
		}
		public T LastElement {
			get {
				#region Error Check
				if( Data.Count<1 )
					MauronCode.Error("Data is empty!,(LastElement)", this, ErrorType_index.Instance);

				#endregion
				return Data[LastIndex];
			}
		}
		public bool TryLastElement(ref T result) {
			return TryIndex(Count-1, ref result);
		}
		
		public T this[int key] {
			get { return Value(key);}
		}

		//Return Modifiers: Remove
		public T Pop {
			get {
				if(IsReadOnly)
					throw Error("Is protected!,(Pop)",this,ErrorType_protected.Instance);
				if(IsEmpty)
					throw Error("Is empty!,(Pop)",this,ErrorType_index.Instance);
				T obj = LastElement;
				RemoveByKey(Count-1);
				return obj;
			}
		}
		public T Shift {
			get {
				if( IsReadOnly )
					throw Error("Is protected!,(Shift)", this, ErrorType_protected.Instance);
				if( IsEmpty )
					throw Error("Is empty!,(Shift)", this, ErrorType_index.Instance);
				T obj = FirstElement;
				RemoveByKey(0);
				return obj;
			}
		}

		//Numeric (int)
		public int Count {
			get { return Data.Count; }
		}
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
		public int IndexOf (T item) {
			return L_data.IndexOf(item);
		}

		//Delegates
		public delegate void Delegate_performEach (T obj);
		public delegate int Sort_comparer (T source, T other);

		#region ICollection Members
		//Conversion: ICollection
		public ICollection<T> Values {
			get {
				return Data;
			}
		}
		//Add
		void ICollection<T>.Add (T item) {
			AddValue(item);
		}
		void ICollection<T>.Clear ( ) {
			Clear();
		}
		//copy to array
		void ICollection<T>.CopyTo (T[] array, int arrayIndex) {
			CopyTo(array, arrayIndex);
		}
		bool ICollection<T>.Remove (T item) {
			if( !ContainsValue(item) ) { return false; }
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
		#region IList<T> Members
		void IList<T>.Insert (int index, T item) {
			if(IsReadOnly)
				throw Error("Protected!,(Insert)",this,ErrorType_protected.Instance);
				L_data.Insert(index,item);
			return;
		}
		void IList<T>.RemoveAt (int index) {
			if( IsReadOnly )
				throw Error("Protected!,(RemoveAt)", this, ErrorType_protected.Instance);
			L_data.RemoveAt(index);
		}
		T IList<T>.this[int index] {
			get {
				return Value(index);
			}
			set {
				if( IsReadOnly )
					throw Error("Protected!,(SetValue)", this, ErrorType_protected.Instance);
					SetValue(index,value);
			}
		}
		#endregion
		#region I_dataCollection<int,T> Members
		I_dataCollection<int, T> I_dataCollection<int, T>.SetValue (int key, T value) {
			return SetValue(key, value);
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

		public override bool IsProtectable { 
			get { 
				return true;
			} 
		}
		public override string Name { 
			get {
				return "dataList";
			} 
		}

	}
}