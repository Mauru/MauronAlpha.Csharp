using System;
using MauronAlpha.Projects;
using MauronAlpha.HandlingData;
namespace MauronAlpha.ConsoleApp.Linux
{
	class MainClass
	{
		//A superbasic console
		public class MauronConsole:MauronCode_project {
			public MauronConsole():base(
			ProjectType_generic.Instance,
			"Console Application in Linux") {

			}
			public MauronConsole(string title):this() {
				SetTitle(title);
				SetTitleVisible(true);
				Clear();
			}

			//title
			private static string STR_title="MauronConsole Window in Linux";
			public string Title { get { return STR_title; } }
			public MauronConsole SetTitle(string title){
				STR_title = title;
				return this;
			}

			//print output as MauronCode.Debug
			private bool B_writeAsDebug=true;
			public bool WriteAsDebug { get { return B_writeAsDebug; } }
			public MauronConsole SetWriteAsDebug(bool status){
				B_writeAsDebug = status;
				return this;
			}

			//show the title in window at all times
			private bool B_titleVisible = false;
			public bool TitleVisible {
				get {
					return B_titleVisible;
				}
			}
			public MauronConsole SetTitleVisible(bool visible){
				B_titleVisible = visible;
				return this;
			}

			//Show the line Numbers
			private bool B_lineNumbersVisible = false;
			public bool LineNumbersVisible {
				get {
					return B_lineNumbersVisible;
				}
			}
			public MauronConsole SetLineNumbersVisible(bool visible){
				B_lineNumbersVisible = visible;
				return this;
			}

			//Does the console linenumber start at 0 or at 1
			private bool B_linesStartAtZero = true;
			public bool LinesStartAtZero {
				get {
					return B_linesStartAtZero;
				}
			}
			public MauronConsole SetLineStartAtZero(bool status){
				B_linesStartAtZero = status;
				return this;
			}

			//The LineBuffer
			public MauronCode_dataList<Line> LineBuffer = new MauronCode_dataList<Line> ();

			//The character that seperates line number and text
			private string CHAR_LineSeperator= "#";
			public string LineSeperator { get {
				return CHAR_LineSeperator;
			} }

			private Line LINE_active;
			public Line ActiveLine {
				get {
					if (LINE_active == null) {
						LineBuffer.Add (Line.New (this));
					}
					return LineBuffer.LastElement;
				}
			}
			private MauronConsole SetActiveLine(Line line){
				LINE_active = line;
				return this;
			}

			#region output to window
			//Clear the window
			public MauronConsole Clear(){
				System.Console.Clear();
				SetActiveLine (FirstLine);
				if (TitleVisible) {
					Write (Title);
				}
				return this;
			}
			//Write a line
			public MauronConsole Write(string s){
				if (!WriteAsDebug) {
					Debug (s, this);
					return this;
				}
				LineBuffer.Add (Line.New(this,s));
				SetActiveLine (LineBuffer.LastElement);
				WriteLine (LineBuffer.LastElement);
				return this;
			}
			public MauronConsole WriteLine(Line line){
				Console.WriteLine(MakeLineStart(line)+line.Text);
				return this;
			}
			public new MauronConsole Debug(string msg, object obj){
				MauronCode.Debug (msg, obj);
				return this;
			}
			//Generate the line seperator
			public string MakeLineStart(Line line){
				if (!LineNumbersVisible) {
					return null;
				}
				int lineindex=line.Index;
				if(TitleVisible){
					lineindex = lineindex -1;
					if (line.Index == 1) {
						return null;
					}
				}
				if (LinesStartAtZero) {
					lineindex=lineindex-1;
				}
				return ""+lineindex+LineSeperator;
			}
			#endregion



			#region Lines
			//Get the first Line
			public Line FirstLine {
				get {
					if (LineBuffer.IsEmpty) {
						LineBuffer.Add (new Line (this, LineBuffer.NextIndex));
					}
					return LineBuffer.FirstElement;
				}
			}
			//Get the last Line
			public Line LastLine {
				get {
					if (LineBuffer.IsEmpty) {
						LineBuffer.Add (new Line (this, LineBuffer.NextIndex));
					}
					return LineBuffer.LastElement;
				}
			}
			#endregion


		}

		public class Line:MauronCode_dataObject {

			//constructor
			public Line(MauronConsole console,int index){
				SetIndex(index);
				SetWindow(console);
			}
			public Line(MauronConsole console,int index, string text){
				SetIndex(index);
				SetWindow(console);
				SetText(text); 
			}

			//is the line empty (unset)
			private bool B_isEmpty = true;
			public bool IsEmpty {
				get {
					return B_isEmpty;
				}
			}
			private Line SetIsEmpty(bool status){
				B_isEmpty = status;
				return this;
			}

			//Clear the text
			public Line Clear(){
				SetText(null);
				SetIsEmpty (true);
				return this;
			}


			//the Text
			private string STR_text;
			public string Text { get { return STR_text; } }
			public Line SetText(string text) {
				STR_text = text;
				SetIsEmpty(false);
				return this;
			}

			private MauronConsole M;
			public MauronCode Window { 
				get {
					if (M == null) {
						MauronCode.Error ("Invalid output source (currently hardcoded to console)", this);
					}
					return M; 
				}
			}
			public Line SetWindow(MauronConsole m){
				M = m;
				return this;
			}

			public static Line New(MauronConsole window, string s) {
				return new Line (window,window.LineBuffer.NextIndex,s);
			}
			public static Line New(MauronConsole window){
				return new Line (window, window.LineBuffer.NextIndex);
			}


			//the line number
			private int INT_index;
			public int Index { get { return INT_index; } }
			public Line SetIndex(int n){
				INT_index = n;
				return this;
			}
		}
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
