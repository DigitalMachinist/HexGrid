using System;

namespace ca.axoninteractive.Geometry.Hex
{
	/// <summary>
	/// Represents directionality in the context of a pointy-topped hexagon's faces, where each
	/// direction points from the center of the hex and faces perpendicularly to the face that 
	/// it represents (bisecting that face and pointing outward directly through its middle).
	/// Note that cardinal directions are used here to make these easier to distinguish.
	/// </summary>
	public enum DirectionEnum : int
	{
		E  = 0, 
		SE = 1, 
		SW = 2, 
		W  = 3, 
		NW = 4, 
		NE = 5
	}
}
