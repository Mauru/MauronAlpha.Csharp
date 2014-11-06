using MauronAlpha;
using MauronAlpha.ExplainingCode;
using MauronAlpha.Hardware;

using System;
using System.Collections.Generic;

namespace MauronAlpha.Devices {

	public abstract class Computer:HardwareComponent_computer {
		public OS OS;
		public HardwareComponent[] Hardware;
	}

    public class PC:Computer {
    }

	public class HardwareManager : MauronCode_singleton {
		#region Singleton
		private static volatile HardwareManager instance=new HardwareManager();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static HardwareManager ( ) { }
		public static HardwareManager Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance = new HardwareManager();
					}
				}
				return instance;
			}
		}
		#endregion
		
		public Stack<Device> Devices=new Stack<Device>();

	}

}
