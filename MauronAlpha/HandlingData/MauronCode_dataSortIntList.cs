using System;
using System.Collections.Generic;

namespace MauronAlpha.HandlingData {

	//Supposed to become a MauronCode_dataIndex<MauronCode_dataList<T>>
	public class MauronCode_dataSortIntList<T_value>:MauronCode_dataObject, IEnumerable<MauronCode_dataList<T_value>> {

		//constructor
		public MauronCode_dataSortIntList( ) : base() {}

		private MauronCode_dataIndex<MauronCode_dataList<T_value>> DATA_index = new MauronCode_dataIndex<MauronCode_dataList<T_value>>();

		public MauronCode_dataSortIntList<T_value> SetValue( int key, T_value value ) {
			if( !ContainsKey( key ) ) {
				DATA_index.SetValue( key, new MauronCode_dataList<T_value>() );
			}

			MauronCode_dataList<T_value> map = DATA_index.Value(key);
			map.AddValue( value );

			return this;
		}

		public bool ContainsKey( int key ) {
			return DATA_index.ContainsKey( key );
		}

		public MauronCode_dataList<T_value> Values( int key ) {
			if( !ContainsKey( key ) )
				return new MauronCode_dataList<T_value>();
			return DATA_index.Value( key );
		}

		public long Count {
			get {
				return DATA_index.CountKeys;
			}
		}

		public long CountValidKeys {
			get {
				return DATA_index.CountValidKeys;
			}
		}

		public MauronCode_dataList<long> KeysAsList {
			get {
				return DATA_index.KeysAsList;
			}
		}

		public ICollection<long> Keys {
			get {
				return DATA_index.Keys;
			}
		}

		public ICollection<long> ValidKeys {
			get {
				return DATA_index.ValidKeys;
			}
		}





		#region IEnumerable<MauronCode_dataList<T_value>> Members

		public IEnumerator<MauronCode_dataList<T_value>> GetEnumerator( ) {
			return new ENUMERATE_MauronCode_dataSortIntList<T_value>( this );
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator( ) {
			return new ENUMERATE_MauronCode_dataSortIntList<T_value>( this );
		}

		#endregion
	}

	//An Enumerator for the MauronCode_dataSortListInt dataObject - foreach VALUELIST in mauronCode_dataIndex of object
	public class ENUMERATE_MauronCode_dataSortIntList<T_value>:MauronCode_dataObject,IEnumerator<MauronCode_dataList<T_value>> {

		//constructor
		public ENUMERATE_MauronCode_dataSortIntList( MauronCode_dataSortIntList<T_value> map ): base() {
			baseObject = map;
			baseValidKeys = new MauronCode_dataList<long>(map.ValidKeys);
			baseCount = map.CountValidKeys;
		}

		//properties
		private  MauronCode_dataSortIntList<T_value> baseObject;
		private MauronCode_dataList<long> baseValidKeys;
		private long baseCount = 0;
		private int currentIndex = -1;

		#region IEnumerator<MauronCode_dataList<T_value>> Members

		public MauronCode_dataList<T_value> Current {
			get {
				return baseObject.Values( MapIndex(currentIndex) );
			}
		}

		#endregion

		//get an actual cross reference to the keys : //WARNING : Converts LONG to int without range check! This could cause an error if index is too long
		private int MapIndex( int index ) {
			return ( int ) baseValidKeys.Value( index );
		}


		#region IDisposable Members

		public void Dispose( ) {
			baseObject = null;
			baseCount = 0;
			baseValidKeys = null;
		}

		#endregion

		#region IEnumerator Members

		object System.Collections.IEnumerator.Current {
			get {
				return baseObject.Values( MapIndex( currentIndex ) );
			}
		}

		public bool MoveNext( ) {
			currentIndex++;
			return ( currentIndex < baseObject.Count );
		}

		public void Reset( ) {
			currentIndex = -1;
		}

		#endregion
	}

	//A description of the dataType
	public sealed class DataType_dataRegistry : DataType {
		#region singleton
		private static volatile DataType_dataRegistry instance=new DataType_dataRegistry();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_dataRegistry ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dataRegistry();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataRegistry"; } }

	}

	//A description of the dataType
	public sealed class DataType_enumerator : DataType {
		#region singleton
		private static volatile DataType_enumerator instance=new DataType_enumerator();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_enumerator( ) {
		}
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_enumerator();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get {
				return "enumerator";
			}
		}

	}

}