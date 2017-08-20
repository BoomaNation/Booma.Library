using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Authentication.Client.Async
{
	/// <summary>
	/// Waits for the provided <see cref="IAsyncResult"/> to be completed.
	/// </summary>
	internal sealed class WaitForFuture : IEnumerator
	{
		/// <summary>
		/// The async result being tracked.
		/// </summary>
		public IAsyncResult AsyncResult { get; }

		//TODO: Is it safe to leave this as null?
		/// <inheritdoc />
		public object Current { get; }

		public WaitForFuture(IAsyncResult asyncResult)
		{
			if (asyncResult == null) throw new ArgumentNullException(nameof(asyncResult));

			AsyncResult = asyncResult;
		}

		/// <inheritdoc />
		public bool MoveNext()
		{
			//Indicate the the enumerator is still going until the async result is complete.
			return !AsyncResult.IsCompleted;
		}

		/// <inheritdoc />
		public void Reset()
		{
			//See: https://msdn.microsoft.com/en-us/library/system.collections.ienumerator(v=vs.110).aspx
			//The Reset method is provided for COM interoperability and does not need to be fully implemented; instead, the implementer can throw a NotSupportedException.
			throw new NotSupportedException();
		}
	}
}
