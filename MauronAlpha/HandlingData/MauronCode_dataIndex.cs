using System;
using System.Collections.Generic;
using System.Collections;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.HandlingData {

	//A data index is a numerical index of generics - it does not reindex its element like dataList
	public class MauronCode_dataIndex<T>:MauronCode_dataObject, 
	ICollection<T>,
	IEnumerable<T>,
	IEquatable<MauronCode_dataIndex<T>>,
	I_protectable<MauronCode_dataIndex<T>> {
		
		//constructor
		public MauronCode_dataIndex():base(DataType_dataIndex.Instance) {}

		private static long[] EmptyKeySet = new long[0]{};

		//The data
		private MauronCode_dataTree<long,T> DATA_values = new MauronCode_dataTree<long,T>(MauronCode_dataIndex<T>.EmptyKeySet);

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
		public ICollection<long> InvalidKeys {
			get {
				return DATA_values.InvalidKeys;
			}
		}
		//The keys of Data
		public ICollection<long> Keys { 
			get {
				return DATA_values.Keys;
			}
		}

		//ICollection<T> Values
		public ICollection<T> Values {
			get {
				return DATA_values.Values;
			}
		}

		public MauronCode_dataList<T> ValuesAsList { 
			get {
				return DATA_values.ValuesAsList;				
			}
		}

		//Booleans
		public bool Equals(MauronCode_dataIndex<T> other) {
			MauronCode_dataList<long> otherKeys = new MauronCode_dataList<long>(other.Keys).SortWith(Delegate_sortKeys);
			MauronCode_dataList<long> myKeys = new MauronCode_dataList<long>(Keys).SortWith(Delegate_sortKeys);
			if(!myKeys.Equals(otherKeys))
				return false;
			foreach(long key in myKeys){
				bool state = ContainsValueAtKey(key);
				if(state!=other.ContainsValueAtKey(key))
					return false;
				if(state) {
					T myValue = Value(key);
					T otherValue = other.Value(key);
					if(!myValue.Equals(otherValue))
						return false;
				}
			}
			return true;
		}
		private bool B_isReadOnly=false;
		public bool IsReadOnly { 
			get { return B_isReadOnly; } 
		}
		public bool IsEmpty {
			get {
				foreach( long key in Keys ) {
					if( ContainsValueAtKey(key) ) {
						return true;
					}
				}
				return false;
			}
		}
		public bool ContainsKey (long key) {
			return DATA_values.ContainsKey(key);
		}
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

		//Methods
		public MauronCode_dataIndex<T> SetIsReadOnly (bool state) {
			B_isReadOnly=state;
			return this;
		}

		//Modifiers: Add
		/// <summary>
		/// Sets a Value by Key
		/// </summary>
		/// <param name="key">the key to set</param>
		/// <param name="value">the value to set</param>
		/// <remarks>TODO: This operation SHOULD be volatile.</remarks>
		/// <remarks>You should theoretically lock this object while expanding...</remarks>
		/// <returns>returns self</returns>
		public MauronCode_dataIndex<T> SetValue (long key, T value) {
			if( IsReadOnly )
				throw Error("Is ReadOnly!,(SetValue)", this, ErrorType_protected.Instance);

			if( !DATA_values.ContainsKey(key) )
				DATA_values.AddKey(key);

			DATA_values.SetValue(key, value);

			return this;
		}
		public MauronCode_dataIndex<T> Prepend(MauronCode_dataList<T> list) {
			long offset = FirstIndex;
			for(int n=list.Count-1; n>=0; n--){
				long newIndex = offset-1;
				SetValue(newIndex,list.Value(n));
			}
			return this;
		}
		public MauronCode_dataIndex<T> Append(MauronCode_dataList<T> list){
			long offset = LastIndex;
			for(int n=0; n<list.Count; n++){
				long newIndex = offset+1;
				SetValue(newIndex,list.Value(n));
			}
			return this;
		}
		
		//Modifiers: Remove
		public MauronCode_dataIndex<T> Clear() {
			if( IsReadOnly )
				throw Error("Is Read Only!,(Clear)", this, ErrorType_protected.Instance);
			DATA_values=new MauronCode_dataTree<long,T>(EmptyKeySet);
			return this;
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

		//Queries: Single
		public T Value (long key) {
			if( !DATA_values.IsSet(key) ) {
				throw NullError("Value not Set!,{"+key+"},(Value),", this, typeof(T));
			}
			return DATA_values.Value(key);
		}

		//Indexers (long)
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
	
		//ICollection<T> Members
		void ICollection<T>.Add (T item) {
			SetValue(NextIndex,item);
		}
		void ICollection<T>.Clear ( ) {
			Clear();
		}
		void ICollection<T>.CopyTo (T[] array, int arrayIndex) {
			int index=arrayIndex;
			foreach( T obj in Values ) {
				array[index]=obj;
				index++;
			}
		}

		bool ICollection<T>.Contains (T item) {
			return ContainsValue(item);
		}
		bool ICollection<T>.Remove (T item) {
			DATA_values.UnsetByValue(item);
			return true;
		}

		int ICollection<T>.Count {
			get { return Convert.ToInt32(CountValidValues); }
		}

		//Delegates
		private static int Delegate_sortKeys(long source, long other) {
			return source.CompareTo(other);
		}

		//Enumeration
		public IEnumerator<T> GetEnumerator ( ) {
			return new Enumerator_dataIndex<T>(this);
		}
		IEnumerator IEnumerable.GetEnumerator ( ) {
			return new Enumerator_dataIndex<T>(this);
		}
	}

	//The Enumerator for dataIndex
	public class Enumerator_dataIndex<TValue> : IEnumerator<TValue> {
			
		//The collection
		MauronCode_dataList<TValue> Enumerables;
		TValue Value_current = default(TValue);

		//constructor
		public Enumerator_dataIndex ( MauronCode_dataIndex<TValue> list ) {
			Enumerables = list.ValuesAsList;
			Value_current=default(TValue);
		}

		private int Index_current = -1;
	
		public TValue Current {
			get {
				return Value_current;
			}
		}
		object IEnumerator.Current {
			get {
				return Value_current;
			}
		}

		public void Dispose ( ) {
			Enumerables = null;
		}

		public bool MoveNext ( ) {
			Index_current++;
			if( Index_current>=Enumerables.Count )
				return false;
			else
				Value_current=Enumerables.Value(Index_current);

			return true;
		}

		public void Reset ( ) {
			Index_current = -1;
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