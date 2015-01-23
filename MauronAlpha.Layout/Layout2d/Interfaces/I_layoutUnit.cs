using System;
using MauronAlpha.Interfaces;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Collections;
using MauronAlpha.Layout.Layout2d.Context;

namespace MauronAlpha.Layout.Layout2d.Interfaces {
    
    public interface I_layoutUnit:IEquatable<I_layoutUnit> {
        I_eventHandler EventHandler { get; }

        bool CanHaveChildren { get; }
        bool CanHaveParent { get; }
        bool IsParent { get; }
        bool IsChild { get; }
        bool HasParent { get; }
        bool HasChildren { get; }

        I_layoutUnit Parent { get; }
        I_layoutUnit ChildByIndex(int index);

        I_layoutUnit AddChildAtIndex(int index, I_layoutUnit unit);

        Layout2d_unitCollection Children { get; }

        Layout2d_context Context { get; }

		Layout2d_unitType UnitType { get; }

    }

}
