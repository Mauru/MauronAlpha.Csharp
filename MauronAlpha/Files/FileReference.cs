using System;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Files {

    public class FileReference:MauronCode_dataObject,I_dataObject {
		#region I_dataObject
		internal override string[] STR_propertyKeys { get { return new String[] {"Location","FileType","Content"}; } }
		#endregion
		
        public String Location { 
			get { throw new NotImplementedException(); } 
			set { throw new NotImplementedException(); } 
		}

        public FileType FileType;

        public double FileSize;
        public bool Loaded = false;

		//The Content of the File
        private string STR_content;
        public String Content { 
			get {
				if (!Loaded) {
					throw new MauronCode_error("FileReference has not loaded yet",this);
				}
				return STR_content;
			}
		}

        public void Load() { throw new NotImplementedException(); }

        public void UnLoad() {
            this.STR_content = null;
            this.Loaded = false;
        }





		public override string[] ProperyKeys {
			get { throw new NotImplementedException(); }
		}

		string[] I_dataObject.PropertyKeys {
			get { throw new NotImplementedException(); }
		}
	}

}