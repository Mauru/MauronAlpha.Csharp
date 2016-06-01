using MauronAlpha.MonoGame.SpaceGame.Actuals;
using MauronAlpha.MonoGame.SpaceGame.Interfaces;

namespace MauronAlpha.MonoGame.SpaceGame.Units {
	
	
	public class Sector:GameObject, I_Habitable {

		public Sector(I_Habitable parent): base() {
			Parent = parent;
			Population = new Population(this);
		}

		public GameList<Structure> Structures;
		public I_Habitable Parent;
		public Population Population;



		public GameLocation Location {
			get { return Parent.Location; }
		}

		public DataObjects.MoveData MovementDataFor(DataObjects.I_Movable obj) {
			throw new System.NotImplementedException();
		}

		public override Quantifiers.GameName Name {
			get { throw new System.NotImplementedException(); }
		}
		
		public bool IsAttached {
			get { return true; }
		}


		public override bool IsBeing {
			get { return Parent.IsBeing; }
		}

		public override bool IsEquipment {
			get { throw new System.NotImplementedException(); }
		}

		public override bool IsQuantity {
			get { throw new System.NotImplementedException(); }
		}

		public override bool IsResource {
			get { throw new System.NotImplementedException(); }
		}
	}

	public interface I_Attachable { }
}
