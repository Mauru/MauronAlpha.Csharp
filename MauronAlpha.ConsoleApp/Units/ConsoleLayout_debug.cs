using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.ConsoleApp.Collections;
using MauronAlpha.ConsoleApp.Interfaces;

namespace MauronAlpha.ConsoleApp.Units {

	public class ConsoleLayout_debug : ConsoleApp_unit {
		//constructor
        public ConsoleLayout_debug() : base() { }
		public ConsoleLayout_debug(Vector2d position, Vector2d size) : base(position, size)
        {
            FORM_content.SetHeight(1);
        }
	}
}
