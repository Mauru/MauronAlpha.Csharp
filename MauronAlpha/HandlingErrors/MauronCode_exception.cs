using System;

namespace MauronAlpha.HandlingErrors {

	//Describes either a correctable Verification Problem
	//Or a Precursor State to a serious Error
	public class MauronCode_exception : Exception {

		#region The (Potential) Error Source
		private object OBJ_errorSource;
		public object ErrorSource {
			get {
				if(OBJ_errorSource==null){
					MauronCode.NullError("ErrorSource can not be null!",this,typeof(object));
				}
				return OBJ_errorSource;				
			}
		}
		private MauronCode_exception SetErrorSource(object source){
			OBJ_errorSource = source;
			return this;
		}
		public bool ContainsErrorSource { 
			get {
				return (OBJ_errorSource == null);
			}			
		}
		#endregion

		#region The (Potential) Error Resolution
		private ErrorResolution ERR_resolution;
		public ErrorResolution ErrorResolution {
			get {
				if(ERR_resolution == null){
					MauronCode.NullError("ErrorResolution can not be null!",this,typeof(ErrorResolution));
				}
				return ERR_resolution;
			}
		}
		private MauronCode_exception SetErrorResolution(ErrorResolution resolution){
			ERR_resolution=resolution;
			return this;
		}
		public bool ContainsErrorResolution {
			get {
				return (ERR_resolution==null);
			}
		}
		#endregion

		public MauronCode_exception (string message, object source, ErrorResolution resolution)
			: base(message) {

			#region Basic ExceptionHandler
			MauronCode.Debug(message, source);
			SetErrorSource(source);
			#endregion
		
		}

		#region A ErrorSource that is Delayed
		public static object DelayedSource {
			get {
				return new ExceptionHandler(typeof(object),true);
			}
		}
		#endregion

		#region A ErrorResolution that is delayed
		public static ErrorResolution DelayedResolution { 
			get {
				return ErrorResolution.Delayed;
			}
		}
		#endregion
	}

}