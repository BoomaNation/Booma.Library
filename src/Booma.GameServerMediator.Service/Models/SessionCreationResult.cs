using System;

namespace GaiaOnline
{
	public sealed class SessionCreationResult
	{
		private Guid? _SessionGuid { get; }

		/// <summary>
		/// The session guid.
		/// </summary>
		public Guid SessionGuid => _SessionGuid.Value;

		/// <summary>
		/// Indicates if the session was succesfully created.
		/// </summary>
		public bool isSessionCreated => _SessionGuid.HasValue;

		/// <summary>
		/// Creates a failed session
		/// </summary>
		public SessionCreationResult()
		{
			_SessionGuid = null;
		}

		public SessionCreationResult(Guid sessionGuid)
		{
			_SessionGuid = sessionGuid;
		}
	}
}