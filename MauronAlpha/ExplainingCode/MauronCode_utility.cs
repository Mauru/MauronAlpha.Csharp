using System;

using MauronAlpha.ExplainingCode;

namespace MauronAlpha {

	// a class that implements the utility codetype
	public class MauronCode_utility : MauronCode {
		public MauronCode_utility ( ) : base(CodeType_utility.Instance) { }
	}

	//A class that offers exclusively static functions
	public sealed class CodeType_utility : CodeType {
		#region singleton
		private static volatile CodeType_utility instance=new CodeType_utility();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_utility ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_utility();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "utility"; } }

	}

}