using MauronAlpha;

using System;

namespace MauronAlpha.Geometry._2d {

	//offers several utility functions for 2d Calculations
	public class Geometry2d : GeometryHelper {

		//constructor
		public Geometry2d():base(){}
		
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
						min.X=p.X;
					if( p.Y<min.Y )
						min.Y=p.Y;
					if( p.X>max.X )
						max.X=p.X;
					if( p.Y>max.Y )
						max.Y=p.Y;
				}
			}
			return new Vector2d[2] { min, max };
		}

		//get the centerr of a collection of points
		public static Vector2d PolygonCenter(Vector2d[] points) {
			Vector2d[] minmax=	PolygonBounds(points);
			Vector2d d = minmax[0].Difference(minmax[1]).Divide(2);
			return minmax[0].Add(d);
		}
		public static Vector2d PolygonCenter (Polygon2d p) {
			return PolygonCenter(p.Points);
		}
	}

}