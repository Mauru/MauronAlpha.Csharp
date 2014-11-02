using MauronAlpha.Input.Keyboard;
using MauronAlpha.HandlingData;
using MauronAlpha.Forms.Units;
using System;


namespace MauronAlpha.ConsoleApp {


	public class KeyMap_mauronConsole:KeyMap {			

		//Constructor
		public KeyMap_mauronConsole(MauronConsole target) {
			SetTarget(target);
		}

		#region The related Console
		private MauronConsole C_target;
		public MauronConsole Target {
			get {
				if( C_target==null ) {
					NullError("Console can not be null!,(Target)", this, typeof(MauronConsole));
				}
				return C_target;
			}
		}
		public KeyMap_mauronConsole SetTarget (MauronConsole target) {
			C_target=target;
			return this;
		}
		#endregion

		#region the KeyScripts
		MauronCode_dataMap<SpecialKey> FunctionKeys=new MauronCode_dataMap<SpecialKey>();
		#endregion

		public static void Key_RemoveLastChar(FormUnit_textField text) {
			
		}
	}

}
