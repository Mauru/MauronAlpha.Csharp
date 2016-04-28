using MauronAlpha.MonoGame.Utility;
using MauronAlpha.MonoGame.Resources;
using MauronAlpha.MonoGame.Scripts;

namespace MauronAlpha.MonoGame.Utility {

	//A Data class that keeps track of all components
	public class GameManager:MonoGameComponent {
		public ResourceManager Resources;
		public RenderManager Renderer;
		public GameObjectManager Objects;
		public GameEngine Engine;
		public GameLogic Logic;
		public MonoGameWrapper Process;
	}
}
