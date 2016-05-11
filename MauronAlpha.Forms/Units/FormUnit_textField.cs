﻿using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;

using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Collections;

using MauronAlpha.Forms.DataObjects;
using MauronAlpha.Forms.Interfaces;

namespace MauronAlpha.Forms.Units {
	
	//A Entity waiting for user input
	public class FormUnit_textField : FormComponent_unit, I_layoutUnit {
		
		//constructor
		public FormUnit_textField():base( FormType_textField.Instance ) {
			DATA_caret = new CaretPosition(DATA_text);
		}

		//The actual TextObject
		Text DATA_text = new Text();
		public Text AsText { get { return DATA_text; } }

		//return an actal string representation
		public string AsString {
			get {
				return DATA_text.AsString;
			}
		}

		//CaretPosition
		CaretPosition DATA_caret;
		public CaretPosition CaretPosition {
			get {
				if (DATA_caret == null)
					DATA_caret = new CaretPosition(DATA_text);
				return DATA_caret;
			}
		}

		//count the number of "lines"
		public int CountLines {
			get {
				return DATA_text.Lines.Count;
			}
		}
		
		//count as a contextual 
		public TextContext CountAsContext {
			get {
				int p = 0;
				int l = 0;
				int w = 0;
				int c = 0;
				foreach (Paragraph pu in DATA_text.Paragraphs) {
					p++;
					foreach (Line lu in pu.Lines) {
						l++;
						foreach (Word wu in lu.Words) {
							w++;
							foreach (Character cu in wu.Characters)
								c++;
						}
					}
				}
				return new TextContext(p, l, w, c);
			}
		}

		//Querying
		public Paragraph ActiveParagraph {
			get {
				return DATA_text.ByIndex(CaretPosition.Paragraph);
			}
		}

		//Querying
		public Line LineByIndex (int index){
			int count = CountLines;
			if (index < 0 || index > count)
				throw Error("Index out of bounds!,{" + index + "},(LineByIndex)", this, ErrorType_bounds.Instance);
			else if (index == count)
				return DATA_text.FirstLine;
			return DATA_text.LineByIndex(index);
		}
		public Line ActiveLine {
			get {
				return ActiveParagraph.ByIndex(CaretPosition.Line);
			}
		}
		public Line LastLine {
			get {
				return DATA_text.LastLine;
			}
		}
		public Line FirstLine {
			get {
				return DATA_text.FirstLine;
			}
		}

		public Word ActiveWord {
			get {
				return ActiveLine.ByIndex(CaretPosition.Word);
			}
		}

		public Character ActiveCharacter {
			get {
				return ActiveWord.ByIndex(CaretPosition.Character);
			}
		}

		public MauronCode_dataList<Line> Lines {
			get {
				return DATA_text.Lines;
			}
		}

		//Booleans
		public bool IsEmpty {
			get {
				return DATA_text.IsEmpty;
			}
		}

		public bool HasLine(int n) {
			return DATA_text.HasLineAtIndex(n);
		}
		
		//Methods
		public bool AddAsLines(string text) {
			Lines ll = new Lines(text);
			if (!IsEmpty)
				LastLine.TryEnd();
			Paragraph p = DATA_text.LastChild;
			foreach (Line l in ll) {
				if (p.IsEmpty)
					p.TryAdd(l);
				else if (p.HasParagraphBreak) {
					p = p.Next;
					p.TryAdd(l);
				}
				else
					p.TryAdd(l);
			}
			DATA_caret.SetContext(DATA_text.Edit);
			return true;
		}

		//Methods
		public void SetText(string text) {
			Characters cc = new Characters(text);
			Paragraphs pp = new Paragraphs(cc);
			DATA_text = new Text(pp);
			CaretPosition.SetContext(DATA_text.End);
		}
		public void SetPosition(float x, float y) {
			Position.Set(x, y);
		}

		public void AddAsLine(string text) {
			ContextQuery query = new ContextQuery(DATA_text, CaretPosition.Context);
			query.TrySolve();
			if (!query.Line.HasLineOrParagraphBreak)
				query.Line.TryAdd(Words.LineBreak);
			Lines ll = new Lines(text);
			query.Paragraph.Insert(ll,query.Line.Index+1);
			TextOperation op = ReIndexer.MergeNext(query.Line);
			TextOperation.CompleteOperations(op);
			CaretPosition.SetContext(ll.LastElement.End);
		}

		public void AddAsParagraph(string text) {
			ContextQuery query = new ContextQuery(DATA_text, CaretPosition.Context);
			query.TrySolve();
			if (!query.Character.IsParagraphBreak)
				query.Word.Insert(Characters.ParagraphBreak, query.Character.Index + 1);
			TextOperation.ReIndexAhead(query.Character);
			Lines ll = new Lines(text);
			query.Paragraph.Next.TryAdd(ll);
			TextOperation.ReIndexAhead(query.Character);
			CaretPosition.SetContext(ll.LastElement.End);
		}

		public void InsertAtWord(int index, string text) {
			Word w = DATA_text.WordByIndex(index);
			Words ww = new Words(text);
			w.Parent.Insert(ww, w.Index);
			TextOperation.ReIndexAhead(w.FirstChild);
			CaretPosition.SetContext(ww.LastElement.End);
		}

		//debug
		public I_debugInterface DebugInterface;

		public void Debug(string msg) {
			if (DebugInterface == null)
				return;
			DebugInterface.SubmitDebugMessage("{FormUnit_textField} "+msg);
		}
	
		//Methods for updateing and changing the text
		public void InsertAndUpdateContext(Character c) {
			ContextQuery query = new ContextQuery(DATA_text, CaretPosition.Context);
			query.TrySolve();

			TextOperation op = TextOperation.InsertCharacter(c, query.Result);
			TextOperation.CompleteOperations(op);

			CaretPosition.SetContext(c.Context);
		}
	
	}

	//Form Description
	public sealed class FormType_textField : Layout2d_unitType {
		#region singleton
		private static volatile FormType_textField instance = new FormType_textField();
		private static object syncRoot = new System.Object();

		//constructor singleton multithread safe
		static FormType_textField ( ) { }
		public static Layout2d_unitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance = new FormType_textField();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "textField"; } }

		public override bool CanHaveChildren {
			get { return true; }
		}
		public override bool CanHaveParent {
			get { return true; }
		}
		public override bool CanHide {
			get { return true; }
		}
		public override bool IsDynamic {
			get { return true; }
		}
	}

}