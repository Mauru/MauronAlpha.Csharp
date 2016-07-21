namespace MauronAlpha.MonoGame {
	using MauronAlpha.Events.Units;

	/// <summary> Event fired when a component is "ready" </summary>///
	public class ReadyEvent :EventUnit_event {

		GameLogic DATA_Logic;
		GameEngine DATA_Engine;
		GameManager DATA_Manager;
		GameContentManager DATA_Content;
		GameRenderer DATA_Renderer;

		public ReadyEvent() : base("Ready") { }
		public ReadyEvent(GameLogic o) : this() {
			DATA_Logic = o;
		}
		public ReadyEvent(GameEngine o): this() {
			DATA_Engine = o;
		}
		public ReadyEvent(GameManager o): this() {
			DATA_Manager = o;
		}
		public ReadyEvent(GameContentManager o): this() {
			DATA_Content = o;
		}
		public ReadyEvent(GameRenderer o): this() {
			DATA_Renderer = o;
		}

	}

}
