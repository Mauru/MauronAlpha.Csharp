using MauronAlpha;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;

using System;

namespace MauronAlpha.Geometry.Geometry2d.Utility {

	//offers several utility functions for 2d Calculations
	public class GeometryHelper2d : GeometryHelper {

		//constructor
		public GeometryHelper2d():base(){}
		
		//given an array of points, return the smallest xy, largest xy
		public static Vector2d[] PolygonBounds (Vector2d[] points) {
			Vector2d min=null;
			Vector2d max=null;
			foreach( Vector2d p in points ) {
				if( min==null ) {
					min=new Vector2d(p);
					max=new Vector2d(p);
				}
				else {
					if( p.X<min.X )
						min.SetX(p.X);
					if( p.Y<min.Y )
						min.SetY(p.Y);
					if( p.X>max.X )
						max.SetX(p.X);
					if( p.Y>max.Y )
						max.SetY(p.Y);
				}
			}
			return new Vector2d[2] { min, max };
		}

		//get the centerr of a collection of points
		public static Vector2d PolygonCenter(Vector2d[] points) {
			Vector2d[] minmax=	PolygonBounds(points);
			Vector2d d = (Vector2d) minmax[0].Difference(minmax[1]).Divide(2);
			return minmax[0].Add(d);
		}
		public static Vector2d PolygonCenter (Polygon2d p) {
			return PolygonCenter(p.Points);
		}
	}

}