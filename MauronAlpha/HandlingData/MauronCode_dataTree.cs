using System;
using System.Collections;
using System.Collections.Generic;


using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//An associative array with a fixed size, tracks if values have been set or not
	public class MauronCode_dataTree<TKey, TValue> : MauronCode_dataObject,
	IEnumerable<TValue>,
	I_instantiable {

		//Constructors
		public MauronCode_dataTree() : base() {
			DATA_keys=new TKey[]{};
			DATA_values=new TValue[]{};
			DATA_wasSet=new bool[]{};
		}
		public MauronCode_dataTree(ICollection<TKey> keys):this() {
			//initialize keys
			DATA_keys=new TKey[keys.Count];

            keys.CopyTo(DATA_keys, 0);
			
			if(keys.Count>0) {
				//we need to check if the keys are unique unfortunately
				//TODO("Finish each on each check");
				DelegateHandler_equals<TKey> D = new DelegateHandler_equals<TKey>();
				long index_compare=0;
				if( ForEachKeys_bool(DATA_keys, D.D_objectEquals,1,index_compare) ) {
					throw Error("Duplicate Keys Detected!,{"+index_compare+"|"+DATA_keys.Length+"},(Constructor)",this,ErrorType_constructor.Instance);
				}


				//initialize empty values
				DATA_values=new TValue[keys.Count];

				//initialize the boolean set counter
				DATA_wasSet=new bool[keys.Count];
			}

		}
		public MauronCode_dataTree(ICollection<TKey> keys, ICollection<TValue> values):this(keys){
			if(keys.Count!=values.Count){
				throw Error("Keys/Values have different length!,(Constructor)",this,ErrorType_constructor.Instance);
			}

			TKey[] v_key = new TKey[keys.Count];
			keys.CopyTo(v_key, 0);

			TValue[] v_value = new TValue[keys.Count];
			values.CopyTo(v_value,0);

			for(long n=0; n<values.Count ; n++) {
				SetValue(v_key[n],v_value[n]);
			}
		}

		//Branches (Keys)
		private TKey[] DATA_keys;
		//Leaves (Values)
		private TValue[] DATA_values;
		//Tracks if each value was set
		private bool[] DATA_wasSet;
		
		//Booleans
		private bool IndexIsSet(long index){
			if(index<0||index>=Count) {
				Exception("Index out of Bounds!,{"+index+"}",this,ErrorResolution.ExpectedReturn);
				return false;
			}
			return DATA_wasSet[index];
		}
		//Is The item valid
		public bool IsValid {
			get {
				return DATA_keys != null 
				&& DATA_wasSet != null
				&& DATA_keys.Length == DATA_wasSet.Length;
			}
		}
		private bool B_isReadOnly = false;
		public bool IsReadOnly { 
			get { 
				return B_isReadOnly;
			}
		}

		/// <summary>
		/// Checks
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public bool ContainsValueAtIndex(long index) {
			return IndexIsSet(index);
		}
		public bool ContainsIndex(long n) {
			return (n>-1&&n<DATA_keys.Length);
		}
		public bool IsSet(TKey key){
			return (ContainsKey(key) && DATA_wasSet[IndexByKey(key)]==true);
		}

		//Does Data contain a Key
		public bool ContainsKey(TKey key) {
			DelegateHandler_equals<TKey> D=new DelegateHandler_equals<TKey>();
			if( ForEachKeys_compare_bool(DATA_keys, key, D.D_objectEquals, 1) ) {
				return true;
			}
			return false;
		}				

		#region Delegate Functions
		///<summary>Cycle through each component of a array and test tem against each other</summary>
		///<remarks>Does not test against itself.</remarks>
		///<param name="limitTrue">0: Test for all, >0: Test until true has been returned n times</param>
		private bool ForEachKeys_bool(TKey[] keys, DelegateMethod_objectInteraction_bool<TKey> comparer, long limitTrue) {
			TKey origin;
			TKey target;
			long countTrue = 0;
			for(long i=0; i<keys.Length; i++) {
				origin = keys[i];
				for(long b=0; b<keys.Length; b++) {
					if(i!=b){
						target=keys[b];
						if(comparer(origin,target)) {
							countTrue++;
							if(limitTrue>0&&countTrue>=limitTrue) {
								return true;
							}
						}
					}
				}
			}
			return false;
		}
		private bool ForEachKeys_bool (TKey[] keys, DelegateMethod_objectInteraction_bool<TKey> comparer, long limitTrue, long store_lastIndex) {
			TKey origin;
			TKey target;
			long countTrue=0;
			for( long i=0; i<keys.Length; i++ ) {
				store_lastIndex = i;
				origin=keys[i];
				for( long b=0; b<keys.Length; b++ ) {
					if( i!=b ) {
						target=keys[b];
						if( comparer(origin, target) ) {
							countTrue++;
							if( limitTrue>0&&countTrue>=limitTrue ) {
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		///<summary>Cycle through each component of a array and test against target</summary>
		///<param name="limitTrue">0: Test for all, >0: Test until true has been returned n times</param>
		private bool ForEachKeys_compare_bool(TKey[] keys, TKey target, DelegateMethod_objectInteraction_bool<TKey> comparer, long limitTrue) {
			TKey origin;
			long countTrue=0;
			for( long i=0; i<keys.Length; i++ ) {
				origin=keys[i];
				if( comparer(origin, target) ) {
					countTrue++;
					if( limitTrue>0&&countTrue>=limitTrue ) {
						return true;
					}
				}
			}
			return false;
		}

		///<summary>Cycle through each component of a array and test against target</summary>
		///<param name="limitTrue">0: Test for all, >0: Test until true has been returned n times</param>
		private TValue[] ForEachValues_compare_return(TValue[] values, TValue target, DelegateMethod_objectInteraction_bool<TValue> comparer, long limitTrue){
			TValue[] result=new TValue[0];
			if(CountValidValues==0){
				return result;
			}
			for(long n=0; n<CountKeys; n++){
				if(IndexIsSet(n)){
					TValue candidate = DATA_values[n];
					if(comparer(candidate,target)) {
						TValue[] tmp_result=new TValue[result.Length];
						result.CopyTo(tmp_result,0);
						tmp_result[tmp_result.Length-1]=candidate;
						result=tmp_result;
					}
					if(limitTrue>0&&result.Length>=limitTrue){
						return result;
					}
				}
			}
			return result;
		}
		private long[] ForEachValues_compare_returnIndex(TValue[] values, TValue target, DelegateMethod_objectInteraction_bool<TValue> comparer, long limitTrue) {
			long[] result=new long[0];
			for(long index=0;index<Count;index++){
				if(IndexIsSet(index)) {
					TValue candidate = DATA_values[index];
					if(comparer(candidate,target)){
						long newIndex=result.Length;
						long[] tmp_result = new long[newIndex];
						result.CopyTo(tmp_result,0);
						tmp_result[newIndex]=index;
						result=tmp_result;
					}
					if(limitTrue>0&&result.Length>=limitTrue){
						return result;
					}
				}
			}
			return result;
		}

		//Delegate Handler : Generates Generic (<...>) Delegate functions: (Equals)
		private DelegateHandler_equals<TKey> DelegateKeyHandler {
			get {
				return new DelegateHandler_equals<TKey>();
			}
		}
		private DelegateHandler_equals<long> DelegateIndexHandler {
			get {
				return new DelegateHandler_equals<long>();
			}
		}
		private DelegateHandler_equals<TValue> DelegateValueHandler {
			get {
				return new DelegateHandler_equals<TValue>();
			}
		}

		//Internal class for a DelegateHandler
		private class DelegateHandler_equals<varType> {

			//keep track of the last used value
			private varType VAR_lastOrigin;
			public varType LastOrigin {
				get {
					return VAR_lastOrigin;
				}
			}

			//Perform "Equals()" Comparison for two objects
			public bool DEL_objectEquals(varType origin, varType target) {
				VAR_lastOrigin=origin;
				if(target == null && origin == null)
					return true;
				if(target == null || origin == null)
					return false;
				return origin.Equals(target);
			}

			//Return DEL_objectEquals as DelegateMethod
			public DelegateMethod_objectInteraction_bool<varType> D_objectEquals {
				get {
					return DEL_objectEquals;
				}
			}

		}
		
		//Delegate: Perform an action on each key @return: bool
		public delegate bool DelegateMethod_collection_bool<varType> (varType[] keys);
		//Delegate: Perform an action with two elements of the same type
		public delegate bool DelegateMethod_objectInteraction_bool<varType> (varType origin, varType target);
		#endregion

		//Indexers/Counters (long)
		/// <summary>
		/// Returns the number of KEYS in the dataTree
		/// <remarks>Note that this does not mean valid values!</remarks>
		/// </summary>
		public long Count { 
			get { 
				return DATA_keys.Length;
			} 
		}
		public long CountKeys {
			get {
				return DATA_keys.Length;
			}
		}
		public long CountValidKeys {
			get {
				long result=0;
				for( long index=0; index<Keys.Count; index++ ) {
					if( IndexIsSet(index) ) {
						result++;
					}
				}
				return result;
			}
		}
		public long CountInvalidKeys {
			get {
				long result=0;
				for( long index=0; index<Keys.Count; index++ ) {
					if( !IndexIsSet(index) ) {
						result++;
					}
				}
				return result;
			}
		}
		public long CountValidValues {
			get {
				long result=0;
				for( long index=0; index<Count; index++ ) {
					if( IndexIsSet(index) ) {
						result++;
					}
				}
				return result;
			}
		}
		public long IndexByKey(TKey key) {
			long n = -1;
			foreach (TKey k in DATA_keys) {
				n++;
				if (k.Equals(key))
					return n;
			}
			return -1;
		}

		//Keys
		public MauronCode_dataList<TKey> Keys {
			get {
				return new MauronCode_dataList<TKey>(DATA_keys).SetIsReadOnly(true);
			}
		}
		public ICollection<TKey> ValidKeys {
			get {
				TKey[] result=new TKey[CountValidKeys];
				for( long index=0; index<Count; index++ ) {
					if( IndexIsSet(index) ) {
						long newIndex=result.Length;
						result[newIndex]=DATA_keys[index];
					}
				}
				return result;
			}
		}
		public ICollection<TKey> InvalidKeys {
			get {
				TKey[] result=new TKey[CountValidKeys];
				for( long index=0; index<Count; index++ ) {
					if( !IndexIsSet(index) ) {
						long newIndex = result.Length;
						result[newIndex] = DATA_keys[index];
					}
				}
				return result;
			}
		}
		public TKey KeyByIndex (long index) {
			if( !ContainsIndex(index) ) {
				throw Error("Index out of Bounds!,{"+index+"},(KeyByIndex)", this, ErrorType_bounds.Instance);
			}
			return DATA_keys[index];
		}

		//Values
		public MauronCode_dataIndex<TValue> Values {
			get {
				MauronCode_dataIndex<TValue> result=new MauronCode_dataIndex<TValue>();
				for(long n = 0; n < DATA_values.Length; n++) {
					if(IndexIsSet(n))
						result.SetValue(n,DATA_values[n]);
				}
				return result;
			}
		}
		public MauronCode_dataList<TValue> ValuesAsList {
			get {
				MauronCode_dataList<TValue> result = new MauronCode_dataList<TValue>();
				for( long n=0; n<DATA_values.Length; n++ ) {
					if( IndexIsSet(n) )
						result.AddValue(DATA_values[n]);
				}
				return result;
			}
		}
		public ICollection<TValue> ValidValues {
			get {
				TValue[] result=new TValue[CountValidValues];
				for( long index=0; index<Count; index++ ) {
					if( IndexIsSet(index) ) {
						long newIndex=result.Length;
						result[newIndex]=DATA_values[index];
					}
				}
				return result;
			}
		}
		public bool TryGet(TKey key, ref TValue val) {
			long index = IndexByKey(key);
			if (index < 0)
				return false;
			val = DATA_values[index];
			return true;
		}

		//Methods
		public MauronCode_dataTree<TKey, TValue> Instance {
			get {
				MauronCode_dataTree<TKey, TValue> instance=new MauronCode_dataTree<TKey, TValue>(Keys);
				foreach( TKey key in ValidKeys ) {
					if( IsSet(key) )
						instance.SetValue(key, Value(key));
				}
				return Instance;
			}
		}
		public MauronCode_dataTree<TKey, TValue> SetIsReadOnly (bool state) {
			B_isReadOnly=state;
			return this;
		}
		//Modify: Add
		public MauronCode_dataTree<TKey, TValue> SetValue (TKey key, TValue value) {
			if(IsReadOnly)
				throw Error("Is protected!,(SetValue)",this, ErrorType_protected.Instance);

			long index = IndexByKey( key );

			if( index < 0 )
				throw Error("Index/Key not initialized!,{"+index+"},(SetValue)", this, ErrorType_index.Instance);

			DATA_values[index] = value;
			DATA_wasSet[index] = true;

			return this;
		}
		public MauronCode_dataTree<TKey, TValue> SetValue (TKey key, TValue value, bool b_generateKey) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetValue)", this, ErrorType_protected.Instance);

			long index=IndexByKey(key);

			if( index<0 ) {
				if( b_generateKey ) {
					AddKey(key);
					index = IndexByKey(key);
				} else
					throw Error("Invalid Index!,{"+index+"},(SetValue)", this, ErrorType_index.Instance);
			}
			DATA_values[index]=value;
			DATA_wasSet[index]=true;

			return this;
		}
		public MauronCode_dataTree<TKey, TValue> SetValues(ICollection<TKey> keys, ICollection<TValue> values) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetValues)", this, ErrorType_protected.Instance);

			long length = values.Count;

			if( length!=Count )
				throw Error("Values must be same length as keys!,(SetValues)",this,ErrorType_bounds.Instance);

			TValue[] arr_values = new TValue[length-1];
			values.CopyTo(arr_values,0);

			for( long n=0; n<length; n++ ) {
				DATA_values[n]=arr_values[n];
				DATA_wasSet[n]=true;
			}
			return this;
		}
		//Add a key, set it to empty
		public MauronCode_dataTree<TKey, TValue> AddKey (TKey key) {
			if( IsReadOnly )
				throw Error("Is protected!,(AddKey)", this, ErrorType_protected.Instance);
			if( ContainsKey(key) )
				throw Error("Key is allready set!,(AddKey)", this, ErrorType_protected.Instance);

			//Create instance of DataSet with new Length
			long newIndex=Count;
			TKey[] tmp_keys=new TKey[newIndex+1];
			TValue[] tmp_values=new TValue[newIndex+1];
			bool[] tmp_wasSet=new bool[newIndex+1];

			for( long n=0; n<newIndex; n++ ) {
				tmp_keys[n]=DATA_keys[n];
				if( IndexIsSet(n) )
					tmp_values[n]=DATA_values[n];
				tmp_wasSet[n]=DATA_wasSet[n];
			}

			//Insert new Key
			tmp_keys[newIndex]=key;
			tmp_wasSet[newIndex]=false;

			DATA_keys=tmp_keys;
			DATA_values=tmp_values;
			DATA_wasSet=tmp_wasSet;

			return this;
		}
		
		//Modify: Remove
		//Unset value by Key
		public MauronCode_dataTree<TKey, TValue> UnsetByKey (TKey key) {
			if( !IsSet(key) ) {
				Exception("Value not set!,(UnsetValue)", this, ErrorResolution.DoNothing);
				return this;
			}
			long index=IndexByKey(key);
			RemoveDataByIndex(index, false);
			return this;
		}
		//Actively remove a data Entry from the DataSets, either completely or only the value
		private MauronCode_dataTree<TKey, TValue> RemoveDataByIndex (long index, bool b_removeKey) {

			//Error Check
			if( !ContainsIndex(index) ) {
				throw Error("Index out of Bounds!,{"+index+"}", this, ErrorType_bounds.Instance);
			}

			//Form temporary Data Sets
			bool[] tmp_wasSet;
			TKey[] tmp_keys;
			TValue[] tmp_values=new TValue[Count-1];

			//Initialize DataSets with correct length
			if( b_removeKey ) {
				tmp_keys=new TKey[Count-1];
				tmp_wasSet=new bool[Count-1];
			}
			else {
				tmp_keys=DATA_keys;
				tmp_wasSet=DATA_wasSet;
			}

			//populate tmp arrays with values
			for( long n=0; n<Count; n++ ) {
				long newIndex=tmp_keys.Length;
				//not the index
				if( n!=index ) {
					tmp_keys[newIndex]=DATA_keys[n];
					if( IndexIsSet(index) ) {
						tmp_values[newIndex]=DATA_values[n];
					}
					tmp_wasSet[newIndex]=DATA_wasSet[n];
				}
				else if( !b_removeKey ) {
					tmp_keys[newIndex]=DATA_keys[n];
					tmp_wasSet[newIndex]=false;
				}
			}

			//Commit to active dataSet
			DATA_keys=tmp_keys;
			DATA_values=tmp_values;
			DATA_wasSet=tmp_wasSet;

			return this;
		}
		//Remove a key and its values
		public MauronCode_dataTree<TKey, TValue> RemoveKey (TKey key) {
			if( IsReadOnly ) {
				throw Error("Is protected!,(RemoveKey)", this, ErrorType_protected.Instance);
			}
			if( !ContainsKey(key) ) {
				throw Error("Invalid key!", this, ErrorType_index.Instance);
			}
			RemoveDataByIndex(IndexByKey(key), true);
			return this;
		}
		//Unset value by value
		public MauronCode_dataTree<TKey, TValue> UnsetByValue (TValue value) {
			long[] indexes=ForEachValues_compare_returnIndex(DATA_values, value, DelegateValueHandler.DEL_objectEquals, 0);
			foreach( long index in indexes ) {
				RemoveDataByIndex(index, false);
			}
			return this;
		}

		//Query: Single
		public TValue Value (TKey key) {
			if( !IsSet(key) ) {
				throw Error("No Value set! {"+key.ToString()+"},(Value)", this, ErrorType_index.Instance);
			}
			return DATA_values[IndexByKey(key)];
		}
		object ICloneable.Clone ( ) {
			return Instance;
		}

		public MauronCode_dataList<KeyValuePair<TKey, TValue>> AsKeyValuePairs {
			get {
				MauronCode_dataList<KeyValuePair<TKey, TValue>> result = new MauronCode_dataList<KeyValuePair<TKey,TValue>>();
				foreach (TKey key in DATA_keys)
					if (IsSet(key))
						result.Add(new KeyValuePair<TKey, TValue>(key, Value(key)));
				return result;
			}
		}

		//Enumerator
		public IEnumerator<TValue> GetEnumerator() {
			return new Enumerator_dataTree<TKey,TValue>(this);
		}
		IEnumerator IEnumerable.GetEnumerator ( ) {
			return new Enumerator_dataTree<TKey, TValue>(this);
		}
	}

	public class Enumerator_dataTree<TKey,TValue>:IEnumerator<TValue> {
		
		//constructor
		public Enumerator_dataTree(MauronCode_dataTree<TKey,TValue> list){
			Enumerables = list.ValuesAsList;
		}

		private MauronCode_dataList<TValue> Enumerables;
		private int Index_current = -1;

		private TValue Value_current = default(TValue);
		public TValue Current {
			get { 
				return Value_current;
			}
		}

		public void Dispose ( ) {
			Enumerables = null;
		}

		object System.Collections.IEnumerator.Current {
			get { 
				return Value_current;
			}
		}

		public bool MoveNext ( ) {
			Index_current++;
			if( Index_current >= Enumerables.Count )
				return false;
			Value_current = Enumerables.Value( Index_current );
			return true;
		}

		public void Reset ( ) {
			Index_current = -1;
			Value_current = default(TValue);
		}
	}

	//A Description of the DataType
	public sealed class DataType_dataTree : DataType {
		#region singleton
		private static volatile DataType_dataTree instance=new DataType_dataTree();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_dataTree ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dataTree();
					}
				}
				return instance;
			}
		}
		#endregion

		public override bool IsProtectable { get { return true; } }
		public override string Name { get { return "dataTree"; } }

	}

}