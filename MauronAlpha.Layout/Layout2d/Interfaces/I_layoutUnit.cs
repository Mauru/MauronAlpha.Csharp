using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Collections;
using MauronAlpha.Layout.Layout2d.Context;

namespace MauronAlpha.Layout.Layout2d.Interfaces {
    
    public interface I_layoutUnit {
        Layout2d_unitReference AsReference { get; }
        I_eventHandler EventHandler { get; }

        bool Exists { get; }
        bool CanHaveChildren { get; }
        bool CanHaveParent { get; }
        bool IsDynamic { get; }
        bool IsParent { get; }
        bool IsChild { get; }
        bool IsStatic { get; }
        bool HasParent { get; }
        bool HasChildren { get; }
		bool IsReference { get; }

        Layout2d_unitReference Parent { get; }
        Layout2d_unitReference ChildByIndex(int index);

        I_layoutUnit AddChildAtIndex(int index, I_layoutUnit unit);
		I_layoutUnit AsOriginal { get; }

        Layout2d_unitCollection Children { get; }

        Layout2d_context Context { get; }

		Layout2d_unitType UnitType { get; }

    }

}
