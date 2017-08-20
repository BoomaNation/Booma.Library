using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Client
{
	public static class UnityAsyncTaskExtensions
	{
		/// <summary>
		/// Creates a continuation for the specified <see cref="Task{TResult}"/> that will
		/// invoke the provided <see cref="continuation"/> when the <see cref="future"/> completes.
		/// </summary>
		/// <typeparam name="T">The Task's TResult type.</typeparam>
		/// <param name="future">The task.</param>
		/// <param name="behaviour">The <see cref="MonoBehaviour"/> to schedule this continuation to run in.</param>
		/// <param name="continuation">The continuation.</param>
		public static Task<T> UnityAsyncContinueWith<T>(this Task<T> future, MonoBehaviour behaviour, Action<T> continuation)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			//Start the coroutine that continues on the Task's completion.
			behaviour.StartCoroutine(future.UnityAsyncCoroutine(continuation));

			//We fluently return this so you can register different types of continuations since this didn't produce new tasks
			return future;
		}

		/// <summary>
		/// Creates a continuation for the specified <see cref="Task{TResult}"/> that will
		/// invoke the provided <see cref="continuation"/> when the <see cref="future"/> completes.
		/// </summary>
		/// <typeparam name="T">The Task's TResult type.</typeparam>
		/// <param name="future">The task.</param>
		/// <param name="behaviour">The <see cref="MonoBehaviour"/> to schedule this continuation to run in.</param>
		/// <param name="continuation">The continuation.</param>
		/// <param name="optionalAdditionalContinuations">Optional additional continuations to schedule.</param>
		public static Task<T> UnityAsyncContinueWith<T>(this Task<T> future, MonoBehaviour behaviour, Action<T> continuation, params Action<T>[] optionalAdditionalContinuations)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			//Start the coroutine that continues on the Task's completion.
			behaviour.StartCoroutine(future.UnityAsyncCoroutine(continuation, optionalAdditionalContinuations));

			//We fluently return this so you can register different types of continuations since this didn't produce new tasks
			return future;
		}

		/// <summary>
		/// Creates a continuation for the specified <see cref="Task"/> that will
		/// invoke the provided <see cref="continuation"/> when the <see cref="future"/> completes.
		/// </summary>
		/// <param name="future">The task.</param>
		/// <param name="behaviour">The <see cref="MonoBehaviour"/> to schedule this continuation to run in.</param>
		/// <param name="continuation">The continuation.</param>
		/// <param name="optionalAdditionalContinuations">Optional additional continuations to schedule.</param>
		public static Task UnityAsyncContinueWith(this Task future, MonoBehaviour behaviour, Action continuation, params Action[] optionalAdditionalContinuations)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			//Start the coroutine that continues on the Task's completion.
			behaviour.StartCoroutine(future.UnityAsyncCoroutine(continuation, optionalAdditionalContinuations));

			//We fluently return this so you can register different types of continuations since this didn't produce new tasks
			return future;
		}

		/// <summary>
		/// Creates a continuation for the specified <see cref="Task"/> that will
		/// invoke the provided <see cref="continuation"/> when the <see cref="future"/> completes.
		/// </summary>
		/// <param name="future">The task.</param>
		/// <param name="behaviour">The <see cref="MonoBehaviour"/> to schedule this continuation to run in.</param>
		/// <param name="continuation">The continuation.</param>
		public static Task UnityAsyncContinueWith(this Task future, MonoBehaviour behaviour, Action continuation)
		{
			if (future == null) throw new ArgumentNullException(nameof(future));
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));
			if (continuation == null) throw new ArgumentNullException(nameof(continuation));

			//Start the coroutine that continues on the Task's completion.
			behaviour.StartCoroutine(future.UnityAsyncCoroutine(continuation));

			//We fluently return this so you can register different types of continuations since this didn't produce new tasks
			return future;
		}

		/// <summary>
		/// Creates a continuation for the specified <see cref="Task{TResult}"/> that will
		/// invoke the provided <see cref="continuation"/> when the <see cref="future"/> completes.
		/// The method returns a future for the <see cref="continuation"/>'s return value.
		/// </summary>
		/// <typeparam name="T">The Task's TResult type.</typeparam>
		/// <typeparam name="TResult">The result type of the <see cref="Func{T, TResult}"/> continuation.</typeparam>
		/// <param name="future">The task.</param>
		/// <param name="behaviour">The <see cref="MonoBehaviour"/> to schedule this continuation to run in.</param>
		/// <param name="continuation">The continuation.</param>
		/// <returns>A future that wraps the value of the <see cref="continuation"/>'s result.</returns>
		public static Task<TResult> UnityAsyncContinueWith<T, TResult>(this Task<T> future, MonoBehaviour behaviour, Func<T, TResult> continuation)
		{
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));

			//See: https://msdn.microsoft.com/en-us/library/dd449174(v=vs.110).aspx
			TaskCompletionSource<TResult> result = new TaskCompletionSource<TResult>();

			//Start the coroutine that continues on the Task's completion.
			behaviour.StartCoroutine(future.UnityAsyncCoroutine(continuation, result));

			return result.Task;
		}

		/// <summary>
		/// Creates a continuation for the specified <see cref="Task{TResult}"/> that will
		/// invoke the provided <see cref="continuation"/> when the <see cref="future"/> completes.
		/// Additionally it will set the value and completion of the returned task when the provided <see cref="continuation"/>'s
		/// returned Task is finished.
		/// </summary>
		/// <typeparam name="T">The Task's TResult type.</typeparam>
		/// <typeparam name="TResult">The result type of the <see cref="Func{T, TResult}"/> continuation.</typeparam>
		/// <param name="future">The task.</param>
		/// <param name="behaviour">The <see cref="MonoBehaviour"/> to schedule this continuation to run in.</param>
		/// <param name="continuation">The continuation.</param>
		/// <returns>A future for the provided <see cref="continuation"/>.</returns>
		public static Task<TResult> UnityAsyncContinueWith<T, TResult>(this Task<T> future, MonoBehaviour behaviour, Func<T, Task<TResult>> continuation)
		{
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));

			//See: https://msdn.microsoft.com/en-us/library/dd449174(v=vs.110).aspx
			TaskCompletionSource<TResult> result = new TaskCompletionSource<TResult>();

			//Start the coroutine that continues on the Task's completion.
			behaviour.StartCoroutine(future.UnityAsyncCoroutine(continuation, result));

			return result.Task;
		}

		/// <summary>
		/// Creates a continuation for the specified <see cref="Task{TResult}"/> that will
		/// invoke the provided <see cref="continuation"/> when the <see cref="future"/> completes.
		/// The method returns a future for the <see cref="continuation"/>'s return value.
		/// </summary>
		/// <typeparam name="T">The Task's TResult type.</typeparam>
		/// <typeparam name="TResult">The result type of the <see cref="Func{T, TResult}"/> continuation.</typeparam>
		/// <param name="future">The task.</param>
		/// <param name="behaviour">The <see cref="MonoBehaviour"/> to schedule this continuation to run in.</param>
		/// <param name="continuation">The continuation.</param>
		/// <returns>A future that wraps the value of the <see cref="continuation"/>'s result.</returns>
		public static Task<TResult> UnityAsyncContinueWith<T, TResult>(this Task<T> future, MonoBehaviour behaviour, Func<TResult> continuation)
		{
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));

			//See: https://msdn.microsoft.com/en-us/library/dd449174(v=vs.110).aspx
			TaskCompletionSource<TResult> result = new TaskCompletionSource<TResult>();

			//Start the coroutine that continues on the Task's completion.
			behaviour.StartCoroutine(future.UnityAsyncCoroutine(continuation, result));

			return result.Task;
		}

		/// <summary>
		/// Creates a continuation for the specified <see cref="Task{TResult}"/> that will
		/// invoke the provided <see cref="continuation"/> when the <see cref="future"/> completes.
		/// Additionally it will set the value and completion of the returned task when the provided <see cref="continuation"/>'s
		/// returned Task is finished.
		/// </summary>
		/// <typeparam name="T">The Task's TResult type.</typeparam>
		/// <typeparam name="TResult">The result type of the <see cref="Func{T, TResult}"/> continuation.</typeparam>
		/// <param name="future">The task.</param>
		/// <param name="behaviour">The <see cref="MonoBehaviour"/> to schedule this continuation to run in.</param>
		/// <param name="continuation">The continuation.</param>
		/// <returns>A future for the provided <see cref="continuation"/>.</returns>
		public static Task<TResult> UnityAsyncContinueWith<T, TResult>(this Task<T> future, MonoBehaviour behaviour, Func<Task<TResult>> continuation)
		{
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));

			//See: https://msdn.microsoft.com/en-us/library/dd449174(v=vs.110).aspx
			TaskCompletionSource<TResult> result = new TaskCompletionSource<TResult>();

			//Start the coroutine that continues on the Task's completion.
			behaviour.StartCoroutine(future.UnityAsyncCoroutine(continuation, result));

			return result.Task;
		}

		/// <summary>
		/// Creates a continuation for the specified <see cref="Task{TResult}"/> that will
		/// invoke the provided <see cref="continuation"/> when the <see cref="future"/> completes.
		/// The method returns a future for the <see cref="continuation"/>'s return value.
		/// </summary>
		/// <typeparam name="T">The Task's TResult type.</typeparam>
		/// <typeparam name="TResult">The result type of the <see cref="Func{T, TResult}"/> continuation.</typeparam>
		/// <param name="future">The task.</param>
		/// <param name="behaviour">The <see cref="MonoBehaviour"/> to schedule this continuation to run in.</param>
		/// <param name="continuation">The continuation.</param>
		/// <returns>A future that wraps the value of the <see cref="continuation"/>'s result.</returns>
		public static Task<TResult> UnityAsyncContinueWith<TResult>(this Task future, MonoBehaviour behaviour, Func<TResult> continuation)
		{
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));

			//See: https://msdn.microsoft.com/en-us/library/dd449174(v=vs.110).aspx
			TaskCompletionSource<TResult> result = new TaskCompletionSource<TResult>();

			//Start the coroutine that continues on the Task's completion.
			behaviour.StartCoroutine(future.UnityAsyncCoroutine(continuation, result));

			return result.Task;
		}

		/// <summary>
		/// Creates a continuation for the specified <see cref="Task{TResult}"/> that will
		/// invoke the provided <see cref="continuation"/> when the <see cref="future"/> completes.
		/// Additionally it will set the value and completion of the returned task when the provided <see cref="continuation"/>'s
		/// returned Task is finished.
		/// </summary>
		/// <typeparam name="T">The Task's TResult type.</typeparam>
		/// <typeparam name="TResult">The result type of the <see cref="Func{T, TResult}"/> continuation.</typeparam>
		/// <param name="future">The task.</param>
		/// <param name="behaviour">The <see cref="MonoBehaviour"/> to schedule this continuation to run in.</param>
		/// <param name="continuation">The continuation.</param>
		/// <returns>A future for the provided <see cref="continuation"/>.</returns>
		public static Task<TResult> UnityAsyncContinueWith<TResult>(this Task future, MonoBehaviour behaviour, Func<Task<TResult>> continuation)
		{
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));

			//See: https://msdn.microsoft.com/en-us/library/dd449174(v=vs.110).aspx
			TaskCompletionSource<TResult> result = new TaskCompletionSource<TResult>();

			//Start the coroutine that continues on the Task's completion.
			behaviour.StartCoroutine(future.UnityAsyncCoroutine(continuation, result));

			return result.Task;
		}
	}
}
