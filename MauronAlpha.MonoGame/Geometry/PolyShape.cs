namespace MauronAlpha.MonoGame.Geometry {

	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.HandlingData;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Utility;

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	//Basic shape class - a polygon
	public class PolyShape : Polygon2d, I_MonoShape {

		GameManager _game;
		public GameManager Game {
			get {
				return _game;
			}
		}

		public GameRenderer Renderer {
			get {
				return _game.Renderer;
			}
		}

		//Constructor
		public PolyShape(GameManager game) : base() { }

		public ShapeDefinition Definition {
			get { return new ShapeType_poly(); }
		}


		public new Vector2dList Points {
			get {
				return base.TransformedPoints;
			}
		}

		public virtual TriangulationData TriangulationData {
			get {
				if (DATA_calculate == null)
					DATA_calculate = Triangulate();
				return DATA_calculate;
			}
		}
		TriangulationData DATA_calculate;
		private TriangulationData Triangulate() {
			TriangulationData data = new TriangulationData();
			data.Polygon = this;
			data.Triangles = new TriangleList(this);
			data.Vertices = data.Triangles.AsPositionColor;
			return data;
		}

		public Vector2d RenderTargetSize {
			get { return Bounds.Size; }
		}

		public long LastRendered {
			get {
				if(_result == null)
					return 0;
				return _result.Time;
			}
		}

		I_RenderResult _result;
		public Rendering.I_RenderResult RenderResult {
			get {
				return _result;			
			}
		}
		public void SetRenderResult(I_RenderResult result) {
			_result = result;
		}
		public bool HasRenderResult {
			get {
				return _result != null;
			}
		}

		I_RenderResult _outline;
		public Rendering.I_RenderResult Outline {
			get {
				return _outline;
			}
		}

		public virtual GameRenderer.RenderMethod RenderMethod {
			get { return ShapeRenderer.RenderShapeInWorldSpace; }
		}

		VertexBuffer _buffer;
		public virtual VertexBuffer GenerateVertexBuffer(long time) {
			if(_buffer == null)
				 _buffer = new VertexBuffer(_game.Engine.GraphicsDevice,typeof(VertexPositionColor),TriangulationData.Vertices.Length,BufferUsage.WriteOnly);
			_buffer.SetData<VertexPositionColor>(TriangulationData.Vertices);
			return _buffer;
		}

		public RenderOrders Orders {
			get { throw new System.NotImplementedException(); }
		}

		public Vector2d SizeAsVector2d{
			get {
				return base.Size;
			}
		}

		Vector2 _position;
		public virtual Vector2 PositionAsVector2 {
			get {
				if(_position == null)
					_position = new Vector2(Position.FloatX, Position.FloatY);
				return _position;
			}
		}

		public I_MonoShape AsMonoShape() {
			return this;
		}
	}

	//Shape Description
	public class ShapeType_poly : ShapeDefinition {

		public override string Name { get { return "poly"; } }

		public override bool UsesSpriteBatch {
			get { return false; }
		}

		public override bool UsesVertices {
			get { return false; }
		}

		public override bool IsPolygon {
			get { return true; }
		}
	}


}
