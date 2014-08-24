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

		#region Add to the TextBuffer
		public MauronConsole Write (string s) {
			TextBuffer.AddString(s);
			return this;
		}
		#endregion

		#region Output to window

		//Write a whole Line
		private MauronConsole OutputLine(TextComponent_line line) {
			Console.WriteLine(MakeLineStart(line)+line.AsString);
			return this;
		}

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
		#region Getting a specific Line in the TextBuffer //TODO: Add Error Handling by exception
		public TextComponent_line ActiveLine {
			get {
				return LineByIndex(LineIndex);
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
			if(n<0){
				return LineException("Invalid LineIndex {"+n+"}",this,FirstLine);
			}else if(n>=TextBuffer.LineCount){
				return LineException("Invalid LineIndex {"+n+"}",this,LastLine);
			}
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
		#region Getting a specific Character in the TextBuffer //TODO: Add Error Handling by exception
		public TextComponent_character ActiveCharacter {
			get {
				return TextBuffer.CharacterByContext(new TextContext(LineIndex,WordIndex,CharacterIndex));
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
			if(n<0){
				return CharacterException("Invalid CharacterIndex {"+n+"}",this,FirstCharacter);
			}
			if(TextBuffer.CharacterCount<n){
				return CharacterException("Invalid CharacterIndex {"+n+"}",this,LastCharacter);
			}
			return TextBuffer.CharacterByIndex(n);
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