using System;

using MauronAlpha.ExplainingCode;

namespace MauronAlpha.Files {

	public class MauronCode_filereference:MauronCode {
		public MauronCode_filereference ( ) : base(CodeType_filereference.Instance) { }
	}
	//Generic snippets of code
	public sealed class CodeType_filereference : CodeType {
		#region singleton
		private static volatile CodeType_filereference instance=new CodeType_filereference();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_filereference ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_filereference();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "filereference"; } }
	}
}
