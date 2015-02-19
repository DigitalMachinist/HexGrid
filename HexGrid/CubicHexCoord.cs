using System;
using System.Linq;

namespace ca.axoninteractive.Geometry.HexGrid
{
	public struct CubicHexCoord
	{
		#region Members

		public int x;
		public int y;
		public int z;

		#endregion
		#region Constants

		private static readonly CubicHexCoord[] DIAGONALS = {
			new CubicHexCoord(  1, -2,  1 ),
			new CubicHexCoord( -1, -1,  2 ),
			new CubicHexCoord( -2,  1,  1 ),
			new CubicHexCoord( -1,  2, -1 ),
			new CubicHexCoord(  1,  1, -2 ),
			new CubicHexCoord(  2, -1, -1 )
		};

		private static readonly CubicHexCoord[] DIRECTIONS = {
			new CubicHexCoord(  1, -1,  0 ),
			new CubicHexCoord(  0, -1,  1 ),
			new CubicHexCoord( -1,  0,  1 ),
			new CubicHexCoord( -1,  1,  0 ),
			new CubicHexCoord(  0,  1, -1 ),
			new CubicHexCoord(  1,  0, -1 )
		};

		#endregion
		#region Constructors

		public CubicHexCoord( int x, int y, int z ) 
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		#endregion
		#region Type Conversions

		public AxialHexCoord ToAxial()
		{
			int q = this.x;
			int r = this.z;

			return new AxialHexCoord( q, r );
		}

		public OffsetHexCoord ToOffset()
		{
			int q = this.x + ( this.z - ( this.z & 1 ) ) / 2;
			int r = this.z;

			return new OffsetHexCoord( q, r );
		}

		#endregion
		#region Operator Overloads

		public static CubicHexCoord operator +( CubicHexCoord lhs, CubicHexCoord rhs ) 
		{
			int x = lhs.x + rhs.x;
			int y = lhs.y + rhs.y;
			int z = lhs.z + rhs.z;

			return new CubicHexCoord( x, y, z );
		}

		public static CubicHexCoord operator -( CubicHexCoord lhs, CubicHexCoord rhs ) 
		{
			int x = lhs.x + rhs.x;
			int y = lhs.y + rhs.y;
			int z = lhs.z + rhs.z;

			return new CubicHexCoord( x, y, z );
		}

		#endregion
		#region Instance Methods

		public CubicHexCoord Diagonal( DiagonalEnum direction )
		{
			return this + DIAGONALS[ (int)direction ];
		}

		public CubicHexCoord[] Diagonals()
		{
			return new CubicHexCoord[ 6 ] {
				this + DIAGONALS[ (int)DiagonalEnum.ESE ], 
				this + DIAGONALS[ (int)DiagonalEnum.S   ], 
				this + DIAGONALS[ (int)DiagonalEnum.WSW ], 
				this + DIAGONALS[ (int)DiagonalEnum.WNW ], 
				this + DIAGONALS[ (int)DiagonalEnum.N   ], 
				this + DIAGONALS[ (int)DiagonalEnum.ENE ]
			};
		}

		public int DistanceTo( CubicHexCoord other )
		{
			return CubicHexCoord.Distance( this, other );
		}

		public CubicHexCoord[] LineTo( CubicHexCoord other )
		{
			return CubicHexCoord.Line( this, other );
		}

		public CubicHexCoord Neighbor( DirectionEnum direction )
		{
			return this + DIRECTIONS[ (int)direction ];
		}

		public CubicHexCoord[] Neighbors()
		{
			return new CubicHexCoord[ 6 ] {
				this + DIRECTIONS[ (int)DirectionEnum.E  ], 
				this + DIRECTIONS[ (int)DirectionEnum.SE ], 
				this + DIRECTIONS[ (int)DirectionEnum.SW ], 
				this + DIRECTIONS[ (int)DirectionEnum.W  ], 
				this + DIRECTIONS[ (int)DirectionEnum.NW ], 
				this + DIRECTIONS[ (int)DirectionEnum.NE ]
			};
		}
		
		public CubicHexCoord[] RingAsCenter( int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			return CubicHexCoord.Ring( this, range, startDirection );
		}

		public CubicHexCoord RotateAsCenter( CubicHexCoord toRotate, RotationEnum rotation )
		{
			return CubicHexCoord.Rotate( this, toRotate, rotation );
		}

		public CubicHexCoord Scale( int factor )
		{
			return CubicHexCoord.Scale( this, factor );
		}

		public CubicHexCoord[] SpiralInwardToCenter( int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			return CubicHexCoord.SpiralInward( this, range, startDirection );
		}

		public CubicHexCoord[] SpiralOutwardFromCenter( int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			return CubicHexCoord.SpiralOutward( this, range, startDirection );
		}

		#endregion
		#region Static Methods

		public static CubicHexCoord[] Area( CubicHexCoord center, int range )
		{
			if ( range < 0 )
			{
				throw new ArgumentOutOfRangeException( "range must be a non-negative integer value." );
			}
			else if ( range == 0 )
			{
				return new CubicHexCoord[ 1 ] { center };
			}

			CubicHexCoord[] result = new CubicHexCoord[ 2 * range + 1 ];

			for ( int i = 0, dx = -range; dx <= range; dx++ )
			{
				int dyMinBound = Math.Max( -range, -dx - range );
				int dyMaxBound = Math.Max(  range, -dx + range );

				for ( int dy = dyMinBound; dy <= dyMaxBound; i++, dy++ )
				{
					int dz = -dx - dy;
					result[ i ] = center + new CubicHexCoord( dx, dy, dz );
				}
			}

			return result;
		}

		public static CubicHexCoord DiagonalDiff( DiagonalEnum direction )
		{
			return DIAGONALS[ (int)direction ];
		}

		public static CubicHexCoord DirectionDiff( DirectionEnum direction )
		{
			return DIRECTIONS[ (int)direction ];
		}

		public static CubicHexCoord[] IntersectRanges( CubicHexCoord[] a, CubicHexCoord[] b )
		{
			// See http://www.redblobgames.com/grids/hexagons/ (Heading: Intersecting ranges)
			throw new NotImplementedException( "Feature not suppored yet!" );
		}

		public static int Distance( CubicHexCoord a, CubicHexCoord b )
		{
			int dx = Math.Abs( a.x - b.x );
			int dy = Math.Abs( a.y - b.y );
			int dz = Math.Abs( a.z - b.z );

			return Math.Max( Math.Max( dx, dy ), dz );
		}

		public static CubicHexCoord[] Line( CubicHexCoord start, CubicHexCoord end )
		{
			int distance = CubicHexCoord.Distance( start, end );

			CubicHexCoord[] result = new CubicHexCoord[ distance ];

			for ( int i = 0; i <= distance; i++ )
			{
				float xLerp = start.x + ( end.x - start.x ) * 1f / distance * i;
				float yLerp = start.y + ( end.y - start.y ) * 1f / distance * i;
				float zLerp = start.z + ( end.z - start.z ) * 1f / distance * i;

				result[ i ] = CubicHexCoord.Round( new FloatCubic( xLerp, yLerp, zLerp ) );
			}

			return result;
		}

		public static CubicHexCoord[] Ring( CubicHexCoord center, int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			if ( range <= 0 )
			{
				throw new ArgumentOutOfRangeException( "range must be a positive integer value." );
			}
			
			CubicHexCoord[] result = new CubicHexCoord[ 6 * range ];

			CubicHexCoord cube = center + CubicHexCoord.Scale( DIRECTIONS[ (int)startDirection ], range );

			for ( int i = 0; i < 6; i++ )
			{
				for ( int j = 0; j < range; j++ )
				{
					result[ 6 * i + j ] = cube;
					cube = cube.Neighbor( (DirectionEnum)i );
				}
			}

			return result;
		}

		public static CubicHexCoord Rotate( CubicHexCoord center, CubicHexCoord toRotate, RotationEnum rotation )
		{
			// See http://www.redblobgames.com/grids/hexagons/ (Heading: Rotation)
			throw new NotImplementedException( "Feature not suppored yet!" );
		}

		public static CubicHexCoord Round( FloatCubic cubic )
		{
			int rx = (int)Math.Round( cubic.x );
			int ry = (int)Math.Round( cubic.y );
			int rz = (int)Math.Round( cubic.z );

			int xDiff = (int)Math.Abs( rx - cubic.x );
			int yDiff = (int)Math.Abs( ry - cubic.y );
			int zDiff = (int)Math.Abs( rz - cubic.z );

			if ( xDiff > yDiff && xDiff > zDiff )
			{
				rx = -ry - rz;
			}
			else if ( yDiff > zDiff )
			{
				ry = -rx - rz;
			}
			else
			{
				rz = -rx - ry;
			}

			return new CubicHexCoord( rx, ry, rz );
		}

		public static CubicHexCoord Scale( CubicHexCoord toScale, int factor )
		{
			return new CubicHexCoord(
				toScale.x * factor,
				toScale.y * factor,
				toScale.z * factor
			);
		}

		public static CubicHexCoord[] SpiralInward( CubicHexCoord center, int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			if ( range <= 0 )
			{
				throw new ArgumentOutOfRangeException( "range must be a positive integer value." );
			}
			else if ( range == 0 )
			{
				return new CubicHexCoord[ 1 ] { center };
			}

			int arraySize = 1;
			for ( int i = range; i > 0; i++ )
			{
				arraySize += 6 * i;
			}

			CubicHexCoord[] result = new CubicHexCoord[ arraySize ];

			result[ result.Length - 1 ] = center;

			int arrayIndex = result.Length - 1;
			for ( int i = range; i >= 1; i-- ) {
				CubicHexCoord[] ring = CubicHexCoord.Ring( center, i, startDirection );
				arrayIndex -= ring.Length;
				ring.CopyTo( result, arrayIndex );
			}
				
			return result;
		}

		public static CubicHexCoord[] SpiralOutward( CubicHexCoord center, int range, DirectionEnum startDirection = DirectionEnum.E )
		{
			if ( range <= 0 )
			{
				throw new ArgumentOutOfRangeException( "range must be a positive integer value." );
			}
			else if ( range == 0 )
			{
				return new CubicHexCoord[ 1 ] { center };
			}

			int arraySize = 1;
			for ( int i = range; i > 0; i++ )
			{
				arraySize += 6 * i;
			}

			CubicHexCoord[] result = new CubicHexCoord[ arraySize ];

			result[ 0 ] = center;

			int arrayIndex = 1;
			for ( int i = 1; i <= range; i++ ) {
				CubicHexCoord[] ring = CubicHexCoord.Ring( center, i, startDirection );
				ring.CopyTo( result, arrayIndex );
				arrayIndex += ring.Length;
			}
				
			return result;
		}

		#endregion
	}
}
