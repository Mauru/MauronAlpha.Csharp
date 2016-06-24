using System;

using System.Collections.Generic;
namespace MauronAlpha.HandlingData {
	
	public class MauronCode_dataRelation<K, V> : MauronCode_dataObject,IEquatable<KeyValuePair<K,V>> {

		K DATA_key;
		V DATA_value;

		private bool B_keyIsNull = false;
		public bool KeyIsNull {
			get { return B_keyIsNull; }
		}
		private bool B_valueIsNull = false;
		public bool ValueIsNull {
			get { return B_valueIsNull; }
		}
		
		public K Key {
			get {
				if (B_keyIsNull)
					return default(K);
				else
					return DATA_key;
			}
		}
		public V Value {
			get {
				if (B_valueIsNull)
					return default(V);
				else
					return DATA_value;
			}
		}

		public bool IsNull {
			get {
				return (B_keyIsNull && B_valueIsNull);
			}
		}
		public MauronCode_dataRelation(K key)	: base(DataType_dataRelation.Instance) {
			DATA_key = key;
			B_valueIsNull = true;
		}
		public MauronCode_dataRelation(): base(DataType_dataRelation.Instance) {
			B_keyIsNull = true;
			B_valueIsNull = true;
		}
		public MauronCode_dataRelation(K key, V value)	: base(DataType_dataRelation.Instance) {

			DATA_key = key;
			DATA_value = value;
				
		}
		public MauronCode_dataRelation(KeyValuePair<K, V> data): base(DataType_dataRelation.Instance) {
			if (data.Key.Equals(default(K)))
				B_keyIsNull = true;
			else
				DATA_key = data.Key;
			if(data.Value.Equals(default(V)))
				B_valueIsNull = true;
			else
				DATA_value = data.Value;
		}
		/*public MauronCode_dataRelation(Nullable<K> key, Nullable<V> value)	: base(DataType_dataRelation.Instance) {
			if (key.Equals(null))
				B_keyIsNull = true;
			else
				DATA_key = key.Value;

			if (value.Equals(null))
				B_valueIsNull = true;
			else
				DATA_value = value.Value;
		}
		public MauronCode_dataRelation(K key, Nullable<V> value): base(DataType_dataRelation.Instance) {
			DATA_key = key;
			if (value.Equals(null))
				B_valueIsNull = true;
			else
				DATA_value = value.Value;
		}
		public MauronCode_dataRelation(Nullable<K> key, V value)	: base(DataType_dataRelation.Instance) {
			if (key.Equals(null))
				B_keyIsNull = true;
			else
				DATA_key = key.Value;

			DATA_value = value;
		}
		*/
		public MauronCode_dataRelation<V, K> Flip {
			get {
				return new MauronCode_dataRelation<V, K>(DATA_value, DATA_key);
			}
		}

		public bool Equals(KeyValuePair<K, V> other) {
			return Key.Equals(other.Key) && Value.Equals(other.Value);
		}
		public bool Equals(MauronCode_dataRelation<K, V> other) {

			if (B_keyIsNull != other.KeyIsNull)
				return false;
			if (B_valueIsNull != other.ValueIsNull)
				return false;
			if (!Key.Equals(other.Key))
				return false;
			if (!Value.Equals(other.Value))
				return false;

			return true;
		}

		public KeyValuePair<K, V> AsKeyValuePair {
			get {
				K key;
				V value;

				if (!B_keyIsNull)
					key = DATA_key;
				else
					key = default(K);

				if (!B_valueIsNull)
					value = DATA_value;
				else
					value = default(V);

				return new KeyValuePair<K, V>(key, value);
			}
		}

		public MauronCode_dataRelation<K, V> UnsetKey() {
			DATA_key = default(K);
			return this;
		}
		public MauronCode_dataRelation<K, V> SetKey(K key) {
			DATA_key = key;
			return this;
		}
		public MauronCode_dataRelation<K, V> UnsetValue() {
			DATA_value = default(V);
			return this;
		}
		public MauronCode_dataRelation<K, V> SetValue(V value) {
			DATA_value = value;
			return this;
		}

		public bool TryKey(ref K result) {
			if (B_keyIsNull)
				return false;

			result = DATA_key;
			return true;
		}
		public bool TryValue(ref V result) {
			if (B_valueIsNull)
				return false;

			result = DATA_value;
			return true;
		}
	}

	//Data definition
	public sealed class DataType_dataRelation : DataType {
		#region singleton
		private static volatile DataType_dataRelation instance=new DataType_dataRelation();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static DataType_dataRelation ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dataRelation();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataRelation"; } }

	}

}
