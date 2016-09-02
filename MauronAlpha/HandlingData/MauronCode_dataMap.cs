using System;
using System.Collections.Generic;

using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//A data array that maps a string to a generic
	public class MauronCode_dataMap<TValue> : MauronCode_dataObject,IEnumerable<TValue> {

		//constructor
		public MauronCode_dataMap():base(DataType_dataDictionary.Instance){
			DATA_keys = new string[]{};
			DATA_values = new MauronCode_dataTree<long,TValue>();
		}
		public MauronCode_dataMap (ICollection<string> keys, ICollection<TValue> values)
			: this() {
			if( keys.Count!=values.Count ) {
				throw Error("Keys/Values have different length!", this, ErrorType_constructor.Instance);
			}
			string[] v_key=new string[keys.Count];
			keys.CopyTo(v_key, 0);

			TValue[] v_value=new TValue[keys.Count];
			values.CopyTo(v_value, 0);

			for( long n=0; n<values.Count; n++ ) {
				SetValue(v_key[n], v_value[n]);
			}
		}

        private bool B_isReadOnly = false;
        public bool IsReadOnly {
            get { return B_isReadOnly;  }
        }

		public bool IsEmpty {
			get {
				foreach(string s in DATA_keys)
					return true;
				return false;
			}
		}

        #region Working with Keys
        public bool ContainsKey(string key) {
            foreach (string k in DATA_keys) {
                if (k == key) {
                    return true;
                }
            }
            return false;
        }
		public long KeyCount {
			get {
				return DATA_keys.Length;
			}
		}
        public MauronCode_dataList<string> Keys {
            get {
				return new MauronCode_dataList<string>(DATA_keys);
            }
        }
        public int IndexOfKey(string key) {
			int n = -1;
            foreach(string s in Keys) {
				n++;        
                if (key == s)
                    return n;
            }
            //Exception("Invalid Index!", this, ErrorResolution.ReturnNegativeIndex);
            return -1;
        }
        public MauronCode_dataMap<TValue> AddKey(string key) {
            if (IsReadOnly) {
                throw Error("Is protected!,(AddKey)", this, ErrorType_protected.Instance);
            }
			int index = IndexOfKey(key);

			

            if (index >= 0) {
                throw Error("Key allready Exists!,{" + key + "},(AddKey)", this, ErrorType_index.Instance);
            }

            int newIndex = DATA_keys.Length;

			//Create a new list of keys
			string[] newKeys = new string[newIndex+1];
            Keys.CopyTo(newKeys, 0);

			newKeys[newIndex] = key;


            DATA_keys = newKeys;

			DATA_values.AddKey (newIndex);

            return this;
        }
        public bool IsSet(string key) {
			long index = IndexOfKey(key);
			return DATA_values.ContainsIndex(index);
		}
		
		#endregion

        #region Working with Values
        public ICollection<TValue> Values {
            get { return DATA_values.ValidValues; }
        }
        public TValue Value(string key) {
            int n = IndexOfKey(key);
            if(n<0){
                throw Error("Invalid Index!,{"+key+"},(Value)",this,ErrorType_index.Instance);
            }
            if (n >= DATA_values.Count) {
                throw Error("Invalid Index!,{" + key + "},(Value)", this, ErrorType_index.Instance);
            }
            return DATA_values.Value(n);
        }
        public MauronCode_dataMap<TValue> SetValue(string key, TValue value) {
            if (IsReadOnly)
            {
				throw Error("Is protected!,(SetValue)", this, ErrorType_protected.Instance);
            }

            int n = IndexOfKey(key);
            if (n < 0)
            {
                AddKey(key);
                n = IndexOfKey(key);
            }
			DATA_values.SetValue(n, value);

            return this;
        }
        public bool ContainsValueAtIndex(long index){
			return DATA_values.ContainsIndex(index);
		}
		public TValue ValueByIndex(long index) {
			return DATA_values.Value(index);
		}

		public bool TryGet(string key, ref TValue result) {
			int n = IndexOfKey(key);
			if (n < 0)
				return false;
			return DATA_values.TryGet(n, ref result);
		}
		#endregion

		public void Add(string data, TValue val) {
			SetValue(data, val);
		}
		public void Add(KeyValuePair<string, TValue> val) {
			SetValue(val.Key, val.Value);
		}
		public void Add(MauronCode_dataRelation<string, TValue> val) {
			SetValue(val.Key, val.Value);
		}

        public MauronCode_dataMap<TValue> SetIsReadOnly(bool state) {
            B_isReadOnly = state;
            return this;
        }

		//Data
		private string[] DATA_keys;
		private MauronCode_dataTree<long,TValue> DATA_values;

		public IEnumerator<TValue> GetEnumerator() {
			return new DataMap_enumerator<TValue>(DATA_values.ValuesAsList);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
	
	public class DataMap_enumerator<TValue>:IEnumerator<TValue> {

		MauronCode_dataList<TValue> _values;
		int _current = -1;

		//constructor
		public DataMap_enumerator(MauronCode_dataList<TValue> map){
			_values = map;
		}

		public TValue Current {
			get { 
				try {
					return _values[_current];
				}
				catch (MauronCode_error){
					throw new MauronCode_error("Enumeration error: Invalid index {"+_current+"} in dataMap!",this,ErrorType_index.Instance);
				}
			}
		}

		public void Dispose ( ) {
			_values = null;
		}

		object System.Collections.IEnumerator.Current {
			get {
				return Current;
			}
		}

		public bool MoveNext ( ) {
			_current++;
			return _current < _values.Count;
		}

		public void Reset ( ) {
			_current = -1;
		}
	}	
}
