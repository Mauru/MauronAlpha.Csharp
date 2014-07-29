using MauronAlpha.GameEngine.Rendering;

namespace MauronAlpha.GameEngine.ObjectRelations {
	public interface I_RenderObjectParent {
		I_Drawable FirstChild { get; }
		I_Drawable LastChild { get; }
		I_Drawable[] Children { get; }
		void AddChild(I_Drawable child);
		void RemoveChild(I_Drawable child);
	}
}
