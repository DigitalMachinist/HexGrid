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
			HexGrid grid = new HexGrid( 2f );
			Vec2D point = grid.OffsetToPoint( new OffsetHexCoord( 1, 2 ) );

			float xExpected = 2 * 2f * 1.5f;
			float zExpected = 1 * 2f * (float)Math.Sqrt( 3 );
			
			Assert.That( point.x, Is.InRange<float>( xExpected - EPSILON, xExpected + EPSILON ) );
			Assert.That( point.z, Is.InRange<float>( zExpected - EPSILON, zExpected + EPSILON ) );
		}

		[Test]
		public void PointToCubic()
		{
			HexGrid grid = new HexGrid( 2f );
			CubicHexCoord cubic = grid.PointToCubic( new Vec2D( 2.5f, 5.5f ) );
			
			Assert.That( cubic.x, Is.EqualTo(  2 ) );
			Assert.That( cubic.y, Is.EqualTo(  0 ) );
			Assert.That( cubic.z, Is.EqualTo( -2 ) );
		}

		#endregion
	}
}
