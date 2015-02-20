using System;
using System.Linq;

namespace ca.axoninteractive.Geometry.Hex
{
	/// <summary>
	/// A logical hex-grid implementation based on Amit Patel's examples at 
	/// http://www.redblobgames.com/grids/hexagons/. Creating a new hex grid does not create an
	/// array in memory or any other concrete data. It provides a service to query for data such as
	/// what 2D point at which a given hex index appears at or which hex a given 2D point is over.
	/// </summary>
    public class HexGrid
    {
		#region Members

		private float __hexRadius;

		#endregion

		#region Constants

		public const float SQRT_3 = 1.7320508075688772935274463415059f;
		public const float TWO_THIRDS = 2f / 3f;
		
		#endregion

		#region Properties

		/// <summary>
		/// The distance from the center of a hex to one of its vertices.
		/// </summary>
		public float HexRadius { 
			get { return __hexRadius; } 
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
		}

		#endregion

		#region Instance Methods

		/// <summary>
		/// Get the point on the x-z cartesian plane where the center of the given hex is located.
		/// </summary>
		/// <param name="hex">A hex position, represented by an AxialHexCoord.</param>
		/// <returns>A Vec2D point of the hex's position on the x-z cartesian plane.</returns>
		public 
		Vec2D 
		AxialToPoint( AxialHexCoord hex )
		{
			float x = HexRadius * 1.5f * hex.r;
			float z = HexRadius * SQRT_3 * ( hex.q + 0.5f * hex.r );

			return new Vec2D( x, z );
		}
		
		/// <summary>
		/// Get the point on the x-z cartesian plane where the center of the given hex is located.
		/// </summary>
		/// <param name="hex">A hex position, represented by an OffsetHexCoord.</param>
		/// <returns>A Vec2D point of the hex's position on the x-z cartesian plane.</returns>
		public 
		Vec2D 
		OffsetToPoint( OffsetHexCoord hex )
		{
			float x = HexRadius * 1.5f * hex.r;
			float z = HexRadius * SQRT_3 * ( hex.q + 0.5f * (float)hex.RowParity );

			return new Vec2D( x, z );
		}
		

		/// <summary>
		/// Get the hex that contains the given point on the x-z cartesian plane.
		/// </summary>
		/// <param name="point">A point on the x-z cartesian plane.</param>
		/// <returns>A CubicHexCoord representation of the grid position of the hex that the point
		/// is contained by.</returns>
		public 
		CubicHexCoord 
		PointToCubic( Vec2D point )
		{
			float q = ( point.z * TWO_THIRDS ) / HexRadius;
			float r = ( point.x * ( SQRT_3 - point.z ) / 3f ) / HexRadius;

			return CubicHexCoord.Round( new FloatAxial( q, r ).ToFloatCubic() );
		}

		#endregion
    }
}
