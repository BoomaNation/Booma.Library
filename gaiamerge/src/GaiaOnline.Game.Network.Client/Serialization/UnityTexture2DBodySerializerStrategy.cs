using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSafe.Http.Net;
using UnityEngine;

namespace GaiaOnline
{
	public sealed class UnityTexture2DBodySerializerStrategy : IRequestSerializationStrategy, IResponseDeserializationStrategy, IContentTypeAssociable
	{
		//TODO: Hide array behind for more efficient content-type lookup
		/// <inheritdoc />
		public IEnumerable<string> AssociatedContentType { get; }

		public UnityTexture2DBodySerializerStrategy()
		{
			//See: http://www.ietf.org/rfc/rfc4627.txt
			AssociatedContentType = new string[] { @"image/png" }; //TODO: Add more if needed
		}

		/// <inheritdoc />
		public bool TrySerialize(object content, IRequestBodyWriter writer)
		{
			throw new NotImplementedException($"Texture2D serialization is not supported yet.");
		}

		/// <inheritdoc />
		public TReturnType Deserialize<TReturnType>(IResponseBodyReader reader)
		{
			//We should just assume that the TReturnType is a Texture2DWrapper
			if (typeof(TReturnType) != typeof(Texture2DWrapper))
				throw new InvalidOperationException($"Tried to deserialize to image Type: {typeof(TReturnType).Name} but only {nameof(Texture2DWrapper)} is supported.");

			return (TReturnType)(object)new Texture2DWrapper(reader.ReadAsBytes());
		}

		/// <inheritdoc />
		public async Task<TReturnType> DeserializeAsync<TReturnType>(IResponseBodyReader reader)
		{
			//We should just assume that the TReturnType is a Texture2DWrapper
			if (typeof(TReturnType) != typeof(Texture2DWrapper))
				throw new InvalidOperationException($"Tried to deserialize to image Type: {typeof(TReturnType).Name} but only {nameof(Texture2DWrapper)} is supported.");

			return (TReturnType)(object)new Texture2DWrapper(await reader.ReadAsBytesAsync());
		}
	}
}
