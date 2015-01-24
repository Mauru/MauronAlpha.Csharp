using System;
using System.Collections.Generic;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Collections {
	
	//A Collection of Layout Units
	public class Layout2d_unitCollection : Layout2d_component,
	I_protectable,
	IEquatable<Layout2d_unitCollection>,
	I_instantiable<Layout2d_unitCollection> {
		
	//Constructor
	public Layout2d_unitCollection():base() {
		DATA_units = new MauronCode_dataIndex<I_layoutUnit>();
	}
	private Layout2d_unitCollection(Layout2d_unitCollection original):base() {
		Layout2d_unitCollection instance = new Layout2d_unitCollection();
		ICollection<long> keys = DATA_units.ValidKeys;
		foreach(long key in keys) {
			if(DATA_units.ContainsValueAtKey(key)){ 
				I_layoutUnit unit = DATA_units.Value(key);
				instance.RegisterUnitAtIndex(key,unit);
			}
		}
	}

	//Properties
	private MauronCode_dataIndex<I_layoutUnit> DATA_units;
	public MauronCode_dataList<long> Keys {
		get {
			return DATA_units.KeysAsList;
		}
	}
	public long Count {
		get {
			return DATA_units.CountValidValues;
		}
	}

	//Queries
	public I_layoutUnit UnitByIndex (long index) {
		return DATA_units.Value(index);
	}

	//Booleans
	public bool Equals(Layout2d_unitCollection other) {
		if(IsEmpty!=other.IsEmpty)
			return false;
		if(IsEmpty == true && other.IsEmpty == true)
			return true;
		if(!Count.Equals(other.Count))
			return false;			
		MauronCode_dataList<long> keys = Keys;
		if(!other.Keys.Equals(keys))
			return false;
		foreach(long key in keys) {
			I_layoutUnit unit =  UnitByIndex(key);
			if(!unit.Equals(other.UnitByIndex(key) ))
				return false;
		}
		return true;
	}
	public bool IsEmpty { 
		get {
			if(DATA_units.IsEmpty)
				return true;
			return false;
		}
	}
	public bool ContainsIndex(long index) {
		if(DATA_units == null)
			return false;
		return DATA_units.ContainsValueAtKey(index);
	}
	private bool B_isReadOnly = false;
	public bool IsReadOnly {
		get { return B_isReadOnly; }
	}

	//Methods
	public Layout2d_unitCollection Instance {
		get {
			return new Layout2d_unitCollection(this);
		}
	}
	public Layout2d_unitCollection RegisterUnitAtIndex (long index, I_layoutUnit unit) {
		if( IsReadOnly )
			throw Error("Index is protected!,(RegisterUnitAtIndex)", this, ErrorType_protected.Instance);

		if( index<0 )
			throw Error("Index out of Bounds!,(RegisterUnitAtIndex)", this, ErrorType_bounds.Instance);

		if( index>DATA_units.CountKeys )
			throw Error("Index out of Bounds!,(RegisterUnitAtIndex)", this, ErrorType_bounds.Instance);

		if( DATA_units.ContainsKey(index) )
			Exception("Index is in Use!,{"+index+"},(RegisterUnitAtIndex)", this, ErrorResolution.Replaced);
			
		DATA_units.SetValue(index, unit);
		return this;
	}
	public Layout2d_unitCollection SetIsReadOnly (bool status) {
		B_isReadOnly=status;
		return this;
	}

	}
}