using System;
using MauronAlpha.ErrorHandling;

namespace MauronAlpha {

    public class MauronCode:MauronAlphaCode {
		public CodeType CodeType;
		public MauronCode(CodeType codetype){
			CodeType=codetype;
		}

		//Error handling
		public void Error(string msg, object o){
			throw new MauronCode_error(msg, o, ErrorType_fatal.Instance);
		}
		public void Exception(string msg, object o){
			throw new MauronCode_error(msg, o, ErrorType_exception.Instance);
		}
    }

}