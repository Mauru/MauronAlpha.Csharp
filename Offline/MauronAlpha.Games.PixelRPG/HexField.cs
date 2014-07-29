using MauronAlpha.GameEngine;
using MauronAlpha.Geometry;

using System;

namespace MauronAlpha.Games.PixelRPG {

	//a hexfield in an Axial flat-topped Grid
	public class HexField : Drawable {
		
		//numerical id        
		public readonly short HexIndex;
		//Geometry
		public readonly Hex Hex;

		//Coordinates
		public readonly Vector2d Axial;
		public readonly Vector2d EvenQ;
		public readonly Vector3d Cube;
		//default coordinates are Axial
		public Vector2d Coordinates {
			get {
				return this.Axial;
			}
		}

		//Related Links
		public readonly HexGrid Grid;
		public readonly HexFieldNeighbors Neighbors;
		public readonly HexFieldData Data;

		public override double Width {
			get {
				return ((int) Data.Style.Size)*2;
			}
			set { throw new NotImplementedException(); }
		}
		public override double Height {
			get {
				return (double) Math.Sqrt(3) / 2 * Width;
			}
			set { throw new NotImplementedException(); }
		}

		//constructor
		public HexField (Drawable parent, HexGrid grid, short index, Vector2d axial, HexFieldData data):base(DrawableType_container.Instance) {
			
			Grid = grid;
			Parent = parent;

			Data=data;
			Data.Field=this;

			HexIndex=index;
			Axial=axial;
			EvenQ=HexNeightborsEvenQ.FromAxial(axial);
			Cube=HexNeightborsCube.FromAxial(axial);

			Neighbors=new HexFieldNeighbors(Grid, this);

			//build the hex
			Vector2d c=new Vector2d();
			c.X = EvenQ.X * (Width * 0.75F);
			c.Y = EvenQ.Y * Height;

			//setup the Polygonal Representation of this item
			if( EvenQ.X%2 != 0 ) {
				c.Y += Height/2;
			}
			Hex=new Hex(c, (int) Data.Style.Size);
		}


		public override void E_HasRendered ( ) {
			throw new NotImplementedException();
		}
	}
}