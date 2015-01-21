using MauronAlpha.Geometry.Geometry2d.Shapes;

using MauronAlpha.Events;
using MauronAlpha.Events.Units;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Context;

public class Test {
	
	public static void Main ( ) {
		System.Console.WriteLine("-Program start-");
		
		Test_layoutController layoutController = new Test_layoutController();
		Layout2d_context layoutContext = new Layout2d_context(0,0,40,40,true);
		
		Layout2d_window window = new Layout2d_window("test",layoutController,layoutContext);
		ApplyTestStyle(window);
		System.Console.WriteLine(window.Context.AsString);
		
		System.Console.WriteLine("-Program end-");
		System.Console.ReadKey();
	}

	private static void ApplyTestStyle(Layout2d_window window) {
		Layout2d_container c_title = new Layout2d_container(window);
		
		Layout2d_unitReference reference = c_title.AsReference;
		I_layoutUnit c_stamp = c_title.AsOriginal;
		System.Console.WriteLine(c_stamp.UnitType.AsString);
	}



}
public class Test_layoutController : I_layoutController {

	private EventHandler E_handler;
	public EventHandler EventHandler {
		get {
			if(E_handler == null)
				E_handler = new EventHandler(Clock);
			return E_handler;
		}
	}

	private EventUnit_clock E_clock;
	public EventUnit_clock Clock { 
		get{
			if(E_clock == null)
				E_clock = new EventUnit_clock();
			return E_clock;
		}
	}

}