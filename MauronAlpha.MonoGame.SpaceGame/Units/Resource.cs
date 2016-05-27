using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

namespace MauronAlpha.MonoGame.SpaceGame.Units {

	public class Resource<T> : GameComponent, I_Resource where T : ResourceType {
	}

	public interface I_Resource { }

	
}
