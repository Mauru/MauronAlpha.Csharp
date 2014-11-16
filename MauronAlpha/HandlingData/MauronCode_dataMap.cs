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

		//Type Info
		private Type TYPE_value;

		//Data
		private string[] DATA_keys;
		private TValue[] DATA_values;
		private bool[] DATA_state;

		//Working with values
				
	}	
}
