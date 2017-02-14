namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Assets.Interfaces;

	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;
	
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.FontParser.DataObjects;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	/// <summary> Defines how a Sprite is rendered. </summary>
	public class SpriteDrawCall:MonoGameComponent, I_SpriteDrawCall {

		//Constructors
		public SpriteDrawCall(I_MonoGameTexture texture): base() {
			_texture = texture;
		}

		public SpriteDrawCall(I_MonoGameTexture texture, Rectangle2d mask) : base() {
			_texture = texture;
			_mask = Rectangle2dToRectangle(mask);
		}
		public SpriteDrawCall(I_MonoGameTexture texture, Polygon2dBounds mask): base() {
			_texture = texture;
			_mask = Bounds2Rectangle(mask);
		}
		public SpriteDrawCall(I_MonoGameTexture texture, Rectangle mask): base() {
			_mask = mask;
			_texture = texture;
		}

		public SpriteDrawCall(I_MonoGameTexture texture, Vector2d position):base() {
			_texture = texture;
			_position = position;
		}
		public SpriteDrawCall(I_MonoGameTexture texture, Vector2d position, Color color): base() {
			_texture = texture;
			_position = position;
			_color = color;
		}

		public BlendMode BlendMode {
			get {
				return BlendModes.Alpha;
			}
		}

		I_MonoGameTexture _texture;
		public I_MonoGameTexture Texture {
			get { return _texture; }
		}
		public bool TryTexture(ref I_MonoGameTexture result) {
			if (_texture == null)
				return false;
			result = _texture;
			return true;
		}

		public double Width {
			get {
				if (_texture == null)
					return 0;
				if (_mask != null)
					return _mask.Value.Width;

				return _texture.Width;
			}
		}
		public double Height {
			get {
				if (_texture == null)
					return 0;
				if (_mask != null)
					return _mask.Value.Height;

				return _texture.Height;
			}
		}

		Vector2d _position = Vector2d.Zero;
		public Vector2d Position { get { return _position; } }
		public SpriteDrawCall SetPosition(Vector2d v) {
			_position = v;
			return this;
		}
		public SpriteDrawCall SetPosition(double x, double y) {
			_position = new Vector2d(x, y);
			return this;
		}

		System.Nullable<Rectangle> _posAsRect = null;
		public Rectangle PositionAsRectangle {
			get {
				if (_posAsRect == null) {
					Rectangle result = GeneratePositionRectangle(this);
					_posAsRect = result;
				}
				return _posAsRect.Value;
			}
		}

		System.Nullable<Rectangle> _mask;
		public Rectangle Mask {
			get {
				return _mask.Value;
			}
		}
		public bool HasMask {
			get {
				return _mask.HasValue;
			}
		}
		public bool TryMask(ref Rectangle result) {
			if (_mask == null)
				return false;
			result = _mask.Value;
			return true;
		}

		Color _color = Color.White;
		public Color Color {
			get {
				return _color;
			}
		}
		public void SetColor(Color color) {
			_color = color;
		}



		//Static utility functions
		public static Rectangle Rectangle2dToRectangle(Rectangle2d r) {
			return new Rectangle(
				(int)r.X,
				(int)r.Y,
				(int)r.Width,
				(int)r.Height
			);
		}
		public static Rectangle Bounds2Rectangle(Polygon2dBounds b) {
			return new Rectangle((int) b.X, (int) b.Y, (int) b.Width, (int) b.Height);
		}
		public static Rectangle GeneratePositionRectangle(SpriteDrawCall data) {
			Rectangle result = new Rectangle(data.Position.IntX, data.Position.IntY, (int)data.Width, (int)data.Height);
			System.Diagnostics.Debug.Print("SpriteDrawCall.GeneratePositionRectangle: " + result.ToString());
			return result;
		}

		/// <summary> Generate Poly2dBounds from SpriteDrawCall </summary>
		public static Polygon2dBounds GenerateBounds(SpriteDrawCall data) {
			Rectangle tmp = new Rectangle();
			Vector2d p = data.Position;
			if (data.TryMask(ref tmp))
				return new Polygon2dBounds(p.X, p.Y, tmp.Width, tmp.Height);
			I_MonoGameTexture t = null; 
			if(data.TryTexture(ref t))
				return new Polygon2dBounds(p.X, p.Y, t.Width, t.Height);
			return Polygon2dBounds.Empty;
		}
		public static Polygon2dBounds Rectangle2Bounds(Rectangle r) {
			return new Polygon2dBounds(r.X, r.Y, r.Width, r.Height);
		}

	}

}

namespace MauronAlpha.MonoGame.Theory {

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	using System;

	using MauronAlpha.Geometry.Geometry2d.Units;

	public class SpriteDrawCall : MonoGameComponent {

		Color _tint = Color.White;
		public Color Tint { get { return _tint; } }

		Vector2 _scale = Vector2.One;
		Vector2 Scale { get { return _scale; } }

		Vector2 _position = Vector2.Zero;
		public Vector2 Position { get { return _position; } }

		float _rotation = 0f;
		public float RotationRad { get { return _rotation; } }

		Vector2 _rotOffset = Vector2.Zero;
		public Vector2 RotationOffset {
			get {
				return _rotOffset;
			}
		}

		Nullable<Rectangle> _mask;
		public bool HasMask {
			get {
				return _mask.HasValue;
			}
		}
		public Rectangle Mask { get { return _mask.Value; } }

		Texture2D _texture;
		Texture2D Texture {
			get {
				return _texture;
			}
		}

		Polygon2dBounds _transformedBounds;
		Polygon2dBounds Bounds {
			get {
				if (_transformedBounds == null)
					_transformedBounds = CalculateBounds(this);
				return _transformedBounds;
			}
		}

		public static Polygon2dBounds CalculateBounds(SpriteDrawCall obj) {
			Rectangle size;
			if (obj.HasMask)
				size = obj.Mask;
			else
				size = obj.Texture.Bounds;

			Polygon2dBounds result = TransformHelper.CalculateTransformedBounds(
				obj.Texture.Width,
				obj.Texture.Height,
				obj.RotationRad,
				obj.RotationOffset.X,
				obj.RotationOffset.Y,
				obj.Scale.X,
				obj.Scale.Y,
				obj.Position.X,
				obj.Position.Y
			);

			return result;
		}

	}

}

namespace MauronAlpha.MonoGame {

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Transformation;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	public class TransformHelper:MonoGameComponent {


		public static Polygon2dBounds CalculateTransformedBounds(double width, double height, double radRotation, double rotateOffsetX, double rotateOffsetY, double xScale, double yScale, double finalTranslateX, double finalTranslateY) {
			Matrix2d rotation = Matrix2d.CreateRotationRad(radRotation);
			Matrix2d translate = Matrix2d.CreateTranslation(rotateOffsetX, rotateOffsetY);
			Matrix2d scaled = Matrix2d.CreateScale(xScale,yScale);

			Matrix2d combined = scaled.MultipliedCopy(translate).MultipliedCopy(translate).MultipliedCopy(rotation);

			Rectangle2d rectangle = Rectangle2d.CreateAlignTopLeft(width, height);

			Vector2dList transformed = combined.ApplyToCopy(rectangle.Points);
			Polygon2dBounds result = new Polygon2dBounds(transformed);
			result.Offset(finalTranslateX, finalTranslateY);
			return result;

		}
	}

}