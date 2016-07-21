using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events;
namespace MauronAlpha.MonoGame.Resources {

	//Content Resources are files which hold significant amounts of data such as databases, textures and files
	public abstract class GameResource:MonoGameComponent,I_eventSender {
		//properties
		protected ResourceType TYPE_resource;
		public ResourceType ResourceType { get { return TYPE_resource;} }


		//prop: bool
		private bool B_loaded = false;
		public bool IsLoaded { get { return B_loaded; } }

		EventHandler EventHandler = new EventHandler();

		//constructor
		public GameResource(ResourceType type) : base() {
			TYPE_resource = type;
		}

		public void SetLoaded(bool status) {
			B_loaded = status;
			if (!B_loaded)
				return;
			
			SendEvent(EventHandler.GenerateEvent("loaded", this));
		}


		public I_eventSender SendEvent(Events.Units.EventUnit_event e) {
			EventHandler.SubmitEvent(e, this);
			return this;
		}
	}

}
