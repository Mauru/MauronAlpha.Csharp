using MauronAlpha.ProjectTypes;
using MauronAlpha.GameEngine;

using System;

namespace MauronAlpha.GameEngine.MonoGame {

	//Wrapper for the programm
	public class MonoGameOS : MauronAlpha.OSWrapper {
		public GameInstance Self;
		public MonoGameSound Sound;
		public MonoGameGraphics Graphics;

		public override void Start (ProgramInstance program) {
			this.Self=program as GameInstance;
		}
	
	}

	//A Component of the MonoGameWrapper
	public abstract class MonoGameComponent : MauronCode_subtype {}
	public abstract class MonoGameComponentWrapper : MonoGameComponent { }

	//Whatever controls sound in Monogame
	public sealed class MonoGameSound : MonoGameComponentWrapper {
		#region Singleton
		private static volatile MonoGameSound instance=new MonoGameSound();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static MonoGameSound ( ) { }

		public static MonoGameComponent Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance = new MonoGameSound();
					}
				}
				return instance;
			}
		}
		#endregion
	}

	//whatever controls the graphics in monogame
	public sealed class MonoGameGraphics : MonoGameComponentWrapper {
		#region Singleton
		private static volatile MonoGameGraphics instance=new MonoGameGraphics();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static MonoGameGraphics ( ) { }

		public static MonoGameComponent Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new MonoGameGraphics();
					}
				}
				return instance;
			}
		}
		#endregion
	}

}