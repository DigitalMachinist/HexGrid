using System;
using System.Linq;

namespace ca.axoninteractive.Geometry.Hex
{
	/// <summary>
	/// A logical hex-grid implementation based on Amit Patel's examples at 
	/// http://www.redblobgames.com/grids/hexagons/. Creating a new hex grid does not create an
	/// array in memory or any other concrete data. It provides a service to query for data such as
	/// what 2D point at which a given hex index appears at or which hex a given 2D point is over.
	///
	/// Coordinate Systems
	/// ==================
	/// 
	/// See http://www.redblobgames.com/grids/hexagons/ for a complete explanation of Axial, Cubic 
	/// and Offset coordinate spaces. This hex grid implementation orients hexes in a pointy-topped 
	/// odd-row offset configuration, and otherwise uses the same axes and conventions as those in 
	/// Amit Patel's examples.
	///
	/// It should be noted that when converting from point to hex space, the x-axis is the 
	/// horizontal axis with values increasing from left to right, and the y-axis is the vertical 
	/// axis with values increasing from top to bottom (POSITIVE Y IS DOWNWARD).
	/// </summary>
    public class HexGrid
    {
		#region Members

		private float __hexRadius;
		private Vec2D __slice;

		#endregion


		#region Constants

		public const float SQRT_3     = 1.7320508075688772935274463415059f;
		public const float ONE_THIRD  = 1f / 3f;
		public const float TWO_THIRDS = 2f / 3f;
		
		#endregion


		#region Properties

		/// <summary>
		/// The distance from the center of a hex to one of its vertices.
		/// </summary>
		public float HexRadius { 
			get { return __hexRadius; } 
		}

		/// <summary>
		/// The dimensions of a single "slice" of a hex, where each hex is bisected horizontally 
		/// and then quartered vertically to produce 8 equally-sized slices.
		/// </summary>
		public Vec2D Slice {
			get { return __slice; }
		}

		#endregion


		#region Constructors

		/// <summary>
		/// Create a new HexGrid given the radius of all hexes within the grid.
		/// </summary>
		/// <param name="hexRadius">The distance from the center of any given hex to one of its
		/// vertices.</param>
		public 
		HexGrid( float hexRadius )
		{
			__hexRadius = hexRadius;
			__slice = new Vec2D( 0.5f * SQRT_3 * HexRadius, 0.5f * HexRadius );
		}

		#endregion


		#region Instance Methods

		/// <summary>
		/// Get the point on the x-z cartesian plane where the center of the given hex is located.
		/// </summary>
		/// <param name="hex">A hex position, represented by an AxialHexCoord.</param>
		/// <returns>A Vec2D point of the hex's position on the x-y cartesian plane.</returns>
		public 
		Vec2D 
		AxialToPoint( AxialHexCoord hex )
		{
			float x = HexRadius * SQRT_3 * ( hex.q + 0.5f * hex.r );
			float y = HexRadius * 1.5f * hex.r;

			return new Vec2D( x, y );
		}
		
		/// <summary>
		/// Get the point on the x-z cartesian plane where the center of the given hex is located.
		/// </summary>
		/// <param name="hex">A hex position, represented by an OffsetHexCoord.</param>
		/// <returns>A Vec2D point of the hex's position on the x-y cartesian plane.</returns>
		public 
		Vec2D 
		OffsetToPoint( OffsetHexCoord hex )
		{
			float x = HexRadius * SQRT_3 * ( hex.q + 0.5f * (float)hex.RowParity );
			float y = HexRadius * 1.5f * hex.r;

			return new Vec2D( x, y );
		}
		

		/// <summary>
		/// Get the hex that contains the given point on the x-z cartesian plane.
		/// </summary>
		/// <param name="point">A point on the x-y cartesian plane.</param>
		/// <returns>A CubicHexCoord representation of the grid position of the hex that the point
		/// is contained by.</returns>
		public 
		CubicHexCoord 
		PointToCubic( Vec2D point )
		{
			// This function exhibited one of the nastiest bugs I've ever come across in my career 
			// thanks to a subtle order or bracketing error resulting in bad order of operations.
			//
			// The offending line of code:
			// float q = ( point.x * ( SQRT_3 - point.y ) * ONE_THIRD ) / HexRadius;
			//
			// evaluates to:
			// float q = ( point.x * ONE_THIRD * SQRT_3 - point.x * point.y * ONE_THIRD ) / HexRadius;
			//
			// Resulting is a weird distortion on the coordinate space because the x and y 
			// coordinates of the input point were getting multiplied together in the 2nd term!
			//
			// The correct version of the code is now implemented below -- after 2 solid days of 
			// meticulous and demoralizing debugging!!!

			float q = ( point.x * ONE_THIRD * SQRT_3 - point.y * ONE_THIRD ) / HexRadius;
			float r = ( point.y * TWO_THIRDS ) / HexRadius;

			return new FloatAxial( q, r ).ToFloatCubic().Round();
		}


		/// <summary>
		/// Returns a DirectionEnum identifying the triangle by which the given point is contained.
		/// </summary>
		/// <param name="point">A point on the x-y cartesian plane.</param>
		/// <returns>A DirectionEnum representing the direction of the triangle that the point is 
		/// contained by within the hex (relative to the center of the hex).</returns>
		public 
		DirectionEnum
		PointToDirectionInHex( Vec2D point )
		{
			AxialHexCoord axial = PointToCubic( point ).ToAxial();
			//AxialHexCoord axial = PointToAxial( point );

			Vec2D center = AxialToPoint( axial );
			Vec2D topLeft = center - new Vec2D( 0.5f * SQRT_3 * HexRadius, HexRadius );
			Vec2D fromTopLeft = point - topLeft;
			
			int hSlice = (int)Math.Floor( fromTopLeft.x / Slice.x );
			int vSlice = (int)Math.Floor( fromTopLeft.y / Slice.y );

			Vec2D withinSlice = new Vec2D(
				fromTopLeft.x % Slice.x,
				fromTopLeft.y % Slice.y
			);

			ParityEnum parity = ParityEnum.Even;
			if ( ( ( hSlice & 1 ) + ( vSlice & 1 ) ) == 1 )
			{
				// One of the two dimensions is odd, otherwise parity is even by default.
				parity = ParityEnum.Odd;
			}
			
			//
			// Debugging output associated with a horrible bug in PointToCubic()
			//
			//Console.WriteLine( "Point: x: " + point.x + ", y: " + point.y );
			//Console.WriteLine( "Axial: q: " + axial.q + ", r: " + axial.r );
			//Console.WriteLine( "Center: x: " + center.x + ", y: " + center.y );
			//Console.WriteLine( "Top Left: x: " + topLeft.x + ", y: " + topLeft.y );
			//Console.WriteLine( "From Top Left: x: " + fromTopLeft.x + ", y: " + fromTopLeft.y );
			//Console.WriteLine( "Slice: x: " + Slice.x + ", y: " + Slice.y );
			//Console.WriteLine( "Slice Index: x: " + hSlice + ", y: " + vSlice );
			//Console.WriteLine( "Within Slice: x: " + withinSlice.x + ", y: " + withinSlice.y );
			//Console.WriteLine( "Parity: " + parity );

			// Compute and return the triangle by which the point is contained.
			bool isInvalid = false;
			DirectionEnum direction = DirectionEnum.E;
			TriangleEnum triangle = WhichTriangle( withinSlice, parity );


			if ( hSlice == 0 )
			{
				switch ( vSlice )
				{
					case 0:
					if ( triangle == TriangleEnum.Bottom )
					{
						direction = DirectionEnum.NW;
					}
					else
					{
						isInvalid = true;
					}
					break;
					
					case 1:
					direction = ( triangle == TriangleEnum.Top ) ? DirectionEnum.NW : DirectionEnum.W;
					break;

					case 2:
					direction = ( triangle == TriangleEnum.Top ) ? DirectionEnum.W  : DirectionEnum.SW;
					break;

					case 3:
					if ( triangle == TriangleEnum.Top )
					{
						direction = DirectionEnum.SW;
					}
					else
					{
						isInvalid = true;
					}
					break;

					default:
					isInvalid = true;
					break;
				}
			}
			else // hSlice == 1
			{
				switch ( vSlice )
				{
					case 0:
					if ( triangle == TriangleEnum.Bottom )
					{
						direction = DirectionEnum.NE;
					}
					else
					{
						isInvalid = true;
					}
					break;

					case 1:
					direction = ( triangle == TriangleEnum.Top ) ? DirectionEnum.NE : DirectionEnum.E;
					break;

					case 2:
					direction = ( triangle == TriangleEnum.Top ) ? DirectionEnum.E  : DirectionEnum.SE;
					break;

					case 3:
					if ( triangle == TriangleEnum.Top )
					{
						direction = DirectionEnum.SE;
					}
					else
					{
						isInvalid = true;
					}
					break;

					default:
					isInvalid = true;
					break;
				}
			}
			
			//
			// Debugging output associated with a horrible bug in PointToCubic()
			//
			//Console.WriteLine( "Triangle: " + triangle );
			//Console.WriteLine( "Direction: " + direction );
			//Console.WriteLine( "" );
			//Console.WriteLine( "" );

			if ( isInvalid )
			{ 
				throw new InvalidOperationException( "This should never happen!" );
			}
			else
			{
				return direction;
			}
		}

		
		/// <summary>
		/// Returns a TriangleEnum representing the triangle (within the current slice, either 
		/// top or bottom, that the point belongs to).
		/// </summary>
		/// <param name="withinSlice">A point on the x-z cartesian plane, relative to the top-left 
		/// of the current slice.</param>
		/// <param name="parity">The "parity" of the slice, indicating wither the border seaparting 
		/// the two triangles within the slice goes from top-left to bottom-right (ODD) or 
		/// bottom-left to top-right (EVEN).</param>
		/// <returns>A TriangleEnum representing the triangle (within the current slice, either 
		/// top or bottom, that the point belongs to).</returns>
		private TriangleEnum WhichTriangle( Vec2D withinSlice, ParityEnum parity )
		{
			float xFraction = withinSlice.x / Slice.x;
			float yBorder = 0f;

			if ( parity == ParityEnum.Even )
			{
				// When parity is even, the border is bottom-left to top-right.
				yBorder = Slice.y * xFraction;
			}
			else
			{
				// When parity is odd, the border is top-left to bottom-right.
				yBorder = Slice.y * ( 1f - xFraction );
			}

			return ( withinSlice.y < yBorder ) ? TriangleEnum.Top : TriangleEnum.Bottom;
		}

		#endregion
    }
}
