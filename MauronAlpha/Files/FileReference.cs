using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Files {

    public class FileReference:MauronCode_dataObject,I_dataObject {
		
		public FileReference():base(DataType_plain.Instance) {}

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
					throw Error( "FileReference has not loaded yet!,(Content)", this, ErrorType_outOfSynch.Instance );
				}
				return STR_content;
			}
		}

        public void Load() { throw new NotImplementedException(); }

        public void UnLoad() {
            this.STR_content = null;
            this.Loaded = false;
        }

	}

}