using System;
using MauronAlpha.Projects;
using MauronAlpha.HandlingData;
using MauronAlpha.ExplainingCode;
using MauronAlpha.Text.Units;
using MauronAlpha.Text;

namespace MauronAlpha.ConsoleApp.Linux
{

	//A superbasic console
	public class MauronConsole : MauronCode_project, I_textDisplay {
		public MauronConsole ( )
			: base(
				ProjectType_generic.Instance,
				"Console Application in Linux") {

		}
		public MauronConsole (string title)
			: this() {
			SetTitle(title);
			SetTitleVisible(true);
			Clear();
		}

		//title
		private static string STR_title="MauronConsole Window in Linux";
		public string Title { get { return STR_title; } }
		public MauronConsole SetTitle (string title) {
			STR_title=title;
			return this;
		}

		//print output as MauronCode.Debug
		private bool B_writeAsDebug=true;
		public bool WriteAsDebug { get { return B_writeAsDebug; } }
		public MauronConsole SetWriteAsDebug (bool status) {
			B_writeAsDebug=status;
			return this;
		}

		//show the title in window at all times
		private bool B_titleVisible=false;
		public bool TitleVisible {
			get {
				return B_titleVisible;
			}
		}
		public MauronConsole SetTitleVisible (bool visible) {
			B_titleVisible=visible;
			return this;
		}

		//Show the line Numbers
		private bool B_lineNumbersVisible=false;
		public bool LineNumbersVisible {
			get {
				return B_lineNumbersVisible;
			}
		}
		public MauronConsole SetLineNumbersVisible (bool visible) {
			B_lineNumbersVisible=visible;
			return this;
		}

		//Does the console linenumber start at 0 or at 1
		private bool B_linesStartAtZero=true;
		public bool LinesStartAtZero {
			get {
				return B_linesStartAtZero;
			}
		}
		public MauronConsole SetLineStartAtZero (bool status) {
			B_linesStartAtZero=status;
			return this;
		}

		//The LineBuffer
		public MauronCode_dataList<TextComponent_line> LineBuffer=new MauronCode_dataList<TextComponent_line>();

		//The character that seperates line number and text
		private string CHAR_LineSeperator="#";
		public string LineSeperator {
			get {
				return CHAR_LineSeperator;
			}
		}

		private TextComponent_line LINE_active;
		public TextComponent_line ActiveLine {
			get {
				if( LINE_active==null ) {
					LineBuffer.Add(TextComponent_line.New(this));
				}
				return LineBuffer.LastElement;
			}
		}
		private MauronConsole SetActiveLine (TextComponent_line line) {
			LINE_active=line;
			return this;
		}

		#region output to window
		//Clear the window
		public MauronConsole Clear ( ) {
			System.Console.Clear();
			SetActiveLine(FirstLine);
			if( TitleVisible ) {
				Write(Title);
			}
			return this;
		}
		//Write a line
		public MauronConsole Write (string s) {
			if( !WriteAsDebug ) {
				Debug(s, this);
				return this;
			}
			LineBuffer.Add(TextComponent_line.New(this, s));
			SetActiveLine(LineBuffer.LastElement);
			WriteLine(LineBuffer.LastElement);
			return this;
		}
		public MauronConsole WriteLine (TextComponent_line line) {
			Console.WriteLine(MakeLineStart(line)+line.Text);
			return this;
		}
		public new MauronConsole Debug (string msg, object obj) {
			MauronCode.Debug(msg, obj);
			return this;
		}
		//Generate the line seperator
		public string MakeLineStart (TextComponent_line line) {
			if( !LineNumbersVisible ) {
				return null;
			}
			int lineindex=line.Index;
			if( TitleVisible ) {
				lineindex=lineindex-1;
				if( line.Index==1 ) {
					return null;
				}
			}
			if( LinesStartAtZero ) {
				lineindex=lineindex-1;
			}
			return ""+lineindex+LineSeperator;
		}
		#endregion



		#region Lines
		//Get the first Line
		public TextComponent_line FirstLine {
			get {
				if( LineBuffer.IsEmpty ) {
					LineBuffer.Add(new TextComponent_line(this, LineBuffer.NextIndex));
				}
				return LineBuffer.FirstElement;
			}
		}
		//Get the last Line
		public TextComponent_line LastLine {
			get {
				if( LineBuffer.IsEmpty ) {
					LineBuffer.Add(new TextComponent_line(this, LineBuffer.NextIndex));
				}
				return LineBuffer.LastElement;
			}
		}
		#endregion


	}


	class MainClass {
		//Testing MauronCode_datalist
		public static void Main (string[] args)
		{
			//Create environment
			MauronConsole m = new MauronConsole("Testing MauronCode_datalist");

			MauronCode_dataList<string> list = new MauronCode_dataList<string> ();
			list.Add("L");
			foreach (string s in list) {
				m.Write (s);
			}

		}
	}

}
