using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Booma
{
	//TODO: Document
	[Table("GameSessions")]
	public class GameSessionModel
	{
		/// <summary>
		/// The unique primary key that can reference the character this session is for.
		/// </summary>
		[Key]
		[Required]
		//[ForeignKey(nameof(User))]
		public int CharacterId { get; private set; }

		/// <summary>
		/// Unique GUID for the session.
		/// </summary>
		[Required]
		public Guid SessionGuid { get; private set; }

		/// <summary>
		/// The IP the session was created for.
		/// </summary>
		[Required]
		public string SessionIp { get; private set; }

		/// <summary>
		/// The time that the session was created.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime SessionCreationTime { get; private set; }

		public GameSessionModel(int userId, Guid sessionGuid, string sessionIp)
		{
			if(sessionIp == null) throw new ArgumentNullException(nameof(sessionIp));
			if(userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

			CharacterId = userId;
			SessionGuid = sessionGuid;
			SessionIp = sessionIp;
		}

		protected GameSessionModel()
		{
			
		}
	}
}
