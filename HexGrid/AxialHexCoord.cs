using System;
using System.Linq;

namespace ca.axoninteractive.Geometry.Hex
{
	/// <summary>
	/// Represents the position of a hex within a hex grid using axial coordinates (q = East, 
	/// r = Southeast). This results in a grid with a naturally parallelgraphic form, but can 
	/// still be stored in a regtangular array resulting in a small number of wasted cells.
	/// </summary>
	/// <remarks>This type is the less computationally efficient type to use for hex grid 
	/// computations than CubicHexCoord, as all of the work has to be done by the CubicHexCoord 
	/// type and converting between AxialHexCoord and CubicHexCoord is a small cost every time 
	/// a result must be obtained.</remarks>
	public struct AxialHexCoord
	{
		#region Members

		public int q;
		public int r;

		#endregion
		
		
		#region Constructors
		
		/// <summary>
		/// Create a new AxialHexCoord given the coordinates q and r.
		/// </summary>
		/// <param name="q">The column position of the hex within the grid.</param>
		/// <param name="r">The row position of the hex within the grid.</param>
		public 
		AxialHexCoord( int q, int r ) 
		{
			this.q = q;
			this.r = r;
		}

		#endregion
		
		
		#region Type Conversions
		
		/// <summary>
		/// Return this hex as a CubicHexCoord.
		/// </summary>
		/// <returns>A CubicHexCoord representing the hex.</returns>
		public 
		CubicHexCoord 
		ToCubic()
		{
			int x = this.q;
			int z = this.r;
			int y = -x - z;

			return new CubicHexCoord( x, y, z );
		}

		#endregion
		
		
		#region Operator Overloads
		
		/// <summary>
		/// Add 2 AxialHexCoords together and return the result.
		/// </summary>
		/// <param name="lhs">The AxialHexCoord on the left-hand side of the + sign.</param>
		/// <param name="rhs">The AxialHexCoord on the right-hand side of the + sign.</param>
		/// <returns>A new AxialHexCoord representing the sum of the inputs.</returns>
		public static 
		AxialHexCoord 
		operator +( AxialHexCoord lhs, AxialHexCoord rhs ) 
		{
			int q = lhs.q + rhs.q;
			int r = lhs.r + rhs.r;

			return new AxialHexCoord( q, r );
		}

		
		/// <summary>
		/// Subtract 1 AxialHexCoord from another and return the result.
		/// </summary>
		/// <param name="lhs">The AxialHexCoord on the left-hand side of the - sign.</param>
		/// <param name="rhs">The AxialHexCoord on the right-hand side of the - sign.</param>
		/// <returns>A new AxialHexCoord representing the difference of the inputs.</returns>
		public static 
		AxialHexCoord 
		operator -( AxialHexCoord lhs, AxialHexCoord rhs ) 
		{
			int q = lhs.q - rhs.q;
			int r = lhs.r - rhs.r;

			return new AxialHexCoord( q, r );
		}

		
		/// <summary>
		/// Check if 2 AxialHexCoords represent the same hex on the grid.
		/// </summary>
		/// <param name="lhs">The AxialHexCoord on the left-hand side of the == sign.</param>
		/// <param name="rhs">The AxialHexCoord on the right-hand side of the == sign.</param>
		/// <returns>A bool representing whether or not the AxialHexCoords are equal.</returns>
		public static 
		bool 
		operator ==( AxialHexCoord lhs, AxialHexCoord rhs ) 
		{
			return ( lhs.q == rhs.q ) && ( lhs.r == rhs.r );
		}

		
		/// <summary>
		/// Check if 2 AxialHexCoords represent the different hexes on the grid.
		/// </summary>
		/// <param name="lhs">The AxialHexCoord on the left-hand side of the != sign.</param>
		/// <param name="rhs">The AxialHexCoord on the right-hand side of the != sign.</param>
		/// <returns>A bool representing whether or not the AxialHexCoords are unequal.</returns>
		public static 
		bool 
		operator !=( AxialHexCoord lhs, AxialHexCoord rhs ) 
		{
			return ( lhs.q != rhs.q ) || ( lhs.r != rhs.r );
		}


		/// <summary>
		/// Get a hash reflecting the contents of the AxialHexCoord.
		/// </summary>
		/// <returns>An integer hash code reflecting the contents of the AxialHexCoord.</returns>
		public override
		int 
		GetHashCode()
		{
			// See http://stackoverflow.com/questions/7813687/right-way-to-implement-gethashcode-for-this-struct
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + q.GetHashCode();
				hash = hash * 23 + r.GetHashCode();
				return hash;
			}
		}

		
		/// <summary>
		/// Check if this AxialHexCoord is equal to an arbitrary object.
		/// </summary>
		/// <returns>Whether or not this AxialHexCoord and the given object are equal.</returns>
		public override
		bool 
		Equals( object obj )
		{
			if ( obj == null )
			{
				return false;
			}            

            if ( GetType() != obj.GetType() )
			{
                return false;
			}  
			
			AxialHexCoord other = (AxialHexCoord)obj;

			return ( this.q == other.q ) && ( this.r == other.r );
		}

		#endregion
	}
}
