using System;

using MauronAlpha.Files.FileTypes;
using MauronAlpha.Files.DataObjects;
using MauronAlpha.Files.Interfaces;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Files {

    public class FileReference:FileComponent,I_dataObject {
		
		public FileReference(I_fileUnit unit):base() {
			DATA_location = unit.Location.Instance;
		}

		private FileLocation DATA_location;
        public FileLocation Location { 
			get {
				if (DATA_location == null)
					throw NullError("Location can not be null!", this, typeof(FileLocation));
				return DATA_location;
			} 
		}

		private FileType TYPE_file;
		public FileType FileType {
			get {
				if (TYPE_file == null)
					throw NullError("FileType can not be null!,(FileType)", this, typeof(FileType));
				return TYPE_file;
			}
		}

		private double SIZE_file;
		public double FileSize { get {
			return SIZE_file;
		} }

		private bool B_loaded = false;
		public bool Loaded {
			get {
				return B_loaded;
			}
		}

		//The Content of the File
        private string STR_content;
        public String Content { 
			get {
				if (!Loaded)
					throw Error( "FileReference has not loaded yet!,(Content)", this, ErrorType_outOfSynch.Instance );
				return STR_content;
			}
		}

        public void Load() { throw new NotImplementedException(); }

        public void UnLoad() {
            STR_content = null;
            B_loaded = false;
        }

	}

}