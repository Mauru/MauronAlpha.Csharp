using System;
using System.Collections.Generic;
using MauronAlpha.HandlingData;

public class Test {
	static MauronCode_dataList<int> RandomList (int size) {
		MauronCode_dataList<int> ret=new MauronCode_dataList<int>();
		Random rand=new Random(1);
		for( int i=0; i<size; i++ ) {
			ret.Add(rand.Next(size));
		}
		return ret;
	}

	static void DumpList (IList<int> list) {
		foreach(int val in list) {
			Console.Write(val);
			Console.Write(", ");
		}
		Console.WriteLine();
	}

	public static int comparer_int (int source, int other) {
		if( source==other )
			return 0;
		if( source<other )
			return -1;
		return 1;
	}


	public static void Main ( ) {
		MauronCode_dataList<int> list=RandomList(100);
		DumpList(list);
		Console.WriteLine("----");
		Sorter<int> sorter = new Sorter<int>();
		sorter.Sort(list,comparer_int);
		DumpList(list);
		Console.ReadKey();
	}

	public class Sorter<T> {

		public void Sort (IList<T> list, del_comparer comparer) {
			MyQuickSort(list, 0, list.Count, comparer);
		}

		public delegate int del_comparer (T source, T other);

		public int MyPartition (IList<T> list, int left, int right, del_comparer comparer) {
			int start=left;
			T pivot=list[start];
			left++;
			right--;

			while( true ) {
				while( left<=right&&comparer(list[left], pivot)<1 )
					left++;

				while( left<=right&&comparer(list[right], pivot)>0 )
					right--;

				if( left>right ) {
					list[start]=list[left-1];
					list[left-1]=pivot;

					return left;
				}


				T temp=list[left];
				list[left]=list[right];
				list[right]=temp;

			}
		}

		public void MyQuickSort (IList<T> list, int left, int right, del_comparer comparer) {
			if( list==null||list.Count<=1 )
				return;

			if( left<right ) {
				int pivotIdx=MyPartition(list, left, right, comparer);
				//Console.WriteLine("MQS " + left + " " + right);
				//DumpList(list);
				MyQuickSort(list, left, pivotIdx-1, comparer);
				MyQuickSort(list, pivotIdx, right, comparer);
			}
		}
	}
}