using System;
using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//A class that contains data and datamethods
	public abstract class MauronCode_dataObject : MauronCode, I_dataObject {
		public MauronCode_dataObject(DataType dataType):base(CodeType_dataObject.Instance) {}

		private DataType DT_dataType;
		public DataType DataType { get {
			if (DT_dataType == null){
				throw NullError("DataType can not be null!,(DataType)", this,typeof(DataType));
			}
			return DT_dataType;
		} }
		private void SetDataType(DataType dataType) {
			DT_dataType = dataType;
		}
	
	}

	//Functionality
	public sealed class CodeType_dataObject : CodeType {
		#region singleton
		private static volatile CodeType_dataObject instance=new CodeType_dataObject();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_dataObject ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_dataObject();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataObject"; } }

	}
}