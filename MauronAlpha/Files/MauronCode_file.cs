using System;

namespace MauronAlpha.Files {

	public class MauronCode_file : MauronCode {
		public MauronCode_file ( ) : base(CodeType_file.Instance) { }
	}
	public sealed class CodeType_file : CodeType {
		#region singleton
		private static volatile CodeType_file instance=new CodeType_file();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_file ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_file();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "file"; } }
	}

}
