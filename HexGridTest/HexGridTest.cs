using System;
using NUnit.Framework;
using ca.axoninteractive.Geometry.Hex;

namespace ca.axoninteractive.Geometry.HexGridTest
{
	[TestFixture]
	public class HexGridTest
	{
		// The range in which floating-point numbers are consider equal.
		public const float EPSILON = 0.000001f;

		#region Properties

		[Test]
		public void PropertyHexRadius()
		{
			float hexRadius = new HexGrid( 2f ).HexRadius;

			Assert.That( hexRadius, Is.EqualTo( 2f ) );
		}

		#endregion

		#region Constructors

		[Test]
		public void ConstructorHexRadius()
		{
			HexGrid grid = new HexGrid( 2f );

			Assert.That( grid.HexRadius, Is.EqualTo( 2f ) );
		}
		
		#endregion

		#region Instance Methods

		[Test]
		public void AxialToPoint()
		{
			HexGrid grid = new HexGrid( 2f );
			Vec2D point = grid.AxialToPoint( new AxialHexCoord( 1, 2 ) );
			
			Assert.That( point.x, Is.InRange<float>( 6.000000f - EPSILON, 6.000000f + EPSILON ) );
			Assert.That( point.z, Is.InRange<float>( 6.928203f - EPSILON, 6.928203f + EPSILON ) );
		}

		[Test]
		public void OffsetToPoint()
		{
			//float x = HexRadius * 1.5f * hex.r;
			//float z = HexRadius * SQRT_3 * ( hex.q + 0.5f * (float)hex.RowParity );

			//return new Vec2D( x, z );


			// TODO


			HexGrid grid = new HexGrid( 2f );
			Vec2D point = grid.OffsetToPoint( new OffsetHexCoord( 1, 2 ) );
			
			Assert.That( point.x, Is.InRange<float>( 6.000000f - EPSILON, 6.000000f + EPSILON ) );
			Assert.That( point.z, Is.InRange<float>( 6.928203f - EPSILON, 6.928203f + EPSILON ) );
		}

		[Test]
		public void PointToCubic()
		{
			//float q = ( point.z * TWO_THIRDS ) / HexRadius;
			//float r = ( point.x * ( SQRT_3 - point.z ) / 3f ) / HexRadius;

			//return CubicHexCoord.Round( new FloatAxial( q, r ).ToFloatCubic() );


			// TODO


			HexGrid grid = new HexGrid( 2f );
			CubicHexCoord cubic = grid.PointToCubic( new Vec2D( 10f, 10f ) );
			
			Assert.That( cubic.x, Is.InRange<float>( 6.000000f - EPSILON, 6.000000f + EPSILON ) );
			Assert.That( cubic.y, Is.InRange<float>( 6.928203f - EPSILON, 6.928203f + EPSILON ) );
			Assert.That( cubic.z, Is.InRange<float>( 6.928203f - EPSILON, 6.928203f + EPSILON ) );
		}

		#endregion
	}
}
