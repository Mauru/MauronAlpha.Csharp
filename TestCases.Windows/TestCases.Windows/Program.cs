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
		list.SortWith(comparer_int);
		DumpList(list);
		Console.ReadKey();
	}

}