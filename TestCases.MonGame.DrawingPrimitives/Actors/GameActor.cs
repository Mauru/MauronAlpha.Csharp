using MauronAlpha.MonoGame.DataObjects;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Events;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.MonoGame.Resources;
using MauronAlpha.MonoGame.Utility;

namespace MauronAlpha.MonoGame.Actors {

	public abstract class GameActor:GameObject,I_eventSubscriber,I_eventSender {
		public abstract RenderInstructions RenderInstructions { get; }
		public RenderStatus RenderStatus;

		public abstract Polygon2dBounds Bounds { get; }

		internal RenderLevel DATA_level;
		public RenderLevel Index { get { return DATA_level; } }

		public Vector2d Position { get; }
		internal Vector2d DATA_position = new Vector2d(0);

		internal EventHandler EventHandler;

		internal GameManager Manager;
		internal RenderManager Renderer { get { return Manager.Renderer; } }
		internal ResourceManager Resources { get { return Manager.Resources; } }

		//constructor
		public GameActor(RenderLevel index, GameManager manager) : base(true) {
			DATA_level = index;
			Index.SetActor(this);
			EventHandler = new EventHandler();
			Manager = manager;
			RenderStatus = new RenderStatus(this);
		}
		
		public bool Equals(I_eventSubscriber other) {
			return Id.Equals(other.Id);
		}

		public bool ReceiveEvent(Events.Units.EventUnit_event e) {
			return false;
		}
		public EventHandler.DELEGATE_trigger TriggerOfCode(string Code) {
			return EventHandler.NothingHappens;
		}
		public I_eventSender SendEvent(Events.Units.EventUnit_event e) {
			EventHandler.SubmitEvent(e, this);
			return this;
		}
		public EventUnit_event GenerateEvent(string code) {
			return EventHandler.GenerateEvent(code, this);
		}
		public EventUnit_event GenerateEvent(string code, I_eventSender sender) {
			return EventHandler.GenerateEvent(code, sender);
		}
	
		public void RequestRender(GameActor actor) {

			long step = Renderer.CurrentStep;
			if (RenderStatus.LastSheduled >= step)
				return;
			RenderStatus.LastSheduled = step;
			Renderer.AddRequest(Index, RenderInstructions);
		}
	}

}
