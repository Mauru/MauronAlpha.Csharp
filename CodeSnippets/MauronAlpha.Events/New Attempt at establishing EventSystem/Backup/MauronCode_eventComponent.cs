using System;

using MauronAlpha.ExplainingCode;

namespace MauronAlpha.Events {
	
	//Base class for all event-related code
	public class MauronCode_eventComponent:MauronCode {

		//constructor
		public MauronCode_eventComponent():base(CodeType_eventComponent.Instance){}

	}

	//Code Description
	public sealed class CodeType_eventComponent : CodeType {
		#region singleton
		private static volatile CodeType_eventComponent instance=new CodeType_eventComponent();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_eventComponent ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_eventComponent();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "eventComponent"; } }
	}

}
