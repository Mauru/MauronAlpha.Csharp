using MauronAlpha.MonoGame.Entities.Units;

namespace MauronAlpha.MonoGame.Geography.Units {
	public class GeoValue:EntityValue {
		public GeoValue() : base(T_GeoValue.Instance) { }
	}

	public class T_GeoValue : EntityValueType {
		public static T_GeoValue Instance {
			get {
				return new T_GeoValue();
			}
		}
		bool B_isInFinitePoitive = false;
		public bool IsInfinitePositive { get { return B_isInFinitePoitive; } }

		bool B_isInFiniteNegative = false;
		public bool IsInfiniteNegative {
			get {
				return IsInfinitePositive;
			}
		}
		long Value = 0;

		public static EntityValue InfinitePositive {
			get {
				return new EntityValue(T_Relation.Instance, 0, true, false);
			}
		}
		public static EntityValue Null {
			get {
				return new EntityValue(T_Relation.Instance, true);
			}
		}

		public long Max { get { return 100; } }
		public long Min { get { return -100; } }
	}
}
