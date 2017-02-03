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
	public partial class PreRenderProcess: MonoGameComponent, I_sender<PreRenderProcessComplete>, I_SpriteDrawInfo {

		//constructors:Shape
		public PreRenderProcess(GameManager game, string name, I_polygonShape2d shape) : base() {
			_name = name;
			_game = game;
			_shape = shape;
			_renderType = RenderTypes.Shape;
		}
		public PreRenderProcess(GameManager game, string name, I_polygonShape2d shape, Color color)	: this(game, name, shape) {
			_color = color;
		}
		public PreRenderProcess(GameManager game, string name, I_polygonShape2d shape, Vector2d offset)	: this(game, name, shape) {
			_shapeTransform = TransformationData2D.AlignedTopLeft(offset);
		}
		public PreRenderProcess(GameManager game, string name, I_polygonShape2d shape, Vector2d offset, Color color): this(game, name, shape, offset) {
			_color = color;
		}
		public PreRenderProcess(GameManager game, string name, I_polygonShape2d shape, I_Shader shader, Color color): this(game, name, shape, color) {
			_shader = shader;
		}
		
		//constructors:Sprite
		public PreRenderProcess(GameManager game, string name, MonoGameTexture texture, Polygon2dBounds bounds) {
			_game = game;
			_name = name;
			_sprites = new SpriteBuffer(texture,bounds);
			_renderType = RenderTypes.Sprite;
		}
		public PreRenderProcess(GameManager game, string name, SpriteBuffer buffer, Color color): base() {
			_game = game;
			_name = name;
			_sprites = buffer;
			_renderType = RenderTypes.Sprite;
			_color = color;
		}

		//constructors:Misc/Lines
		public PreRenderProcess(GameManager game, string name, I_polygonShape2d shape, Color color, string renderType)	: base() {
			_game = game;
			_name = name;
			_color = color;
			_shape = shape;
			_renderType = renderType;
			if (_renderType.Equals(RenderTypes.Lines))
				_generateLines = true;
		}

		//initialization (forms object buffers and triangulation)
		public void Prepare() {
			if (_renderType.Equals(RenderTypes.Shape))
				PrepareShapeBuffer();
			else if (_renderType.Equals(RenderTypes.Sprite))
				PrepareSpriteBuffer();
			else if (_renderType.Equals(RenderTypes.Lines))
				PrepareLineBuffer();
			else
				return;
		}



		public Polygon2dBounds Bounds {
			get {
				if (_renderType.Equals(RenderTypes.Shape))
					return _shapes.Bounds;
				else if (_renderType.Equals(RenderTypes.Sprite))
					return _sprites.Bounds;
				else if (_renderType.Equals(RenderTypes.Lines))
					return _lines.Bounds;
				return Polygon2dBounds.Empty;
			}
		}

		public bool HasMask {
			get {
				return false;
			}
		}

		//Visual Style
		Color _color;
		public Color Color {
			get {
				if (_color == null)
					_color = Color.White;
				return _color;
			}
		}

		BlendMode _blendMode;
		public BlendMode BlendMode {
			get {
				if (_blendMode == null)
					_blendMode = BlendModes.Opaque;
				return _blendMode;
			}
		}

		I_Shader _shader;
		public I_Shader Shader { get { return _shader; } }
		public bool TryShader(ref I_Shader result) {
			if (_shader == null)
				return false;
			result = _shader;
			return true;
		}


		//Shapes
		I_polygonShape2d _shape;
		public I_polygonShape2d Shape {
			get {
				return _shape;
			}
		}

		TransformationData2D _shapeTransform;

		ShapeBuffer _shapes;
		public ShapeBuffer Shapes { get { return _shapes; } }
		public bool TryShapes(ref ShapeBuffer result) {
			if (_shapes == null)
				return false;
			result = _shapes;
			return true;
		}
		void PrepareShapeBuffer() {
			if (_shape == null)
				return;

			Vector2dList points = _shape.Points;

			if (_shapeTransform != null)
				points = _shapeTransform.ApplyToVector2dListAsCopy(points);

			//Determine Bounds
			Polygon2dBounds bounds = Polygon2dBounds.FromPoints(points);
			
			//Create Triangulation-data
			TriangulationData data = TriangulationData.CreateFromVector2dList(points, null, bounds);

			ShapeBuffer result = new ShapeBuffer(data, bounds);

			_shapes = result;
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
		void PrepareLineBuffer() {
			if (!_generateLines)
				return;
			if (_shape == null)
				return;
			if (_lines == null) {
				Segment2dList segments = _shape.Segments;
				_lines = new LineBuffer(segments, _thickness, Color);
			}
			if (!_lines.HasBounds)
				_lines.SetBounds(LineBuffer.GenerateBounds(_lines));
		}

		bool _generateLines = false;
		int _thickness = 1;
		public int LineThickness { get { return _thickness; } }

		//Sprites
		SpriteBuffer _sprites;
		public SpriteBuffer Sprites { get { return _sprites; } }
		public bool TrySprites(ref SpriteBuffer result) {
			if (_sprites == null)
				return false;
			result = _sprites;
			return true;
		}
		void PrepareSpriteBuffer() {
			if (_sprites == null)
				return;
			if (!_sprites.HasBounds)
				_sprites.SetBounds(SpriteBuffer.GenerateBounds(_sprites));				
		}

		//Handling the result

		public void TurnRenderResultIntoSpriteData(GameManager game, PreRenderProcess process,ref SpriteBuffer outputBuffer, Vector2d position) {
			I_RenderResult result = null;
			if (!process.TryResult(ref result) || outputBuffer == null)
				return;
			PreRenderedTexture texture = new PreRenderedTexture(result);
			SpriteData data = new SpriteData(texture);
			data.SetPosition(position);
			outputBuffer.Add(data);		
		}

		//Event handling
	
		public string AsString {
			get {
				return RenderType + "." + Bounds.AsString;
			}
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


	public partial class PreRenderProcess : MonoGameComponent, I_sender<PreRenderProcessComplete>, I_SpriteDrawInfo {

		//Global references
		GameManager _game;
		public GameManager Game { get { return _game; } }

		//Name of the PreRendered Texture
		string _name;
		public string Name { get { return _name; } }

		//Defines the renderType
		string _renderType;
		public string RenderType {
			get {
				if (_shape != null)
					return RenderTypes.Shape;
				return RenderTypes.Error;
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
			System.Diagnostics.Debug.Print("PreRendered: " + Name + ".");
		}
		public void SetRenderResult(Texture2D texture, long time) {
			RenderResult result = new RenderResult(time, texture);
			SetRenderResult(result);
		}

	}
}
