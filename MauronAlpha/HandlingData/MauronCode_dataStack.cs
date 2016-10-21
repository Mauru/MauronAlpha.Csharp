using System;
using System.Collections.Generic;

namespace MauronAlpha.HandlingData {

	public class MauronCode_dataStack<T> : MauronCode_dataObject, I_dataObject {
		Stack<T> Data = new Stack<T>();
		public MauronCode_dataStack() : base() { }

		public MauronCode_dataStack<T> Add(T obj) {
			Data.Push(obj);
			return this;
		}
		public MauronCode_dataStack<T> Add(MauronCode_dataStack<T> other) {
			while(!other.IsEmpty) { 
				T obj = other.Pop;
				Add(obj);
			}
			return this;
		}
		public T Pop {
			get {
				return Data.Pop();
			}
		}
		public T LastElement {
			get {
				return Data.Peek();
			}
		}
		public long Count {
			get {
				return Data.Count;
			}
		}
		public bool IsEmpty {
			get {
				return Data.Count == 0;
			}
		}

	}

	//A class that offers exclusively static functions
	public sealed class DataType_dataStack : DataType {
		#region singleton
		private static volatile DataType_dataStack instance = new DataType_dataStack();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static DataType_dataStack() { }
		public static DataType Instance {
			get {
				if (instance == null) {
					lock (syncRoot) {
						instance = new DataType_dataStack();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataStack"; } }

	}

}
