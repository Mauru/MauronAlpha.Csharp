﻿using System;
using MauronAlpha.HandlingErrors;
using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingData.Hashing;


namespace MauronAlpha {

	//Base Class of 
    public class MauronCode:MauronAlphaCode, I_MauronCode {

		public CodeType CodeType;
		public MauronCode(CodeType codetype){
			CodeType=codetype;
		}

		public static MauronCode_error Error (string msg, object o,ErrorType errorType) {
			MauronCode_error e = new MauronCode_error(msg, o, errorType);
			return e.TriggerCreationEvent(true);
		}
		
		public static MauronCode_error NullError (string msg, object o, Type expected) {
			MauronCode_error e = new MauronCode_error(msg+" #[Expected:"+expected.FullName+"]", o, ErrorType_nullError.Instance);
			return e.TriggerCreationEvent(true);
		}

		public static MauronCode_error Exception(string msg, object o, ErrorResolution resolution){
			MauronCode_error e=new MauronCode_error(msg, o, ErrorType_exception.Instance);
			return e.TriggerCreationEvent(false);
		}

		private string UNIQUE_objectId;
		public string Id {
			get {
				if( UNIQUE_objectId==null ) {
					Type t = this.GetType();
					string id = t.AssemblyQualifiedName+MauronCode_hash.Unique;
					UNIQUE_objectId=id;
				}
				return UNIQUE_objectId;
			}
		}

    }

}