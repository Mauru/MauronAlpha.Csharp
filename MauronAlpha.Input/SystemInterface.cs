using System;
using MauronAlpha.ExplainingCode;

namespace MauronAlpha.ConsoleApp {

	//A class that connects to functionality that might be limited to a certain operating system
	public class SystemInterface:MauronCode {
		
		//constructor
		public SystemInterface():base(CodeType_systemInterface.Instance){}

	}

	//A Class using the singleton multithread pattern
	public sealed class CodeType_systemInterface : CodeType {
		#region singleton
		private static volatile CodeType_systemInterface instance=new CodeType_systemInterface();
		private static object syncRoot=new Object();

		//constructor singleton multithread safe
		static CodeType_systemInterface ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_systemInterface();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "systemInterface"; } }
	}

}
