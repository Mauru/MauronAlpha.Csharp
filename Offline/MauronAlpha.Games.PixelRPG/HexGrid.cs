using MauronAlpha.GameEngine;
using MauronAlpha.Geometry;

using System.Collections.Generic;
using System;

namespace MauronAlpha.Games.PixelRPG {

	//a axial flat-topped hexgrid
	public class HexGrid : Drawable {

/* Required methods ad functions */

		public override double Height {
			get { throw new System.NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}
		public override double Width {
			get { throw new System.NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

/* Individual code */

		//Setup
		public readonly HexField[] Fields;
		public readonly short Rows=0;
		public readonly short Columns=0;

		//Field Maps
		public readonly Dictionary<string, HexField> IndexAxial=new Dictionary<string, HexField>();
		public readonly Dictionary<string, HexField> IndexEvenQ=new Dictionary<string, HexField>();
		public readonly Dictionary<string, HexField> IndexCube=new Dictionary<string, HexField>();
		public readonly Dictionary<int, HexField> IndexInt=new Dictionary<int, HexField>();

		//constructor
		public HexGrid (Drawable parent, short rows, short columns, HexFieldData data) : base(DrawableType_container.Instance) {
			Parent = parent;
			Rows=rows;
			Columns=columns;

			//Reset Fields
			Fields=new HexField[Rows*Columns];

			//Generate Grid
			short q=0;
			short r=0;
			for( short i=0; i<(Rows*Columns); i++ ) {
				
				//new row
				if( i>0&&q%Columns==0 ) {
					q=0;
					r++;
				}
				else if( i>0 ) {
					q++;
				}

				//Create Relative Coordinates
				Vector2d evenq=new Vector2d(q, r);
				Vector2d axial=HexNeighborsAxial.FromEvenQ(evenq);
				Vector3d cube=HexNeightborsCube.FromAxial(axial);

				//create Hexfield
				HexField h=new HexField(this, this, i, axial, data.Instance);
				
				//Store in FieldMap
				IndexEvenQ[evenq.ToString]=h;
				IndexAxial[axial.ToString]=h;
				IndexCube[cube.ToString]=h;
				IndexInt[i]=h;
				Fields[i]=h;

				AddChild(h);
			}

		}

		//get fields by coordinate system
		public HexField FieldByAxial (Vector2d v) {
			return this.IndexAxial[v.ToString];
		}
		public HexField FieldByEvenQ (Vector2d v) {
			return this.IndexEvenQ[v.ToString];
		}
		public HexField FieldByCube (Vector3d v) {
			return this.IndexCube[v.ToString];
		}
		public HexField FieldByInt (short i) {
			return this.IndexInt[i];
		}

		public override void E_HasRendered ( ) {
			throw new NotImplementedException();
		}
	}

}