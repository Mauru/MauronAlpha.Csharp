using MauronAlpha.MonoGame.Actors;

namespace MauronAlpha.MonoGame.DataObjects {

	public class RenderStatus:MonoGameComponent {
		GameActor Actor;
		public long LastSheduled;

		public RenderStatus(GameActor actor) : base() {
			Actor = actor;
		}

	}

}
