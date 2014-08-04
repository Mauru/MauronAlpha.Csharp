using System;
using MauronAlpha.Projects;
using MauronAlpha.HandlingData;
using MauronAlpha.ExplainingCode;
using MauronAlpha.Text.Units;
using MauronAlpha.Text;

using MauronAlpha.Events;

namespace MauronAlpha.ConsoleApp
{
	//A superbasic console
	public class MauronConsole : MauronCode_project, I_textDisplay, I_eventSender, I_eventReceiver {

		//constructor
		public MauronConsole ( )
			: base(
				ProjectType_generic.Instance,
				"MauronConsole Application") {

		}
		public MauronConsole (string title)
			: this() {
			SetTitle(title);
			SetTitleVisible(true);
			SetIsEnvironment(true);
			Clear();
		}

		#region The Environment

		//Is the applicaton a one time only process?
		private bool B_isEnvironment=false;
		public bool IsEnvironment { get { return B_isEnvironment; } }
		public MauronConsole SetIsEnvironment(bool status) {
			B_isEnvironment=status;
			return this;
		}

		#endregion

		#region Window title
		private static string STR_title="MauronConsole Application";
		public string Title { get { return STR_title; } }
		public MauronConsole SetTitle (string title) {
			STR_title=title;
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
		#endregion

		#region Output to window

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
			TextBuffer.Add(TextComponent_line.New(this, s));
			SetActiveLine(TextBuffer.LastElement);
			WriteLine(TextBuffer.LastElement);
			return this;
		}

		//Write a whole Line
		public MauronConsole WriteLine (TextComponent_line text) {
			Console.WriteLine(MakeLineStart(text)+text.Text);
			return this;
		}

		//Write a Debug message
		public new MauronConsole Debug (string msg, object obj) {
			MauronCode.Debug(msg, obj);
			return this;
		}

		//print output as MauronCode.Debug
		private bool B_writeAsDebug=true;
		public bool WriteAsDebug { get { return B_writeAsDebug; } }
		public MauronConsole SetWriteAsDebug (bool status) {
			B_writeAsDebug=status;
			return this;
		}
	
		#endregion

		#region I_TextDisplay
		public I_textDisplay WriteLine (TextComponent text)
		{
			return WriteLine (text);

		}

		public I_textDisplay Write (TextComponent text)
		{
			return Write (text);
		}
		private TextBuffer TXT_buffer = new TextBuffer ();
		public TextBuffer TextBuffer {
			get {
				return TXT_buffer;
			}
		}
		public MauronConsole SetTextBuffer(TextBuffer buffer){
			TXT_buffer = buffer;
			return this;
		}
		#endregion

		#region Lines

		#region Line Start and -number display

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

		//The character that seperates line number and text
		private string CHAR_LineSeperator="#";
		public string LineSeperator {
			get {
				return CHAR_LineSeperator;
			}
		}

		#endregion

		//Generate the line seperator
		private string MakeLineStart (TextComponent_line line) {
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

		//The active Line
		private TextComponent_line LINE_active;
		public TextComponent_line ActiveLine {
			get {
				if( LINE_active==null ) {
					TextBuffer.Add(TextComponent_line.New(this));
				}
				return TextBuffer.LastElement;
			}
		}
		private MauronConsole SetActiveLine (TextComponent_line line) {
			LINE_active=line;
			return this;
		}

		//Get the first Line
		public TextComponent_line FirstLine {
			get {
				if( TextBuffer.IsEmpty ) {
					TextBuffer.Add(new TextComponent_line(this, TextBuffer.NextIndex));
				}
				return TextBuffer.FirstElement;
			}
		}
		
		//Get the last Line
		public TextComponent_line LastLine {
			get {
				if( TextBuffer.IsEmpty ) {
					TextBuffer.Add(new TextComponent_line(this, TextBuffer.NextIndex));
				}
				return TextBuffer.LastElement;
			}
		}
		
		#endregion

		#region I_eventSender

		//The event clock for handling timed events
		private MauronCode_eventClock CLOCK_eventlock;
		public MauronCode_eventClock EventClock { get { return CLOCK_eventlock; } }
		public MauronConsole SetEventClock (MauronCode_eventClock clock) {
			CLOCK_eventlock=clock;
			return this;
		}

		//Initialize the Event Handling
		public I_eventSender InitializeEventHandling ( ) {
			MauronCode_event updateEvent = new MauronCode_event();
			return this;
		}

		#endregion

		#region I_eventReceiver

		//Receive an event
		public I_eventReceiver ReceiveEvent (MauronCode_event e) {
			return this;
		}

		//Check for an event condition
		public bool IsEventCondition (MauronCode_event e) {
			return false;
		}

		#endregion






	}
}