using MauronAlpha;

using System;

namespace MauronAlpha.GameEngine {

	//Class describing a type of game
    public abstract class GameType:MauronCode_subtype {
		public abstract string Name { get; }
	}
	
	//A Generic Game
	public class GameType_generic : MauronAlpha.GameEngine.GameType {
		#region Singleton
		private static volatile GameType_generic instance=new GameType_generic();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static GameType_generic ( ) { }
		public static GameType Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new GameType_generic();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name {
			get { return "generic"; }
		}
	}

}
