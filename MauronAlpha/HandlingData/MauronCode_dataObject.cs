using System;

namespace MauronAlpha.HandlingData {

	//A class that contains data and datamethods
	public abstract class MauronCode_dataObject : MauronCode, I_dataObject {
		public MauronCode_dataObject():base(CodeType_dataObject.Instance) {}
		
		#region I_dataObject
		public abstract string[] ProperyKeys { get; }
		#endregion

		internal abstract string[] STR_propertyKeys { get; }
		public string[] PropertyKeys {
			get { return STR_propertyKeys; }
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