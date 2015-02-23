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
			OffsetHexCoord offset = new CubicHexCoord( 0, -2, 2 ).ToOffset();

			Assert.That( offset.q, Is.EqualTo( 1 ) );
			Assert.That( offset.r, Is.EqualTo( 2 ) );
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
			CubicHexCoord cubic = new CubicHexCoord( 1, 0, -1 );
			CubicHexCoord[] area = CubicHexCoord.Area( cubic, 2 );

			// Center
			Assert.Contains( new CubicHexCoord(  1,  0, -1 ), area );
			// Distance 1
			Assert.Contains( new CubicHexCoord(  0,  1, -1 ), area );
			Assert.Contains( new CubicHexCoord(  1,  1, -2 ), area );
			Assert.Contains( new CubicHexCoord(  2,  0, -2 ), area );
			Assert.Contains( new CubicHexCoord(  2, -1, -1 ), area );
			Assert.Contains( new CubicHexCoord(  1, -1,  0 ), area );
			Assert.Contains( new CubicHexCoord(  0,  0,  0 ), area );
			// Distance 2
			Assert.Contains( new CubicHexCoord( -1,  2, -1 ), area );
			Assert.Contains( new CubicHexCoord(  0,  2, -2 ), area );
			Assert.Contains( new CubicHexCoord(  1,  2, -3 ), area );
			Assert.Contains( new CubicHexCoord(  2,  1, -3 ), area );
			Assert.Contains( new CubicHexCoord(  3,  0, -3 ), area );
			Assert.Contains( new CubicHexCoord(  3, -1, -2 ), area );
			Assert.Contains( new CubicHexCoord(  3, -2, -1 ), area );
			Assert.Contains( new CubicHexCoord(  2, -2,  0 ), area );
			Assert.Contains( new CubicHexCoord(  1, -2,  1 ), area );
			Assert.Contains( new CubicHexCoord(  0, -1,  1 ), area );
			Assert.Contains( new CubicHexCoord( -1,  0,  1 ), area );
			Assert.Contains( new CubicHexCoord( -1,  1,  0 ), area );
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
			CubicHexCoord startCubic = new CubicHexCoord( 1, 0, -1 );
			CubicHexCoord endCubic = new CubicHexCoord( -2, 2, 0 );
			CubicHexCoord[] line = startCubic.LineTo( endCubic );

			Assert.That( line, Is.EquivalentTo( new CubicHexCoord[ 4 ] {
				new CubicHexCoord(  1,  0, -1 ),
				new CubicHexCoord(  0,  1, -1 ),
				new CubicHexCoord( -1,  1,  0 ),
				new CubicHexCoord( -2,  2,  0 )
			} ) );
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
			CubicHexCoord cubic = new CubicHexCoord( 1, 0, -1 );
			CubicHexCoord[] ring = cubic.RingAround( 2, DirectionEnum.W );

			Assert.That( ring, Is.EquivalentTo( new CubicHexCoord[ 12 ] {
				new CubicHexCoord( -1,  2, -1 ),
				new CubicHexCoord(  0,  2, -2 ),
				new CubicHexCoord(  1,  2, -3 ),
				new CubicHexCoord(  2,  1, -3 ),
				new CubicHexCoord(  3,  0, -3 ),
				new CubicHexCoord(  3, -1, -2 ),
				new CubicHexCoord(  3, -2, -1 ),
				new CubicHexCoord(  2, -2,  0 ),
				new CubicHexCoord(  1, -2,  1 ),
				new CubicHexCoord(  0, -1,  1 ),
				new CubicHexCoord( -1,  0,  1 ),
				new CubicHexCoord( -1,  1,  0 )
			} ) );
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
			CubicHexCoord cubic = new CubicHexCoord( 1, 0, -1 );
			CubicHexCoord[] spiral = cubic.SpiralAroundOutward( 2, DirectionEnum.W );

			Assert.That( spiral, Is.EquivalentTo( new CubicHexCoord[ 19 ] {
				// Distance 2
				new CubicHexCoord( -1,  2, -1 ),
				new CubicHexCoord(  0,  2, -2 ),
				new CubicHexCoord(  1,  2, -3 ),
				new CubicHexCoord(  2,  1, -3 ),
				new CubicHexCoord(  3,  0, -3 ),
				new CubicHexCoord(  3, -1, -2 ),
				new CubicHexCoord(  3, -2, -1 ),
				new CubicHexCoord(  2, -2,  0 ),
				new CubicHexCoord(  1, -2,  1 ),
				new CubicHexCoord(  0, -1,  1 ),
				new CubicHexCoord( -1,  0,  1 ),
				new CubicHexCoord( -1,  1,  0 ),
				// Distance 1
				new CubicHexCoord(  0,  1, -1 ),
				new CubicHexCoord(  1,  1, -2 ),
				new CubicHexCoord(  2,  0, -2 ),
				new CubicHexCoord(  2, -1, -1 ),
				new CubicHexCoord(  1, -1,  0 ),
				new CubicHexCoord(  0,  0,  0 ),
				// Center
				new CubicHexCoord(  1,  0, -1 )
			} ) );
		}
		
		[Test]
		public void SpiralAroundOutward()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 0, -1 );
			CubicHexCoord[] spiral = cubic.SpiralAroundOutward( 2, DirectionEnum.W );

			Assert.That( spiral, Is.EquivalentTo( new CubicHexCoord[ 19 ] {
				// Center
				new CubicHexCoord(  1,  0, -1 ),
				// Distance 1
				new CubicHexCoord(  0,  1, -1 ),
				new CubicHexCoord(  1,  1, -2 ),
				new CubicHexCoord(  2,  0, -2 ),
				new CubicHexCoord(  2, -1, -1 ),
				new CubicHexCoord(  1, -1,  0 ),
				new CubicHexCoord(  0,  0,  0 ),
				// Distance 2
				new CubicHexCoord( -1,  2, -1 ), 
				new CubicHexCoord(  0,  2, -2 ),
				new CubicHexCoord(  1,  2, -3 ),
				new CubicHexCoord(  2,  1, -3 ),
				new CubicHexCoord(  3,  0, -3 ),
				new CubicHexCoord(  3, -1, -2 ),
				new CubicHexCoord(  3, -2, -1 ),
				new CubicHexCoord(  2, -2,  0 ),
				new CubicHexCoord(  1, -2,  1 ),
				new CubicHexCoord(  0, -1,  1 ),
				new CubicHexCoord( -1,  0,  1 ),
				new CubicHexCoord( -1,  1,  0 )
			} ) );
		}

		#endregion


		#region Static Methods

		[Test]
		public void StaticArea()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 0, -1 );
			CubicHexCoord[] area = CubicHexCoord.Area( cubic, 2 );

			// Center
			Assert.Contains( new CubicHexCoord(  1,  0, -1 ), area );
			// Distance 1
			Assert.Contains( new CubicHexCoord(  0,  1, -1 ), area );
			Assert.Contains( new CubicHexCoord(  1,  1, -2 ), area );
			Assert.Contains( new CubicHexCoord(  2,  0, -2 ), area );
			Assert.Contains( new CubicHexCoord(  2, -1, -1 ), area );
			Assert.Contains( new CubicHexCoord(  1, -1,  0 ), area );
			Assert.Contains( new CubicHexCoord(  0,  0,  0 ), area );
			// Distance 2
			Assert.Contains( new CubicHexCoord( -1,  2, -1 ), area );
			Assert.Contains( new CubicHexCoord(  0,  2, -2 ), area );
			Assert.Contains( new CubicHexCoord(  1,  2, -3 ), area );
			Assert.Contains( new CubicHexCoord(  2,  1, -3 ), area );
			Assert.Contains( new CubicHexCoord(  3,  0, -3 ), area );
			Assert.Contains( new CubicHexCoord(  3, -1, -2 ), area );
			Assert.Contains( new CubicHexCoord(  3, -2, -1 ), area );
			Assert.Contains( new CubicHexCoord(  2, -2,  0 ), area );
			Assert.Contains( new CubicHexCoord(  1, -2,  1 ), area );
			Assert.Contains( new CubicHexCoord(  0, -1,  1 ), area );
			Assert.Contains( new CubicHexCoord( -1,  0,  1 ), area );
			Assert.Contains( new CubicHexCoord( -1,  1,  0 ), area );
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
			CubicHexCoord startCubic = new CubicHexCoord( 1, 0, -1 );
			CubicHexCoord endCubic = new CubicHexCoord( -2, 2, 0 );
			CubicHexCoord[] line = CubicHexCoord.Line( startCubic, endCubic );

			Assert.That( line, Is.EquivalentTo( new CubicHexCoord[ 4 ] {
				new CubicHexCoord(  1,  0, -1 ),
				new CubicHexCoord(  0,  1, -1 ),
				new CubicHexCoord( -1,  1,  0 ),
				new CubicHexCoord( -2,  2,  0 )
			} ) );
		}

		[Test]
		public void StaticRing()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 0, -1 );
			CubicHexCoord[] ring = CubicHexCoord.Ring( cubic, 2, DirectionEnum.W );

			Assert.That( ring, Is.EquivalentTo( new CubicHexCoord[ 12 ] {
				new CubicHexCoord( -1,  2, -1 ),
				new CubicHexCoord(  0,  2, -2 ),
				new CubicHexCoord(  1,  2, -3 ),
				new CubicHexCoord(  2,  1, -3 ),
				new CubicHexCoord(  3,  0, -3 ),
				new CubicHexCoord(  3, -1, -2 ),
				new CubicHexCoord(  3, -2, -1 ),
				new CubicHexCoord(  2, -2,  0 ),
				new CubicHexCoord(  1, -2,  1 ),
				new CubicHexCoord(  0, -1,  1 ),
				new CubicHexCoord( -1,  0,  1 ),
				new CubicHexCoord( -1,  1,  0 )
			} ) );
		}

		[Test]
		public void StaticRotate()
		{
			// Not yet implemented
			Assert.Ignore();
		}

		[Test]
		public void StaticSpiralInward()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 0, -1 );
			CubicHexCoord[] spiral = CubicHexCoord.SpiralOutward( cubic, 2, DirectionEnum.W );

			Assert.That( spiral, Is.EquivalentTo( new CubicHexCoord[ 19 ] {
				// Distance 2
				new CubicHexCoord( -1,  2, -1 ),
				new CubicHexCoord(  0,  2, -2 ),
				new CubicHexCoord(  1,  2, -3 ),
				new CubicHexCoord(  2,  1, -3 ),
				new CubicHexCoord(  3,  0, -3 ),
				new CubicHexCoord(  3, -1, -2 ),
				new CubicHexCoord(  3, -2, -1 ),
				new CubicHexCoord(  2, -2,  0 ),
				new CubicHexCoord(  1, -2,  1 ),
				new CubicHexCoord(  0, -1,  1 ),
				new CubicHexCoord( -1,  0,  1 ),
				new CubicHexCoord( -1,  1,  0 ),
				// Distance 1
				new CubicHexCoord(  0,  1, -1 ),
				new CubicHexCoord(  1,  1, -2 ),
				new CubicHexCoord(  2,  0, -2 ),
				new CubicHexCoord(  2, -1, -1 ),
				new CubicHexCoord(  1, -1,  0 ),
				new CubicHexCoord(  0,  0,  0 ),
				// Center
				new CubicHexCoord(  1,  0, -1 )
			} ) );
		}

		[Test]
		public void StaticSpiralOutward()
		{
			CubicHexCoord cubic = new CubicHexCoord( 1, 0, -1 );
			CubicHexCoord[] spiral = CubicHexCoord.SpiralOutward( cubic, 2, DirectionEnum.W );

			Assert.That( spiral, Is.EquivalentTo( new CubicHexCoord[ 19 ] {
				// Center
				new CubicHexCoord(  1,  0, -1 ),
				// Distance 1
				new CubicHexCoord(  0,  1, -1 ),
				new CubicHexCoord(  1,  1, -2 ),
				new CubicHexCoord(  2,  0, -2 ),
				new CubicHexCoord(  2, -1, -1 ),
				new CubicHexCoord(  1, -1,  0 ),
				new CubicHexCoord(  0,  0,  0 ),
				// Distance 2
				new CubicHexCoord( -1,  2, -1 ), 
				new CubicHexCoord(  0,  2, -2 ),
				new CubicHexCoord(  1,  2, -3 ),
				new CubicHexCoord(  2,  1, -3 ),
				new CubicHexCoord(  3,  0, -3 ),
				new CubicHexCoord(  3, -1, -2 ),
				new CubicHexCoord(  3, -2, -1 ),
				new CubicHexCoord(  2, -2,  0 ),
				new CubicHexCoord(  1, -2,  1 ),
				new CubicHexCoord(  0, -1,  1 ),
				new CubicHexCoord( -1,  0,  1 ),
				new CubicHexCoord( -1,  1,  0 )
			} ) );
		}
		
		#endregion
	}
}
