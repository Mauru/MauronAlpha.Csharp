using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;
using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha.Input.Keyboard.Units;

using MauronAlpha.ConsoleApp.Interfaces;

namespace MauronAlpha.ConsoleApp.Collections {

	//Class that holds all special keySequences to handle input
	public class ConsoleApp_keyCommands:ConsoleApp_component {

		//Booleans
		public delegate bool DELEGATE_commandMethod();

		public bool IsCommand(KeyPress key) {
			KeyPressSequence test = new KeyPressSequence(key);
			// && DATA_commands.ContainsKey(DATA_map.Value(command))
			foreach (KeyPressSequence command in DATA_map.Keys) { 
				if(command.Equals(test))
					return true;
			}
			return false;
		}
		public bool ExecuteCommand(KeyPress key) { 
			foreach(KeyPressSequence command in DATA_map.Keys) {
				string identifier = DATA_map.Value(command);
				if(command.Equals(key)&&DATA_commands.ContainsKey(identifier)) {
					DELEGATE_commandMethod method = DATA_commands.Value(identifier);
					return method();
				}
			}
			return false;
		}

		//DATA objects
		private MauronCode_dataTree<KeyPressSequence,string> DATA_map = new MauronCode_dataTree<KeyPressSequence,string>();
		private MauronCode_dataTree<string, DELEGATE_commandMethod> DATA_commands = new MauronCode_dataTree<string, DELEGATE_commandMethod>();

		public int Count { get { return (int) DATA_map.Count;  } }

		//constructor
		public ConsoleApp_keyCommands() : base() {
			AssignKeys();
		}

		public MauronCode_dataTree<KeyPressSequence, string> Commands { get { return DATA_map; } }

		//Methods
		public ConsoleApp_keyCommands SetCommand(string command, DELEGATE_commandMethod method){
			DATA_commands.SetValue(command, method, true);
			return this;
		}
		private ConsoleApp_keyCommands AssignKeys() {
			//quit
			KeyPress key = new KeyPress('q').SetIsCtrlKeyDown(true);
			DATA_map.SetValue(new KeyPressSequence(key), "quit", true);
			//copy
			key = new KeyPress('c').SetIsCtrlKeyDown(true);
			DATA_map.SetValue(new KeyPressSequence(key), "copy", true);
			//paste
			key = new KeyPress('v').SetIsCtrlKeyDown(true);
			DATA_map.SetValue(new KeyPressSequence(key), "paste", true);
			//cut
			key = new KeyPress('x').SetIsCtrlKeyDown(true);
			DATA_map.SetValue(new KeyPressSequence(key), "cut", true);
			//select all
			key = new KeyPress('a').SetIsCtrlKeyDown(true);
			DATA_map.SetValue(new KeyPressSequence(key), "select_all", true);
			//Arrow-keys
			key = new KeyPress("LeftArrow");
			DATA_map.SetValue(new KeyPressSequence(key), "moveLeft", true);
			key = new KeyPress("RightArrow");
			DATA_map.SetValue(new KeyPressSequence(key), "moveRight", true);
			key = new KeyPress("DownArrow");
			DATA_map.SetValue(new KeyPressSequence(key), "moveDown", true);
			key = new KeyPress("UpArrow");
			DATA_map.SetValue(new KeyPressSequence(key), "moveUp", true);
			key = new KeyPress("Backspace");
			DATA_map.SetValue(new KeyPressSequence(key), "Backspace", true);
			key = new KeyPress("Delete");
			DATA_map.SetValue(new KeyPressSequence(key), "Delete", true);
			key = new KeyPress("Enter");
			DATA_map.SetValue(new KeyPressSequence(key), "Enter", true);

			return this;
		}

		//Command delegates
		public DELEGATE_commandMethod CommandByKey(KeyPress key) {
			foreach (KeyPressSequence command in DATA_map.Keys) {
				string identifier = DATA_map.Value(command);
				if (command.Equals(key) && DATA_commands.ContainsKey(identifier)) {
					DELEGATE_commandMethod method = DATA_commands.Value(identifier);
					return method;
				}
			}
			throw Error("Invalid command!,(CommandByKey)", this, ErrorType_scope.Instance);
		}

	}
}
