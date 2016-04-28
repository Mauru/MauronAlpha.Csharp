			if (!unit.IsParagraphBreak)
				return false;

			Word insert = new Word(unit);
			Line line = DATA_text.LineByContext(context);
			Word word = line.ByIndex(context.Word);

			TextQuery lookAhead = new TextQuery(DATA_text, context);
			
			//insert at left
			if (context.Character <= 0) {

				if (word.IsParagraphBreak) {
					int index = word.Index;
					//we need to remove the current break and move it to a new paragraph
					line.Remove(index);
					//insert new break;
					line.TryAdd(insert);
					Line newLine = new Line(word);
					Paragraph newParagraph = new Paragraph(new Line(Words.LineBreak));
					newParagraph.TryAdd(newLine);
					return line.Text.TryAdd(newParagraph, line.Parent.Index);
				}

				if (word.IsLineBreak) {
					//remove all following lines
					Lines move = line.Parent.SplitAt(line.Index + 1);

					//remove the linebreak
					int index = word.Index;
					line.Remove(index);

					//we need to insert a NEW linebreak here
					line.Insert(Words.LineBreak, index);
					//we need a new line with the paragraph break
					Line pbreak = new Line(new Word(unit));
					line.Parent.TryAdd(pbreak);

					//we need to add the linebreak (word) into move
					move.InsertValueAt(0, new Line(word));

					//create following paragraph with moved lines
					if (move.HasParagraphBreak) {
						Paragraph newParagraph = new Paragraph(move);
						return line.Text.TryAdd(newParagraph, line.Parent.Index + 1);
					}
					//prepend moved lines to following paragraph
					Paragraph next = line.Parent.Next;
					move = move.Reverse();
					foreach (Line insertLine in move)
						next.Insert(insertLine, 0);
					return true;
				}

				//is there a previous word
				Word previous = null;
				bool foundPrev = word.TryPrevious(ref previous);

				// there is a previous word
				if (foundPrev) {

					//split line at word
					Words split = line.SplitAt(word.Index);

					//End the current line
					previous.Parent.TryAdd(Words.LineBreak);

					//split the paragraph
					Lines move = previous.Paragraph.SplitAt(line.Index + 1);

					//add unit (pbreak) to next line - this is the UNIT we want to insert
					previous.Paragraph.Insert(new Line(insert), previous.Parent.Index + 1);

					//we still have any following words (split) and the following lines (move)
					if (move.HasParagraphBreak) {
						move.FixAsContainedParagraph();
						Paragraph newP = new Paragraph(move);
						previous.Text.TryAdd(newP, line.Parent.Index + 1);
						//still need to add split
						if (split.HasLineBreak) {
							Line newLine = new Line(split);
							return newP.Insert(newLine, 0);
						}
						//regular add to new line
						split.Reverse();
						Line firstLine = newP.FirstChild;
						foreach (Word w in split)
							firstLine.Insert(w, 0);
						return true;
					}
					Paragraph next = previous.Paragraph.Next;
					move.Reverse();
					foreach (Line l in move)
						next.Insert(l, 0);

					//add split
					if (split.HasLineBreak) {
						Line newLine = new Line(split);
						return next.Insert(newLine, 0);
					}
					//regular add to new line
					split.Reverse();
					Line fLine = next.FirstChild;
					foreach (Word w in split)
						fLine.Insert(w, 0);
					return true;
				}

				// oof - we DON't have a previous word
				// to summarize, word is a regular character, tab or ws or empty
				// we got insert with a paragraph break

				Line prev = null;
				bool foundPrevLine = word.Parent.TryPrevious(ref prev);

				Paragraph p = word.Paragraph;
				int lineIndex = word.Parent.Index;

				//we need to insert a linebreak to allow a pbreak
				if (!foundPrevLine) {
					p.Insert(new Line(Words.LineBreak), lineIndex);
					lineIndex++;
				}
				else {
					if(!prev.LastChild.IsLineBreak)

				}
					Lines following = p.SplitAt(lineIndex);
					//insert line with PBreak
					Line newBreak = new Line(insert);
					p.TryAdd(newBreak);

					if (following.HasParagraphBreak) {
						Paragraph inject = new Paragraph(following);
						return p.Parent.TryAdd(inject, p.Index + 1);
					}
					Paragraph next = p.Next;
					
					//need to check if last element of following is a linebreak
					if (following.IsEmpty)
						return true;

					Line test = following.LastElement;
					if (test.LastChild.IsLineBreak) {
						following.Reverse();
						foreach (Line l in following)
							next.Insert(l, 0);
						return true;
					}

					//the last element of following needs to be integrated into the first line of next
					Words merge = following.Pop.Words.Reverse();
					Line firstNext = next.FirstChild;

					foreach (Word w in merge)
						firstNext.Insert(w, 0);

					return true;					
				}

				//we found a previous line
				

			}




