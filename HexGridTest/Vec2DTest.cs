using System;
using NUnit.Framework;
using ca.axoninteractive.Geometry.Hex;

namespace ca.axoninteractive.Geometry.HexGridTest
{
	[TestFixture]
	public class Vec2DTest
	{
		// The range in which floating-point numbers are consider equal.
		public const float EPSILON = 0.000001f;


		#region Constructors

		[Test]
		public void ConstructorXZ()
		{
			Vec2D point = new Vec2D( 1f, 2f );

			Assert.That( point.x, Is.EqualTo( 1f ) );
			Assert.That( point.y, Is.EqualTo( 2f ) );
		}

		[Test]
		public void ConstructorParameterless()
		{
			Vec2D point = new Vec2D();

			Assert.That( point.x, Is.EqualTo( 0f ) );
			Assert.That( point.y, Is.EqualTo( 0f ) );
		}

		#endregion


		#region Operator Overloads

		[Test]
		public void OperatorOverloadPlus()
		{
			Vec2D point = new Vec2D( 1f, 2f ) + new Vec2D( 3f, 4f );

			Assert.That( point.x, Is.InRange<float>( 4f - EPSILON, 4f + EPSILON ) );
			Assert.That( point.y, Is.InRange<float>( 6f - EPSILON, 6f + EPSILON ) );
		}

		[Test]
		public void OperatorOverloadMinus()
		{
			Vec2D point = new Vec2D( 4f, 3f ) - new Vec2D( 1f, 2f );

			Assert.That( point.x, Is.InRange<float>( 3f - EPSILON, 3f + EPSILON ) );
			Assert.That( point.y, Is.InRange<float>( 1f - EPSILON, 1f + EPSILON ) );
		}

		#endregion


		#region Instance Methods

		[Test]
		public void Distance()
		{
			Vec2D point1 = new Vec2D( 1, 1 );
			Vec2D point2 = new Vec2D( 3, 3 );
			
			float length = point1.Distance( point2 );
			float expected = (float)( 2 * Math.Sqrt( 2 ) );
			
			Assert.That( length, Is.InRange<float>( expected - EPSILON, expected + EPSILON ) );
		}

		[Test]
		public void Length()
		{
			Vec2D point = new Vec2D( 2, 2 );
			
			float length = point.Length();
			float expected = (float)( 2 * Math.Sqrt( 2 ) );
			
			Assert.That( length, Is.InRange<float>( expected - EPSILON, expected + EPSILON ) );
		}

		#endregion
	}
}
