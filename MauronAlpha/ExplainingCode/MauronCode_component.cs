using System;

namespace MauronAlpha.ExplainingCode {

	//A Code-Package Defining class
	public class MauronCode_component:MauronCode {	
		//constructor
		public MauronCode_component():base(CodeType_component.Instance) {}

		public static CodeType StaticCodeType {
			get {
				return CodeType_component.Instance;
			}
		}
	}

	//An "Explaining class" - which serves to describe the function of a class
	public sealed class CodeType_component : CodeType {
		#region singleton
		private static volatile CodeType_component instance=new CodeType_component();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_component ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_component();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "component"; } }

	}
}
