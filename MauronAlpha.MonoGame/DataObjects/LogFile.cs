namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.FileSystem.Units;

	using System.Collections.Generic;
	
	public class LogFile:MonoGameComponent {

		File _file;

		GameManager _game;

		bool _enabled = true;

		public LogFile(GameManager game, Directory dir, string name) : base() {
			_game = game;
			_file = new File(dir, name, "log");
		}
		public void LogOnce(object source, string data) {
			if(!_enabled)
				return;
			string message = source.GetType().ToString()+"#"+_game.TimeStamp+":"+data;
			_file.Append(message,true);
			_file.Append("#ENDED LOGGING#", true);
			_enabled = false;
		}
		public void LogOnce(object source, ICollection<string> data) {
			if(!_enabled)
				return;
			string message = source.GetType().ToString()+"#"+_game.TimeStamp+":#ARRAY#";
			_file.Append(message,true);
			foreach(string str in data)
				_file.Append(str, true);
			_file.Append("#ENDED LOGGING#", true);
			_enabled = false;
		}

		public void Log(object source,string data) {
			if(!_enabled)
				return;
			string message = source.GetType().ToString()+"#"+_game.TimeStamp+":"+data;
			_file.Append(message,true);
		}

	}


}
