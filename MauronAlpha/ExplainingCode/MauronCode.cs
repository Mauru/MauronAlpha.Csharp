﻿using System;
using MauronAlpha.ErrorHandling;
using MauronAlpha.ExplainingCode;

namespace MauronAlpha {

    public class MauronCode:MauronAlphaCode {
		public CodeType CodeType;
		public MauronCode(CodeType codetype){
			CodeType=codetype;
		}

		//Error handling
		public static MauronCode_error Error(string msg, object o){
			MauronCode_error e = new MauronCode_error(msg, o, ErrorType_fatal.Instance);
			throw e;
		}
		public static MauronCode_error Exception(string msg, object o){
			throw new MauronCode_error(msg, o, ErrorType_exception.Instance);
		}
    }

}