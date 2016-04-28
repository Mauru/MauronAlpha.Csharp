using MauronAlpha.Geometry;

using System;

namespace MauronAlpha.Games.PixelRPG {

	//a hexagon (flat topped)
	public class Hex : Polygon2d {
		public double Size=0;
		public new Vector2d Center=new Vector2d(0, 0);

		public new Segment2d[] Segments {
			get {
				Segment2d[] r=new Segment2d[6];
				for( int i=0; i<6; i++ ) {
					r[i]=new Segment2d(Points[i], Points[(i+1)]);
				}
				return r;
			}
		}
		public new Vector2d[] Points=new Vector2d[6];

		public Hex (Vector2d center, double size) {
			for( int i=0; i<6; i++ ) {
				double angle= (double) (2*Math.PI/6*i);
				Vector2d n=new Vector2d(
					center.X + size + (double) Math.Cos(angle),
					center.Y + size + (double) Math.Sin(angle)
				);
				Points[i]=n;
				Center=center;
				Size=size;
			}
		}
		public Hex ( ) { }
	}

}
