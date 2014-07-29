using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha {

	public class MauronCode_generic:MauronCode {
		public MauronCode_generic ( ) : base(CodeType_generic.Instance) { }
	}
	//Generic snippets of code
	public sealed class CodeType_generic : CodeType {
		#region singleton
		private static volatile CodeType_generic instance=new CodeType_generic();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_generic ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_generic();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "generic"; } }
	}
}
