using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ca.axoninteractive.Geometry.HexGrid
{
	public struct FloatCubic
	{
		#region Members

		public float x;
		public float y;
		public float z;

		#endregion
		#region Constructors

		public FloatCubic( CubicHexCoord cubic ) 
		{
			this.x = (float)cubic.x;
			this.y = (float)cubic.y;
			this.z = (float)cubic.z;
		}

		public FloatCubic( float x, float y, float z ) 
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		#endregion
		#region Type Conversions

		public CubicHexCoord ToCubic()
		{
			int x = (int)this.x;
			int y = (int)this.y;
			int z = (int)this.z;

			return new CubicHexCoord( x, y, z );
		}
		public FloatAxial ToFloatAxial()
		{
			float q = this.x;
			float r = this.z;

			return new FloatAxial( q, r );
		}

		#endregion
		#region Instance Methods

		public FloatCubic Scale( float factor )
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
