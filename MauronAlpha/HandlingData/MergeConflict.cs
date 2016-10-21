using System;

namespace MauronAlpha.HandlingData {

	//A dataType that is related to a merge between two objects
	//It keeps references to whatever data was overwritten when two objects get merged
	public class MergeConflict:MauronCode_dataObject {

		public MergeConflict(object source, object target, object key, object value):base() {
			SetSource(source);
			SetSourceType(source.GetType());
			
			SetTarget(target);
			SetTargetType(target.GetType());

			SetKey(key);
			SetKeyType(key.GetType());

			SetValue(value);
			SetValueType(value.GetType());
		}

		//Source of the conflict
		private object OBJ_source;
		public object Source {
			get{
				if( OBJ_source==null ) {
					throw NullError("Source can not be null!,(Source)",this,typeof(object));
				}
				return OBJ_source;
			}
		}
		public MergeConflict SetSource(object source) {
			OBJ_source=source;
			return this;
		}

		//The Target of the conflict
		private object OBJ_target;
		public object Target { get {
			if(OBJ_target==null) {
				throw NullError("Target can not be null!,(Target)", this, typeof(object));
			}
			return OBJ_target;
		} }
		public MergeConflict SetTarget (object target) {
			OBJ_target=target;
			return this;
		}

		//The Type of the Source
		private Type T_sourceType;
		public Type SourceType { get {
			if(T_sourceType==null) {
				throw NullError("SourceType can not be null!,(SourceType)", this, typeof(Type));
			}
			return T_sourceType;
		} }
		public MergeConflict SetSourceType(Type t){
			T_sourceType=t;
			return this;
		}

		//The Type of the target
		private Type T_targetType;
		public Type TargetType{ get {
			if(T_targetType==null) {
				throw NullError("TargetType an not be null!,(TargetType)", this, typeof(Type));
			}
			return T_targetType;
		} }
		public MergeConflict SetTargetType (Type t) {
			T_targetType=t;
			return this;
		}

		//The Value being set
		private object OBJ_value;
		public object Value { get { return OBJ_value; } }
		public MergeConflict SetValue(object value){
			OBJ_value=value;
			return this;
		}

		//The ValueType
		private Type T_valueType;
		public Type ValueType { get {
			return T_valueType;
		} }
		public MergeConflict SetValueType(Type t) {
			T_valueType=t;
			return this;
		}

		//The Key
		private object OBJ_key;
		public object Key { get { return OBJ_key; } }
		public MergeConflict SetKey(object key) {
			OBJ_key=key;
			return this;
		}

		//They KeyType
		private Type T_keyType;
		public Type KeyType { get { return T_keyType; } }
		public MergeConflict SetKeyType(Type t) {
			T_keyType=t;
			return this;
		}
	}
	
	//Describes MergeConflict
	public sealed class DataType_mergeConflict : DataType {
		#region singleton
		private static volatile DataType_mergeConflict instance=new DataType_mergeConflict();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_mergeConflict ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_mergeConflict();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "mergeConflict"; } }
	}

}