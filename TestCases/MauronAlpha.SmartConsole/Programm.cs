using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MauronAlpha.SmartConsole {
	using MauronAlpha;
	using MauronAlpha.FileSystem;
	using System.Windows.Forms;

	using System.Threading;



    class Programm {
		//Lowlevel keyboard hook constants
		private const int WH_KEYBOARD_LL = 13;
		private const int WM_KEYDOWN = 0x0100;
		private const int WM_SYSKEYDOWN = 0x0104;
		private const int WM_KEYUP = 0x0101;
		private static LowLevelKeyboardProc _proc = HookCallback;
		private static IntPtr _hookID = IntPtr.Zero;

		static void Main(string[] args) {
			ThreadManager manager = ThreadManager.Instance;
			SmartConsoleInstance console = new SmartConsoleInstance(manager, "SampleConsole");

			_hookID = SetHook(_proc);

				Application.Run();
				bool canExit = false;
				while(!canExit)
					console.Run(ref canExit);
			
			UnhookWindowsHookEx(_hookID);
		}

		private static IntPtr SetHook(LowLevelKeyboardProc proc) {
			using (Process curProcess = Process.GetCurrentProcess())
			using (ProcessModule curModule = curProcess.MainModule) {
				return SetWindowsHookEx(
					WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0
				);
			}
		}
		private delegate IntPtr LowLevelKeyboardProc(
			int nCode, IntPtr wParam, IntPtr lParam
		);
		private static IntPtr HookCallback(
			int nCode, IntPtr wParam, IntPtr lParam
		) {
			System.Console.Write("Hook Event");
			if (nCode >= 0) {
				if(wParam == (IntPtr)WM_KEYDOWN) {
					int vkCode = Marshal.ReadInt32(lParam);
					Console.WriteLine("DOWN" +(Keys) vkCode);
				}
				else if (wParam == (IntPtr)WM_SYSKEYDOWN) {
					int vkCode = Marshal.ReadInt32(lParam);
					Console.WriteLine("SYSDOWN" + (Keys)vkCode);
				}
				else if (wParam == (IntPtr)WM_KEYUP) {
					int vkCode = Marshal.ReadInt32(lParam);
					Console.WriteLine("UP" +(Keys) vkCode);
				}
				else {
					int vkCode = Marshal.ReadInt32(lParam);
					Console.WriteLine("UNKNOWN" + (Keys) vkCode);
				}
			}
			return CallNextHookEx(_hookID, nCode, wParam, lParam);
		}
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(
			int idHook,LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId
		);
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(
			IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam
		);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);
    }

	public sealed class ThreadManager :MauronCode_utility {



		Thread Main;
		Thread Calculator;

		private static volatile ThreadManager instance;
		private static object syncRoot = new System.Object();

		private ThreadManager() {}

		public static ThreadManager Instance {
		get {
			if (instance == null) {
				lock (syncRoot) {
					if (instance == null) 
						instance = new ThreadManager();
				}
			}
			return instance;
		}
	}



	}

	public class InputManager :MauronCode_utility { }

	public class WindowsInput :InputManager { }
}

namespace MauronAlpha.SmartConsole {
	using System.Windows.Input;

	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;
	
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;
	
	using MauronAlpha.SmartConsole.Events;

	public class SmartConsoleInstance :MauronCode_utility,I_sender<ReadyEvent>, I_subscriber<ReadyEvent> {
		ThreadManager Threads;

		Characters TXT_name;
		public Characters Name {
			get {
			return TXT_name;
			}
		}

		public SmartConsoleInstance(ThreadManager threads, string name) {

			Threads = threads;
			TXT_name = new Characters(name);
			InputManager Input = new InputManager();

		}

		public void Run(ref bool state) {

			return;
		}

		public bool CanExit {	get { return false; }	}

		Text Buffer = new Text();

		//EventHandler<ReadyEvent>
		Subscriptions<ReadyEvent> Listeners = new Subscriptions<ReadyEvent>();
		public void Subscribe(I_subscriber<ReadyEvent> s) {
			Listeners.Add(s);
		}
		public void UnSubscribe(I_subscriber<ReadyEvent> s) {
			Listeners.Remove(s);
		}
		public bool ReceiveEvent(ReadyEvent e) {
			Listeners.ReceiveEvent(e);
			return true;
		}
		public bool Equals(I_subscriber<ReadyEvent> other) {
			return Id.Equals(other.Id);
		}
	
		public delegate void InputEventHandler(object sender, System.EventArgs args);

	}





}

namespace MauronAlpha.SmartConsole.Events {
	using MauronAlpha.Events.Units;
	public class ReadyEvent :EventUnit_event {

		public ReadyEvent() : base("READY") { }

	}

}