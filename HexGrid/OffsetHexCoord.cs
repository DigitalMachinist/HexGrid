using System;
using System.Linq;

namespace ca.axoninteractive.Geometry.HexGrid
{
	// Assumes pointy-topped, odd-row offset hex grid layout.
	public struct OffsetHexCoord
	{
		#region Members

		public int q;
		public int r;

		#endregion
		#region Constants

		// [ PARITY, DIRECTION ]
		private static readonly OffsetHexCoord[,] DIRECTIONS = {
			{
				// Even-parity rows
				new OffsetHexCoord(  0,  1 ),
				new OffsetHexCoord(  1,  0 ),
				new OffsetHexCoord(  1, -1 ),
				new OffsetHexCoord(  0, -1 ),
				new OffsetHexCoord( -1, -1 ),
				new OffsetHexCoord( -1,  0 )
			},
			{
				// Odd-parity rows
				new OffsetHexCoord(  0,  1 ),
				new OffsetHexCoord(  1,  1 ),
				new OffsetHexCoord(  1,  0 ),
				new OffsetHexCoord(  0, -1 ),
				new OffsetHexCoord( -1,  0 ),
				new OffsetHexCoord( -1,  1 )
			}
		};

		#endregion
		#region Properties

		public bool IsOdd { get { return ( this.Parity == 1 ); } }
		public int Parity { get { return ( this.r & 1 ); } }

		#endregion
		#region Constructors

		public OffsetHexCoord( int q, int r )
		{
			this.q = q;
			this.r = r;
		}

		#endregion
		#region Type Conversions

		public AxialHexCoord ToAxial()
		{
			return this.ToCubic().ToAxial();
		}

		public CubicHexCoord ToCubic()
		{
			int x = this.q - ( this.r - ( this.r & 1 ) ) / 2;
			int z = this.r;
			int y = -x - z;

			return new CubicHexCoord( x, y, z );
		}

		#endregion
		#region Operator Overloads
		
		public static OffsetHexCoord operator +( OffsetHexCoord lhs, OffsetHexCoord rhs ) 
		{
			int q = lhs.q + rhs.q;
			int r = lhs.r + rhs.r;

			return new OffsetHexCoord( q, r );
		}

		public static OffsetHexCoord operator -( OffsetHexCoord lhs, OffsetHexCoord rhs ) 
		{
			int q = lhs.q - rhs.q;
			int r = lhs.r - rhs.r;

			return new OffsetHexCoord( q, r );
		}

		#endregion
	}
}
