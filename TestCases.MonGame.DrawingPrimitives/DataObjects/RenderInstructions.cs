using MauronAlpha.MonoGame.Actors;

namespace MauronAlpha.MonoGame.DataObjects {

	public class RenderInstructions:MonoGameComponent {
		public bool IsSpriteFont = false;
		public bool IsMesh = false;
		public bool HasChildren = false;

		public static RenderInstructions_gameStage GameStage {
			get {
				return RenderInstructions_gameStage.Instance;
			}
		}
	}

}
