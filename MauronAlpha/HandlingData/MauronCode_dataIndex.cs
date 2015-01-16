using System;
using System.Collections.Generic;

using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//A data index is a numerical index of generics - it does not reindex its element like dataList
	public class MauronCode_dataIndex<T>:MauronCode_dataObject, ICollection<T> {
		
		//constructor
		public MauronCode_dataIndex():base(DataType_dataIndex.Instance) {}

		private static long[] EmptyKeySet = new long[0]{};

		//The data
		private MauronCode_dataTree<long,T> DATA_values = new MauronCode_dataTree<long,T>(MauronCode_dataIndex<T>.EmptyKeySet);

		#region Custom DataObject functions

		//Set a value
		public ICollection<T> Values {
			get {
				return DATA_values.Values;
			}
		}
		public T Value(long key) {
			if( !DATA_values.IsSet(key) ) {
				throw NullError("Value not Set!,{"+key+"},(Value),",this,typeof(T));
			}
			return DATA_values.Value(key);
		}
		/// <summary>
		/// Sets a Value by Key
		/// </summary>
		/// <param name="key">the key to set</param>
		/// <param name="value">the value to set</param>
		/// <remarks>TODO: This operation SHOULD be volatile.</remarks>
		/// <remarks>You should theoretically lock this object while expanding...</remarks>
		/// <returns>returns self</returns>
		public MauronCode_dataIndex<T> SetValue(long key, T value){
			
			if(IsReadOnly) {
				throw Error("Is ReadOnly!,(SetValue)",this,ErrorType_protected.Instance);
			}

			//creating an instance to preserve functionality for as long as possible
			MauronCode_dataTree<long,T> tree_new =  DATA_values.Instance;
			if( !DATA_values.ContainsValueAtIndex(key)) {
				if(!DATA_values.ContainsKey(key)){
					tree_new.AddKey(key);	
				}else{
					//throw an exception
					Exception("Replacing existing Value!,{"+key+"},(SetValue)", this, ErrorResolution.ExpectedReturn);
				}
				tree_new.SetValue(key, value);
			}
			DATA_values = tree_new;

			return this;
		}

		//is the index ReadOnly
		private bool B_isReadOnly = false;
		public bool IsReadOnly { get { return B_isReadOnly; } }
		public MauronCode_dataIndex<T> SetIsReadOnly(bool state) {
			B_isReadOnly=state;
			return this;
		}
		public bool IsEmpty {
			get {
				foreach(long key in Keys){
					if(ContainsValueAtKey(key)) {
						return true;
					}
				}
				return false;
			}
		}

		//get a list of all keys (remember that keys on dataTrees can be unreliable!, use / implement validKeys for better precision)
		public MauronCode_dataList<long> KeysAsList {
			get {
				return DATA_values.Keys;
			}
		}

		//returns all keys that lead to a result (a built in ContainsValueAtKey check)
		public ICollection<long> ValidKeys {
			get {
				return DATA_values.ValidKeys;
			}
		}

		//Does data contain a key
		public bool ContainsKey(long key){
			return DATA_values.ContainsKey(key);
		}

		//Does data contain the specified object
		public bool ContainsValue(T item){
			foreach(T candidate in DATA_values.ValidValues){
				if(candidate.Equals(item)){
					return true;
				}
			}
			return false;
		}

		public bool ContainsValueAtKey(long key) {
			return DATA_values.IsSet(key);
		}

		//The keys of Data
		public ICollection<long> Keys { get {
			return DATA_values.Keys;
		} }

		//The number of T in collection
		public long CountValidValues {
			get {
				return DATA_values.CountValidValues;
			}
		}
		public long CountKeys {
			get {
				return DATA_values.CountKeys;
			}
		}
		public long CountValidKeys {
			get {
				return DATA_values.CountValidKeys;
			}
		}

		/// <summary>
		/// Removes a value by key
		/// </summary>
		/// <param name="key">the key to remove</param>
		/// <remarks>Throws an Error if the key is not set</remarks>
		/// <returns>self</returns>
		public MauronCode_dataIndex<T> RemoveByKey(long key){
			if( IsReadOnly ) {
				throw Error("Is Read Only!,(RemovebyKey)", this, ErrorType_protected.Instance);
			}
			if(! DATA_values.ContainsKey(key)){
				throw Error("Invalid index!,{"+key+"},(RemoveByKey)",this,ErrorType_index.Instance);
			}
			DATA_values.RemoveKey(key);
			return this;
		}

		//Clear
		public MauronCode_dataIndex<T> Clear() {
			if( IsReadOnly ) {
				throw Error("Is Read Only!,(Clear)", this, ErrorType_protected.Instance);
			}
			DATA_values=new MauronCode_dataTree<long,T>(EmptyKeySet);
			return this;
		}

		#region Specific to this class

		public long NextIndex { 
			get {
				return DATA_values.Keys.Count;
			}		
		}
		public long LastIndex {
			get{
				long result = DATA_values.Keys.Count-1;
				return LastIndex;
			}
		}
		public long FirstIndex {
			get {
				return(DATA_values.Keys.FirstElement);
			}
		}
		#endregion

		#endregion
	
		#region ICollection<T>

		void ICollection<T>.Add (T item) {
			SetValue(NextIndex,item);
		}

		void ICollection<T>.Clear ( ) {
			Clear();
		}

		bool ICollection<T>.Contains (T item) {
			return ContainsValue(item);
		}

		void ICollection<T>.CopyTo (T[] array, int arrayIndex) {
			int index=arrayIndex;
			foreach(T obj in Values){
				array[index]=obj;
				index++;
			}
		}

		int ICollection<T>.Count {
			get { return Convert.ToInt32(CountValidValues); }
		}

		bool ICollection<T>.IsReadOnly {
			get { return IsReadOnly; }
		}

		bool ICollection<T>.Remove (T item) {
			DATA_values.UnsetByValue(item);
			return true;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator ( ) {
			return DATA_values.ValidValues.GetEnumerator();
		}

		#endregion

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) {
			return DATA_values.ValidValues.GetEnumerator();
		}
	}

	//A description of the dataType
	public sealed class DataType_dataIndex:DataType {
		#region singleton
		private static volatile DataType_dataIndex instance=new DataType_dataIndex();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_dataIndex ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_dataIndex();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "dataIndex"; } }
		public override bool IsProtectable {
			get {
				return true;
			}
		}
		/// <summary>
		/// Is the item locked during modification?
		/// </summary>
		public bool IsLockedDuringModification {
			get {
				return false;
			}
		}
	}

}