namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Rendering.Collections;

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
	public class PreRenderProcess: MonoGameComponent, I_sender<PreRenderProcessComplete> {

		GameManager _game;
		public PreRenderProcess(GameManager game, string name, I_polygonShape2d shape) : base() {
			_name = name;
			_game = game;
			_shape = shape;
			_renderType = RenderTypes.Shape;
		}
		public PreRenderProcess(GameManager game, string name, I_polygonShape2d shape, Vector2d offset): base() {
			_name = name;
			_game = game;
			_offset = offset;
			_shape = shape;
			_renderType = RenderTypes.Shape;
		}

		string _name;
		public string Name { get { return _name; } }

		Vector2d _offset;
		Vector2d _position;
		Matrix2d _matrix;

		Color _color;
		public Color Color {
			get {
				if (_color == null)
					_color = Color.White;
				return _color;
			}
		}


		//Shapes
		I_polygonShape2d _shape;
		public I_polygonShape2d Shape {
			get {
				return _shape;
			}
		}

		ShapeBuffer _shapes;
		public ShapeBuffer Shapes { get { return _shapes; } }
		public void PrepareShapeBuffer() {
			if (_shape == null)
				return;

			ShapeBuffer result = new ShapeBuffer();
			Vector2dList points = _shape.Points;
			if (_offset != null)
				points = points.CopyWithOffset(_offset);
			result.Add(TriangulationData.CreateFromVector2dList(points, this.Color));

			_shapes = result;
		}

		//Lines
		LineBuffer _lines;
		public LineBuffer Lines {
			get { return _lines; }
		}
		public void PrepareLineBuffer() {
			if (_shape == null)
				return;

			Segment2dList segments = _shape.Segments;
			LineBuffer result = new LineBuffer(segments,_thickness);

		}

		int _thickness = 1;
		public int LineThickness { get { return _thickness; } }


		//Sprites
		SpriteBuffer _sprites;
		public SpriteBuffer Sprites { get { return _sprites; } }

		string _renderType;
		public string RenderType {
			get {
				if(_shape!=null)
					return RenderTypes.Shape;
				return RenderTypes.Error;
			}
		}

		bool _usesEvents = true;

		public Polygon2dBounds Bounds {
			get {
				if (_renderType.Equals(RenderTypes.Shape))
					return _shapes.Bounds;
				else if(_renderType.Equals(RenderTypes.Sprite))
					return _sprites.Bounds;
				else if (_renderType.Equals(RenderTypes.Lines))
					return _lines.Bounds;
				return Polygon2dBounds.Empty;
			}
		}

		I_RenderResult _result;
		public I_RenderResult Result { get { return _result; } }
		public void SetRenderResult(I_RenderResult result) {
			_result = result;
			if (_subscriptions != null && _usesEvents)
				_subscriptions.ReceiveEvent(new PreRenderProcessComplete(this));
		}
		public void SetRenderResult(Texture2D texture, long time) {
			_result = new RenderResult(time, texture);
		}

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
	}

	public class PreRenderProcessComplete:EventUnit_event {

		PreRenderProcess _target;
		public PreRenderProcess Target { get { return _target; } }
		public PreRenderProcessComplete(PreRenderProcess target):base("Complete") {
			_target = target;
		}

	}
}
