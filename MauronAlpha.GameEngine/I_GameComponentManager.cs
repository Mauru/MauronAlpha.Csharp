using MauronAlpha.GameEngine.Events;

namespace MauronAlpha.GameEngine {

	//Interface for Classes that store objects in a central dictionary and are singletons
	interface I_GameComponentManager : I_GameEventSender, I_GameEventListener, I_GameComponent { }

}