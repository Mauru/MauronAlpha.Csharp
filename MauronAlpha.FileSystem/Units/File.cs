﻿using MauronAlpha.FileSystem.Interfaces;
using MauronAlpha.FileSystem.DataObjects;
using MauronAlpha.FileSystem.Collections;

namespace MauronAlpha.FileSystem.Units {

	public class File:FileSystem_component, I_file {

		public File() : base() { }
		public File(Directory directory, string name, string extension) : this() {
			DATA_directory = directory;
			STR_name = name;
			STR_extension = extension;
		}

		public File(Directory d, string name): this() {
			if(name == null)
				throw new FileSystemError("Invalid filename {" + name + "}",this);
			DATA_directory = d;
			int index = name.LastIndexOf('.');
			STR_name = name.Substring(0, index);
			STR_extension = name.Substring(index + 1);			
		}

		public string FileName {
			get {
				return Name + "." + Extension;
			}
		}

		public string Path {
			get {
				if (Directory == null)
					return "";
				return Directory.Path + System.IO.Path.AltDirectorySeparatorChar + Name + '.' + Extension;
			}
		}

		private string STR_name;
		public string Name {
			get {
				return STR_name;
			}
		}

		private string STR_extension;
		public string Extension {
			get {
				return STR_extension;
			}
		}

		private Directory DATA_directory;
		public Directory Directory {
			get {
				return DATA_directory;
			}
		}
		
		/// <summary> Sets the directory of the file, DOES NOT COPY OR MOVE THE FILE </summary>
		public void SetDirectory(Directory d) {
			DATA_directory = d;
		}

		private FileType DATA_fileType = FileTypes.Generic;
		public FileType FileType {
			get { return DATA_fileType; }
		}

		public System.IO.Stream Stream {
			get {
				if(DATA_directory == null)
					throw new FileSystemError("Undefined Directory.", this);
				return System.IO.File.OpenRead(Path);
			}
		}
		public System.IO.StreamReader StreamReader {
			get {
				if(DATA_directory == null)
					throw new FileSystemError("Undefined Directory.", this);
				return new System.IO.StreamReader(Path);
			}
		}

		public Collections.AccessRights Rights {
			get { return new AccessRights(this); }
		}

		public bool IsReadOnly {
			get { return Rights.CanRead; }
		}

		public bool Delete() {
			if (!System.IO.File.Exists(Path))
				return true;
			System.IO.File.Delete(Path);
			return true;
		}

		public bool Append(string text, bool onNewLine) {
			try {
				using (System.IO.StreamWriter fs = System.IO.File.AppendText(Path)) {
					if(onNewLine)
						fs.WriteLine(text);
					else
						fs.Write(text);
				}
			}catch(System.Exception ex) {
				return false;
			}
			return true;
		}
		
		public bool Clear() {
			try {
				System.IO.FileStream fs = System.IO.File.Open(Path, System.IO.FileMode.Open);
				fs.SetLength(0);
				fs.Close();
			}
			catch (System.Exception ex) {
				return false;
			}
			return true;
		}

		//WARNING: Not optimized!
		public System.Collections.Generic.List<string> Contents {
			get {
				System.Collections.Generic.List<string> data = new System.Collections.Generic.List<string>();
				try {
					using (System.IO.StreamReader sr = System.IO.File.OpenText(Path)) {
						string s = "";
						while ((s = sr.ReadLine()) != null) {
							data.Add(s);
						}
					}
				}
				catch (System.Exception e) {
					return data;
				}
				return data;
			}
		}

		public System.Byte[] ContentAsByteArray {
			get {
				try { 
					return System.IO.File.ReadAllBytes(Path);
				}
				catch (System.Exception ex) {
					throw new FileSystemError("Could not load file.", this);
				}
			}
		}

		public bool Exists {
			get {
				return System.IO.File.Exists(Path);
			}
		}
	}
	public class FileType_generic : FileType {

		public override string Name {
			get { return "Generic"; }
		}

		public override bool IsDirectory {
			get { return false; }
		}

		public static FileType_generic Instance { get { return new FileType_generic(); } }
	
	}
}
