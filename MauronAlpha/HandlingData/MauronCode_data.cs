using System;
using MauronAlpha.ExplainingCode;

namespace MauronAlpha.HandlingData {

	//A class that contains a single data value as either string or binary
	public class MauronCode_data:MauronCode,I_data {
		public MauronCode_data():base(CodeType_data.Instance){}

		#region I_data
		private string STR_name;
		public string Name {
			get { return STR_name; }
		}
		public MauronCode_data SetName(string name){
			STR_name=name;
			return this;
		}
		#endregion
	}

	//Describing component
	public sealed class CodeType_data : CodeType {
		#region singleton
		private static volatile CodeType_data instance=new CodeType_data();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_data ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_data();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name { get { return "data"; } }
	}

}