using System;
using MauronAlpha.ExplainingCode;
using MauronAlpha.ExplainingCode.Languages;

namespace MauronAlpha.HandlingData {

	//A class that contains a single data value as either string or binary
	public class DataObject_plain:MauronCode_dataObject,I_data {

		//A data object that contains a single value and possibly a name
		public DataObject_plain ( string name, CodeStyle codeStyle ) : base() { }

		#region I_data
		private string STR_name;
		public string Name {
			get { return STR_name; }
		}
		public DataObject_plain SetName (string name) {
			STR_name=name;
			return this;
		}
		#endregion
	}
}