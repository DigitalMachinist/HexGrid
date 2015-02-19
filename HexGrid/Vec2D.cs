using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ca.axoninteractive.Geometry.HexGrid
{
	public struct Vec2D
	{
		#region Members

		public float x;
		public float z;

		#endregion
		#region Constructors

		public Vec2D( float x, float z )
		{
			this.x = x;
			this.z = z;
		}

		#endregion
		#region Operator Overloads

		public static Vec2D operator +( Vec2D lhs, Vec2D rhs ) 
		{
			float x = lhs.x + rhs.x;
			float z = lhs.z + rhs.z;

			return new Vec2D( x, z );
		}

		public static Vec2D operator -( Vec2D lhs, Vec2D rhs ) 
		{
			float x = lhs.x + rhs.x;
			float z = lhs.z + rhs.z;

			return new Vec2D( x, z );
		}

		#endregion
	}
}
