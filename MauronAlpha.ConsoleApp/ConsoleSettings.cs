﻿using System;
using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;
using MauronAlpha.Projects;

namespace MauronAlpha.ConsoleApp {
	
	public class ConsoleSettings:ProjectSettings {

		private MauronConsole MC_parent;
		private MauronConsole Parent {
			get { 
				if (MC_parent == null) {
					NullError ("Parent can not be null", this, typeof(MauronConsole));
				}
				return MC_parent;
			}
		}
		private ConsoleSettings SetParent(MauronConsole parent){
			MC_parent = parent;
			return this;
		}

		//constructor
		public ConsoleSettings(MauronConsole console){
			SetParent (console);
		}

		#region How to treat whitespaces and other special characters

		private bool B_visualizeStrings=false;
		public bool VisualizeStrings {
			get {
				return B_visualizeStrings;
			}
		}
		public ConsoleSettings SetVisualizeStrings (bool status) {
			B_visualizeStrings=status;
			return this;
		}

		#endregion

		#region The Environment (i.e. Console stays open)

		//Is the applicaton a one time only process?
		private bool B_isEnvironment=false;
		public bool IsEnvironment { get { return B_isEnvironment; } }
		public ConsoleSettings SetIsEnvironment (bool status) {
			B_isEnvironment=status;
			return this;
		}

		#endregion

		#region Show Window title

		//show the title in window at all times
		private bool B_titleVisible=false;
		public bool TitleVisible {
			get {
				return B_titleVisible;
			}
		}
		public ConsoleSettings SetTitleVisible (bool visible) {
			B_titleVisible=visible;
			return this;
		}

		#endregion

		#region Print Output as MauronCode.Debug
		private bool B_writeAsDebug=true;
		public bool WriteAsDebug { get { return B_writeAsDebug; } }
		public ConsoleSettings SetWriteAsDebug (bool status) {
			B_writeAsDebug=status;
			return this;
		}
		#endregion

		#region Show the line Numbers
		private bool B_lineNumbersVisible=false;
		public bool LineNumbersVisible {
			get {
				return B_lineNumbersVisible;
			}
		}
		public ConsoleSettings SetLineNumbersVisible (bool visible) {
			B_lineNumbersVisible=visible;
			return this;
		}
		#endregion

		#region The character that seperates line number and text
		private string CHAR_LineSeperator="#";
		public string LineSeperator {
			get {
				return CHAR_LineSeperator;
			}
		}	
		#endregion
	
	}

}
