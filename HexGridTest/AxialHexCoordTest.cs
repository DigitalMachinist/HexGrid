using System;
using NUnit.Framework;
using ca.axoninteractive.Geometry.Hex;

namespace ca.axoninteractive.Geometry.HexGridTest
{
	[TestFixture]
	public class AxialHexCoordTest
	{
		#region Constructors

		[Test]
		public void ConstructorQR()
		{
			AxialHexCoord axial = new AxialHexCoord( 1, 2 );

			Assert.That( axial.q, Is.EqualTo( 1 ) );
			Assert.That( axial.r, Is.EqualTo( 2 ) );
		}

		[Test]
		public void ConstructorParameterless()
		{
			AxialHexCoord axial = new AxialHexCoord();

			Assert.That( axial.q, Is.EqualTo( 0 ) );
			Assert.That( axial.r, Is.EqualTo( 0 ) );
		}

		#endregion

		#region Type Conversions

		[Test]
		public void ToCubic()
		{
			CubicHexCoord cubic = new AxialHexCoord( 1, 2 ).ToCubic();

			Assert.That( cubic.x, Is.EqualTo(  1 ) );
			Assert.That( cubic.y, Is.EqualTo( -3 ) );
			Assert.That( cubic.z, Is.EqualTo(  2 ) );
		}

		#endregion

		#region Operator Overloads

		[Test]
		public void OperatorOverloadPlus()
		{
			AxialHexCoord axial = new AxialHexCoord( 1, 2 ) + new AxialHexCoord( 3, 4 );

			Assert.That( axial.q, Is.EqualTo( 4 ) );
			Assert.That( axial.r, Is.EqualTo( 6 ) );
		}

		[Test]
		public void OperatorOverloadMinus()
		{
			AxialHexCoord axial = new AxialHexCoord( 4, 3 ) - new AxialHexCoord( 1, 2 );

			Assert.That( axial.q, Is.EqualTo( 3 ) );
			Assert.That( axial.r, Is.EqualTo( 1 ) );
		}

		[Test]
		public void OperatorOverloadEquals()
		{
			bool isTrue  = new AxialHexCoord( 1, 2 ) == new AxialHexCoord( 1, 2 );
			bool isFalse = new AxialHexCoord( 1, 2 ) == new AxialHexCoord( 3, 4 );

			Assert.That( isTrue,  Is.True  );
			Assert.That( isFalse, Is.False );
		}

		[Test]
		public void OperatorOverloadNotEquals()
		{
			bool isTrue  = new AxialHexCoord( 1, 2 ) != new AxialHexCoord( 3, 4 );
			bool isFalse = new AxialHexCoord( 1, 2 ) != new AxialHexCoord( 1, 2 );
			
			Assert.That( isTrue,  Is.True  );
			Assert.That( isFalse, Is.False );
		}

		#endregion
	}
}
