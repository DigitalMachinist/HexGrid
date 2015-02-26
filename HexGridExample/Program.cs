using System;

namespace ca.axoninteractive.Geometry.Hex
{
	class Program
	{
		static void Main( string[] args )
		{
			//FloatCubic floatCubic = new FloatAxial( -0.01f, -1f ).ToFloatCubic();
			//CubicHexCoord rounded = floatCubic.Round();
			//AxialHexCoord axial = rounded.ToAxial();
			
			HexGrid grid = new HexGrid( 2f );
			float offset = 0.5f * grid.HexRadius;
			//grid.PointToDirectionInHex( new Vec2D(     0f,     0f ) );
			grid.PointToDirectionInHex( new Vec2D(  0.10f,  0.10f ) );
			grid.PointToDirectionInHex( new Vec2D( -0.10f, -0.10f ) );
			grid.PointToDirectionInHex( new Vec2D(     0f,     0f ) );
			grid.PointToDirectionInHex( new Vec2D(  0.99f,  1.00f ) );
			grid.PointToDirectionInHex( new Vec2D( -0.99f, -1.00f ) );
		}
	}
}
