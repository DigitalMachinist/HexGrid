using System;
using NUnit.Framework;
using ca.axoninteractive.Geometry.Hex;

namespace ca.axoninteractive.Geometry.HexGridTest
{
	[TestFixture]
	public class FloatAxialTest
	{
		// The range in which floating-point numbers are consider equal.
		public const float EPSILON = 0.000001f;

		#region Constructors

		[Test]
		public void ConstructorAxial()
		{
			AxialHexCoord axial = new AxialHexCoord( 1, 2 );
			FloatAxial floatAxial = new FloatAxial( axial );
			
			Assert.That( floatAxial.q, Is.InRange<float>( 1f - EPSILON, 1f + EPSILON ) );
			Assert.That( floatAxial.r, Is.InRange<float>( 2f - EPSILON, 2f + EPSILON ) );
		}

		[Test]
		public void ConstructorQR()
		{
			FloatAxial floatAxial = new FloatAxial( 1f, 2f );

			Assert.That( floatAxial.q, Is.EqualTo( 1f ) );
			Assert.That( floatAxial.r, Is.EqualTo( 2f ) );
		}

		[Test]
		public void ConstructorParameterless()
		{
			FloatAxial floatAxial = new FloatAxial();

			Assert.That( floatAxial.q, Is.EqualTo( 0f ) );
			Assert.That( floatAxial.r, Is.EqualTo( 0f ) );
		}

		#endregion

		#region Type Conversions

		[Test]
		public void ToFloatCubic()
		{
			FloatCubic floatCubic = new FloatAxial( 1f, 2f ).ToFloatCubic();

			Assert.That( floatCubic.x, Is.InRange<float>(  1f - EPSILON,  1f + EPSILON ) );
			Assert.That( floatCubic.y, Is.InRange<float>( -3f - EPSILON, -3f + EPSILON ) );
			Assert.That( floatCubic.z, Is.InRange<float>(  2f - EPSILON,  2f + EPSILON ) );
		}

		#endregion
	}
}
