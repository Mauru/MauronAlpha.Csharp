﻿using MauronAlpha.Text.Units;
using MauronAlpha.Text.Utility;

using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Forms.Units {
	
	//A entity waiting for user input
	public class FormUnit_textField : FormComponent_unit, I_layoutUnit {
		
		//constructor
		public FormUnit_textField():base() {}
       
		private TextUnit_text TXT_text;

		private TextUtility_encoding Encoding;

		private Layout2d_position XY_position;

	}
}