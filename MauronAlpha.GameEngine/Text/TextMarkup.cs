using System;

namespace MauronAlpha.GameEngine.Text {

	//enables text to be parsed for specific code
	public abstract class TextMarkup : MauronCode_subtype {
		public abstract string Name { get; }
	}

	//default text markup
	public sealed class TextMarkup_default : TextMarkup {
		#region Singleton
		private static volatile TextMarkup_default instance=new TextMarkup_default();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static TextMarkup_default ( ) { }
		public static TextMarkup Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new TextMarkup_default();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "default"; }	}
	}

}