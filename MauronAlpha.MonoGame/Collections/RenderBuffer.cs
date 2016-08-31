namespace MauronAlpha.MonoGame.Collections {
	using MauronAlpha.MonoGame.DataObjects;

	public class RenderBuffer :MonoGameComponent  {
		Stack<MonoGameTexture> Textures;
		Stack<MonoGameShape> Shapes;
		Stack<MonoGameLines> Lines;
		Stack<MonoGameText> Text;
	}
}