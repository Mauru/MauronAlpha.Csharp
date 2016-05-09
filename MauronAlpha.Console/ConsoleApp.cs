﻿using MauronAlpha.Projects;
using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Forms.Units;
using MauronAlpha.Forms.Interfaces;
using MauronAlpha.Events.Units;
using MauronAlpha.Input.Keyboard.Events;
using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.Events.Collections;

using MauronAlpha.Console.Utility;

namespace MauronAlpha.Console {

	//Sample for a keyboard controlled console programm
	public class ConsoleApp:ConsoleComponent, I_sender<Event_keyUp>, I_subscriber<Event_keyUp> {
		Subscriptions<Event_keyUp> Subscriptions;
		ConsoleInput Input = new ConsoleInput();

		public ConsoleApp() : base() { }

		public bool CanExit = false;

		public void DoNothing() { }

		public void Start() {
			System.Console.TreatControlCAsInput = true;
			Input.Subscribe(this);
			Input.Listen();
		}

		public void Subscribe(I_subscriber<Event_keyUp> s) {
			if (Subscriptions == null)
				Subscriptions = new Subscriptions<Event_keyUp>();
			Subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<Event_keyUp> s) {
			if (Subscriptions == null)
				return;
			Subscriptions.Remove(s);
		}
		public bool ReceiveEvent(Event_keyUp e) {
			if (Subscriptions != null)
				Subscriptions.ReceiveEvent(e);
			System.Console.WriteLine("Received input event:" + e.KeyPress.AsString);
			Input.Listen();
			return true;
		}
		public bool Equals(I_subscriber<Event_keyUp> other) {
			return Id.Equals(other.Id);
		}

	
	}

}
