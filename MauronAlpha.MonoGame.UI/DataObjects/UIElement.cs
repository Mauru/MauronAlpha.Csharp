namespace MauronAlpha.MonoGame.UI.DataObjects { 
	using MauronAlpha.MonoGame.UI.Interfaces;
	using MauronAlpha.MonoGame.UI.Collections;
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.TextProcessing.Units;

	public abstract class UIElement :UIComponent, I_Drawable, I_UIHierarchyObject {

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

		public abstract Polygon2dBounds Bounds {
			get;
		}

		public abstract List<Polygon2d> Shapes {
			get;
		}
		public abstract List<MonoGameTexture> Sprites {
			get;
		}
		public abstract MonoGameTexture Rendered {
			get;
		}

		public virtual bool NeedsRenderUpdate {
			get { return true; }
		}

		long _lastRendered = 0;
		MonoGameTexture _renderResult;
		public void SetRenderResult(MonoGameTexture t, long renderTime) {
			_renderResult = t;
			_lastRendered = renderTime;
		}

		public GameRenderer Renderer {
			get { return _game.Renderer; }
		}

		I_UIHierarchyObject I_UIHierarchyObject.Parent {
			get { return Parent; }
		}

		HierarchyChildren I_UIHierarchyObject.Children {
			get { return Children; }
		}
	}

}