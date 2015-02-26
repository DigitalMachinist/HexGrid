using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ca.axoninteractive.Geometry.Hex
{
	/// <summary>
	/// A simple 2D vector implementation to store coordinates on the x-y cartesian plane.
	/// This doesn't really do anything important. It's just a container used by HexGrid to 
	/// return world position values pack to the caller.
	/// </summary>
	public struct Vec2D
	{
		#region Members

		public float x;
		public float y;

		#endregion
		
		
		#region Constructors
		
		/// <summary>
		/// Create a new Vec2D given the coordinates x and z (on the world x-y plane).
		/// </summary>
		/// <param name="x">The position of this point on the x-axis.</param>
		/// <param name="y">The position of this point on the y-axis.</param>
		public 
		Vec2D( float x, float y )
		{
			this.x = x;
			this.y = y;
		}

		#endregion
		
		
		#region Operator Overloads
		
		/// <summary>
		/// Add 2 Vec2Ds together and return the result.
		/// </summary>
		/// <param name="lhs">The Vec2D on the left-hand side of the + sign.</param>
		/// <param name="rhs">The Vec2D on the right-hand side of the + sign.</param>
		/// <returns>A new Vec2D representing the sum of the inputs.</returns>
		public static 
		Vec2D 
		operator +( Vec2D lhs, Vec2D rhs ) 
		{
			float x = lhs.x + rhs.x;
			float y = lhs.y + rhs.y;

			return new Vec2D( x, y );
		}
		

		/// <summary>
		/// Subtract 1 Vec2D from another and return the result.
		/// </summary>
		/// <param name="lhs">The Vec2D on the left-hand side of the - sign.</param>
		/// <param name="rhs">The Vec2D on the right-hand side of the - sign.</param>
		/// <returns>A new Vec2D representing the difference of the inputs.</returns>
		public static 
		Vec2D 
		operator -( Vec2D lhs, Vec2D rhs ) 
		{
			float x = lhs.x - rhs.x;
			float y = lhs.y - rhs.y;

			return new Vec2D( x, y );
		}

		#endregion


		#region Instance Methods
		
		/// <summary>
		/// Returns the distance from this Vec2D to the given other.
		/// </summary>
		/// <param name="other">Any other Vec2D.</param>
		/// <returns>A float representing the distance from this Vec2D to the given other.
		/// </returns>
		public float Distance( Vec2D other )
		{
			return ( other - this ).Length();
		}
		

		/// <summary>
		/// Returns float representing the length of this Vec2D.
		/// </summary>
		/// <returns>A float representing the length of this Vec2D.</returns>
		public float Length()
		{
			return (float)Math.Sqrt( this.x * this.x + this.y * this.y );
		}

		#endregion
	}
}
