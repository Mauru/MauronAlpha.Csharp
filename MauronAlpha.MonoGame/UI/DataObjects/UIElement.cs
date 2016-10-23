namespace MauronAlpha.MonoGame.UI.DataObjects { 
	using MauronAlpha.MonoGame.UI.Interfaces;
	using MauronAlpha.MonoGame.UI.Collections;

	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

	using MauronAlpha.TextProcessing.Units;

	using MauronAlpha.Events.Interfaces;

	using Microsoft.Xna.Framework;

	public abstract class UIElement :UIComponent, I_Renderable, I_UIHierarchyObject, I_subscriber<RenderEvent> {

		//constructor
		public UIElement(GameManager game) : base() {
			_game = game;
		}
		GameManager _game;
		public GameManager Game {
			get { return _game; }
		}

		public virtual UIElement Parent { get { return null; } }
		public HierarchyChildren Children = new HierarchyChildren();

		public virtual bool CanHaveChildren { get { return true; } }
		public virtual bool CanHaveParent { get { return true; } }
		public virtual bool UsesTabOrder { get { return false; } }
		public virtual bool IsClickable { get { return false; } }

		public virtual void OnMouseUp() { }
		public virtual void OnMouseDown() { }
		public virtual void OnMouseOver() { }
		public virtual void OnMouseOut() { }

		int _tabOrder = 0;

		Polygon2dBounds _bounds;
		public virtual Polygon2dBounds Bounds {
			get {
				if(_bounds == null)
					_bounds = new Polygon2dBounds(0, 0);
				return _bounds;
			}
		}

		bool _needUpdate = true;
		public virtual bool NeedsRenderUpdate {
			get { return _needUpdate; }
		}

		public void NeedRenderUpdate(long time) {
			RenderRequest request = new RenderRequest(this, time);
			request.Subscribe(this);
			Renderer.AddRequest(request);
		}

		I_RenderResult _rendered;
		public I_RenderResult RenderResult {
			get {
				return _rendered;
			}
		}
		public bool HasRenderResult {
			get {
			return _rendered != null;
			}
		}

		public long LastRendered {
			get {
				if(_rendered == null)
					return 0;
				return _rendered.Time;
			}
		}

		public GameRenderer Renderer {
			get { return _game.Renderer; }
		}

		Matrix2d _matrix;
		Matrix2d Matrix {
			get {
				if(_matrix == null)
					_matrix = Matrix2d.Identity;
				return _matrix;
			}
		}
		public Vector2d Position {
			get {
				return Matrix.Translation;
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

		I_UIHierarchyObject I_UIHierarchyObject.Parent {
			get { return Parent; }
		}

		HierarchyChildren I_UIHierarchyObject.Children {
			get { return Children; }
		}

		public bool ReceiveEvent(RenderEvent e) {
			_rendered = e.Result;
			_needUpdate = false;
			return true;
		}

		public bool Equals(I_subscriber<RenderEvent> other) {
			return Id.Equals(other.Id);
		}

		public abstract Vector2d SizeAsVector2d { get; }

		public virtual Vector2d RenderTargetSize {
			get { return Game.Renderer.ScreenSize; }
		}

		public virtual I_RenderResult Outline {
			get { return new RenderResult(); }
		}

		public virtual RenderOrders Orders {
			get { return new RenderOrders(); }
		}

		public void SetRenderResult(I_RenderResult result) {
			_rendered = result;
		}


		public abstract GameRenderer.RenderMethod RenderMethod {
			get;
		}

		public abstract System.Type RenderPresetType {
			get;
		}

		public virtual I_MonoShape AsMonoShape() {
			throw new GameError("UIElement is not castable as Shape!", this);
		}

		public abstract ShapeBuffer ShapeBuffer { get; }
		public abstract bool IsPolygon { get; }
	
	}

}