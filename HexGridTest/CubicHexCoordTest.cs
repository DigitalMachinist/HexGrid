using System;
using NUnit.Framework;
using ca.axoninteractive.Geometry.Hex;

namespace ca.axoninteractive.Geometry.HexGridTest
{
	[TestFixture]
	public class CubicHexCoordTest
	{
		#region Constructors
		
		[Test]
		public void ConstructorXYZ()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 2, 3 );

			Assert.That( cubic.x, Is.EqualTo( 1 ) );
			Assert.That( cubic.y, Is.EqualTo( 2 ) );
			Assert.That( cubic.z, Is.EqualTo( 3 ) );
		}

		[Test]
		public void ConstructorParameterless()
		{
			CubicHexCoord cubic = new CubicHexCoord();

			Assert.That( cubic.x, Is.EqualTo( 0 ) );
			Assert.That( cubic.y, Is.EqualTo( 0 ) );
			Assert.That( cubic.z, Is.EqualTo( 0 ) );
		}

		#endregion


		#region Type Conversions
		
		[Test]
		public void ToAxial()
		{
			AxialHexCoord axial = new CubicHexCoord( 1, 2, 3 ).ToAxial();

			Assert.That( axial.q, Is.EqualTo( 1 ) );
			Assert.That( axial.r, Is.EqualTo( 3 ) );
		}
		
		[Test]
		public void ToOffset()
		{
			OffsetHexCoord offset = new CubicHexCoord( 1, 2, 3 ).ToOffset();

			Assert.That( offset.q, Is.EqualTo( 3 ) );
			Assert.That( offset.q, Is.EqualTo( 1 ) );
		}

		#endregion


		#region Operator Overloads

		[Test]
		public void OperatorOverloadPlus()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 2, 3 ) + new CubicHexCoord( 4, 5, 6 );

			Assert.That( cubic.x, Is.EqualTo( 5 ) );
			Assert.That( cubic.y, Is.EqualTo( 7 ) );
			Assert.That( cubic.z, Is.EqualTo( 9 ) );
		}
		
		[Test]
		public void OperatorOverloadMinus()
		{
			CubicHexCoord cubic = new CubicHexCoord( 6, 5, 4 ) - new CubicHexCoord( 1, 2, 3 );

			Assert.That( cubic.x, Is.EqualTo( 5 ) );
			Assert.That( cubic.y, Is.EqualTo( 3 ) );
			Assert.That( cubic.z, Is.EqualTo( 1 ) );
		}

		[Test]
		public void OperatorOverloadEquals()
		{
			bool isTrue  = new CubicHexCoord( 1, 2, 3 ) == new CubicHexCoord( 1, 2, 3 );
			bool isFalse = new CubicHexCoord( 1, 2, 3 ) == new CubicHexCoord( 4, 5, 6 );

			Assert.That( isTrue,  Is.True  );
			Assert.That( isFalse, Is.False );
		}

		[Test]
		public void OperatorOverloadNotEquals()
		{
			bool isTrue  = new CubicHexCoord( 1, 2, 3 ) != new CubicHexCoord( 4, 5, 6 );
			bool isFalse = new CubicHexCoord( 1, 2, 3 ) != new CubicHexCoord( 1, 2, 3 );

			Assert.That( isTrue,  Is.True  );
			Assert.That( isFalse, Is.False );
		}

		#endregion


		#region Instance Methods
		
		[Test]
		public void AreaAround()
		{
			// TODO
			Assert.Inconclusive();
		}

		[Test]
		public void Diagonal()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 2, 3 ).Diagonal( DiagonalEnum.ESE );

			Assert.That( cubic.x, Is.EqualTo( 2 ) );
			Assert.That( cubic.y, Is.EqualTo( 0 ) );
			Assert.That( cubic.z, Is.EqualTo( 4 ) );
		}
		
		[Test]
		public void Diagonals()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 2, 3 );
			CubicHexCoord[] diagonals = cubic.Diagonals();

			Assert.That( diagonals, Is.EquivalentTo( new CubicHexCoord[ 6 ] {
				cubic.Diagonal( DiagonalEnum.ESE ),
				cubic.Diagonal( DiagonalEnum.S   ),
				cubic.Diagonal( DiagonalEnum.WSW ),
				cubic.Diagonal( DiagonalEnum.WNW ),
				cubic.Diagonal( DiagonalEnum.N   ),
				cubic.Diagonal( DiagonalEnum.ENE )
			} ) );
		}
		
		[Test]
		public void DistanceTo()
		{
			CubicHexCoord cubic1 = new CubicHexCoord( 0, 0, 0 );
			CubicHexCoord cubic2 = cubic1.Neighbor( DirectionEnum.E ).Neighbor( DirectionEnum.SE );
			int distance = cubic1.DistanceTo( cubic2 );

			Assert.That( distance, Is.EqualTo( 2 ) );
		}
		
		[Test]
		public void LineTo()
		{
			// TODO
			Assert.Inconclusive();
		}
		
		[Test]
		public void Neighbor()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 2, 3 ).Neighbor( DirectionEnum.E );

			Assert.That( cubic.x, Is.EqualTo( 2 ) );
			Assert.That( cubic.y, Is.EqualTo( 1 ) );
			Assert.That( cubic.z, Is.EqualTo( 3 ) );
		}
		
		[Test]
		public void Neighbors()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 2, 3 );
			CubicHexCoord[] neighbors = cubic.Neighbors();

			Assert.That( neighbors, Is.EquivalentTo( new CubicHexCoord[ 6 ] {
				cubic.Neighbor( DirectionEnum.E  ),
				cubic.Neighbor( DirectionEnum.SE ),
				cubic.Neighbor( DirectionEnum.SW ),
				cubic.Neighbor( DirectionEnum.W  ),
				cubic.Neighbor( DirectionEnum.NW ),
				cubic.Neighbor( DirectionEnum.NE )
			} ) );
		}
		
		[Test]
		public void RingAround()
		{
			// TODO
			Assert.Inconclusive();
		}
		
		[Test]
		public void RotateAroundOther()
		{
			// Not yet implemented
			Assert.Ignore();
		}
		
		[Test]
		public void RotateOtherAround()
		{
			// Not yet implemented
			Assert.Ignore();
		}
		
		[Test]
		public void Scale()
		{
			CubicHexCoord cubic = CubicHexCoord.DirectionDiff( DirectionEnum.SE );
			CubicHexCoord scaled = cubic.Scale( 3 );

			Assert.That( scaled.x, Is.EqualTo( 3 * cubic.x ) );
			Assert.That( scaled.y, Is.EqualTo( 3 * cubic.y ) );
			Assert.That( scaled.z, Is.EqualTo( 3 * cubic.z ) );
		}
		
		[Test]
		public void SpiralAroundInward()
		{
			// TODO
			Assert.Inconclusive();
		}
		
		[Test]
		public void SpiralAroundOutward()
		{
			// TODO
			Assert.Inconclusive();
		}

		#endregion


		#region Static Methods

		[Test]
		public void StaticArea()
		{
			// TODO
			Assert.Inconclusive();
		}

		[Test]
		public void StaticDiagonalDiff()
		{
			CubicHexCoord cubic = CubicHexCoord.DiagonalDiff( DiagonalEnum.ESE );

			Assert.That( cubic.x, Is.EqualTo(  1 ) );
			Assert.That( cubic.y, Is.EqualTo( -2 ) );
			Assert.That( cubic.z, Is.EqualTo(  1 ) );
		}

		[Test]
		public void StaticDirectionDiff()
		{
			CubicHexCoord cubic = CubicHexCoord.DirectionDiff( DirectionEnum.E );

			Assert.That( cubic.x, Is.EqualTo(  1 ) );
			Assert.That( cubic.y, Is.EqualTo( -1 ) );
			Assert.That( cubic.z, Is.EqualTo(  0 ) );
		}

		[Test]
		public void StaticIntersectRanges()
		{
			// Not yet implemented
			Assert.Ignore();
		}

		[Test]
		public void StaticLine()
		{
			// TODO
			Assert.Inconclusive();
		}

		[Test]
		public void StaticRing()
		{
			// TODO
			Assert.Inconclusive();
		}

		[Test]
		public void StaticRotate()
		{
			// Not yet implemented
			Assert.Ignore();
		}

		[Test]
		public void StaticRound()
		{
			FloatCubic floatCubic = new FloatAxial( 1.2f, 2.2f ).ToFloatCubic();
			CubicHexCoord rounded = CubicHexCoord.Round( floatCubic );
			AxialHexCoord axial = rounded.ToAxial();

			Assert.That( axial.q, Is.EqualTo( 1 ) );
			Assert.That( axial.r, Is.EqualTo( 2 ) );
		}

		[Test]
		public void StaticSpiralInward()
		{
			// TODO
			Assert.Inconclusive();
		}

		[Test]
		public void StaticSpiralOutward()
		{
			// TODO
			Assert.Inconclusive();
		}
		
		#endregion
	}
}
