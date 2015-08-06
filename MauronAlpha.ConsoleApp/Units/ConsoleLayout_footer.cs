using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.ConsoleApp.Units
{
    public class ConsoleLayout_footer : ConsoleApp_unit
    {

        //constructor
        public ConsoleLayout_footer() : base() { }
        public ConsoleLayout_footer(Vector2d position, Vector2d size)
            : base(position, size)
        {
            FORM_content.SetHeight(1);
        }

    }
}
