using System;
using System.Collections.Generic;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	/// <summary> Maps objects of GENERICTYPE1 to object of GENERICTYPE2 | BaseType Dictionary </summary>
	public class MauronCode_dataReference<TKey, TValue> : MauronCode_dataObject, IDictionary<TKey, TValue> {

		public MauronCode_dataReference() : base(DataType_dataReference.Instance) { }

		private Dictionary<TKey, TValue> DATA = new Dictionary<TKey, TValue>();

		public bool IsEmpty {
			get {
				return DATA.Count < 1;
			}
		}
		public bool ContainsKey(TKey key) {
			return DATA.ContainsKey(key);
		}
		public bool ContainsValue(TValue value) {
			foreach (TValue val in DATA.Values)
				if (val.Equals(value))
					return true;
			return false;
		}
		public bool ContainsValueAtKey(TKey key, TValue value) {
			TValue result;
			bool success = DATA.TryGetValue(key, out result);
			if (!success)
				return false;
			return result.Equals(value);
		}

		public bool TryFind(TKey key, out TValue value) {
			return DATA.TryGetValue(key, out value);
		}

		public TValue Value(TKey key) {
			TValue result;
			bool success = DATA.TryGetValue(key, out result);
			if(!success)
				throw Error("Invalid Index!,(Value)",this,ErrorType_index.Instance);
			return result;			
		}

		public MauronCode_dataReference<TKey, TValue> SetValue(TKey key, TValue value) {
			DATA[key] = value;
			return this;
		}
		public MauronCode_dataReference<TKey, TValue> RemoveByKey(TKey key) {
			if(!ContainsKey(key))
				return this;
			DATA.Remove(key);
			return this;
		}

		public MauronCode_dataList<TKey> Keys {
			get {
				return new MauronCode_dataList<TKey>(DATA.Keys);
			}
		}
		public MauronCode_dataList<TValue> Values {
			get {
				return new MauronCode_dataList<TValue>(DATA.Values);
			}
		}
		public MauronCode_dataList<MauronCode_dataRelation<TKey,TValue>> AsRelationList {
			get {
				MauronCode_dataList<MauronCode_dataRelation<TKey, TValue>> result = new MauronCode_dataList<MauronCode_dataRelation<TKey, TValue>>();
				if (IsEmpty)
					return result; 
				ICollection<TKey> keys = DATA.Keys;
				
				foreach(TKey k in keys) {
					MauronCode_dataRelation<TKey,TValue> kvp;
					TValue v = default(TValue);
					if (TryFind(k, out v))
						 kvp = new MauronCode_dataRelation<TKey, TValue>(k,v);

					else {
						kvp = new MauronCode_dataRelation<TKey, TValue>();
						kvp.SetKey(k);
						kvp.UnsetValue();
					}
					result.Add(kvp);
				}
				return result;
			}
		}

		public int Count {
			get { return DATA.Count; }
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value) {
			SetValue(key, value);
		}

		bool IDictionary<TKey, TValue>.ContainsKey(TKey key) {
			return ContainsKey(key);
		}

		ICollection<TKey> IDictionary<TKey, TValue>.Keys {
			get {
				return DATA.Keys;
			}
		}

		bool IDictionary<TKey, TValue>.Remove(TKey key) {
			return DATA.Remove(key);
		}

		bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) {
			return DATA.TryGetValue(key, out value);
		}

		ICollection<TValue> IDictionary<TKey, TValue>.Values {
			get { return DATA.Values; }
		}

		TValue IDictionary<TKey, TValue>.this[TKey key] {
			get {
				return Value(key);
			}
			set {
				SetValue(key, value);
			}
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) {
			DATA.Add(item.Key, item.Value);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Clear() {
			DATA.Clear();
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) {
			if (!ContainsKey(item.Key))
				return false;
			TValue value = DATA[item.Key];
			return value.Equals(item.Value);


		}

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
			int offset = arrayIndex;
			foreach (TKey key in DATA.Keys) {
				KeyValuePair<TKey, TValue> instance = new KeyValuePair<TKey, TValue>(key, Value(key));
				array[offset] = instance;
				offset++;
			}
		}

		int ICollection<KeyValuePair<TKey, TValue>>.Count {
			get { return Count; }
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly {
			get { return IsReadOnly; }
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) {
			if (!ContainsValueAtKey(item.Key,item.Value))
				return false;
			RemoveByKey(item.Key);
			return true;
		}

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() {
			return DATA.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return DATA.GetEnumerator();
		}
	}

	//A Description of the DataType
	public sealed class DataType_dataReference : DataType {
		#region singleton
		private static volatile DataType_dataReference instance = new DataType_dataReference();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static DataType_dataReference() { }
		public static DataType Instance {
			get {
				if (instance == null) {
					lock (syncRoot) {
						instance = new DataType_dataReference();
					}
				}
				return instance;
			}
		}
		#endregion

		public override bool IsProtectable { get { return true; } }
		public override string Name { get { return "dataReference"; } }

	}

}
