using System;
using MauronAlpha.ExplainingCode;

namespace MauronAlpha.Forms {

	//A visual component of a programm
	public class DisplayObject:MauronCode {

		//constructor
		public DisplayObject():base(CodeType_displayObject.Instance){
		}

	}

	//CodeTyope description
	public sealed class CodeType_displayObject : CodeType {
		#region singleton
		private static volatile CodeType_displayObject instance=new CodeType_displayObject();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_displayObject ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_displayObject();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "displayObject"; } }
	}

}
