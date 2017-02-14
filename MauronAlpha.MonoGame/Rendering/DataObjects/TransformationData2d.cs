namespace MauronAlpha.MonoGame.Rendering.DataObjects {

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Transformation;
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Utility;

	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.MonoGame.Collections;

	/// <summary> Holds data related to offset,position and transformation of an object. </summary>
	public class TransformationData2D : MonoGameComponent {

		private TransformationData2D() : base() { }

		private TransformationData2D(Vector2d distanceToCenter)	: base() {
			_topLeftToCenter = distanceToCenter;
		}

		public static TransformationData2D AlignedTopLeft(Vector2d distanceToCenter) {
			return new TransformationData2D(distanceToCenter);
		}

		// Defines if the shape has been offset from its center
		Vector2d _topLeftToCenter;

		// The Vector2d Movement along XY that is added after all transformations are done. Included in _finalMatrix.
		Vector2d _finalTranslation = Vector2d.Zero;

		// The Rotations Applied as Rad, never really applied - just to track;
		double _rotationRad = 0;

		// The matrices used to come to the final matrix
		Matrix2dChain _matricesToApply = new Matrix2dChain();

		// The matrixChain to achieve a tranlsation for Verbose tracking/debugging
		List<string> _orderOfAppliedActionsAsString = new List<string>();

		public bool Transform(string transformationType, Vector2d xy) {

			//Scale
			if (transformationType == "S") {
				Matrix2d factor = Matrix2d.CreateScale(xy.X, xy.Y);
				_matricesToApply.Add(factor);

				_orderOfAppliedActionsAsString.Add("S");

				_transformMatrixNeedsToBeRecalculated = true;
				_finalMatrixNeedsToBeRecalculated = true;

				_transformMatrix = null;
				_finalMatrix = null;

				return true;
			}

			// Rotation
			if (transformationType == "R") {
				//Rotations as vector2d are ignored.
				return false; ;
			}

			// Translation
			if (transformationType == "T") {
				Matrix2d factor = Matrix2d.CreateTranslation(xy.X, xy.Y);
				_matricesToApply.Add(factor);

				_orderOfAppliedActionsAsString.Add("S");

				_transformMatrixNeedsToBeRecalculated = true;
				_finalMatrixNeedsToBeRecalculated = true;
				_transformMatrix = null;
				_finalMatrix = null;

				return true;
			}

			// Movement - Vector2d applied after transformations
			if (transformationType == "M") {

				Vector2d result = _finalTranslation.Add(xy.X, xy.Y);

				_orderOfAppliedActionsAsString.Add("M");

				_finalMatrixNeedsToBeRecalculated = true;
				_finalMatrix = null;

				return true;
			}

			//Unknown Translation
			return false;



		}
		public bool Transform(string tranformationType, long value) {

			// Rotation
			if (tranformationType != "R") {
				//only rotations accept long values
				return false; ;
			}

			Matrix2d factor = Matrix2d.CreateRotationRad(value);
			_orderOfAppliedActionsAsString.Add("R");
			_transformMatrixNeedsToBeRecalculated = true;
			_finalMatrixNeedsToBeRecalculated = true;
			_transformMatrix = null;
			_rotationRad += value;
			_finalMatrix = null;

			_matricesToApply.Add(factor);

			return true;

		}

		// The matrix with all transformations applied but without translation or offset
		Matrix2d _transformMatrix;
		bool _transformMatrixNeedsToBeRecalculated = true;
		void CalculateTransformMatrix() {
			_transformMatrix = _matricesToApply.CalculateAppliedResult();
			_transformMatrixNeedsToBeRecalculated = false;
		}

		// PostTransform PostTranslation
		Matrix2d _finalMatrix;
		bool _finalMatrixNeedsToBeRecalculated = true;
		long _timeWhenTheLastMatrixWasCalculated = -1;
		void CalculateFinalMatrix(long time) {
			Matrix2d transformations = _matricesToApply.CalculateAppliedResult();
			_transformMatrix = transformations;
			_transformMatrixNeedsToBeRecalculated = false;

			Matrix2d final = Matrix2d.Identity.AddToTranslation(_topLeftToCenter.X, _topLeftToCenter.Y);
			final = final.MultipliedCopy(transformations);

			_finalMatrix = final;
			_finalMatrixNeedsToBeRecalculated = false;
			_timeWhenTheLastMatrixWasCalculated = time;
		}


		/// <summary> Returns current matrix - calculates it if necessary. Time is set as timestamp whenever a matrix was last calculated.</summary>
		/// <param name="time">Timestamp to be set if a recalculation is needed.</param>
		/// <returns> Returns the _finalMatrix </returns>
		public Matrix2d FinalMatrix(long time) {
			if (!_finalMatrixNeedsToBeRecalculated)
				return _finalMatrix;
			CalculateFinalMatrix(time);
			return _finalMatrix;
		}

		public Polygon2dBounds CalculateFinalBounds(long time, Rectangle2d bounds) {
			if (_finalMatrixNeedsToBeRecalculated)
				CalculateFinalMatrix(time);

			Vector2dList final = bounds.Points;
			if (!_topLeftToCenter.IsZero)
				final.AddAll(_topLeftToCenter);

			if (_transformMatrix == null && !_matricesToApply.IsEmpty)
				final = _matricesToApply.CalculateAppliedResult().ApplyToCopy(final);

			if (!_finalTranslation.IsZero)
				final.AddAll(_finalTranslation);

			Polygon2dBounds result = new Polygon2dBounds(final);
			return result;
		}

		public Vector2dList ApplyToVector2dListAsCopy(long time, Vector2dList points, Vector2d offset) {
			Vector2dList result = points.Copy.AddAll(offset);
			if (_transformMatrixNeedsToBeRecalculated) {
				CalculateTransformMatrix();
				result = _transformMatrix.ApplyToCopy(result);
			}
			if (!_finalTranslation.IsZero)
				result.AddAll(_finalTranslation);

			return result;
		}
		public Vector2dList ApplyToVector2dListAsCopy(Vector2dList points) {
			Vector2dList result;
			if (_topLeftToCenter.IsZero)
				result = points.Copy;
			else
				result = points.CopyWithOffset(_topLeftToCenter.Inverted);



			_transformMatrix.ApplyTo(ref result);

			return result;


		}

		//Creates a transformation-object with just basic scale applied
		public static TransformationData2D CreateScale(Vector2d scale) {

			TransformationData2D result = new TransformationData2D();
			result.Transform(TransformationShortCuts.Scale, scale);

			return result;

		}
		public static TransformationData2D CreateScale(long factor) {

			TransformationData2D result = new TransformationData2D();
			result.Transform(TransformationShortCuts.Scale, factor);

			return result;

		}
	}

	public struct TransformationShortCuts {

		public static string Rotate { get { return "R"; } }
		public static string Translate { get { return "T"; } }
		public static string Scale { get { return "S"; } }

		public static string R { get { return "Rotate"; } }
		public static string T { get { return "Translate"; } }
		public static string S { get { return "Scale"; } }

	}
}