namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.MonoGame.Interfaces;

	/// <summary>Returns the load an initialize state of the core components</summary>
	public class StatusInfo :MonoGameComponent {

		GameManager DATA_manager;
		public StatusInfo(GameManager manager) {
			DATA_manager = manager;
		}

	}

}