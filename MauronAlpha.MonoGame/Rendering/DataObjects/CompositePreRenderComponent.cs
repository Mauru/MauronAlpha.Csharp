namespace MauronAlpha.MonoGame.Rendering.DataObjects {

	using Microsoft.Xna.Framework;

	/// <summary> Defines how individual components of a RenderComposite are combined. </summary>
	public class CompositePreRenderComponent : MonoGameComponent {

		string _assettName;
		Color _tint;
		BlendMode _blendMode;

		TransformationData2D _transform;

		public CompositePreRenderComponent(string assettName, Color tint, BlendMode blendMode)
			: base() {
			_assettName = assettName;
			_tint = tint;
			_blendMode = blendMode;
		}
		public CompositePreRenderComponent(string assettName, Color tint, BlendMode blendMode, TransformationData2D transform)
			: this(assettName, tint, blendMode) {
			_transform = transform;
		}
	}

}