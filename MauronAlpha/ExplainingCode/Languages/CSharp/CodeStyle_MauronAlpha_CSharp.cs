using System;

namespace MauronAlpha.ExplainingCode.Languages.Csharp {

	// Code created in Csharp for MauronAlpha
    public sealed class CodeStyle_mauronAlpha_CSharp:CodeStyle {
		#region singleton
		private static volatile CodeStyle_mauronAlpha_CSharp instance=new CodeStyle_mauronAlpha_CSharp();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeStyle_mauronAlpha_CSharp ( ) { }
		public static CodeStyle Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeStyle_mauronAlpha_CSharp();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "mauronAlpha_Csharp"; } }

    }

}
