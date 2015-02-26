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

		// The result of (float)Math.Sqrt( 3f ) just for convenience.
		public const float SQRT_3  = 1.7320508075688772935274463415059f;

		#region Properties

		[Test]
		public void PropertyHexRadius()
		{
			float hexRadius = new HexGrid( 2f ).HexRadius;

			Assert.That( hexRadius, Is.EqualTo( 2f ) );
		}

		[Test]
		public void PropertySlice()
		{
			HexGrid grid	= new HexGrid( 2f );
			float hexRadius = grid.HexRadius;
			Vec2D slice		= grid.Slice;

			float xExpected = 0.5f * SQRT_3 * hexRadius;
			float yExpected = 0.5f * hexRadius;
			
			Assert.That( slice.x, Is.InRange<float>( xExpected - EPSILON, xExpected + EPSILON ) );
			Assert.That( slice.y, Is.InRange<float>( yExpected - EPSILON, yExpected + EPSILON ) );
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
			HexGrid grid	= new HexGrid( 2f );
			float hexRadius	= grid.HexRadius;
			Vec2D point		= grid.AxialToPoint( new AxialHexCoord( 10, 10 ) );

			float xExpected = hexRadius * SQRT_3 * ( 10 + 10 / 2 );
			float yExpected = hexRadius * ( 3f / 2f ) * 10;
			
			Assert.That( point.x, Is.InRange<float>( xExpected - EPSILON, xExpected + EPSILON ) );
			Assert.That( point.y, Is.InRange<float>( yExpected - EPSILON, yExpected + EPSILON ) );
		}

		[Test]
		public void OffsetToPoint()
		{
			HexGrid grid = new HexGrid( 2f );
			float hexRadius = grid.HexRadius;

			// Test an even row hex
			Vec2D point = grid.OffsetToPoint( new OffsetHexCoord( 10, 10 ) );
			float xExpected = hexRadius * SQRT_3 * 10;
			float yExpected = hexRadius * ( 3f / 2f ) * 10;
			
			Assert.That( point.x, Is.InRange<float>( xExpected - EPSILON, xExpected + EPSILON ) );
			Assert.That( point.y, Is.InRange<float>( yExpected - EPSILON, yExpected + EPSILON ) );

			// Text an odd row hex
			point = grid.OffsetToPoint( new OffsetHexCoord( 10, 11 ) );
			xExpected = hexRadius * SQRT_3 * ( 10 + 0.5f );
			yExpected = hexRadius * ( 3f / 2f ) * 11;
			
			Assert.That( point.x, Is.InRange<float>( xExpected - EPSILON, xExpected + EPSILON ) );
			Assert.That( point.y, Is.InRange<float>( yExpected - EPSILON, yExpected + EPSILON ) );
		}

		[Test]
		public void PointToCubic()
		{
			HexGrid grid = new HexGrid( 3f );
			float hexRadius = grid.HexRadius;
			CubicHexCoord cubic = grid.PointToCubic( new Vec2D( 10f, 10f ) );

			float rExpected = 10f / ( ( 3f / 2f ) * hexRadius );
			float qExpected = 10f / ( SQRT_3 * hexRadius * ( 1f + 0.5f * rExpected ) );

			Console.WriteLine( "Axial: q: " + qExpected + ", r: " + rExpected );

			float xExpected = qExpected;
			float zExpected = rExpected;
			float yExpected = -xExpected - zExpected;
			
			Console.WriteLine( "Cubic: x: " + xExpected + ", y: " + yExpected + ", z: " + zExpected );
			
			// Now that I'm close enough to guess, I'll just do that rather than trying to emulate 
			// the rounding all the way through. Not accurate, but good enough for government work.
			Assert.That( cubic.x, Is.EqualTo(  1 ) );
			Assert.That( cubic.y, Is.EqualTo( -3 ) );
			Assert.That( cubic.z, Is.EqualTo(  2 ) );
		}

		[Test]
		public void PointToDirectionInHex()
		{
			HexGrid grid = new HexGrid( 2f );
			Vec2D hexPos = grid.AxialToPoint( new AxialHexCoord( 10, 10 ) );
			float offset = 0.5f * grid.HexRadius;
			
			Vec2D[] points = new Vec2D[ 12 ] {
				new Vec2D(	hexPos.x + offset,	hexPos.y + 0f * offset - 0.01f	), // 0a
				new Vec2D(	hexPos.x + offset,	hexPos.y + 0f * offset + 0.01f	), // 0b
				new Vec2D(	hexPos.x + offset,	hexPos.y + 1f * offset - 0.01f	), // 1a
				new Vec2D(	hexPos.x + offset,	hexPos.y + 1f * offset + 0.01f	), // 1b
				new Vec2D(	hexPos.x - offset,	hexPos.y + 1f * offset + 0.01f	), // 2a
				new Vec2D(	hexPos.x - offset,	hexPos.y + 1f * offset - 0.01f	), // 2b
				new Vec2D(	hexPos.x - offset,	hexPos.y + 0f * offset + 0.01f	), // 3a
				new Vec2D(	hexPos.x - offset,	hexPos.y + 0f * offset - 0.01f	), // 3b
				new Vec2D(	hexPos.x - offset,	hexPos.y - 1f * offset + 0.01f	), // 4a
				new Vec2D(	hexPos.x - offset,	hexPos.y - 1f * offset - 0.01f	), // 4b
				new Vec2D(	hexPos.x + offset,	hexPos.y - 1f * offset - 0.01f	), // 5a
				new Vec2D(	hexPos.x + offset,	hexPos.y  -1f * offset + 0.01f	)  // 5b
			};

			DirectionEnum[] results = new DirectionEnum[ 12 ];
			for ( int i = 0; i < points.Length; i++ )
			{
				results[ i ] = grid.PointToDirectionInHex( points[ i ] );
			}

			Assert.That( results, Is.EquivalentTo( new DirectionEnum[ 12 ] {
				DirectionEnum.E,  // 0a
				DirectionEnum.E,  // 0b 
				DirectionEnum.SE, // 1a 
				DirectionEnum.SE, // 1b
				DirectionEnum.SW, // 2a
				DirectionEnum.SW, // 2b
				DirectionEnum.W,  // 3a
				DirectionEnum.W,  // 3b
				DirectionEnum.NW, // 4a
				DirectionEnum.NW, // 4b
				DirectionEnum.NE, // 5a
				DirectionEnum.NE  // 5b
			} ) );
		}

		#endregion
	}
}
