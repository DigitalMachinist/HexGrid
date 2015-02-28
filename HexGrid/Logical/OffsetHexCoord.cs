using System;
using System.Linq;

namespace ca.axoninteractive.Geometry.Hex
{
	/// <summary>
	/// Represents the position of a hex within a hex grid using the Odd-Row offset grid layout
	/// scheme. This means that all hexes are pointy-topped and each odd row is offset in the 
	/// positive r direction by half of a hex width. Offset coordinates are a natural fit for 
	/// storage in a rectangular array of memory, and can be an ideal storage format for hexes.
	/// </summary>
	/// <remarks>This type is the least computationally efficient type to use for hex grid 
	/// computations, as all of the work has to be done by the CubicHexCoord type and converting 
	/// between OffsetHexCoord and CubicHexCoord is the most computationally expensive of the 
	/// type conversions provided by this library.</remarks>
	public struct OffsetHexCoord
	{
		#region Members

		public int q;
		public int r;

		#endregion
		
		
		#region Properties
		
		/// <summary>
		/// Return whether or not this hex belongs to an odd-numbered row.
		/// </summary>
		public bool IsOddRow { get { return ( this.RowParity == ParityEnum.Odd ); } }

		
		/// <summary>
		/// Return the row parity of the hex (whether its row number is even or odd).
		/// </summary>
		public ParityEnum RowParity { get { return (ParityEnum)( this.r & 1 ); } }

		#endregion
		
		
		#region Constructors
		
		/// <summary>
		/// Create a new OffsetHexCoord given the coordinates q and r.
		/// </summary>
		/// <param name="q">The column position of the hex within the grid.</param>
		/// <param name="r">The row position of the hex within the grid.</param>
		public 
		OffsetHexCoord( int q, int r )
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
			// Made a scary change here. Expect this to break!
			int x = this.q - ( this.r - (int)RowParity ) / 2;
			int z = this.r;
			int y = -x - z;

			return new CubicHexCoord( x, y, z );
		}

		#endregion
		
		
		#region Operator Overloads
		
		/// <summary>
		/// Add 2 OffsetHexCoords together and return the result.
		/// </summary>
		/// <param name="lhs">The OffsetHexCoord on the left-hand side of the + sign.</param>
		/// <param name="rhs">The OffsetHexCoord on the right-hand side of the + sign.</param>
		/// <returns>A new OffsetHexCoord representing the sum of the inputs.</returns>
		public static 
		OffsetHexCoord 
		operator +( OffsetHexCoord lhs, OffsetHexCoord rhs ) 
		{
			int q = lhs.q + rhs.q;
			int r = lhs.r + rhs.r;

			return new OffsetHexCoord( q, r );
		}
		

		/// <summary>
		/// Subtract 1 OffsetHexCoord from another and return the result.
		/// </summary>
		/// <param name="lhs">The OffsetHexCoord on the left-hand side of the - sign.</param>
		/// <param name="rhs">The OffsetHexCoord on the right-hand side of the - sign.</param>
		/// <returns>A new OffsetHexCoord representing the difference of the inputs.</returns>
		public static 
		OffsetHexCoord 
		operator -( OffsetHexCoord lhs, OffsetHexCoord rhs ) 
		{
			int q = lhs.q - rhs.q;
			int r = lhs.r - rhs.r;

			return new OffsetHexCoord( q, r );
		}

		
		/// <summary>
		/// Check if 2 OffsetHexCoords represent the same hex on the grid.
		/// </summary>
		/// <param name="lhs">The OffsetHexCoord on the left-hand side of the == sign.</param>
		/// <param name="rhs">The OffsetHexCoord on the right-hand side of the == sign.</param>
		/// <returns>A bool representing whether or not the OffsetHexCoords are equal.</returns>
		public static 
		bool 
		operator ==( OffsetHexCoord lhs, OffsetHexCoord rhs ) 
		{
			return ( lhs.q == rhs.q ) && ( lhs.r == rhs.r );
		}

		
		/// <summary>
		/// Check if 2 OffsetHexCoords represent the different hexes on the grid.
		/// </summary>
		/// <param name="lhs">The OffsetHexCoord on the left-hand side of the != sign.</param>
		/// <param name="rhs">The OffsetHexCoord on the right-hand side of the != sign.</param>
		/// <returns>A bool representing whether or not the OffsetHexCoords are unequal.</returns>
		public static 
		bool 
		operator !=( OffsetHexCoord lhs, OffsetHexCoord rhs ) 
		{
			return ( lhs.q != rhs.q ) || ( lhs.r != rhs.r );
		}


		/// <summary>
		/// Get a hash reflecting the contents of the OffsetHexCoord.
		/// </summary>
		/// <returns>An integer hash code reflecting the contents of the OffsetHexCoord.</returns>
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
		/// Check if this OffsetHexCoord is equal to an arbitrary object.
		/// </summary>
		/// <returns>Whether or not this OffsetHexCoord and the given object are equal.</returns>
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
			
			OffsetHexCoord other = (OffsetHexCoord)obj;

			return ( this.q == other.q ) && ( this.r == other.r );
		}

		#endregion
	}
}
