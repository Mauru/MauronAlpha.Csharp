using System.Collections.Generic;
using System;

namespace MauronAlpha.HandlingData {

	//A list of numerically sorted data
	public class MauronCode_dataList<T>:MauronCode_dataObject,ICollection<T> {

		//constructor
		public MauronCode_dataList() {
			Clear();
		}

		//Data
		private List<T> L_data=null;
		public List<T> Data {
			get { 
				if (L_data == null) {
					Clear ();		
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

		//Contains
		public bool Contains(T obj) {
			return Data.Contains(obj);
		}
		public bool ContainsKey(int i) {
			return (Data[i]!=null);
		}
		
		//Perform an action on each element
		public delegate void Delegate_performeach(T obj);
		public MauronCode_dataList<T> Each(Delegate_performeach doStuff){
			foreach(T obj in L_data){
				doStuff(obj);
			}
			return this;
		}

		
		//return an instance of this list, do not instance the objects
		public MauronCode_dataList<T> Instance { get {
			MauronCode_dataList<T> ret = new MauronCode_dataList<T>();
			foreach(T obj in Data) {
				ret.Add(obj);
			}
			return this;
		}}

		//Indexes
		public int NextIndex { get {
			if (Data.Count == 0) {
				return 0;
			}
			return Data.Count;
		} }
		public int LastIndex { 
			get {
				if(Data.Count>0){
					return Data.Count-1;
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
					MauronCode.Error ("Data is empty!", this);
				}
				return Data [FirstIndex];
			}
		}
		//Get the last element
		public T LastElement {
			get {
				if (Data.Count < 1) {
					MauronCode.Error ("Data is empty",this);
				}
				return Data [LastIndex];
			}
		}
		public bool IsEmpty {
			get {
				return Data.Count == 0; 
			}
		} 

		#region ICollection
		//Add
		public MauronCode_dataList<T> Add (T obj) {
			L_data.Add(obj);
			return this;
		}
		public MauronCode_dataList<T> AddAt (T obj, int n) {
			if( !ContainsKey(n) ) {
				Error("Invalid index ["+n+"]", this);
			}
			Data[n]=obj;
			return this;
		}
		void ICollection<T>.Add (T item) {
			Data.Add(item);
		}

		//clear
		public MauronCode_dataList<T> Clear ( ) {
			L_data=new List<T>();
			return this;
		}
		void ICollection<T>.Clear ( ) {
			Clear();
		}

		//copy to array
		public void CopyTo (T[] array, int arrayIndex) {
			foreach(T obj in Data) {
				array.CopyTo(array, arrayIndex);
			}
		}

		//count
		public int Count {
			get { return Data.Count; }
		}

		//readonly
		internal bool B_readOnly = false;
		public bool ReadOnly { get {
			return B_readOnly;
		} }
		public MauronCode_dataList<T> SetReadOnly(bool b) {
			B_readOnly=b;
			return this;
		}
		public bool IsReadOnly {
			get { return B_readOnly; }
		}

		//Remove
		public MauronCode_dataList<T> Remove (T obj) {
			if( ReadOnly ) {
				Error("DataList is readOnly!", this);
			}
			Data.Remove(obj);
			return this;
		}
		public MauronCode_dataList<T> RemoveAt (int n) {
			if( !ContainsKey(n) ) {
				Error("Invalid index ["+n+"]", this);
			}
			if( ReadOnly ) {
				Error("DataList is readOnly!", this);
			}
			Data.RemoveAt(n);
			return this;
		}
		bool ICollection<T>.Remove (T item) {
			if( ReadOnly ) {
				Error("DataList is readOnly!", this);
			}
			Remove(item);
			return true;
		}

		#endregion

		public IEnumerator<T> GetEnumerator ( ) {
			return Data.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) {
			return Data.GetEnumerator ();
		}
	}

}
