using System;
using System.Linq;

namespace ca.axoninteractive.Geometry.HexGrid
{
	public struct AxialHexCoord
	{
		#region Members

		public int q;
		public int r;

		#endregion
		#region Constants

		private static readonly AxialHexCoord[] DIRECTIONS = {
			new AxialHexCoord(  1,  0 ),
			new AxialHexCoord(  0,  1 ),
			new AxialHexCoord( -1,  1 ),
			new AxialHexCoord( -1,  0 ),
			new AxialHexCoord(  0, -1 ),
			new AxialHexCoord(  1, -1 ),
		};

		#endregion
		#region Constructors

		public AxialHexCoord( int q, int r ) 
		{
			this.q = q;
			this.r = r;
		}

		#endregion
		#region Type Conversions

		public CubicHexCoord ToCubic()
		{
			int x = this.q;
			int z = this.r;
			int y = -x - z;

			return new CubicHexCoord( x, y, z );
		}

		public OffsetHexCoord ToOffset()
		{
			return this.ToCubic().ToOffset();
		}

		#endregion
		#region Operator Overloads

		public static AxialHexCoord operator +( AxialHexCoord lhs, AxialHexCoord rhs ) 
		{
			int q = lhs.q + rhs.q;
			int r = lhs.r + rhs.r;

			return new AxialHexCoord( q, r );
		}

		public static AxialHexCoord operator -( AxialHexCoord lhs, AxialHexCoord rhs ) 
		{
			int q = lhs.q - rhs.q;
			int r = lhs.r - rhs.r;

			return new AxialHexCoord( q, r );
		}

		#endregion
	}
}
