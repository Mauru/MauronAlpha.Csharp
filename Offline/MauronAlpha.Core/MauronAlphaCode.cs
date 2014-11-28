using System;

namespace MauronAlpha.Core {

	//Base class for all code
    public abstract class MauronAlphaCode:object, I_MauronAlphaCode {

		//Base constructor
		public MauronAlphaCode():base() {
			
			STR_objectId=GetType().ToString()+"."+GetHashCode();
		
		}
		
		//Debug
		public static void Debug(string msg, object obj){
			if(obj==null) {
				Console.WriteLine("# [NULL OBJECT] > "+msg);
				return;
			}
			Console.WriteLine("# "+obj.GetType().Name+" > "+msg);
		}

		//string : { Type+Hash }
		private string STR_objectId;
		public string Id {
			get {

				return STR_objectId;
			
			}
		}

    }

}