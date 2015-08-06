﻿using System;
using MauronAlpha.HandlingErrors;
using System.Diagnostics;

namespace MauronAlpha.HandlingErrors {

	//A ErrorHandler
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
			StackTrace info = new StackTrace(true);
			PrintStackTrace(info);
			System.Console.ReadKey();
			Environment.Exit(1);
		}

		private MauronCode_error PrintStackTrace(StackTrace info){
			for(int i=0; i<info.FrameCount;i++){
				StackFrame frame = info.GetFrame(i);
				string className = frame.GetMethod().DeclaringType.Name;
				string methodName = frame.GetMethod().Name;
				int lineNumber = frame.GetFileLineNumber();
				System.Console.WriteLine("#"+i+" "+className+" - "+methodName+ "(Line: "+lineNumber+")", this);
			}
			return this;
		}

		//called on error creation by MauronCode
		public MauronCode_error TriggerCreationEvent(bool b_isRuntime) {
			if(ErrorType.ThrowOnCreation){
				throw Instance;
			}
			return this;
		}

		public MauronCode_error Instance {
			get {
				return new MauronCode_error(Message,ErrorSource,ErrorType);
			}
		}
	
	}

	

}
