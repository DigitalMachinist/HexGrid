using System;

namespace ca.axoninteractive.Geometry.Hex
{
	/// <summary>
	/// Represents the row-parity of an OffsetHexCoord (although it can represent the parity of 
	/// anything, really).
	/// </summary>
	public enum ParityEnum : int
	{
		Even = 0,
		Odd  = 1
	}
}
