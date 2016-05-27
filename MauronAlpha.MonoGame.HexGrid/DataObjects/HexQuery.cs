using MauronAlpha.MonoGame.HexGrid.Units;
using MauronAlpha.MonoGame.HexGrid.Collections;
using MauronAlpha.Geometry.Geometry3d.Units;
using MauronAlpha.HandlingData;

namespace MauronAlpha.MonoGame.HexGrid.DataObjects {
	public class HexQuery:HexGridComponent {

		public HexQuery(Grid grid, Vector3d coordinates): base() {
			Grid = grid;
			Coordinates = coordinates;
			Results = new MauronCode_dataMap<Hex>();
			Queried = new MauronCode_dataMap<bool>();
		}

		public MauronCode_dataMap<Hex> Results;
		public MauronCode_dataMap<bool> Queried;

		public Grid Grid;
		public Vector3d Coordinates;

		public Hex Self {
			get {
				if (Queried.ContainsKey("Self"))
					return Results.Value("Self");
				Hex result = null;
				if (!Grid.TryFind(Coordinates, out result))
					result = new Hex(Grid, Coordinates).SetNotFound(true);

				Results.SetValue("Self", result);
				Queried.SetValue("Self", true);
				return result;
			}
		}
		public Hex TopRight {
			get {
				Vector3d query = HexNeighbors.TopRightOf(Coordinates);
				if (Queried.ContainsKey("TopRight"))
					return Results.Value("TopRight");
				Hex result = null;
				if (!Grid.TryFind(query, out result))
					result = new Hex(Grid, query).SetNotFound(true);

				Results.SetValue("TopRight", result);
				Queried.SetValue("TopRight", true);
				return result;
			}
		}
		public Hex Top {
			get {
				Vector3d query = HexNeighbors.TopOf(Coordinates);
				if (Queried.ContainsKey("Top"))
					return Results.Value("Top");
				Hex result = null;
				if (!Grid.TryFind(query, out result))
					result = new Hex(Grid, query).SetNotFound(true);

				Results.SetValue("Top", result);
				Queried.SetValue("Top", true);
				return result;
			}
		}
		public Hex TopLeft {
			get {
				Vector3d query = HexNeighbors.TopLeftOf(Coordinates);
				if (Queried.ContainsKey("TopLeft"))
					return Results.Value("TopLeft");
				Hex result = null;
				if (!Grid.TryFind(query, out result))
					result = new Hex(Grid, query).SetNotFound(true);

				Results.SetValue("TopLeft", result);
				Queried.SetValue("TopLeft", true);
				return result;
			}
		}
		public Hex BottomRight {
			get {
				Vector3d query = HexNeighbors.BottomRightOf(Coordinates);
				if (Queried.ContainsKey("BottomRight"))
					return Results.Value("BottomRight");
				Hex result = null;
				if (!Grid.TryFind(query, out result))
					result = new Hex(Grid, query).SetNotFound(true);

				Results.SetValue("BottomRight", result);
				Queried.SetValue("BottomRight", true);
				return result;
			}
		}
		public Hex Bottom {
			get {
				Vector3d query = HexNeighbors.BottomOf(Coordinates);
				if (Queried.ContainsKey("Bottom"))
					return Results.Value("Bottom");
				Hex result = null;
				if (!Grid.TryFind(query, out result))
					result = new Hex(Grid, query).SetNotFound(true);

				Results.SetValue("Bottom", result);
				Queried.SetValue("Bottom", true);
				return result;
			}
		}
		public Hex BottomLeft { 
			get {
				Vector3d query = HexNeighbors.BottomLeftOf(Coordinates);
				if (Queried.ContainsKey("BottomLeft"))
					return Results.Value("BottomLeft");
				Hex result = null;
				if (!Grid.TryFind(query, out result))
					result = new Hex(Grid, query).SetNotFound(true);

				Results.SetValue("BottomLeft", result);
				Queried.SetValue("BottomLeft", true);
				return result;
			}
		}
		public Hex Left {
			get {
				Vector3d query = HexNeighbors.LeftOf(Coordinates);
				if (Queried.ContainsKey("Left"))
					return Results.Value("Left");
				Hex result = null;
				if (!Grid.TryFind(query, out result))
					result = new Hex(Grid, query).SetNotFound(true);

				Results.SetValue("Left", result);
				Queried.SetValue("Left", true);
				return result;
			}
		}
		public Hex Right {
			get {
				Vector3d query = HexNeighbors.RightOf(Coordinates);
				if (Queried.ContainsKey("Right"))
					return Results.Value("Right");
				Hex result = null;
				if (!Grid.TryFind(query, out result))
					result = new Hex(Grid, query).SetNotFound(true);

				Results.SetValue("Right", result);
				Queried.SetValue("Right", true);
				return result;
			}
		}

		public static MauronCode_dataList<string> Locations = new MauronCode_dataList<string>() { "Self", "TopRight", "Top", "TopLeft", "BottomRight", "Bottom", "BottomLeft", "Left", "Right" };

		public bool IsQueried {
			get {
				foreach (string key in HexQuery.Locations)
					if (!Queried.ContainsKey(key))
						return false;

				return true;
			}
		}
		public bool HasQueried(string location) {
			return Queried.ContainsKey(location);
		}
	
	}
}
