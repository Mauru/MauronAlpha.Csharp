using System;
using MauronAlpha.ExplainingCode;

namespace MauronAlpha.HandlingErrors {
	
	//Describes a Error Resolution Behavior (usually in cases where erronous userinput is interpreted/corrected)
	public class ErrorResolution:MauronCode {
		
		//constructor
		private ErrorResolution(string name, string description):base(CodeType_errorResolution.Instance) {
			STR_name = name;
			STR_description=description;
		}
		
		#region properties
		private string STR_name;
		public string Name {
			get {
				return STR_name;
			}
		}

		private string STR_description;
		public string Description {
			get {
				return STR_description;
			}
		}
		#endregion

		#region Static Defaults

		public static ErrorResolution Replaced {
			get {
				return new ErrorResolution(
					"Replaced",
					"The Item In an Object is allready assigned to. Its Pointer is being replaced. This might be expected behavior."
				);
			}
		}

		public static ErrorResolution Delayed {
			get {
				return new ErrorResolution(
					"Delayed",
					"The Solution is delayed until a later point in the script. This will most likely result in a Fatal Error."
				);
			}
		}

		public static ErrorResolution DoNothing {
			get {
				return new ErrorResolution(
					"DoNothing",
					"Do Nothing, Error ignored."
				);
			}
		}

		public static ErrorResolution ReturnEmpty {
			get {
				return new ErrorResolution(
					"ReturnEmpty",
					"Return an empty version of the expected result, (probably a default or instance)."
				);
			}
		}
        public static ErrorResolution ReturnNegativeIndex {
            get { 
                return new ErrorResolution(
                    "ReturnNegativeIndex",
                    "Return -1 as an Indicator for NULL in a positive numeric index."
                );
            }
        }

		public static ErrorResolution ExpectedReturn {
			get {
				return new ErrorResolution(
					"ExpectedReturn",
					"The Parameters were wrong, but the calling method knows how to react."
				);
			}
		}

		public static ErrorResolution Correct_minimum {
			get {
				return new ErrorResolution(
					"Correct_minimum",
					"Replace the property with the smallest valid value."
				);
			}
		}
		public static ErrorResolution Correct_maximum {
			get {
				return new ErrorResolution(
					"Correct_maximum",
					"Replace the property with the largest valid value."
				);
			}
		}

		public static ErrorResolution Create {
			get {
				return new ErrorResolution(
					"Create",
					"Create a new object with valid properties."
				);
			}
		}
		public static ErrorResolution Create_unreliable {
			get {
				return new ErrorResolution(
					"Create_unreliable",
					"We create a new Object but mark it as unreliable."
				);
			}
		}
		#endregion

		#region Dynamic Defaults
		public static ErrorResolution Function(string functionName) {
			return new ErrorResolution(
				"Function",
				"Use alternative ("+functionName+")."
			);
		}
		#endregion
	}

	//Code Description of this class
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
