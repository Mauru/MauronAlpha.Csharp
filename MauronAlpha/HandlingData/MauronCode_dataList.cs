using System.Collections.Generic;
using System;

namespace MauronAlpha.HandlingData {

	public class MauronCode_dataList<T>:MauronCode_dataObject {
		private List<T> L_data;
		public MauronCode_dataList() {
			
		}
		public T[] AsArray { 
			get {
				return L_data.ToArray();
			}
		}
		public MauronCode_dataList<T> Add(T obj){
			L_data.Add(obj);
			return this;
		}
		public MauronCode_dataList<T> Remove(T obj){
			L_data.Remove(obj);
			return this;
		}
		public bool Contains(T obj) {
			return L_data.Contains(obj);
		}
		public bool ContainsKey(int i) {
			return (L_data[i]!=null);
		}

		public delegate void Delegate_performeach(T obj);
		public MauronCode_dataList<T> Each(Delegate_performeach doStuff){
			foreach(T obj in L_data){
				doStuff(obj);
			}
			return this;
		}

		public MauronCode_dataList<T> Clear() {
			L_data=new List<T>();
			return this;
		}

		//return an instance of this list, do not instance the objects
		public MauronCode_dataList<T> Instance { get {
			MauronCode_dataList<T> ret = new MauronCode_dataList<T>();
			foreach(T obj in L_data) {
				ret.Add(obj);
			}
			return this;
		}}


	}

}
