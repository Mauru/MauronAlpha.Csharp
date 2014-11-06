using System;
using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingErrors;


namespace MauronAlpha {

	//Base Class of 
    public class MauronCode:MauronAlphaCode, I_MauronCode {

		public CodeType CodeType;
		public MauronCode(CodeType codetype){
			CodeType=codetype;
		}

		//Error handling
		public static MauronCode_error Error(string msg, object o){
			MauronCode_error e = new MauronCode_error(msg, o, ErrorType_fatal.Instance);
			throw e;
		}
		public static MauronCode_error Error (string msg, object o,ErrorType errorType) {
			MauronCode_error e=new MauronCode_error(msg, o, errorType);
			throw e;
		}
		public static MauronCode_error NullError (string msg, object o, Type expected) {
			MauronCode_error e = new MauronCode_error(msg+" #[Expected:"+expected.FullName+"]", o, ErrorType_nullError.Instance);
			throw e;
		}
		public static MauronCode_error Exception(string msg, object o, ErrorResolution resolution){
			MauronCode_error e=new MauronCode_error(msg, o, ErrorType_exception.Instance);
			return e;
		}

    }

}