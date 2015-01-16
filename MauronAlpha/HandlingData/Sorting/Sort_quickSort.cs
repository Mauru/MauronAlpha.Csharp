using System;
using System.Collections.Generic;

namespace MauronAlpha.HandlingData.Sorting {
	
	public class Sort_quickSort<T> {

		public void Sort (IList<T> list, Comparer comparer) {
			MyQuickSort(list, 0, list.Count, comparer);
		}

		public delegate int Comparer (T source, T other);

		public int MyPartition (IList<T> list, int left, int right, Comparer comparer) {
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

		public void MyQuickSort (IList<T> list, int left, int right, Comparer comparer) {
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
