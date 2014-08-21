using System;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingErrors {

	public class MauronCode_error : Exception {

		public object ErrorSource;
		protected ErrorType ET_errorType;
		public ErrorType ErrorType { 
			get {
				return ET_errorType;
			}
		}
		private MauronCode_error SetErrorType(ErrorType errorType){
			ET_errorType=errorType;
			return this;
		}
		
		public MauronCode_error (string message, object source, ErrorType errorType)	: base(message) {
			MauronCode.Debug(message,source);
			ErrorSource=source;
			SetErrorType(errorType);
			Environment.Exit(1);
		}

	}

	

}
