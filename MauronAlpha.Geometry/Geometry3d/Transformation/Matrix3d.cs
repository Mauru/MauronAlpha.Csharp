namespace MauronAlpha.Geometry.Geometry3d.Transformation {
	using MauronAlpha.Geometry.Geometry3d.Units;

	using MauronAlpha.HandlingErrors;

	using System;

	public class Matrix3d:GeometryComponent3d {

		public Matrix3d(
			double d11, double d12, double d13, double d14,
			double d21, double d22, double d23, double d24,
			double d31, double d32, double d33, double d34,
			double d41, double d42, double d43, double d44
		): base() {
			m11 = d11; m12 = d12; m13 = d13; m14 = d14;
			m21 = d21; m22 = d22; m23 = d23; m24 = d24;
			m31 = d31; m32 = d32; m33 = d33; m34 = d34;
			m41 = d41; m42 = d42; m43 = d43; m44 = d44;
		}

		public Vector3d Translation { get { return new Vector3d(m14,m24,m34); } }
		public Matrix3d SetTranslation(Vector3d v) {
			m14 = v.X; m24 = v.Y; m34 = v.Z;
			return this;
		}
		public Matrix3d Add(Vector3d v) {
			m14+=v.X;
			m24+=v.Y;
			m34+=v.Z;
			return this;
		}
		
		/// <summary> Generate id matrix with translation from vector </summary>
		public static Matrix3d FromVector3d(Vector3d v) {
			return new Matrix3d(
				1,0,0,v.X,
				0,1,0,v.Y,
				0,0,1,v.Z,
				0,0,0,1
			);
		}

		Vector3d _rotation;
		Vector3d Rotation {
			get {
				return _rotation;
			}
		}

		Vector3d _scale;
		Vector3d Scale { get { return _scale; } }

		//Values
		double m11; double m12; double m13; double m14;	
		double m21; double m22; double m23; double m24;
		double m31; double m32; double m33; double m34;
		double m41;	double m42;	double m43;	double m44;

		public static Matrix3d Identity {
			get {
				return new Matrix3d(
					1, 0, 0, 0,
					0, 1, 0, 0,
					0, 0, 1, 0,
					0, 0, 0, 1
				);
			}
		}

		public double this[int index] {
			get {
				return Value(index);
			}
			set {
				SetValue(index, value);
			}
		}
		public double this[int row, int column]  {
			get {
                return Value((row * 4) + column);
            }
            set {
                SetValue((row * 4) + column, value);
            }
		}

		public double Value(int index) {
			switch(index) {
				case 0: return m11;
				case 1: return m12;
				case 2: return m13;
				case 3: return m14;
				case 4: return m21;
				case 5: return m22;
				case 6: return m23;
				case 7: return m24;
				case 8: return m31;
				case 9: return m32;
				case 10: return m33;
				case 11: return m34;
				case 12: return m41;
				case 13: return m42;
				case 14: return m43;
				case 15: return m44;
				default: throw new MauronCode_error("Invalid index!",this, ErrorType_index.Instance);
			}
		}
		public double Value(int row, int column) {
			return this[row,column];
		}
		public Matrix3d SetValue(int index, double value) { 
			switch(index) {
				case 0: m11 = value; return this;
				case 1: m12 = value; return this;
				case 2: m13 = value; return this;
				case 3: m14 = value; return this;

				case 4: m21 = value; return this;
				case 5:	m22 = value;	return this;
				case 6: m23 = value; return this;
				case 7:	m24 = value;	return this;

				case 8: m31 = value; return this;
				case 9:	m32 = value;	return this;
				case 10: m33 = value; return this;
				case 11: m34 = value; return this;

				case 12: m41 = value; return this;
				case 13: m42 = value; return this;
				case 14: m43 = value; return this;
				case 15: m44 = value; return this;

				default: throw new MauronCode_error("Invalid index!", this, ErrorType_index.Instance);
			}
		}
		public Matrix3d SetValue(int row, int column, double value) {
			return SetValue((row * 4) + column, value);
		}
		public Matrix3d AddToTranslation(double x, double y, double z) {
			m14 += x;
			m24 += y;
			m34 += z;
			return this;
		}

		public Matrix3d Add(Matrix3d o) {
			AddTo(0, o.Value(0)); AddTo(1, o.Value(1));	AddTo(2, o.Value(2)); AddTo(3, o.Value(3));
			AddTo(4, o.Value(4)); AddTo(5, o.Value(5));	AddTo(6, o.Value(6)); AddTo(7, o.Value(7));
			AddTo(8, o.Value(8)); AddTo(9, o.Value(9));	AddTo(10, o.Value(10)); AddTo(11, o.Value(11));
			AddTo(12, o.Value(12)); AddTo(13, o.Value(13));	AddTo(14, o.Value(14)); AddTo(15, o.Value(15));
			return this;
		}
		public Matrix3d AddTo(int index, double value) {
			return SetValue(index, Value(index)+value);
		}
		public Matrix3d Multiply(
			double d11, double d12, double d13, double d14,
			double d21, double d22, double d23, double d24,
			double d31, double d32, double d33, double d34,
			double d41, double d42, double d43, double d44) {

			//declare temp vars
			double	r11, r12, r13, r14,
					r21, r22, r23, r24,
					r31, r32, r33, r34,
					r41, r42, r43, r44;

			// row 1
			r11 = m11 * d11 + m12 * d21 + m13 * d31 + m14 * d41;
			r12 = m11 * d12 + m12 * d22 + m13 * d32 + m14 * d42;
			r13 = m11 * d13 + m12 * d23 + m13 * d33 + m14 * d43;
			r14 = m11 * d14 + m12 * d24 + m13 * d34 + m14 * d44;

			//row 2
			r21 = m21 * d11 + m22 * d21 + m23 * d31 + m24 * d41;
			r22 = m21 * d12 + m22 * d22 + m23 * d32 + m24 * d42;
			r23 = m21 * d13 + m22 * d23 + m23 * d33 + m24 * d43;
			r24 = m21 * d14 + m22 * d24 + m23 * d34 + m24 * d44;

			//row 3
			r31 = m31 * d11 + m32 * d21 + m33 * d31 + m34 * d41;
			r32 = m31 * d12 + m32 * d22 + m33 * d32 + m34 * d42;
			r33 = m31 * d13 + m32 * d23 + m33 * d33 + m34 * d43;
			r34 = m31 * d14 + m32 * d24 + m33 * d34 + m34 * d44;

			//row 4
			r41 = m41 * d11 + m42 * d21 + m43 * d31 + m44 * d41;
			r42 = m41 * d12 + m42 * d22 + m43 * d32 + m44 * d42;
			r43 = m41 * d13 + m42 * d23 + m43 * d33 + m44 * d43;
			r44 = m41 * d14 + m42 * d24 + m43 * d34 + m44 * d44;

			// commit to matrix
			m11 = r11; m12 = r12; m13 = r13; m14 = r14;
			m21 = r21; m22 = r22; m23 = r23; m24 = r24;
			m31 = r31; m32 = r32; m33 = r33; m34 = r34;
			m41 = r41; m42 = r42; m43 = r43; m44 = r44;

			return this;
		}
		public Matrix3d Multiply(Matrix3d o) {
			return Multiply(
				o.Value(0,0), o.Value(0,1), o.Value(0,2), o.Value(0,3),
				o.Value(1,0), o.Value(1,1), o.Value(1,2), o.Value(1,3),
				o.Value(2,0), o.Value(2,1), o.Value(2,2), o.Value(2,3),
				o.Value(3,0), o.Value(3,1), o.Value(3,2), o.Value(3,3)
			);
		}
		public Matrix3d Multiply(double factor) {
			m11*=factor;m12*=factor;m13*=factor;m14*=factor;
			m21*=factor;m22*=factor;m23*=factor;m24*=factor;
			m31*=factor;m32*=factor;m33*=factor;m34*=factor;
			m41*=factor;m42*=factor;m43*=factor;m44*=factor;
			return this;
		}
		public Vector3d Multiply(Vector3d v) {
			return new Vector3d(
				v.X*m11+v.Y*m21+v.Z*m31+m41,
				v.X*m12+v.Y*m22+v.Z*m32+m42,
				v.X*m13+v.Y*m23+v.Z*m33+m43
			);
		}

		public static Matrix3d RotationZ(double n) {
			return new Matrix3d(
				Math.Cos(n),Math.Asin(n),0,0,
				Math.Sin(n),Math.Cos(n),0,0,
				0,0,1,0,
				0,0,0,1
			);
		}
		public static Matrix3d RotationX(double n) {
			return new Matrix3d(
				1, 0, 0, 0,
				0, Math.Cos(n), Math.Asin(n), 0,
				0, Math.Sin(n), Math.Cos(n), 0,
				0, 0, 0, 1
			);
		}
		public static Matrix3d RotationY(double n) {
			return new Matrix3d(
				Math.Cos(n),0,Math.Sin(n),0,
				0,1,0,0,
				Math.Asin(n),0,Math.Cos(n),0,
				0,0,0,1
			);
		}

		public static Matrix3d FromPosition(double x, double y, double z) {
			return new Matrix3d(
				1, 0, 0, x,
				0, 1, 0, y,
				0, 0, 1, y,
				0, 0, 0, 1
			);
		}
	}
}
