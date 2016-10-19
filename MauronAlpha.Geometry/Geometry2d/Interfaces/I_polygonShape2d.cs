using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;

using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Shapes;
using MauronAlpha.Geometry.Geometry2d.Transformation;

//namespace
namespace MauronAlpha.Geometry.Geometry2d.Interfaces {
    
/*=========================================================================
Interface for all Polygon-based Shapes in 2d Space
=========================================================================*/
public interface I_polygonShape2d {

	string Id { get; }

	//Get the current Points a Shape has
	Vector2dList Points { get; }

	//Get a Representation of a Shape as simple shape
	Polygon2dBounds Bounds { get; }
	void SetBounds(Polygon2dBounds bounds);

	Vector2d Center { get; }
	Segment2dList Segments { get; }

	//Equality checker
	bool Equals( I_polygonShape2d other );


	//Instancing
	I_polygonShape2d Copy { get; }

}

}