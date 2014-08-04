using MauronAlpha.ExplainingCode;
using System;

namespace MauronAlpha.Files {

	public abstract class MauronCode_filehandler : MauronCode {
		private string STR_name;
		public string Name { get{ return STR_name; } }
		public virtual void SetName (string name) {
			STR_name=name;
		}

		private string STR_location;
		public string Location { get { return STR_location;} }
		public virtual void SetLocation(string location) {
			STR_location=location;
		}

		public MauronCode_filehandler ( string name, string location) : base(CodeType_filehandler.Instance) { 
			SetLocation(location);
			SetName(name);
		}
	}

	//Generic snippets of code
	public sealed class CodeType_filehandler : CodeType {
		#region singleton
		private static volatile CodeType_filehandler instance=new CodeType_filehandler();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_filehandler ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_filehandler();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "filereference"; } }
	}
}
