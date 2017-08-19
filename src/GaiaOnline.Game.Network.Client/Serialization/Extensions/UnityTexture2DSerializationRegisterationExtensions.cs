using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	public static class UnityTexture2DSerializationRegisterationExtensions
	{
		/// <summary>
		/// Registers a Texture2D serializer in the serializer registeration service.
		/// </summary>
		/// <param name="builder">The builder being built.</param>
		/// <returns>The builder for fluent chaining.</returns>
		public static TSerializationRegisterationType RegisterUnityTexture2DSerializer<TSerializationRegisterationType>(this TSerializationRegisterationType builder)
			where TSerializationRegisterationType : ISerializationStrategyRegister
		{
			//TODO: Once we support sending images, which we probably won't, fix the attribtue to add a real one.
			builder.Register<FakeTempAttribute, UnityTexture2DBodySerializerStrategy>(new UnityTexture2DBodySerializerStrategy());

			//fluently return
			return builder;
		}

		private class FakeTempAttribute : BodyContentAttribute
		{
			
		}
	}
}
