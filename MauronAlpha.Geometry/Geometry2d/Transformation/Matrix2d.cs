namespace MauronAlpha.Geometry.Geometry2d.Transformation {
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.HandlingErrors;

	//Keeps track of all applied transforms to a series of geometrical data
	public class Matrix2d : GeometryComponent2d {
		double	m11, m12, m13,
				m21, m22, m23,
				m31, m32, m33;

		//constructor
		public Matrix2d(
			double d11, double d12, double d13, 
			double d21, double d22, double d23,
			double d31, double d32, double d33
		):base() {
			m11 = d11; m12 = d12; m13 = d13;
			m21 = d21; m22 = d22; m23 = d23;
			m31 = d31; m32 = d32; m33 = d33;
		}

		public Matrix2d Multiply(
			double d11, double d12, double d13,
			double d21, double d22, double d23,
			double d31, double d32, double d33
		) {

			double	r11,r12,r13,
					r21,r22,r23,
					r31,r32,r33;

			//rows 1-3
			r11 = m11 * d11 + m12 * d21 + m13 * d31;
			r12 = m11 * d12 + m12 * d22 + m13 * d32;
			r13 = m11 * d13 + m12 * d23 + m13 * d33;

			r21 = m21 * d11 + m22 * d21 + m23 * d31;
			r22 = m21 * d12 + m22 * d22 + m23 * d32;
			r23 = m21 * d13 + m22 * d23 + m23 * d33;

			r31 = m31 * d11 + m32 * d21 + m33 * d31;
			r32 = m31 * d12 + m32 * d22 + m33 * d32;
			r33 = m31 * d13 + m32 * d23 + m33 * d33;

			//commit to matrix
			m11 = r11; m12 = r12; m13 = r13;
			m21 = r21; m22 = r22; m23 = r23;
			m31 = r31; m32 = r32; m33 = r33;

			return this;

		}

		public Vector2d ApplyTo(double x, double y) {

			double newx = x * m11 + y * m12+m13;
			double newy = x * m21 + y * m22 + m23;

			return new Vector2d(
				newx,
				newy
			);

		}

		public Vector2dList ApplyToCopy(Vector2dList list) {
			Vector2dList result = new Vector2dList();
			foreach (Vector2d v in list)
				result.Add(ApplyToCopyVector2d(v));
			return result;
		}
		public Vector2d ApplyToCopyVector2d(Vector2d v) {
			return ApplyTo(v.X, v.Y);			
		}

		public Matrix2d SetValue(int index, double value) {
			switch (index) {
				case 0: m11 = value; return this;
				case 1: m12 = value; return this;
				case 2: m13 = value; return this;

				case 3: m21 = value; return this;
				case 4: m22 = value; return this;
				case 5: m23 = value; return this;

				case 6: m31 = value; return this;
				case 7: m32 = value; return this;
				case 8: m33 = value; return this;

				default: throw new MauronCode_error("Invalid index!", this, ErrorType_index.Instance);
			}
		}
		public Matrix2d SetValue(int row, int column, double value) {
			return SetValue((row * 3) + column, value);
		}

		public static Matrix2d Identity {
			get {
				return new Matrix2d(
					1,0,0,
					0,1,0,
					0,0,1
				);
			}
		}
		public Matrix2d Copy {
			get {
				return new Matrix2d(m11, m12, m13, m21, m22, m23, m31, m32, m33);
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
		public double this[int row, int column] {
			get {
				return Value((row * 3) + column);
			}
			set {
				SetValue((row * 3) + column, value);
			}
		}
		public double Value(int index) {
			switch (index) {
				case 0: return m11;
				case 1: return m12;
				case 2: return m13;

				case 3: return m21;
				case 4: return m22;
				case 5: return m23;

				case 6: return m31;
				case 7: return m32;
				case 8: return m33;

				default: throw new MauronCode_error("Invalid index!", this, ErrorType_index.Instance);
			}
		}
		public double Value(int row, int column) {
			return this[row, column];
		}

		//BOOL Equals
		public bool Equals( Matrix2d o ) {
			return
				m11 == o[0, 0] && m12 == o[0, 1] && m13 == o[0, 2]
				&& m21 == o[1, 0] && m22 == o[1, 1] && m23 == o[1, 2]
				&& m31 == o[2, 0] && m32 == o[2, 1] && m33 == o[2, 2];

			return true;
		}

		public Matrix2d AddToTranslation(double x, double y) {
			m13 += x;
			m23 += y;
			return this;
		}
		public Vector2d Translation {
			get {
				return new Vector2d(m13, m23);
			}
		}
		public Matrix2d SetTranslation(Vector2d v) {
			return SetTranslation(v.X, v.Y);
		}
		public Matrix2d SetTranslation(double x, double y) {
			m13 = x;
			m23 = y;
			return this;
		}

		public static Matrix2d CreateTranslation(double x, double y) {
			return new Matrix2d(
				1, 0, x,
				0, 1, y,
				0, 0, 1
			);
		}
		public static Matrix2d CreateTranslation(Vector2d v) {
			return CreateTranslation(v.X, v.Y);
		}
	}

}
