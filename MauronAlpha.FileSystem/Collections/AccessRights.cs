using MauronAlpha.FileSystem.Interfaces;

namespace MauronAlpha.FileSystem.Collections {
	
	public class AccessRights:FileSystem_component {

		public AccessRights() : base() { }
		public AccessRights(I_file source) : this() {
			DATA_source = source;
		}

		private I_file DATA_source;

		private bool B_canRead = false;
		public bool CanRead { get {
			return B_canRead;
		} }

		private bool B_canWrite = false;
		public bool CanWrite {
			get {
				return B_canWrite;
			}
		}

		private bool B_canDelete = false;
		public bool CanDelete {
			get {
				return B_canDelete;
			}
		}

	}

}
