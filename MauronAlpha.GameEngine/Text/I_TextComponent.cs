using MauronAlpha.GameEngine.Rendering;

namespace MauronAlpha.GameEngine.Text {

	//base class for text in a TextDisplay
	public interface I_TextComponent:I_Drawable {
		TextDisplay Source {get;set;}
		string AsString { get; }
		GameAsset Asset { get; }
	}

}
