using System;
using System.Collections.Generic;

using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//A data array that maps a string to a generic
	public class MauronCode_dataMap<TValue> : MauronCode_dataObject {

		//constructor
		public MauronCode_dataMap():base(DataType_dataDictionary.Instance){
			DATA_keys = new string[]{};
			DATA_values=new MauronCode_dataTree<long,TValue>();
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
        public string[] Keys {
            get {
                string[] result = new string[DATA_keys.Length-1];
				DATA_keys.CopyTo(result,0);
				return result;
            }
        }
        public int IndexOfKey(string key) {
            for (int n = 0; n < DATA_keys.Length; n++) {
                string k = DATA_keys[n];
                if (k == key) {
                    return n;
                }
            }
            Exception("Invalid Index!", this, ErrorResolution.ReturnNegativeIndex);
            return -1;
        }
        public MauronCode_dataMap<TValue> AddKey(string key) {
            if (IsReadOnly) {
                throw Error("Is protected!,(AddKey)", this, ErrorType_protected.Instance);
            }
            if (IndexOfKey(key) >= 0) {
                throw Error("Key allready Exists!,{" + key + "},(AddKey)", this, ErrorType_index.Instance);
            }
            int newIndex = DATA_keys.Length;
            string[] newKeys = new string[newIndex];
            Keys.CopyTo(newKeys, 0);
            newKeys[newIndex] = key;
            DATA_keys = newKeys;
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
                throw Error("Is protected", this, ErrorType_protected.Instance);
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
		#endregion

        public MauronCode_dataMap<TValue> SetIsReadOnly(bool state) {
            B_isReadOnly = state;
            return this;
        }

		//Data
		private string[] DATA_keys;
		private MauronCode_dataTree<long,TValue> DATA_values;
	}
	
	public class DataMap_enumerator<TValue>:IEnumerator<TValue> {

		//constructor
		public DataMap_enumerator(TValue[] values){
		
		}

		public TValue Current {
			get { throw new NotImplementedException(); }
		}

		public void Dispose ( ) {
			throw new NotImplementedException();
		}

		object System.Collections.IEnumerator.Current {
			get { throw new NotImplementedException(); }
		}

		public bool MoveNext ( ) {
			throw new NotImplementedException();
		}

		public void Reset ( ) {
			throw new NotImplementedException();
		}
	}	
}
