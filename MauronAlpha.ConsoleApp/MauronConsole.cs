using System;

using MauronAlpha.Projects;
using MauronAlpha.HandlingData;
using MauronAlpha.ExplainingCode;

using MauronAlpha.Text.Units;
using MauronAlpha.Text;
using MauronAlpha.Text.Utility;

using MauronAlpha.Input.Keyboard;
using MauronAlpha.Input.Keyboard.Events;

using MauronAlpha.Events;

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
			SetInput (new ConsoleInput (this));

			//Print the title
			ResetScreen();

			//Start Environment Cycle
			SetCanExit(false);
			SubscribeToEvents();
			Input.Listen();
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
			if( Settings.TitleVisible ) {
				OutputHeader();
			}
			OutputBuffer();
			return this;
		}

		public MauronConsole ClearScreen() {
			System.Console.Clear ();
			return this;
		}

		#endregion

		#region Input
		private ConsoleInput IN_keyboard;
		private ConsoleInput Input {
			get { 
				if (IN_keyboard == null) {
					NullError ("Input can not be null!,(Input)",this, typeof(ConsoleInput));
				}
				return IN_keyboard;
			}
		}
		public MauronConsole SetInput(ConsoleInput input) {
			IN_keyboard = input;
			return this;
		}
		#endregion

		#region Output to window

		//Output the TextBuffer
		public MauronConsole OutputBuffer() {
			for(int i=0;i<TextBuffer.LineCount;i++){
				TextComponent_line line = TextBuffer.LineByIndex(i);
				OutputLine(line,Settings.LineNumbersVisible);
			}
			return this;
		}
		//Output the header (title)
		public MauronConsole OutputHeader() {
			for (int i = 0; i < Header.LineCount; i++) {
				TextComponent_line line = Header.LineByIndex(i);
				OutputLine(line,false);
			}
			return this;
		}

		//Ourput a whole Line
		private MauronConsole OutputLine(TextComponent_line line, bool makeLineStart) {
			string output="";
			if(makeLineStart){
				output+=MakeLineStart(line);
			}
			output+=line.AsString;
			Console.WriteLine(output);
			return this;
		}
		#region Line Start (line number)

		//Generate the line seperator
		private string MakeLineStart (TextComponent_line line) {
			//no line start
			if( !Settings.LineNumbersVisible ) {
				return null;
			}
			int lineindex=line.Context.LineOffset;
			return ""+lineindex+Settings.LineSeperator;
		}

		#endregion
		//Write a Debug message
		public new MauronConsole Debug (string msg, object obj) {
			MauronCode.Debug(msg, obj);
			return this;
		}
		#endregion

		#region The Text Buffer

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
		#region Add to the TextBuffer
		public MauronConsole Write (string s) {
			TextBuffer.AddString(s);
			return this;
		}
		#endregion

		#region The Header
		private TextComponent_text TXT_header = new TextComponent_text ();
		public TextComponent_text Header {
			get { 
				return TXT_header;
			}
		}

		#region Application title
		private static string STR_title="MauronConsole Application";
		public string Title { get { return STR_title; } }
		public MauronConsole SetTitle (string title) {
			STR_title=title;
			Header.Clear ();
			Header.AddString (title);
			return this;
		}
		#endregion

		#endregion
		
		#region Defining what Line we are on
		private int INT_line=0;
		public int LineIndex {
			get {
				return INT_line;
			}
		}
		public MauronConsole SetLineIndex (int n) {
			if( n<0 ) {
				INT_line=0;
			}
			else if( n>=TextBuffer.LineCount ) {
				INT_line=TextBuffer.LineCount-1;
			}
			else {
				INT_line=n;
			}
			return this;
		}
		#endregion
		#region Getting a specific Line in the TextBuffer
		public TextComponent_line ActiveLine {
			get {
				return LineByIndex(LineIndex);
			}
		}
		public TextComponent_line FirstLine {
			get {
				#region ErrorResolution
				if(TextBuffer.LineCount<1){
					TextBuffer.AddLine(new TextComponent_line(TextBuffer,new TextContext(0)));
					return LineException("Lines is empty","Created empty line",FirstLine);
				}
				#endregion
				return TextBuffer.FirstLine;
			}
		}
		public TextComponent_line LastLine {
			get {
				#region ErrorResolution
				if( TextBuffer.LineCount<1 ) {
					return LineException("Lines is empty", "Created empty line (FirstLine)", FirstLine);
				}
				#endregion
				return TextBuffer.LastLine;
			}
		}		
		public TextComponent_line LineByIndex(int n){
			#region ErrorResolution
			if(n<0){
				return LineException("Invalid LineIndex {"+n+"}","Used FirstLine",FirstLine);
			}else if(n>=TextBuffer.LineCount){
				return LineException("Invalid LineIndex {"+n+"}","Used LastLine",LastLine);
			}
			#endregion
			return TextBuffer.LineByContext(new TextContext(LineIndex));
		}		
		#endregion

		#region Defining what word we are on
		private int INT_word=0;
		public int WordIndex {
			get {
				return INT_word;
			}
		}
		public MauronConsole SetWordIndex (int n) {
			if( n<0 ) {
				INT_word=0;
			}
			else if( n>=TextBuffer.WordCount ) {
				INT_word=TextBuffer.WordCount-1;
			}
			else {
				INT_word=n;
			}
			return this;
		}
		#endregion
		#region Getting a specific Word in the TextBuffer //TODO: Add Error Handling By Exception
		//The active word
		public TextComponent_word ActiveWord {
			get {
				return WordByIndex(WordIndex);
			}
		}
		public TextComponent_word FirstWord {
			get {
				#region ErrorResolution
				if(TextBuffer.WordCount<1){
					TextBuffer.FirstLine.AddWord(
						new TextComponent_word(
							TextBuffer.FirstLine,
							new TextContext(0,0)
						)
					);
					return WordException("Words is empty","Create empty word",FirstWord);
				}
				#endregion
				return TextBuffer.FirstWord;
			}
		}
		public TextComponent_word LastWord {
			get {
				#region ErrorResolution
				if( TextBuffer.WordCount<1 ) {
					return WordException("Words is empty", "Create empty word (FirstWord)", FirstWord);
				}
				#endregion
				return TextBuffer.LastWord;
			}
		}
		public TextComponent_word WordByIndex (int n) {
			#region ErrorResolution
			if( n<0 ) {
				return WordException("Invalid WordIndex {"+n+"}", "Used FirstWord", FirstWord);
			}
			if( TextBuffer.WordCount<n ) {
				return WordException("Invalid CharacterIndex {"+n+"}", "Used LastWord", LastWord);
			}
			#endregion
			return TextBuffer.WordByIndex(n);
		}
		#endregion

		#region Defining what character we are on
		private int INT_character=0;
		public int CharacterIndex {
			get {
				return INT_character;
			}
		}
		public MauronConsole SetCharacterIndex (int n) {
			if( n<0 ) {
				INT_character=0;
			}
			else if( n>=TextBuffer.CharacterCount ) {
				INT_character=TextBuffer.CharacterCount-1;
			}
			else {
				INT_character=n;
			}
			return this;
		}
		#endregion
		#region Getting a specific Character in the TextBuffer
		public TextComponent_character ActiveCharacter {
			get {
				return TextBuffer.CharacterByContext(new TextContext(LineIndex,WordIndex,CharacterIndex));
			}
		}
		public TextComponent_character FirstCharacter {
			get {
				#region ErrorResolution
				if(TextBuffer.CharacterCount<1){
					TextBuffer.FirstWord.AddCharacter(
						new TextComponent_character(
							FirstWord,
							new TextContext(0,0,0),
							TextHelper.Empty)
						);
					return CharacterException("Characters are empty!","Return empty character",FirstCharacter);
				}
				#endregion
				return TextBuffer.FirstCharacter;
			}
		}
		public TextComponent_character LastCharacter {
			get {
				#region ErrorResolution
				if(TextBuffer.CharacterCount<1){
					return CharacterException("Characters are empty!", "Create empty character (FirstCharacter)", FirstCharacter);
				}
				#endregion
				return TextBuffer.LastCharacter;
			}
		}
		public TextComponent_character CharacterByIndex (int n) {
			#region ErrorResolution
			if(n<0){
				return CharacterException("Invalid CharacterIndex {"+n+"}","Used FirstCharacter",FirstCharacter);
			}
			if(TextBuffer.CharacterCount<n){
				return CharacterException("Invalid CharacterIndex {"+n+"}","Used LastCharacter",LastCharacter);
			}
			#endregion
			return TextBuffer.CharacterByIndex(n);
		}
		#endregion

		#region Receiving and Sending Events
		public MauronConsole SubscribeToEvents ( ) {
			SubscribeToEvent(KeyPressCounter, "KeyUp");
			return this;
		}
		public MauronConsole SubscribeToEvent (MauronCode_eventClock clock, string message) {
			clock.SubscribeToEvent(message,this);
			return this;
		}
		public MauronConsole ReceiveEvent(MauronCode_eventClock clock, MauronCode_event e) {
			//Key Up
			if( e.Message=="KeyUp" ) {
				HandleEvent_keyUp(e as Event_keyUp);
				return this;
			}
			return this;
		}

		public MauronConsole SendEvent (MauronCode_eventClock clock, MauronCode_event e) {
			clock.SubmitEvent(e);
			return this;
		}

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
		#endregion

		#region Received events
		//KeyUp
		private MauronConsole HandleEvent_keyUp (Event_keyUp e) {
			KeyPressCounter.AdvanceTime();
			KeyPress key=e.KeyPress;
			TextBuffer.AddChar(key.Key);
			ResetScreen();
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
			ClearScreen();
			SetCanExit(true);
			return this;
		}

		#endregion
	
		#region Handling CharacterExceptions //TODO: Track Exceptions
		public TextComponent_character CharacterException(string error, string solution, TextComponent_character result){

			return result;
		}
		#endregion
		#region Handling LineExceptions //TODO: Track Exceptions
		public TextComponent_line LineException (string error, string solution, TextComponent_line result) {
			return result;
		}
		#endregion
		#region Handling WordExceptions //TODO: Track Exceptions
		public TextComponent_word WordException (string error, string solution, TextComponent_word result) {
			return result;
		}
		#endregion
	
		#region I_eventSender
		I_eventSender I_eventSender.SendEvent(MauronCode_eventClock clock, MauronCode_event e){
			return SendEvent (clock, e);
		}
		#endregion
		#region I_eventReceiver
		//Subscribe to Events
		I_eventReceiver I_eventReceiver.SubscribeToEvents(){
			return SubscribeToEvents();
		}
		//Subscribe to a single Event
		I_eventReceiver I_eventReceiver.SubscribeToEvent(MauronCode_eventClock clock, string message){
			return SubscribeToEvent (clock, message);
		}
		//Receive an event
		I_eventReceiver I_eventReceiver.ReceiveEvent(MauronCode_eventClock clock, MauronCode_event e){
			return ReceiveEvent (clock, e);
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