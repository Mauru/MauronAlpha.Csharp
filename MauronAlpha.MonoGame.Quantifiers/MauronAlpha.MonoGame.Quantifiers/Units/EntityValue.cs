namespace MauronAlpha.MonoGame.Quantifiers.Units {
	
	public class EntityValue<ET_type>:QuantifierComponent where ET_type:EntityValueType, new() {

		public EntityValueType ValueType {
			get {
				return new ET_type();
			}
		}

		public EntityValue(bool isNull): this() {
			B_isNull = isNull;
		}
		public EntityValue() : this(true) {}
		public EntityValue(float val) : this() {
			B_isNull = false;
			INT_value = val;
		}
		public EntityValue(bool isInfinitePositive, bool isInfiniteNegative) : this() {
			B_isInFinitePoitive = isInfinitePositive;
			B_isInFiniteNegative = IsInfiniteNegative;
			B_isNull = false;
		}

		bool B_isInFinitePoitive = false;
		bool B_isInFiniteNegative = false;
		bool B_isNull = true;
		public bool IsInfinitePositive { 
			get { return B_isInFinitePoitive; } 
		}
		public bool IsInfiniteNegative {
			get {
				return IsInfinitePositive;
			}
		}
		public bool IsNull {
			get {
				return B_isNull;
			}
		}

		float INT_value = 0;
		public float ValueAsNumeric {
			get {
				return INT_value;
			}
		}
		public void SetNumericValue(float value) {
			B_isNull = false;
			INT_value = value;
		}

		public static EntityValue<T_Time> Time {
			get {
				return new EntityValue<T_Time>();
			}
		}
		public static EntityValue<T_Attribute> Attribute {
			get {
				return new EntityValue<T_Attribute>();
			}
		}
		public static EntityValue<T_Relation> Relation {
			get {
				return new EntityValue<T_Relation>();
			}
		}
		public static EntityValue<T_Duration> Duration {
			get {
				return new EntityValue<T_Duration>();
			}
		}

		public EntityValue<ET_type> InfinitePositive {
			get {
				return new EntityValue<ET_type>(true,false);
			}
		}
		public EntityValue<ET_type> Null {
			get {
				return new EntityValue<ET_type>(true);
			}
		}
	
		public bool Equals(EntityValue<EntityValueType> other) {
				if(IsNull && other.IsNull)
					return true;
				if(IsNull || other.IsNull)
					return false;

				if(!ValueType.CanEqual(other.ValueType))
					return false;

				return ValueAsNumeric.Equals(other.ValueAsNumeric);
		}
	}

	public class EntityValueType : QuantifierComponent {
		public virtual string Name { get { return "Null"; } }

		public virtual bool CanEqual(EntityValueType other) {
			return Name.Equals(other.Name);
		}

	}

	//A Timestamp in game history
	public class T_Time : EntityValueType {

		public override string Name { get { return "Time"; } }

		public static T_Time Instance {
			get {
				return new T_Time();
			}
		}
		bool B_isInFinitePoitive = false;
		public bool IsInfinitePositive { get { return B_isInFinitePoitive; } }

		public bool IsInfiniteNegative { 
			get {
				return IsInfinitePositive;
			}
		}

		public long Max {
			get {
				return long.MaxValue;
			}
		}
		public long Min {
			get {
				return long.MinValue;
			}
		}

		public EntityValue<T_Time> InfinitePositive {
			get {
				return new EntityValue<T_Time>(true, false);
			}
		}
		public EntityValue<T_Time> Null {
			get {
				return new EntityValue<T_Time>(true);
			}
		}
	
	}

	//A time-related duration
	public class T_Duration : EntityValueType {

		public override string Name { get { return "Duration"; } }

	}

	public class T_Attribute : EntityValueType {

		public override string Name { get { return "Attribute"; } }

		public static T_Attribute Instance {
			get {
				return new T_Attribute();
			}
		}
		bool B_isInFinitePoitive = false;
		public bool IsInfinitePositive { get { return B_isInFinitePoitive; } }

		public bool IsInfiniteNegative {
			get {
				return IsInfinitePositive;
			}
		}


		public EntityValue<T_Attribute> InfinitePositive {
			get {
				return new EntityValue<T_Attribute>(true, false);
			}
		}
		public EntityValue<T_Attribute> Null {
			get {
				return new EntityValue<T_Attribute>(true);
			}
		}

		public long Max { get { return 100; } }
		public long Min { get { return -100; } }
	}

	public class T_Relation : EntityValueType {

		public override string Name { get { return "Relation"; } }

		public static T_Relation Instance {
			get {
				return new T_Relation();
			}
		}
		bool B_isInFinitePoitive = false;
		public bool IsInfinitePositive { get { return B_isInFinitePoitive; } }

		public bool IsInfiniteNegative {
			get {
				return IsInfinitePositive;
			}
		}

		public EntityValue<T_Relation> InfinitePositive {
			get {
				return new EntityValue<T_Relation>(true, false);
			}
		}
		public EntityValue<T_Relation> Null {
			get {
				return new EntityValue<T_Relation>(true);
			}
		}

		public long Max { get { return 100; } }
		public long Min { get { return -100; } }
	}

	public class T_Percent : EntityValueType {
		public override string Name { get { return "Percent"; } }

		public static T_Relation Instance {
			get {
				return new T_Relation();
			}
		}
		bool B_isInFinitePoitive = false;
		public bool IsInfinitePositive { get { return B_isInFinitePoitive; } }

		public bool IsInfiniteNegative {
			get {
				return IsInfinitePositive;
			}
		}

		public EntityValue<T_Relation> InfinitePositive {
			get {
				return new EntityValue<T_Relation>(true, false);
			}
		}
		public EntityValue<T_Relation> Null {
			get {
				return new EntityValue<T_Relation>(true);
			}
		}

		public long Max { get { return 100; } }
		public long Min { get { return 0; } }

	}
}
