using System;

namespace MauronAlpha {

	//Base class for all code
    public class MauronAlphaCode:object {
		//Debug
		public static void Debug(string msg, object obj){
			if(obj==null) {
				Console.WriteLine("# [NULL OBJECT] > "+msg);
				return;
			}
			Console.WriteLine("# "+obj.GetType().Name+" > "+msg);
		}

		//string : { Type+Hash }
		public string Id {
			get {
				return GetType().ToString() +"."+GetHashCode();
			}
		}

    }

}
