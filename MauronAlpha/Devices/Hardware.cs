using System;

namespace MauronAlpha.Hardware {

	//A piece of hardware
    public abstract class HardwareComponent:Definition {
		public HardwareType HardwareType;
		public HardwareComponent(HardwareType hardwaretype){
			HardwareType=hardwaretype;
		}
    }

	public class HardwareComponent_computer:HardwareComponent {
		public HardwareComponent_computer():base(HardwareType_computer.Instance){}
	}
	public class HardwareComponent_display : HardwareComponent {
		public HardwareComponent_display ( ) : base(HardwareType_display.Instance) { }
	}
	public class HardwareComponent_graphics : HardwareComponent {
		public HardwareComponent_graphics ( ) : base(HardwareType_graphics.Instance) { }
	}
	public class HardwareComponent_sound : HardwareComponent {
		public HardwareComponent_sound ( ) : base(HardwareType_sound.Instance) { }
	}


	public abstract class HardwareType:MauronCode_subtype {
		public abstract string Name {get;}
	}

	public sealed class HardwareType_computer:HardwareType {
		#region Singleton
		private static volatile HardwareType_computer instance=new HardwareType_computer();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static HardwareType_computer ( ) { }
		public static HardwareType Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new HardwareType_computer();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name {
			get { return "computer"; }
		}
	}

	public sealed class HardwareType_display : HardwareType {
		#region Singleton
		private static volatile HardwareType_display instance=new HardwareType_display();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static HardwareType_display ( ) { }
		public static HardwareType Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new HardwareType_display();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name {
			get { return "display"; }
		}
	}

	public sealed class HardwareType_graphics : HardwareType {
		#region Singleton
		private static volatile HardwareType_graphics instance=new HardwareType_graphics();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static HardwareType_graphics ( ) { }
		public static HardwareType Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new HardwareType_graphics();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name {
			get { return "graphics"; }
		}
	}

	public sealed class HardwareType_sound : HardwareType {
		#region Singleton
		private static volatile HardwareType_sound instance=new HardwareType_sound();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static HardwareType_sound ( ) { }
		public static HardwareType Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new HardwareType_sound();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name {
			get { return "sound"; }
		}	
	}


}
