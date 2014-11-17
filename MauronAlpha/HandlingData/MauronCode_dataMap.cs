using System;
using System.Collections.Generic;

using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//A data array that maps a string to a generic
	public class MauronCode_dataMap<TValue> : MauronCode_dataObject {

		//constructor
		public MauronCode_dataMap():base(DataType_dataDictionary.Instance){
			DATA_keys = new string[]{};
			DATA_values=new TValue[] { };
			DATA_state = new bool[] {};
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
        public string[] Keys {
            get {
                return DATA_keys;
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
        #endregion

        #region Working with Values
        public TValue[] Values {
            get { return DATA_values; }
        }
        public TValue Value(string key) {
            int n = IndexOfKey(key);
            if(n<0){
                throw Error("Invalid Index!,{"+key+"},(Value)",this,ErrorType_index.Instance);
            }
            if (n >= DATA_values.Length) {
                throw Error("Invalid Index!,{" + key + "},(Value)", this, ErrorType_index.Instance);
            }
            return DATA_values[n];
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

            return this;
        }
        #endregion

        public MauronCode_dataMap<TValue> SetIsReadOnly(bool state) {
            B_isReadOnly = state;
            return this;
        }

		//Type Info
		private Type TYPE_value;

		//Data
		private string[] DATA_keys;
		private TValue[] DATA_values;
		private bool[] DATA_state;
	}	
}
