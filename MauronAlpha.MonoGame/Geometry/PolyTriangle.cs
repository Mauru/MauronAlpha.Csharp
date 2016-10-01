namespace MauronAlpha.MonoGame.Geometry {
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.MonoGame.Collections;
	
	public class PolyTriangle:PolyShape {

		public PolyTriangle(GameManager game,  Vector2d size) : base(game) { 
		
			Vector2dList pts = new Vector2dList() {

				new Vector2d(0,0),
				new Vector2d(6,0),
				new Vector2d(3,6)

			};

			base.SetPoints( pts );
		}

	}

}
