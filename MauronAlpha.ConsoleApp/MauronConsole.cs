using System;
using MauronAlpha.Projects;
using MauronAlpha.HandlingData;
using MauronAlpha.ExplainingCode;

using MauronAlpha.Text.Units;
using MauronAlpha.Text;
using MauronAlpha.Text.Utility;

using MauronAlpha.Input.Keyboard;

using MauronAlpha.Events;
using MauronAlpha.Events.Defaults;

namespace MauronAlpha.ConsoleApp {
	
	//A superbasic console
	public class MauronConsole : MauronCode_project, I_eventSender, I_eventReceiver {

		#region Constructors

		private MauronConsole ( )
			: base(
				ProjectType_mauronConsole.Instance,
				"MauronConsole Application") {

		}
		public MauronConsole (string title)
			: this() {

			//Settings
			SetTitle(title);
			Settings.SetTitleVisible(true);
			Settings.SetIsEnvironment(true);

			//Start Environment Cycle
			SetCanExit(false);
			SubscribeToEvents();
			WaitForKeyUp();

		}

		#endregion

		#region Application title
		private static string STR_title="MauronConsole Application";
		public string Title { get { return STR_title; } }
		public MauronConsole SetTitle (string title) {
			STR_title=title;
			return this;
		}
		#endregion

		#region Console Settings
		private ConsoleSettings CFG_settings;
		public ConsoleSettings Settings {
			get {
				if(CFG_settings==null){
					SetSettings(new ConsoleSettings(this));
				}
				return CFG_settings;
			}
		}
		public MauronConsole SetSettings(ConsoleSettings settings) {
			CFG_settings=settings;
			return this;
		}
		#endregion

		#region Clear Console Window

		public MauronConsole ResetScreen ( ) {
			ClearScreen ();
			SetActiveLine(FirstLine);
			if( Settings.TitleVisible ) {
				Write(Title);
			}
			return this;
		}

		public MauronConsole ClearScreen() {
			System.Console.Clear ();
			return this;
		}

		#endregion

		#region Output to window

		//Write a line
		public MauronConsole Write (string s) {
			TextComponent_text = TextHelper.ParseString (s);
			TextHelper.MergeText (Text, s);
			return this;
		}

		//Write a whole Line
		public MauronConsole WriteLine (TextComponent_line line) {
			string txt="";
			if(Settings.VisualizeStrings){
				txt=Text.Visualized;
			}else{
				txt=Text.AsString;
			}
			Console.WriteLine(MakeLineStart(line)+txt);
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

		public I_textDisplay WriteLine (TextComponent text) {
			return WriteLine(text);

		}

		public I_textDisplay Write (TextComponent text) {
			return Write(text);
		}
		private TextComponent_text TXT_buffer=new TextComponent_text();
		public TextComponent_text TextBuffer {
			get {
				return TXT_buffer;
			}
		}
		public MauronConsole SetTextBuffer (TextComponent_text buffer) {
			TXT_buffer=buffer;
			return this;
		}

		#endregion

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

		//Generate the line seperator
		private string MakeLineStart (TextComponent_line line) {
			if( !LineNumbersVisible ) {
				return null;
			}
			int lineindex=TextBuffer.LineIndex(line);
			if( TitleVisible ) {
				lineindex=lineindex-1;
				if( TextBuffer.LineIndex(line) ) {
					return null;
				}
			}
			if( LinesStartAtZero ) {
				lineindex=lineindex-1;
			}
			return ""+lineindex+LineSeperator;
		}

		#endregion

		#region Defining what Line we are on

		//The active line
			public TextComponent_line ActiveLine {
				get {
					return TextBuffer.LineByIndex(LineIndex);
				}
			}
			public TextComponent_line FirstLine {
				get {
					return TextBuffer.FirstLine;
				}
			}
			public TextComponent_line LastLine {
				get {
					return TextBuffer.LastLine;
				}
			}		
			public TextComponent_line LineByIndex(int n){
				return TextBuffer.LineByIndex(n);
			}
					
			private int INT_line=0;
			public int LineIndex {
				get {
					return INT_line;
				}	
			}
			public MauronConsole SetLineIndex (int n) {
				if(n<0){
					INT_line=0;
				}else if(n>=TextBuffer.CountLines){
					INT_line=TextBuffer.CountLines-1;
				}else{
					INT_line=n;
				}
				return this;
			}
		
		#endregion

		#region Defining what word we are on
			
			//The active word
			public TextComponent_word ActiveWord {
				get {
					return TextBuffer.WordByIndex(WordIndex);
				}
			}
			public TextComponent_word FirstWord {
				get {
					return TextBuffer.FirstWord;
				}
			}
			public TextComponent_word LastWord {
				get {
					return TextBuffer.LastWord;
				}
			}
			public TextComponent_word WordByIndex (int n) {
				return TextBuffer.WordByIndex(n);
			}
			
			private int INT_word=0;
			public int WordIndex {
				get {
					return INT_word;
				}
			}
			public MauronConsole SetWordIndex(int n){
				if(n<0){
					INT_word=0;
				}else if(n>=TextBuffer.CountWords){
					INT_word=TextBuffer.CountWords-1;
				}else{
					INT_word=n;
				}
				return this;
			}

		#endregion

		#region Defining what character we are on

			//The active character
			public TextComponent_character ActiveCharacter {
				get {
					return TextBuffer.CharacterByIndex(CharacterIndex);
				}
			}
			public TextComponent_character FirstCharacter {
				get {
					return TextBuffer.FirstCharacter;
				}
			}
			public TextComponent_character LastCharacter {
				get {
					return TextBuffer.LastCharacter;
				}
			}
			public TextComponent_character CharacterByIndex (int n) {
				return TextBuffer.CharacterByIndex(n);
			}

			private int INT_character=0;
			public int CharacterIndex {
				get {
					return INT_character;
				}
			}
			public MauronConsole SetCharacterIndex(int n){
				if(n<0){
					INT_character=0;
				}else if(n>=TextBuffer.CountChracters){
					INT_character=TextBuffer.CountChracters-1;
				}else{
					INT_character=n;
				}
				return this;
			}

		#endregion

		#region Events

		#region I_eventReceiver

		public I_eventReceiver SubscribeToEvents ( ) {
			SubscribeToEvent(KeyPressCounter, "keyUp");
			return this;
		}

		public I_eventReceiver SubscribeToEvent (MauronCode_eventClock clock, string message) {
			clock.SubscribeToEvent(message,this);
			return this;
		}

		public I_eventReceiver ReceiveEvent (MauronCode_event e) {
			//Key Up
			if( e.Message=="keyUp" ) {
				Event_keyUp(e);
				return this;
			}
			return this;
		}

		#endregion

		#region I_eventSender

		//The event clock for handling timed events
		private MauronCode_eventClock CLOCK_eventlock;
		public MauronCode_eventClock EventClock {
			get {
				if( CLOCK_eventlock==null ) {
					Error("No Event Clock Set!", this);
				}
				return CLOCK_eventlock;
			}
		}
		public MauronConsole SetEventClock (MauronCode_eventClock clock) {
			CLOCK_eventlock=clock;
			return this;
		}

		public MauronConsole SendEvent (MauronCode_eventClock clock, string eventName, MauronCode_dataSet data) {

			//create a raw event
			MauronCode_event e=new MauronCode_event(
				clock,
				this,
				eventName,
				data
			).SetMessage(eventName).SetData(data);
			clock.SubmitEvent(e);
			return this;
		}
		I_eventSender I_eventSender.SendEvent (MauronCode_eventClock clock, string code, MauronCode_dataSet data) {
			return SendEvent(clock, code, data);
		}

		#endregion

		//KeyUp
		private MauronConsole Event_keyUp(MauronCode_event e) {
			KeyPressCounter.AdvanceTime();
			KeyPress k = e.Data.Value<KeyPress>("KeyPress");
			if(k.IsSpecialKey){

			}else{
				
			}
			return this;
		}
		#endregion

		#region Reacting to KeyPresses
		private MauronCode_eventClock CLOCK_keyPressCounter;
		public MauronCode_eventClock KeyPressCounter {
			get {
				if( CLOCK_keyPressCounter==null ) {
					SetKeyPressCounter(new MauronCode_eventClock(SystemTime.Instance));
				}
				return CLOCK_keyPressCounter;
			}
		}
		public MauronConsole SetKeyPressCounter (MauronCode_eventClock clock) {
			CLOCK_keyPressCounter=clock;
			return this;
		}

		//Wait for a key up event
		public MauronConsole WaitForKeyUp ( ) {
			ConsoleKeyInfo key=System.Console.ReadKey(false);

			KeyPress input=new KeyPress();

			//was the ctrl key pressed
			if( (key.Modifiers&ConsoleModifiers.Shift)!=0 ) {
				input.SetIsShiftKeyDown(true);
			}
			//was the alt key pressed
			if( (key.Modifiers&ConsoleModifiers.Control)!=0 ) {
				input.SetIsCtrlKeyDown(true);
			}
			//was the ctrl 
			if( (key.Modifiers&ConsoleModifiers.Alt)!=0 ) {
				input.SetIsAltKeyDown(true);
			}

			//Set the character 
			input.SetKey(key.KeyChar);

			//throw a new Keyboardevent
			SendEvent(KeyPressCounter, "keyUp", new MauronCode_dataSet("Event Data").SetValue<KeyPress>("KeyPress", input));
			WaitForKeyUp();
			return this;
		}
		#endregion

		#region Special Keys
		
		private KeyboardMap KEYS_map=new KeyboardMap_mauronConsole();
		public KeyboardMap KeyMap { get { return KEYS_map; } }
		public MauronConsole SetKeyboardMap(KeyboardMap map) {
			KEYS_map=map;
			return this;
		}

		#endregion

		#region Exiting the program

		//Tell the program when to exit
		private bool B_canExit=true;
		public bool CanExit { get { return B_canExit; } }
		public MauronConsole SetCanExit (bool status) {
			B_canExit=status;
			return this;
		}
		public MauronConsole Exit ( ) {
			Clear();
			SetCanExit(true);
			return this;
		}

		#endregion
	
	}

	//Project Description
	public sealed class ProjectType_mauronConsole : ProjectType {
		#region singleton
		private static volatile ProjectType_mauronConsole instance=new ProjectType_mauronConsole();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ProjectType_mauronConsole ( ) { }
		public static ProjectType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ProjectType_mauronConsole();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "mauronConsole"; } }
	}
}