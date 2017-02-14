namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Collections;

	/// <summary> Holds a list of instructions on how a RenderComposite is formed. </summary>
	public class CompositePreRenderProcess : List<CompositePreRenderComponent> {
		string _name;

		public CompositePreRenderProcess(string name)
			: base() {
			_name = name;
		}
	}

}