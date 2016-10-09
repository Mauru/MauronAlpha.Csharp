using System;

using MauronAlpha.ExplainingCode;

namespace MauronAlpha.HandlingData {

	//Base class for data components
	public class MauronCode_dataComponent:MauronCode {

		//constructor
		public MauronCode_dataComponent():base() {}
	}

	public sealed class CodeType_dataComponent:CodeType {
		#region singleton
		private static volatile CodeType_dataComponent instance=new CodeType_dataComponent();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_dataComponent ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_dataComponent();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataComponent"; } }

	}
}
