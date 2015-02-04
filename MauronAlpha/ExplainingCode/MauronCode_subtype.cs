using System;

using MauronAlpha.ExplainingCode;

namespace MauronAlpha {

	// a class that implements the utility codetype
	public class MauronCode_subtype : MauronCode {
		public MauronCode_subtype ( ) : base(CodeType_subtype.Instance) { }
	}

	//An "Explaining class" - which serves to describe the function of a class
	public sealed class CodeType_subtype : CodeType {
		#region singleton
		private static volatile CodeType_subtype instance=new CodeType_subtype();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_subtype ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance = new CodeType_subtype();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { 
			get { 
				return "subtype"; 
			} 
		}

	}

}