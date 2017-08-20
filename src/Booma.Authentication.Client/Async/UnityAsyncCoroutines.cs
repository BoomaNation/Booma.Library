using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Booma.Authentication.Client.Async;
using UnityEngine;

namespace Booma.Client
{
	internal static class UnityAsyncCoroutines
	{
		internal static IEnumerator UnityAsyncCoroutine<T>(this Task<T> future, Action<T> continuation)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			yield return new WaitForFuture(future);

			//Result will throw if we encounted exceptions but it will be aggregate exception
			continuation(future.Result);
		}

		//You may wonder why this overload exists. It's for efficiency reasons so we don't need to wrap the a single continuation
		//in a concated enumerable.
		internal static IEnumerator UnityAsyncCoroutine<T>(this Task<T> future, Action<T> continuation, IEnumerable<Action<T>> continuations)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));
			if (continuations == null) throw new ArgumentNullException(nameof(continuations));

			yield return new WaitForFuture(future);

			//Result will throw if we encounted exceptions but it will be aggregate exception
			//We call the first continuation
			continuation(future.Result);

			 continuations.DispatchContinuations(future.Result);
		}

		internal static IEnumerator UnityAsyncCoroutine(this Task future, Action continuation)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			yield return new WaitForFuture(future);

			//Result will throw if we encounted exceptions but it will be aggregate exception
			continuation();
		}

		//You may wonder why this overload exists. It's for efficiency reasons so we don't need to wrap the a single continuation
		//in a concated enumerable.
		internal static IEnumerator UnityAsyncCoroutine(this Task future, Action continuation, IEnumerable<Action> continuations)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));
			if (continuations == null) throw new ArgumentNullException(nameof(continuations));

			yield return new WaitForFuture(future);

			//Result will throw if we encounted exceptions but it will be aggregate exception
			//We call the first continuation
			continuation();

			continuations.DispatchContinuations();
		}

		internal static IEnumerator UnityAsyncCoroutine<T, TResult>(this Task<T> future, Func<T, Task<TResult>> continuation, TaskCompletionSource<TResult> result)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			yield return new WaitForFuture(future);

			Task<TResult> resultValue;
			try
			{
				//Result will throw if we encounted exceptions but it will be aggregate exception
				resultValue = continuation(future.Result);
			}
			catch (Exception e)
			{
				result.SetException(e);
				yield break;
			}

			//Now unlike the non task func we must wait for the new task to finish before we set its completion source
			yield return new WaitForFuture(resultValue);

			result.SetResult(resultValue.Result);
		}

		internal static IEnumerator UnityAsyncCoroutine<T, TResult>(this Task<T> future, Func<T, TResult> continuation, TaskCompletionSource<TResult> result)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			yield return new WaitForFuture(future);

			TResult resultValue;
			try
			{
				//Result will throw if we encounted exceptions but it will be aggregate exception
				resultValue = continuation(future.Result);
			}
			catch (Exception e)
			{
				result.SetException(e);
				yield break;
			}

			result.SetResult(resultValue);
		}

		internal static IEnumerator UnityAsyncCoroutine<T, TResult>(this Task<T> future, Func<Task<TResult>> continuation, TaskCompletionSource<TResult> result)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			yield return new WaitForFuture(future);

			Task<TResult> resultValue;
			try
			{
				//Result will throw if we encounted exceptions but it will be aggregate exception
				resultValue = continuation();
			}
			catch (Exception e)
			{
				result.SetException(e);
				yield break;
			}

			//Now unlike the non task func we must wait for the new task to finish before we set its completion source
			yield return new WaitForFuture(resultValue);

			result.SetResult(resultValue.Result);
		}

		internal static IEnumerator UnityAsyncCoroutine<T, TResult>(this Task<T> future, Func<TResult> continuation, TaskCompletionSource<TResult> result)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			yield return new WaitForFuture(future);

			TResult resultValue;
			try
			{
				//Result will throw if we encounted exceptions but it will be aggregate exception
				resultValue = continuation();
			}
			catch (Exception e)
			{
				result.SetException(e);
				yield break;
			}

			result.SetResult(resultValue);
		}

		internal static IEnumerator UnityAsyncCoroutine<TResult>(this Task future, Func<Task<TResult>> continuation, TaskCompletionSource<TResult> result)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			yield return new WaitForFuture(future);

			Task<TResult> resultValue;
			try
			{
				//Result will throw if we encounted exceptions but it will be aggregate exception
				resultValue = continuation();
			}
			catch (Exception e)
			{
				result.SetException(e);
				yield break;
			}

			//Now unlike the non task func we must wait for the new task to finish before we set its completion source
			yield return new WaitForFuture(resultValue);

			result.SetResult(resultValue.Result);
		}

		internal static IEnumerator UnityAsyncCoroutine<TResult>(this Task future, Func<TResult> continuation, TaskCompletionSource<TResult> result)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			yield return new WaitForFuture(future);

			TResult resultValue;
			try
			{
				//Result will throw if we encounted exceptions but it will be aggregate exception
				resultValue = continuation();
			}
			catch (Exception e)
			{
				result.SetException(e);
				yield break;
			}

			result.SetResult(resultValue);
		}
	}
}