using System.Collections.Generic; using System;  namespace MauronAlpha.HandlingData {  	public class MauronCode_dataList<T>:MauronCode_dataObject,ICollection<T> { 		//constructor 		public MauronCode_dataList() {}  		//Data
		private List<T> L_data; 		public List<T> Data { 			get { return L_data; } 		} 		public T[] AsArray {  			get { 				return L_data.ToArray(); 			} 		} 		public MauronCode_dataList<T> SetData(List<T> data) { 			L_data=data; 			return this; 		} 		 		  		//Contains 		public bool Contains(T obj) { 			return L_data.Contains(obj); 		} 		public bool ContainsKey(int i) { 			return (L_data[i]!=null); 		} 		 		//Perform an action on each element 		public delegate void Delegate_performeach(T obj); 		public MauronCode_dataList<T> Each(Delegate_performeach doStuff){ 			foreach(T obj in L_data){ 				doStuff(obj); 			} 			return this; 		}  		 		//return an instance of this list, do not instance the objects 		public MauronCode_dataList<T> Instance { get { 			MauronCode_dataList<T> ret = new MauronCode_dataList<T>(); 			foreach(T obj in L_data) { 				ret.Add(obj); 			} 			return this; 		}}  		#region ICollection 		//Add 		public MauronCode_dataList<T> Add (T obj) { 			L_data.Add(obj); 			return this; 		} 		public MauronCode_dataList<T> AddAt (T obj, int n) { 			if( !ContainsKey(n) ) { 				Error("Invalid index ["+n+"]", this); 			} 			L_data[n]=obj; 			return this; 		} 		void ICollection<T>.Add (T item) { 			L_data.Add(item); 		}  		//clear 		public MauronCode_dataList<T> Clear ( ) { 			L_data=new List<T>(); 			return this; 		} 		void ICollection<T>.Clear ( ) { 			Clear(); 		}  		//copy to array 		public void CopyTo (T[] array, int arrayIndex) { 			foreach(T obj in L_data) { 				array.CopyTo(array, arrayIndex); 			} 		}  		//count 		public int Count { 			get { return L_data.Count; } 		}  		//readonly 		internal bool B_readOnly = false; 		public bool ReadOnly { get { 			return B_readOnly; 		} } 		public MauronCode_dataList<T> SetReadOnly(bool b) { 			B_readOnly=b; 			return this; 		} 		public bool IsReadOnly { 			get { return B_readOnly; } 		}

		//Remove
		public MauronCode_dataList<T> Remove (T obj) {
			if( ReadOnly ) {
				Error("DataList is readOnly!", this);
			}
			L_data.Remove(obj);
			return this;
		}
		public MauronCode_dataList<T> RemoveAt (int n) {
			if( !ContainsKey(n) ) {
				Error("Invalid index ["+n+"]", this);
			}
			if( ReadOnly ) {
				Error("DataList is readOnly!", this);
			}
			L_data.RemoveAt(n);
			return this;
		}
		bool ICollection<T>.Remove (T item) {
			if( ReadOnly ) {
				Error("DataList is readOnly!", this);
			}
			Remove(item);
			return true;
		}  		#endregion               		public IEnumerator<T> GetEnumerator ( ) { 			throw new NotImplementedException(); 		}  		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) { 			throw new NotImplementedException(); 		} 	}  } 