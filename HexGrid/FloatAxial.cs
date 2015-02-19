using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ca.axoninteractive.Geometry.HexGrid
{
	public struct FloatAxial
	{
		#region Members

		public float q;
		public float r;

		#endregion
		#region Constructors

		public FloatAxial( AxialHexCoord axial ) 
		{
			this.q = (float)axial.q;
			this.r = (float)axial.r;
		}

		public FloatAxial( float q, float r ) 
		{
			this.q = q;
			this.r = r;
		}

		#endregion
		#region Type Conversions

		public FloatCubic ToFloatCubic()
		{
			float x = this.q;
			float z = this.r;
			float y = -x - z;

			return new FloatCubic( x, y, z );
		}

		#endregion
	}
}
