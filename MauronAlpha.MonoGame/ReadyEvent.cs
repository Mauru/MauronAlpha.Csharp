namespace MauronAlpha.MonoGame {
	using MauronAlpha.Events.Units;

	///<summary> Event fired when a component is "ready" </summary>///
	public class ReadyEvent :EventUnit_event {

		GameStateManager DATA_GameState;
		GameLogic DATA_Logic;
		GameEngine DATA_Engine;
		GameManager DATA_Manager;
		AssetManager DATA_Assets;
		GameRenderer DATA_Renderer;

		public ReadyEvent() : base("Ready") { }
		public ReadyEvent(GameLogic o) : this() {
			DATA_Logic = o;
		}
		public ReadyEvent(GameStateManager o) : this() {
			DATA_GameState = o;
		}
		public ReadyEvent(GameEngine o): this() {
			DATA_Engine = o;
		}
		public ReadyEvent(GameManager o): this() {
			DATA_Manager = o;
		}
		public ReadyEvent(AssetManager o): this() {
			DATA_Assets = o;
		}
		public ReadyEvent(GameRenderer o): this() {
			DATA_Renderer = o;
		}

	}

}