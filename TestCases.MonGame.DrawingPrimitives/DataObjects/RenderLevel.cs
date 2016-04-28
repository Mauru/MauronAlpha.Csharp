using MauronAlpha.MonoGame.DataObjects;
using MauronAlpha.MonoGame.Actors;
using MauronAlpha.MonoGame.Utility;

using MauronAlpha.HandlingData;

namespace MauronAlpha.MonoGame.DataObjects {
	
	public class RenderLevel:MonoGameComponent {

		private int INT_index = -1;
		GameManager Manager;
		RenderManager Renderer { get { return Manager.Renderer; } }

		public RenderLevel(int index, GameManager manager):base() {
			INT_index = index;
			Manager = manager;
		}

		public bool IsEmpty {
			get { return (INT_index == -1 || UNIT_actor == null); }
		}

		GameActor UNIT_actor;
		public GameActor Actor { get { return UNIT_actor; } }

		public RenderLevel SetActor(GameActor actor) {
			UNIT_actor = actor;
			RequestRenderUpdate(this);
			return this;
		}

		public virtual void RequestRenderUpdate(RenderLevel target) {
			Renderer.AddRequest(target);
		}
	
	}

}
