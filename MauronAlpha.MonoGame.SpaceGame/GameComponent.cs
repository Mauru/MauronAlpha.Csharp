using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingData;

namespace MauronAlpha.MonoGame.SpaceGame
{
    public class GameComponent:MauronCode_component {}

	public class GameList<T> : MauronCode_dataList<T>  where T:GameComponent { }
}
