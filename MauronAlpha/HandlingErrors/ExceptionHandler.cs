using System;

using MauronAlpha.HandlingData;

namespace MauronAlpha.HandlingErrors {
	
	public class ExceptionHandler:MauronCode_dataObject {
		
		//BOOLEAN: Is The Information on an Exception Delayed (Source and ErrorResolution are asynchronous)
		private bool B_isDelayed;
		public bool IsDelayed { get {
			return B_isDelayed;
		} }
		private ExceptionHandler SetIsDelayed(bool b_delayed) {
			B_isDelayed=b_delayed;
			return this;
		}

		//TYPE: baseType
		private Type TYPE_baseType;
		public Type BaseType {
			get {
				if(TYPE_baseType==null) {
					NullError("BaseType can not be null!",this,typeof(object));
				}
				return TYPE_baseType;
			}
		}
		private ExceptionHandler SetBaseType(Type t) {
			TYPE_baseType=t;
			return this;
		}

		//constructor
		public ExceptionHandler(Type t, bool b_delayed):base() {
			SetIsDelayed(b_delayed);
			SetBaseType(t);
		}
	}

	//Description of this item's DataType
	public sealed class DataType_exceptionHandler:DataType {
		#region Singleton Implementation
		private static volatile DataType_exceptionHandler instance=new DataType_exceptionHandler();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_exceptionHandler ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_exceptionHandler();
					}
				}
				return instance;
			}
		}
		#endregion
	
		//The name
		private string STR_name = "exceptionHandler";
		public override string Name { get {	return STR_name; } }

		#region Optional Inheritance of DataType
		/*
		//Can the item be set to ReadOnly (default: false)
		public override bool IsProtectable {
			get {
				return false;
			}
		}

		//optional converters
		public override bool IsConvertibleTo (Type t) {
			return false;
		}
		public override T Convert<T> (MauronCode_dataObject obj) {
			Error("Can not convert dataObject!,(Covert<T>)", this, ErrorType_fatal.Instance);
			return default(T);
		}
		 * */
		#endregion
	}

}
