using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ca.axoninteractive.Geometry.Hex
{
	/// <summary>
	/// FloatCubic represents a pseudo-position on the hex grid. It does not directly represent 
	/// the position of a hex, but instead is used as a means to compute a hex position by rounding 
	/// a FloatCubic using CubicHexCoord.Round(), which returns a CubicHexCoord. 
	/// </summary>
	public struct FloatCubic
	{
		#region Members

		public float x;
		public float y;
		public float z;

		#endregion
		
		
		#region Constructors
		
		/// <summary>
		/// Create a new FloatCubic given a CubicHexIndex.
		/// </summary>
		/// <param name="cubic">Any CubicHexCoord representing a hex.</param>
		public 
		FloatCubic( CubicHexCoord cubic ) 
		{
			this.x = (float)cubic.x;
			this.y = (float)cubic.y;
			this.z = (float)cubic.z;
		}
		
		
		/// <summary>
		/// Create a new FloatCubic given the coordinates x, y and z.
		/// </summary>
		/// <param name="x">The position on this point on the x-axis.</param>
		/// <param name="y">The position on this point on the y-axis.</param>
		/// <param name="z">The position on this point on the z-axis.</param>
		public 
		FloatCubic( float x, float y, float z ) 
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		#endregion
		
		
		#region Type Conversions
		
		/// <summary>
		/// Return this FloatCubic as a FloatAxial.
		/// </summary>
		/// <returns>A FloatAxial representing this FloatCubic.</returns>
		public 
		FloatAxial 
		ToFloatAxial()
		{
			float q = this.x;
			float r = this.z;

			return new FloatAxial( q, r );
		}

		#endregion
		
		
		#region Instance Methods
		
		/// <summary>
		/// Returns a new CubicHexCoord representing the nearest hex to this FloatCubic.
		/// </summary>
		/// <returns>A new CubicHexCoord representing the nearest hex to this FloatCubic.</returns>
		public 
		CubicHexCoord
		Round()
		{
			int rx = (int)Math.Round( this.x );
			int ry = (int)Math.Round( this.y );
			int rz = (int)Math.Round( this.z );

			int xDiff = (int)Math.Abs( rx - this.x );
			int yDiff = (int)Math.Abs( ry - this.y );
			int zDiff = (int)Math.Abs( rz - this.z );

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


		/// <summary>
		/// Scale the world space by the given factor, causing q and r to change proportionally 
		/// to factor.
		/// </summary>
		/// <param name="factor">The multiplicative factor by which the world space is being 
		/// scaled.</param>
		/// <returns>A new FloatCubic representing the new floating hex position.</returns>
		public 
		FloatCubic 
		Scale( float factor )
		{
			return new FloatCubic(
				this.x * factor,
				this.y * factor,
				this.z * factor
			);
		}

		#endregion
	}
}
