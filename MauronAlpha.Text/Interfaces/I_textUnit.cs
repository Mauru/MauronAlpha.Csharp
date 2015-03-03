using System;
using System.Collections.Generic;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingData;

using MauronAlpha.Text.Units;
using MauronAlpha.Text.Collections;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Interfaces {
	
	//Interface for TextUnits	
	public interface I_textUnit:
	IEquatable<I_textUnit>,
	I_protectable<I_textUnit> {

		I_textUnitType UnitType { get; }

		TextContext Context { get; }
		TextContext CountAsContext { get; }

		string Id { get; }
		string AsString { get; }

		int ChildCount { get; }
		int Index { get; }

		bool IsEmpty { get; }
		bool IsParent { get; }
		bool IsChild { get; }
		bool IsFull { get; }
		bool CanHaveChildren { get; }
		bool CanHaveParent { get; }
		bool EndsParent { get; }

		
		MauronCode_dataList<I_textUnit> Children { get; }
		TextUnitNeighbors Neighbors { get; }

		I_textUnit Parent { get; }
		I_textUnit SetContext (TextContext context);
		I_textUnit UpdateContext( bool updateChildren );
		I_textUnit HandleEndAtIndex( int index );

		I_textEncoding Encoding { get; }

		TextUnit_character FirstCharacter { get; }
		TextUnit_character LastCharacter { get; }

		I_textUnit InsertChildAtIndex( int n, I_textUnit unit, bool updateParent, bool updateChildren );
		I_textUnit RemoveChildAtIndex( int n, bool updateParent, bool updateChildren );

	}


}
