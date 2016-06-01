using MauronAlpha.MonoGame.SpaceGame.Quantifiers;

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	
	public class Modifiers<T>:GameList<T> where T:ValueType {}

	public class Modifier<T> : GameComponent,I_Modifier where T : ValueType { }

	public interface I_Modifier { }


}
