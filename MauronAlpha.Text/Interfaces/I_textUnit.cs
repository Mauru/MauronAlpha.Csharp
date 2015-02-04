using System;
using System.Collections.Generic;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Interfaces {
	
	//Interface for TextUnits	
	public interface I_textUnit:
	IEquatable<I_textUnit>,
	I_protectable<I_textUnit> {

		I_textUnitType UnitType { get; }

		TextContext Context { get; }

		string AsString { get; }

		bool IsEmpty { get; }
		bool IsParent { get; }
		bool IsChild { get; }
		bool CanHaveChildren { get; }
		bool CanHaveParent { get; }
		
		MauronCode_dataList<I_textUnit> Children { get; }
		MauronCode_dataIndex<I_textUnit> Neighbors { get; }

		I_textUnit Parent { get; }

		I_textEncoding Encoding { get; }

		TextUnit_character FirstCharacter { get; }
		TextUnit_character LastCharacter { get; }

	}


}
