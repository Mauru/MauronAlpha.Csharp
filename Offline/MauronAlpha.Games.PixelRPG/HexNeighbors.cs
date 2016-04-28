using MauronAlpha.GameEngine;
using MauronAlpha.Geometry;

using System;

namespace MauronAlpha.Games.PixelRPG {

    // neighbor/conversion table for Axial Grid (with flat-topped hexes)
    public static class HexNeighborsAxial : Object {

        //neighbor offset by Direction
        public static Vector2d Self=new Vector2d(0,0);
        public static Vector2d Top=new Vector2d(0,-1);
        public static Vector2d TopRight=new Vector2d(1,-1);
        public static Vector2d Right=new Vector2d(2,-1);
        public static Vector2d BottomRight=new Vector2d(1,0);
        public static Vector2d Bottom=new Vector2d(0,1);
        public static Vector2d BottomLeft=new Vector2d(-1,1);
        public static Vector2d Left=new Vector2d(-2,1);
        public static Vector2d TopLeft=new Vector2d(-1,0);

        //Neighbor offset Relative
        public static Vector2d self(Vector2d v){
            return v.Instance.Add(Self);
        }
        public static Vector2d top(Vector2d v) {
            return v.Instance.Add(Top);
        }
        public static Vector2d topRight(Vector2d v) {
            return v.Instance.Add(TopRight);
        }
        public static Vector2d right(Vector2d v) {
            return v.Instance.Add(Right);
        }
        public static Vector2d bottomRight(Vector2d v) {
            return v.Instance.Add(BottomRight);
        }
        public static Vector2d bottom(Vector2d v) {
            return v.Instance.Add(Bottom);
        }
        public static Vector2d bottomLeft(Vector2d v) {
            return v.Instance.Add(BottomLeft);
        }
        public static Vector2d left(Vector2d v) {
            return v.Instance.Add(Left);
        }
        public static Vector2d topLeft(Vector2d v) {
            return v.Instance.Add(TopLeft);
        }

        //By string
        public static Vector2d Get(string str) {
            switch (str.ToLower()) {
                case"top":
                    return Top;
                case"topright":
                    return TopRight;
                case"right":
                    return Right;
                case"bottomright":
                    return BottomRight;
                case"bottom":
                    return Bottom;
                case"bottomleft":
                    return BottomLeft;
                case"left":
                    return Left;
                case"topleft":
                    return TopLeft;
            }
            return Self;
        }
        //By string, relative
        public static Vector2d Get(Vector2d v,string str){
            return v.Instance.Add(Get(str));
        }

        //conversion
        public static Vector2d ToEvenQ(Vector2d v) {
            Vector3d t = HexNeightborsCube.FromAxial(v);
            return HexNeightborsCube.ToEvenQ(t);
        }
        public static Vector2d FromEvenQ(Vector2d v) {
            Vector3d t = HexNeightborsCube.FromEvenQ(v);
            return HexNeightborsCube.ToAxial(t);
        }
        public static Vector2d FromCube(Vector3d v) {
            return HexNeightborsCube.ToAxial(v);
        }
        public static Vector3d ToCube(Vector2d v) {
            return HexNeightborsCube.FromAxial(v);
        }

    }
    // neighbor/conversion table for EvenQ Grid (with flat-topped hexes)
    public static class HexNeightborsEvenQ : Object  {
        //neighbor offset by Direction
        public static Vector2d Self = new Vector2d(0, 0);
        public static Vector2d Top = new Vector2d(0, -1);
        public static Vector2d TopRight = new Vector2d(1, -1);
        public static Vector2d Right = new Vector2d(2, 0);
        public static Vector2d BottomRight = new Vector2d(1, 0);
        public static Vector2d Bottom = new Vector2d(0, 1);
        public static Vector2d BottomLeft = new Vector2d(-1, 0);
        public static Vector2d Left = new Vector2d(-2, 0);
        public static Vector2d TopLeft = new Vector2d(-1, -1);

        //Neighbor offset Relative
        public static Vector2d self(Vector2d v) {
            return v.Instance.Add(Self);
        }
        public static Vector2d top(Vector2d v) {
            return v.Instance.Add(Top);
        }
        public static Vector2d topRight(Vector2d v) {
            return v.Instance.Add(TopRight);
        }
        public static Vector2d right(Vector2d v) {
            return v.Instance.Add(Right);
        }
        public static Vector2d bottomRight(Vector2d v) {
            return v.Instance.Add(BottomRight);
        }
        public static Vector2d bottom(Vector2d v) {
            return v.Instance.Add(Bottom);
        }
        public static Vector2d bottomLeft(Vector2d v) {
            return v.Instance.Add(BottomLeft);
        }
        public static Vector2d left(Vector2d v) {
            return v.Instance.Add(Left);
        }
        public static Vector2d topLeft(Vector2d v) {
            return v.Instance.Add(TopLeft);
        }

        //By string
        public static Vector2d Get(string str) {
            switch (str.ToLower()) {
                case "top":
                    return Top;
                case "topright":
                    return TopRight;
                case "right":
                    return Right;
                case "bottomright":
                    return BottomRight;
                case "bottom":
                    return Bottom;
                case "bottomleft":
                    return BottomLeft;
                case "left":
                    return Left;
                case "topleft":
                    return TopLeft;
            }
            return Self;
        }
        //By string, relative
        public static Vector2d Get(Vector2d v, string str) {
            return v.Instance.Add(Get(str));
        }

        //conversion
        public static Vector2d ToAxial(Vector2d v) {
            Vector3d t = HexNeightborsCube.FromEvenQ(v);
            return HexNeightborsCube.ToAxial(t);
        }
        public static Vector2d FromAxial(Vector2d v) {
            Vector3d t = HexNeightborsCube.FromAxial(v);
            return HexNeightborsCube.ToEvenQ(t);
        }
        public static Vector2d FromCube(Vector3d v) {
            return HexNeightborsCube.ToEvenQ(v);
        }
        public static Vector3d ToCube(Vector2d v) {
            return HexNeightborsCube.FromEvenQ(v);
        }    
    }
    // neighbor/conversion table for Cube Grid (with flat-topped hexes)
    public static class HexNeightborsCube : Object {
        //neighbor offset by Direction
        public static Vector3d Self = new Vector3d(0, 0, 0);
        public static Vector3d Top = new Vector3d(0, 1,-1);
        public static Vector3d TopRight = new Vector3d(1, 0,-1);
        public static Vector3d Right = new Vector3d(2,-1, -1);
        public static Vector3d BottomRight = new Vector3d(1, -1, 0);
        public static Vector3d Bottom = new Vector3d(0, -1, 1);
        public static Vector3d BottomLeft = new Vector3d(-1, 0, 1);
        public static Vector3d Left = new Vector3d(-2, 1, 1);
        public static Vector3d TopLeft = new Vector3d(-1, 1, 0);

        //Neighbor offset Relative
        public static Vector3d self(Vector3d v)
        {
            return v.Instance.Add(Self);
        }
        public static Vector3d top(Vector3d v)
        {
            return v.Instance.Add(Top);
        }
        public static Vector3d topRight(Vector3d v)
        {
            return v.Instance.Add(TopRight);
        }
        public static Vector3d right(Vector3d v)
        {
            return v.Instance.Add(Right);
        }
        public static Vector3d bottomRight(Vector3d v)
        {
            return v.Instance.Add(BottomRight);
        }
        public static Vector3d bottom(Vector3d v)
        {
            return v.Instance.Add(Bottom);
        }
        public static Vector3d bottomLeft(Vector3d v)
        {
            return v.Instance.Add(BottomLeft);
        }
        public static Vector3d left(Vector3d v)
        {
            return v.Instance.Add(Left);
        }
        public static Vector3d topLeft(Vector3d v) {
            return v.Instance.Add(TopLeft);
        }

        //By string
        public static Vector3d Get(string str) {
            switch (str.ToLower()) {
                case "top":
                    return Top;
                case "topright":
                    return TopRight;
                case "right":
                    return Right;
                case "bottomright":
                    return BottomRight;
                case "bottom":
                    return Bottom;
                case "bottomleft":
                    return BottomLeft;
                case "left":
                    return Left;
                case "topleft":
                    return TopLeft;
            }
            return Self;
        }
        //By string, relative
        public static Vector3d Get(Vector3d v, string str) {
            return v.Instance.Add(Get(str));
        }
        
        //conversion to other systems
        public static Vector2d ToAxial(Vector3d v) {
            return new Vector2d(v.X, v.Z);
        }
        public static Vector3d FromAxial(Vector2d v) {
            Vector3d r = new Vector3d(v.X, 0, v.Y);
            r.Y = (-r.X - r.Z);
            return r;
        }
        public static Vector2d ToEvenQ(Vector3d v){
            int t = Convert.ToInt32(v.X) & 1;
            return new Vector2d(v.X, v.Z+(v.X+(t))/2);
        }
        public static Vector3d FromEvenQ(Vector2d v){
            Vector3d r = new Vector3d(v.X, 0, 0);
            int t = Convert.ToInt32(v.X) & 1;
            r.Z = v.Y - (v.X + t) / 2;
            r.Y = (-r.X - r.Z);
            return r;
        }

    }

    //a Neighbormap which stores Hex Neighbors
    public class HexFieldNeighbors : GameComponent {
        public HexGrid Grid;
		
		//The related Field
		protected HexField hf_self;

        public HexField Self {
            get {
                return hf_self;
            }
        }
        public HexField Top {
            get {
                Vector2d c = HexNeighborsAxial.top(Self.Coordinates);
                return Grid.FieldByAxial(c);
            } 
        }
        public HexField TopRight {
            get {
                Vector2d c = HexNeighborsAxial.topRight(Self.Coordinates);
                return Grid.FieldByAxial(c);
            }
        }
        public HexField Right {
            get {
                Vector2d c = HexNeighborsAxial.right(Self.Coordinates);
                return Grid.FieldByAxial(c);
            }
        }
        public HexField BottomRight {
            get {
                Vector2d c = HexNeighborsAxial.bottomRight(Self.Coordinates);
                return Grid.FieldByAxial(c);
            }
        }
        public HexField Bottom {
            get {
                Vector2d c = HexNeighborsAxial.bottom(Self.Coordinates);
                return Grid.FieldByAxial(c);
            }
        }
        public HexField BottomLeft {
            get {
                Vector2d c = HexNeighborsAxial.bottomLeft(Self.Coordinates);
                return Grid.FieldByAxial(c);
            }
        }
        public HexField Left {
            get {
                Vector2d c = HexNeighborsAxial.left(Self.Coordinates);
                return Grid.FieldByAxial(c);
            }
        }
        public HexField TopLeft {
            get {
                Vector2d c = HexNeighborsAxial.topLeft(Self.Coordinates);
                return Grid.FieldByAxial(c);
            }
        }

		public HexFieldNeighbors(HexGrid grid, HexField self) {
            Grid = grid;
            hf_self = self;
        }
    }

}
