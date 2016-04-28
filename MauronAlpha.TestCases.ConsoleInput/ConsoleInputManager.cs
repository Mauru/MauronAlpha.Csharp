using MauronAlpha.Projects;
using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Forms.Units;
using MauronAlpha.Forms.Interfaces;
using MauronAlpha.Events.Units;
using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.TestCases.Console {

	//Sample for a keyboard controlled console programm
	public class ConsoleInputManager:MauronCode_project, I_eventSubscriber, I_debugInterface {

		ConsoleInput Input = new ConsoleInput();
		ConsoleRenderer Renderer;

		public ConsoleInputManager() : base(ProjectType.Generic, "TextManager") { }

		EventHandler Events = new EventHandler();

		public bool CanExit = false;

		public void DoNothing() { }

		public FormUnit_textField Text = new FormUnit_textField();
		public FormUnit_textField DebugTxt = new FormUnit_textField();
		public FormUnit_textField ExtraDebug = new FormUnit_textField();

		public void Start() {
			System.Console.TreatControlCAsInput = true;
			Input.Events.SubscribeToCode("input", this);
			AssignKeyCommands();
			Text.DebugInterface = this;
			Renderer = new ConsoleRenderer(this);
			Input.Listen();
		}

		public bool Equals(I_eventSubscriber other) {
			return Id.Equals(other.Id);
		}

		public bool ReceiveEvent(Events.Units.EventUnit_event e) {
			if (e.Code == "input")
				return E_input(e);
			return false;
		}
		public bool E_input(EventUnit_event e) {
			KeyPress key = Input.LastKey;
			if (!Commands.Try(key))
				Text.InsertRegularCharacter(Input.KeyToCharacter(key));
			DebugTxt.SetText(Text.CaretPosition.Context.AsString+"||"+Text.CountAsContext.AsString+">"+ExtraDebug.AsString);
			DebugTxt.SetPosition(0, Text.CountLines);
			Renderer.Update();

			Input.Listen();
			return true;
		}

		KeyCommands Commands = new KeyCommands();

		void AssignKeyCommands() {
			KeyPress key = new KeyPress(SpecialKeys.Enter);
			Commands.Register(key, e_enter);

			key = new KeyPress(SpecialKeys.Backspace);
			Commands.Register(key, e_deleteBackward);

			key = new KeyPress(SpecialKeys.Space);
			Commands.Register(key, e_space);

			key = new KeyPress(SpecialKeys.Tab);
			Commands.Register(key, e_tab);

			key = new KeyPress(SpecialKeys.Delete);
			Commands.Register(key, e_deleteForward);
		}

		void e_enter() {
			Text.InsertLineBreak();
		}
		void e_deleteForward() {
			Text.RemoveNextCharacter();
		}
		void e_deleteBackward() {
			Text.RemovePreviousCharacter();
		}
		void e_space() {
			Text.InsertWhiteSpace();
		}
		void e_tab() {
			Text.InsertTab();
		}

		//Debug
		public void SubmitDebugMessage(string message) {
			ExtraDebug.AddAsLines(message);
		}

	}

}
