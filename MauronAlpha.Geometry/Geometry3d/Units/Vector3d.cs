using System;

namespace MauronAlpha.Geometry.Geometry3d.Units {

	public class Vector3d : GeometryComponent3d {
		public double X=0;
		public double Y=0;
		public double Z=0;
		public Vector3d (Vector3d p) {
			X=p.X;
			Y=p.Y;
			Z=p.Z;
		}
		public Vector3d ( ) { }
		public Vector3d (double x, double y, double z) {
			X=x;
			Y=y;
			Z=z;
		}
		public Vector3d Add (Vector3d v) {
			X+=v.X;
			Y+=v.Y;
			Z+=v.Z;
			return this;
		}
		public Vector3d Subtract (Vector3d v) {
			X-=v.X;
			Y-=v.Y;
			Z-=v.Z;
			return this;
		}
		public Vector3d Difference (Vector3d v) {
			return this.Instance.Subtract(v);
		}
		//get the Absolute x and y (negative to positive)
		public Vector3d Normalized {
			get {
				return new Vector3d(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
			}
		}
		public Vector3d Instance {
			get { return new Vector3d(this); }
		}
		public new string ToString {
			get {
				return "{[x:"+X+"],[y:"+Y+"],[z:"+Z+"]}";
			}
		}
		public bool Equals (Vector3d v) {
			return (v.ToString==this.ToString);
		}

	}

}