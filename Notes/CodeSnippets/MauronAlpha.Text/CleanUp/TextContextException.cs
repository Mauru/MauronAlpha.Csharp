using System;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Text.Context {


	public class TextContextException:MauronCode_exception {

		public TextContextException():base("TextContextException",MauronCode_exception.DelayedSource,MauronCode_exception.DelayedResolution){
			
		}

		
	}
}
