using System;

namespace MauronAlpha.FileTypes {

    public sealed class FileType_mau : MauronAlpha.FileType {
		#region singleton
		private static volatile FileType_mau instance=new FileType_mau();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static FileType_mau ( ) { }
		public static FileType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new FileType_mau();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "Mauron Notice File"; } }
		public override string Extension { get { return "mau"; } }
		public override string Description { 
			get { return "A File that stores notes or datasets for the MauronAlpha Framework"; }
		}
	}

}