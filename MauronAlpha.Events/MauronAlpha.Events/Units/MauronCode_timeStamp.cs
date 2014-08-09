using MauronAlpha.HandlingData;
using System;

namespace MauronAlpha.Events.Units {
	
	//A TimeStamp of an Object
	public class MauronCode_timeStamp:MauronCode_dataObject {

		//the time when this unit was created
		private MauronCode_timeUnit TIME_created;
		public MauronCode_timeUnit Created {
			get {
				if(TIME_created==null){
					Error("TIME_created can not be null!",this);
				}
				return TIME_created;
			}
		}
		public MauronCode_timeStamp SetCreated (MauronCode_timeUnit time) {
			TIME_created=time;
			return this;
		}

		//the time when this unit was last updated
		private MauronCode_timeUnit TIME_updated;
		public MauronCode_timeUnit Updated {
			get {
				if( TIME_updated==null ) {
					SetUpdated(Created.Instance);
				}
				return TIME_updated;
			}
		}
		public MauronCode_timeStamp SetUpdated (MauronCode_timeUnit time) {
			TIME_updated=time;
			return this;
		}

		//constructor
		public MauronCode_timeStamp():base(DataType_timeStamp.Instance){}
	}

	//A description of the dataType
	public sealed class DataType_timeStamp:DataType {
		#region singleton
		private static volatile DataType_timeStamp instance=new DataType_timeStamp();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_timeStamp ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_timeStamp();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "timeStamp"; } }
		
	}

}