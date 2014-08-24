using System;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//Defines the Behavior of the Data stored in a DataObject
	public abstract class DataType:MauronCode_subtype {
		//The name
		public abstract string Name { get; }

		//Can the item be set to ReadOnly (default: false)
		public virtual bool IsProtectable { get {
			return false;
		} }
		
		//optional converters
		public virtual bool IsConvertibleTo(Type t){
			return false;
		}
		public virtual T Convert<T>(MauronCode_dataObject obj){
			Error("Can not convert dataObject!,(Covert<T>)",this,ErrorType_fatal.Instance);
			return default(T);
		}
	}

	//A datatype that holds plain data in either binary or string form
	public sealed class DataType_plain:DataType {
		#region singleton
		private static volatile DataType_plain instance=new DataType_plain();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_plain ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_plain();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "plain"; } }

	}

	//A datatype that holds data in a list or dictionary and offers manipulation functions
	public sealed class DataType_object : DataType {
		#region singleton
		private static volatile DataType_object instance=new DataType_object();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_object ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_object();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "object"; } }

	}

	//A datatype that creates direct clones of its children on instance
	public sealed class DataType_cloning : DataType {
		#region singleton
		private static volatile DataType_cloning instance=new DataType_cloning();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_cloning ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_cloning();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "cloning"; } }

	}

	//A datatype that stores links to other objects and may manipulate its children
	public sealed class DataType_maintaining : DataType {
		#region singleton
		private static volatile DataType_maintaining instance=new DataType_maintaining();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_maintaining ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_maintaining();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "maintaining"; } }

	}

	//A datatype that returns values only and allows no manipulation of its children
	public sealed class DataType_dispenser : DataType {
		#region singleton
		private static volatile DataType_dispenser instance=new DataType_dispenser();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_dispenser ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dispenser();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dispenser"; } }

	}

	//A dataType that accepts values only returning no actual internals except gating error messages (A Gatekeeper)
	public sealed class DataType_inbox : DataType {
		#region singleton
		private static volatile DataType_inbox instance=new DataType_inbox();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_inbox ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_inbox();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "secure"; } }

	}

	//A DataType that does not accept or return any Data - unless it is unlocked
	public sealed class DataType_locked: DataType {
		#region singleton
		private static volatile DataType_locked instance=new DataType_locked();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_locked ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_locked();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "locked"; } }
	}

	//A DataType that starts unlocked but can be locked
	public sealed class DataType_unlocked : DataType {
		#region singleton
		private static volatile DataType_unlocked instance=new DataType_unlocked();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_unlocked ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_unlocked();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "unlocked"; } }
	}

	//Theoretical - A datatype that is completely isolated - it can not be interacted with and all we know is that it is there
	public sealed class DataType_isolated : DataType {
		#region singleton
		private static volatile DataType_isolated instance=new DataType_isolated();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_isolated ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_isolated();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "isolated"; } }
	}

}