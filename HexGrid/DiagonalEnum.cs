using System;

namespace ca.axoninteractive.Geometry.Hex
{
	/// <summary>
	/// Represents a diagonal hex (see http://www.redblobgames.com/grids/hexagons/), relative to
	/// a central pointy-topped hex. Note that cardinal directions are used here to make these 
	/// easier to distinguish.
	/// </summary>
	public enum DiagonalEnum : int
	{
		ESE = 0,
		S   = 1,
		WSW = 2, 
		WNW = 3, 
		N   = 4,
		ENE = 5
	}
}
