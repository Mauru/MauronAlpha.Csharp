using System;
using MauronAlpha.Interfaces;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Collections;
using MauronAlpha.Layout.Layout2d.Context;

namespace MauronAlpha.Layout.Layout2d.Interfaces {
    
	//Base definition for Layout Units
    public interface I_layoutUnit : IEquatable<I_layoutUnit>,
	I_protectable<I_layoutUnit> {

        I_eventHandler EventHandler { get; }

        bool CanHaveChildren { get; }
        bool CanHaveParent { get; }
        bool IsParent { get; }
        bool IsChild { get; }
        bool HasParent { get; }
        bool HasChildren { get; }

        I_layoutUnit Parent { get; }
        I_layoutUnit ChildByIndex(long index);
		I_layoutUnit AddChildAtIndex (long index, I_layoutUnit unit, bool updateRelations);
		
		long NextChildIndex { get; }



		I_layoutUnit SetParent( I_layoutUnit parent, bool updateRelations );
		I_layoutUnit SetContext( Layout2d_context context );
		I_layoutUnit SetEventHandler( I_eventHandler handler );

		Layout2d_unitCollection Children { get;	}

		Layout2d_unitType UnitType { get; }

		Layout2d_context Context { get;	}

    }

}
