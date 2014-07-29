using System;

namespace MauronAlpha {

	// a class that implements the utility codetype
	public class MauronCode_singleton : MauronCode {
		public MauronCode_singleton ( ) : base(CodeType_singleton.Instance) { }
	}

	//A Class using the singleton multithread pattern
	public sealed class CodeType_singleton : CodeType {
		#region singleton
		private static volatile CodeType_singleton instance=new CodeType_singleton();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_singleton ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_singleton();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "singleton"; } }
	}

}