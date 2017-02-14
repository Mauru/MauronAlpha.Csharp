namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Transformation;
	using MauronAlpha.Geometry.Geometry2d.Collections;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Collections;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	/// <summary> A process detailing prerender instructions </summary>
	public partial class PreRenderProcess: MonoGameComponent, I_sender<PreRenderProcessComplete> {
	
		//constructors: Shared, private
		private PreRenderProcess(string name, RenderType type) : base() {
			_name = name;
			_renderType = type;
		}

		//constructors: Sprite
		public PreRenderProcess(string name, MonoGameTexture texture, Polygon2dBounds bounds): this(name, RenderTypes.Sprite) {
			_sprites = new SpriteBuffer(texture,bounds);
		}
		public PreRenderProcess(string name, SpriteBuffer buffer): this(name, RenderTypes.Sprite) {
			_sprites = buffer;
		}

		//constructors: Shape
		public PreRenderProcess(string name, ShapeBuffer buffer): this(name, RenderTypes.Shape) {
			_shapes = buffer;
		}
		public PreRenderProcess(I_subscriber<PreRenderProcessComplete> subscriber, string name, ShapeBuffer buffer): this(name, buffer) {
			_subscriptions.Add(subscriber);
		}

		//constructors: Line
		public PreRenderProcess(string name, LineBuffer buffer): this(name, RenderTypes.Line) {
			_lines = buffer;
		}

		public Polygon2dBounds Bounds {
			get {
				if (_renderType.Equals(RenderTypes.Shape))
					return _shapes.Bounds;
				else if (_renderType.Equals(RenderTypes.Sprite))
					return _sprites.Bounds;
				else if (_renderType.Equals(RenderTypes.Line))
					return _lines.Bounds;
				return Polygon2dBounds.Empty;
			}
		}

		//Shapes
		ShapeBuffer _shapes;
		public ShapeBuffer Shapes { get { return _shapes; } }
		public bool TryShapes(ref ShapeBuffer result) {
			if (_shapes == null)
				return false;
			result = _shapes;
			return true;
		}

		//Lines
		LineBuffer _lines;
		public LineBuffer Lines {
			get { return _lines; }
		}
		public bool TryLines(ref LineBuffer result) {
			if (_lines == null)
				return false;
			result = _lines;
			return true;
		}

		//Sprites
		SpriteBuffer _sprites;
		public SpriteBuffer Sprites { get { return _sprites; } }
		public bool TrySprites(ref SpriteBuffer result) {
			if (_sprites == null)
				return false;
			result = _sprites;
			return true;
		}

		//Name of the PreRendered Texture
		string _name;
		public string Name { get { return _name; } }

		//Defines the renderType
		RenderType _renderType;
		public RenderType RenderType {
			get {
				if (_renderType == null)
					return RenderTypes.Undefined;
				return _renderType;
			}
		}

		public string AsString {
			get {
				return RenderType.Name + "." + Bounds.AsString;
			}
		}

		//Events
		bool _usesEvents = true;
		Subscriptions<PreRenderProcessComplete> _subscriptions;
		public void Subscribe(I_subscriber<PreRenderProcessComplete> s) {
			if (_subscriptions == null)
				_subscriptions = new Subscriptions<PreRenderProcessComplete>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<PreRenderProcessComplete> s) {
			if (_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}

		//Handling Results
		I_RenderResult _result;
		public I_RenderResult RenderResult { get { return _result; } }
		public bool TryResult(ref I_RenderResult result) {
			if (_result == null)
				return false;
			result = _result;
			return true;
		}
		public void SetRenderResult(I_RenderResult result) {
			_result = result;
			if (_subscriptions != null && _usesEvents)
				_subscriptions.ReceiveEvent(new PreRenderProcessComplete(this));
		}
		public void SetRenderResult(Texture2D texture, long time) {
			RenderResult result = new RenderResult(time, texture);
			SetRenderResult(result);
		}

	}

	/// <summary> Event sent when a PreRenderProcess completes </summary>
	public class PreRenderProcessComplete:EventUnit_event {
		PreRenderProcess _target;
		public PreRenderProcess Target { get { return _target; } }
		public PreRenderProcessComplete(PreRenderProcess target):base("Complete") {
			_target = target;
		}

	}

}
