using System;
using MauronAlpha.ExplainingCode;

namespace MauronAlpha.ErrorHandling {
	
	//Describes a Error Resolution Behavior (usually in cases where erronous userinput is interpreted/corrected)
	public class ErrorResolution:MauronCode {
		
		//constructor
		public ErrorResolution():base(CodeType_errorResolution.Instance) {}
		
		private string STR_description;
		public string Description {
			get {
				return STR_description;
			}
		}
		public ErrorResolution SetDescription(string description){
			STR_description=description;
			return this;
		}

		public static ErrorResolution DoNothing {
			get {
				return new ErrorResolution().SetDescription("Do Nothing, Error ignored");
			}
		}


	}

	public sealed class CodeType_errorResolution:CodeType {
		#region Singleton
		private static volatile CodeType_errorResolution instance=new CodeType_errorResolution();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static CodeType_errorResolution ( ) { }
		public static CodeType Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new CodeType_errorResolution();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "errorResolution"; } }
	}


}
