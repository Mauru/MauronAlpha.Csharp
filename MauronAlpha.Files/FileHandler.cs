using MauronAlpha.ExplainingCode;

using MauronAlpha.Files.Units;

using System;
using MauronAlpha.Files.Interfaces;

namespace MauronAlpha.Files {

	public abstract class FileHandler : FileComponent {
		private string STR_name;

		public string Name { get{ return STR_name; } }
		public virtual void SetName (string name) {
			STR_name=name;
		}

		//constructor
		public FileHandler() : base() { }

		private I_fileUnit FILE_target;
	}
}
