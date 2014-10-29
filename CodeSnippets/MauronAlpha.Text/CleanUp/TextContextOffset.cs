using System;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Context {
/*==============================================================================
A Offset to a TextContext
==============================================================================*/
public class TextContextOffset:MauronCode_textComponent {
	//DataTrees
	private MauronCode_dataTree<string,int> Index = new MauronCode_dataTree<string,int>(SharedDataKeys);
	private MauronCode_dataTree<string,int> Offset = new MauronCode_dataTree<string,int>(SharedDataKeys);
	private MauronCode_dataTree<string,bool> ChangeState = new MauronCode_dataTree<string,bool>(SharedDataKeys);
	//DataKeys
	public static string[] SharedDataKeys=new string[3] { "line", "word", "character" };


}

}
