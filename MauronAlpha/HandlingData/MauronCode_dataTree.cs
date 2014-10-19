using System.Collections.Generic;
using System;

using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//An associative array with a fixed size, tracks if values have been set or not
	public class MauronCode_dataTree<TKey,TValue> : MauronCode_dataObject {

		#region Constructors
		private MauronCode_dataTree() : base(DataType_dataTree.Instance) {}
		public MauronCode_dataTree(ICollection<TKey> keys):this() {
			//initialize keys
			DATA_keys=new TKey[keys.Count];
			
			//we need to check if the keys are unique unfortunately
			//TODO("Finish each on each check");
			DelegateHandler_equals<TKey> D = new DelegateHandler_equals<TKey>();
			if( ForEachKeys_bool(DATA_keys, D.D_objectEquals,1) ) {
				Error("Duplicate Keys Detected!",this,ErrorType_constructor.Instance);
			}
			
			keys.CopyTo(DATA_keys,0);

			//initialize empty values
			DATA_values=new TValue[keys.Count];

			//initialize the boolean set counter
			DATA_wasSet=new bool[keys.Count];

		}
		public MauronCode_dataTree(ICollection<TKey> keys, ICollection<TValue> values):this(keys){
			if(keys.Count!=values.Count){
				Error("Keys/Values have different length!",this,ErrorType_constructor.Instance);
			}

			TKey[] v_key = new TKey[keys.Count];
			keys.CopyTo(v_key, 0);

			TValue[] v_value = new TValue[keys.Count];
			values.CopyTo(v_value,0);

			for(int n=0; n<values.Count ; n++) {
				SetValue(v_key[n],v_value[n]);
			}
		}
		
		#endregion

		#region Delegate Functions
		///<summary>Cycle through each component of a array and test tem against each other</summary>
		///<remarks>Does not test against itself.</remarks>
		///<param name="limitTrue">0: Test for all, >0: Test until true has been returned n times</param>
		private bool ForEachKeys_bool(TKey[] keys, DelegateMethod_objectInteraction_bool<TKey> comparer, int limitTrue) {
			TKey origin;
			TKey target;
			int countTrue = 0;
			for(int i=0; i<keys.Length; i++) {
				origin = keys[i];
				for(int b=0; b<keys.Length; b++) {
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
		
		///<summary>Cycle through each component of a array and test against target</summary>
		///<param name="limitTrue">0: Test for all, >0: Test until true has been returned n times</param>
		private bool ForEachKeys_compare_bool(TKey[] keys, TKey target, DelegateMethod_objectInteraction_bool<TKey> comparer, int limitTrue) {
			TKey origin;
			int countTrue=0;
			for( int i=0; i<keys.Length; i++ ) {
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
		private TValue[] ForEachValues_compare_return(TValue[] values, TValue target, DelegateMethod_objectInteraction_bool<TValue> comparer, int limitTrue){
			TValue[] result=new TValue[0];
			if(CountValidValues==0){
				return result;
			}
			for(int n=0; n<Count; n++){
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
		private int[] ForEachValues_compare_returnIndex(TValue[] values, TValue target, DelegateMethod_objectInteraction_bool<TValue> comparer, int limitTrue) {
			int[] result=new int[0];
			for(int index=0;index<Count;index++){
				if(IndexIsSet(index)) {
					TValue candidate = DATA_values[index];
					if(comparer(candidate,target)){
						int newIndex=result.Length;
						int[] tmp_result = new int[newIndex];
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
		private DelegateHandler_equals<int> DelegateIndexHandler {
			get {
				return new DelegateHandler_equals<int>();
			}
		}
		private DelegateHandler_equals<TValue> DelegateValueHandler {
			get {
				return new DelegateHandler_equals<TValue>();
			}
		}

		//Internal class for a DelegateHandler
		private class DelegateHandler_equals<varType> {

			//Perform "Equals()" Comparison for two objects
			public bool DEL_objectEquals(varType origin, varType target) {
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

		//Branches (Keys)
		private TKey[] DATA_keys;
		//Leaves (Values)
		private TValue[] DATA_values;
		//Tracks if each value was set
		private bool[] DATA_wasSet;

		#region Working with the DataSet
		//Actively remove a data Entry from the DataSets, either completely or only the value
		private MauronCode_dataTree<TKey, TValue> RemoveDataByIndex (int index, bool b_removeKey) {

			//Error Check
			if( !ContainsIndex(index) ) {
				Error("Index out of Bounds!,{"+index+"}", this, ErrorType_bounds.Instance);
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
			for( int n=0; n<Count; n++ ) {
				int newIndex=tmp_keys.Length;
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
		#endregion

		#region Working with keys
		public int IndexByKey (TKey key) {
			for( int index=0; index<DATA_keys.Length; index++ ) {
				if( KeyByIndex(index).Equals(key) ) {
					return index;
				}
			}
			return -1;
		}
		public TKey KeyByIndex (int index) {
			if( !ContainsIndex(index) ) {
				Error("Index out of Bounds!,{"+index+"},(KeyByIndex)", this, ErrorType_bounds.Instance);
			}
			return DATA_keys[index];
		}

		public int Count { get { return DATA_keys.Length; } }

		public MauronCode_dataList<TKey> Keys {
			get {
				return new MauronCode_dataList<TKey>(DATA_keys).SetIsReadOnly(true);
			}
		}

		public MauronCode_dataTree<TKey,TValue> AddKey(TKey key) {
			if(ContainsKey(key)){
				Error("Key is allready set!",this,ErrorType_protected.Instance);
			}
			
			//Create instance of DataSet with new Length
			int newIndex = Count;
			TKey[] tmp_keys = new TKey[newIndex];
			TValue[] tmp_values = new TValue[newIndex];
			bool[] tmp_wasSet = new bool[newIndex];

			for(int n=0; n<newIndex; n++) {
				tmp_keys[n]=DATA_keys[n];
				if(IndexIsSet(n)){
					tmp_values[n]=DATA_values[n];
				}
				tmp_wasSet[n]=DATA_wasSet[n];
			}

			//Insert new Key
			tmp_keys[newIndex] = key;
			tmp_wasSet[newIndex] = false;

			return this;
		}

		//Remove a key and its values
		public MauronCode_dataTree<TKey,TValue> RemoveKey(TKey key) {
			if(!ContainsKey(key)) {
				Error("Invalid key!",this,ErrorType_index.Instance);
			}
			RemoveDataByIndex(IndexByKey(key),true);
			return this;
		}
		#endregion

		#region Working with Values

		public MauronCode_dataIndex<TValue> Values {
			get {
				MauronCode_dataIndex<TValue> result=new MauronCode_dataIndex<TValue>();
				for(int n = 0; n < DATA_values.Length; n++) {
					if(IndexIsSet(n)){
						result.SetValue(n,DATA_values[n]);
					}
				}
				return result;
			}
		}

		public TValue Value (TKey key) {
			if( !IsSet(key) ) {
				Error("No Value set! {"+key.ToString()+"},(Value)", this, ErrorType_index.Instance);
			}
			return DATA_values[IndexByKey(key)];
		}
		public MauronCode_dataTree<TKey, TValue> SetValue (TKey key, TValue value) {
			int index=IndexByKey(key);
			if( index<0 ) {
				Error("Invalid Key!", this, ErrorType_index.Instance);
			}
			DATA_values[index]=value;
			DATA_wasSet[index]=true;
			return this;
		}
		public MauronCode_dataTree<TKey, TValue> SetValues(TValue[] values) {
			if(values.Length!=Count) {
				Error("Values must be same length as keys!,(SetValues)",this,ErrorType_bounds.Instance);
			}
			for(int n=0;n<values.Length;n++){
				DATA_values[n]=values[n];
				DATA_wasSet[n]=true;
			}
			return this;
		}

		public ICollection<TValue> ValidValues {
			get {
				TValue[] result=new TValue[CountValidValues];
				for( int index=0; index<Count; index++ ) {
					if( IndexIsSet(index) ) {
						int newIndex=result.Length;
						result[newIndex]=DATA_values[index];
					}
				}
				return result;
			}
		}

		public int CountValidValues {
			get {
				int result=0;
				for( int index=0; index<Count; index++ ) {
					if( IndexIsSet(index) ) {
						result++;
					}
				}
				return result;
			}
		}

		//Unset value by Key
		public MauronCode_dataTree<TKey, TValue> UnsetByKey (TKey key) {
			if( !IsSet(key) ) {
				Exception("Value not set!,(UnsetValue)", this, ErrorResolution.DoNothing);
				return this;
			}
			int index=IndexByKey(key);
			RemoveDataByIndex(index, false);
			return this;
		}

		//Unset value by value
		public MauronCode_dataTree<TKey, TValue> UnsetByValue (TValue value) {
			int[] indexes=ForEachValues_compare_returnIndex(DATA_values, value, DelegateValueHandler.DEL_objectEquals, 0);
			foreach( int index in indexes ) {
				RemoveDataByIndex(index, false);
			}
			return this;
		}
		#endregion



		#region Boolean States

		private bool IndexIsSet(int index){
			if(index<0||index>=Count) {
				Exception("Index out of Bounds!,{"+index+"}",this,ErrorResolution.ExpectedReturn);
				return false;
			}
			return DATA_wasSet[index];
		}

		//Is The item valid
		public bool IsValid {
			get {
				return DATA_keys != null && DATA_keys.IsFixedSize && DATA_values.IsFixedSize && DATA_keys.Length == DATA_values.Length;
			}
		}

		//is the index ReadOnly
		public bool IsReadOnly { get { return false; } }

		public bool ContainsIndex(int n) {
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

		#endregion
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