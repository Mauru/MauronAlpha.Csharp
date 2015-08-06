using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.ConsoleApp.Units
{
    public class ConsoleLayout_header : ConsoleApp_unit
    {

        //constructor
        public ConsoleLayout_header() : base() { }
        public ConsoleLayout_header(Vector2d position, Vector2d size)
            : base(position, size)
        {
            FORM_content.SetHeight(1);
        }

    }
}
