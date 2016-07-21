
			//is there a next word?
			if (foundNextWord) {
				Words splitW = line.SplitAt(word.Index + 1);
				line.TryAdd(Words.LineBreak);
				Line newL = new Line(new Word(unit));
				
				//are there other lines?
				Line nextL = null;
				bool foundNextL = word.Parent.TryNext(ref nextL);

				// there are other lines
				if (foundNextL) {
					//split off lines
					Lines splitLines = word.Paragraph.SplitAt(word.Parent.Index + 1);
					//append unit
					line.Parent.Insert(newL, line.Index + 1);
					
					//self-contained paragraph
					if (splitLines.HasParagraphBreak) {
						nextP = new Paragraph(splitLines);
						nextP.FixParagraphEnd();
						line.Text.TryAdd(nextP, line.Parent.Index + 1);		
					}
					else {
						//is there a next paragraph?
						nextP = null;
						foundNextP = line.Parent.TryNext(ref nextP);

						if(!foundNextP) {
							nextP = new Paragraph();
							line.Text.TryAdd(nextP);
						}

						splitLines.Reverse();
						foreach(Line l in splitLines)
							nextP.Insert(l,0);
					}
				}
				else {
					//no next Line
					foundNextP = line.Parent.TryNext(ref nextP);

					if (!foundNextP) {
						nextP = new Paragraph();
						line.Text.TryAdd(nextP);
					}
				}

				//still have split word and rest of words on line
				Line firstL = nextP.FirstChild;

				//words
				if (splitW.HasLineBreak)
					firstL.Parent.Insert(new Line(splitW), 0);
				else {
					splitW.Reverse();
					foreach (Word w in splitW)
						firstL.Insert(w, 0);
				}
			}
			//no next Word
			else {
				foundNextP = line.Parent.TryNext(ref nextP);
				if (!foundNextP) {
					nextP = new Paragraph();
					line.Text.TryAdd(nextP);
				}

				//are there other lines?
				Line nextL = null;
				bool foundNextL = word.Parent.TryNext(ref nextL);

				if (foundNextL) {
					//split off lines
					Lines splitLines = word.Paragraph.SplitAt(word.Parent.Index + 1);
					//append unit
					Line newL = new Line(new Word(unit));
					line.Parent.Insert(newL, line.Index + 1);

					//self-contained paragraph
					if (splitLines.HasParagraphBreak) {
						nextP = new Paragraph(splitLines);
						nextP.FixParagraphEnd();
						line.Text.TryAdd(nextP, line.Parent.Index + 1);
					}
					else {
						//is there a next paragraph?
						nextP = null;
						foundNextP = line.Parent.TryNext(ref nextP);

						if (!foundNextP) {
							nextP = new Paragraph();
							line.Text.TryAdd(nextP);
						}

						splitLines.Reverse();
						foreach (Line l in splitLines)
							nextP.Insert(l, 0);
					}
				}
			}

			//re-add split chars
			firstL = nextP.FirstChild;
			Word firstW = firstL.FirstChild;

			if (firstW.IsUtility) {
				Word newW = new Word(splitC);
				return firstL.Insert(newW, 0);
			}
			splitC.Reverse();
			foreach (Character c in splitC)
				firstW.TryAdd(c, 0);
			return true;