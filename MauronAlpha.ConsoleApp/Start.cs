using MauronAlpha;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MauronAlpha.ConsoleApp {
	
	public class MauronAlpha_ConsoleLauncher {
		static void Main (string[] args) {
			MauronAlpha_Win7ConsoleApp EXE_CommandLineWindow = new MauronAlpha_Win7ConsoleApp();
			EXE_CommandLineWindow.Start();
		}
	}
}
#region Old Code
/*
	class Start {
		static MAU MAU=MAU.Instance;
		
		static void Main (string[] args) {
		Console.
			System.Console..TreatControlCAsInput=true;
			Console.Title="#MAU>Console - Type #quit; to quit";
			Console.BackgroundColor=ConsoleColor.DarkBlue;
			Console.ForegroundColor=ConsoleColor.Yellow;
			Console.CursorSize=50;
			//Console.TreatControlCAsInput=true;
			while(MAU.Busy){
				MAU.Cycle();
			};			
		}
		public static void ParseLine(string cmd){
			if (cmd!=null&&cmd.Length>2&&cmd.Substring(0,2)=="#."){
				string cmd_actual = cmd.Substring(2,cmd.Length-2);

				//applyicon(%PATH{\"c#file.ico\"},%ALL_FOLDERS)"){
			}
		}
		public static bool AwaitCommand(){
			return MAU.Cycle();
		} 
	}
	
	//A console Application
	public sealed class MAU : MauronCode_singleton, I_MauronAlphaConsole {
		#region Singleton
			private static volatile MAU instance=new MAU();
			private static object syncRoot=new Object();
			//constructor singleton multithread safe
			static MAU ( ) { }
			public static MAU Instance {
				get {
					if( instance==null ) {
						lock( syncRoot ) {
							instance=new MAU();
						}
					}
					return instance;
				}
			}
		#endregion
		
	}

	public interface I_MauronAlphaConsole {}
} 
 * */
#endregion