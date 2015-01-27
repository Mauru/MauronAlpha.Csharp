
namespace MauronAlpha.Layout.Layout2d.Interfaces {

	public interface I_layoutModel {

		I_layoutUnit Member (string key);

		I_layoutModel RenderWith (I_layoutRenderer renderer);

	}

}
