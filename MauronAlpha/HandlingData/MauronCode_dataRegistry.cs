using System;
using System.Collections.Generic;

namespace MauronAlpha.HandlingData {

	//Supposed to become a MauronCode_dataMap<MauronCode_dataList<T>>
	public class MauronCode_dataRegistry<T>:MauronCode_dataObject {

		//constructor
		public MauronCode_dataRegistry():base(DataType_dataRegistry.Instance) {}



	}

	//A description of the dataType
	public sealed class DataType_dataRegistry : DataType {
		#region singleton
		private static volatile DataType_dataRegistry instance=new DataType_dataRegistry();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_dataRegistry ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dataRegistry();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataRegistry"; } }

	}

}