using MauronAlpha.Projects;
using System;
using MauronAlpha.ExplainingCode;

namespace MauronAlpha.ConsoleApp {
	
	//MAU is the command line environment for MauronAlpha
	public class MauronAlpha_Win7ConsoleApp:MauronCode_project {
		//Constructor
		public MauronAlpha_Win7ConsoleApp ( ) : base(ProjectType_Win7ConsoleApp.Instance, MyName) { }
		public static string MyName { get { return "MauronAlpha Windows 7 Command Line Tool"; } }

		public MauronAlpha_Win7ConsoleApp Start ( ) {

			SetConsoleWindowDefaults();
			while(!B_canExit){
				AwaitUserInput();
			}
			return this;
		}

		private bool B_canExit=false;
		public bool CanExit { get { return B_canExit; } }
		public MauronAlpha_Win7ConsoleApp SetCanExit(bool b) {
			B_canExit = b;
			return this;
		}
		public MauronAlpha_Win7ConsoleApp Exit () {
			SetCanExit(true);
			return this;
		}

		//Get the Title
		private string STR_title = MyName;
		public string Title { get { return STR_title; } }
		public MauronAlpha_Win7ConsoleApp SetTitle(string title) {
			STR_title=title;
			System.Console.Title=title;
			return this;
		}

		//Setup the Console Window
		private MauronAlpha_Win7ConsoleApp SetConsoleWindowDefaults() {
			System.Console.TreatControlCAsInput=true;
			System.Console.Title=Title;
			System.Console.BackgroundColor=ConsoleColor.DarkBlue;
			System.Console.ForegroundColor=ConsoleColor.Yellow;
			System.Console.CursorSize=50;
			return this;
		}

		//Clear the screen
		public MauronAlpha_Win7ConsoleApp ClearScreen() {
			System.Console.Clear();
			return this;
		}

		//Wait for any user key press
		private bool B_awaitingUserInput = false;
		public bool AwaitingUserInput { get {
			return B_awaitingUserInput;
		} }
		public MauronAlpha_Win7ConsoleApp AwaitUserInput() {
			B_awaitingUserInput=true;
			SetLastInput(System.Console.ReadLine());
			return this;
		}

		//Write a Line of text to Screen
		public MauronAlpha_Win7ConsoleApp WriteLine(string output){
			SetLastOutput(output);
			System.Console.WriteLine(output);
			return this;
		}

		//Last Output
		private string STR_lastOutput;
		public string LastOutput {
			get {
				return STR_lastOutput;
			}
		}
		public MauronAlpha_Win7ConsoleApp SetLastOutput (string output) {
			STR_lastOutput=output;
			return this;
		}

		//Last Input
		private string STR_lastInput;
		public string LastInput {
			get {
				return STR_lastInput;
			}
		}
		public MauronAlpha_Win7ConsoleApp SetLastInput(string input) {
			STR_lastInput=input;
			return this;
		}

	}

	//Project Description
	public sealed class ProjectType_Win7ConsoleApp : ProjectType, I_Singleton {
		#region singleton
		private static volatile ProjectType_Win7ConsoleApp instance=new ProjectType_Win7ConsoleApp();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ProjectType_Win7ConsoleApp ( ) { }
		public static ProjectType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ProjectType_Win7ConsoleApp();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "CommandLineTool"; } }
	}

}
