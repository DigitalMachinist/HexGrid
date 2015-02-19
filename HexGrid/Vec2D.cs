using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ca.axoninteractive.Geometry.HexGrid
{
	/// <summary>
	/// A simple 2D vector implementation to store coordinates on the x-z cartesian plane.
	/// This doesn't really do anything important. It's just a container used by HexGrid to 
	/// return world position values pack to the caller.
	/// </summary>
	public struct Vec2D
	{
		#region Members

		public float x;
		public float z;

		#endregion
		#region Constructors
		
		/// <summary>
		/// Create a new Vec2D given the coordinates x and z (on the world x-z plane).
		/// </summary>
		/// <param name="x">The position of this point on the x-axis.</param>
		/// <param name="z">The position of this point on the z-axis.</param>
		public 
		Vec2D( float x, float z )
		{
			this.x = x;
			this.z = z;
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
			float z = lhs.z + rhs.z;

			return new Vec2D( x, z );
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
			float x = lhs.x + rhs.x;
			float z = lhs.z + rhs.z;

			return new Vec2D( x, z );
		}

		#endregion
	}
}
