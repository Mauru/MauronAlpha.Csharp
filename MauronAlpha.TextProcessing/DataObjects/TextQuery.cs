using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.Units;

namespace MauronAlpha.TextProcessing.DataObjects {
	
	public class TextQuery:TextComponent {

		public TextQuery() : base() { }
		public TextQuery(Text source, TextContext start) : this() {
			Word word = source.WordByContext(start);
			Characters = new Characters(word.Characters.Range(start.Character));
			Words = new Words(word.Parent.Words.Range(start.Word + 1));
			Lines = new Lines(word.Paragraph.Lines.Range(start.Line + 1));
			Paragraphs = new Paragraphs(word.Text.Paragraphs.Range(start.Paragraph + 1));
		}

		public Characters Characters = new Characters();
		public Words Words = new Words();
		public Lines Lines = new Lines();
		public Paragraphs Paragraphs = new Paragraphs();

	}

}
