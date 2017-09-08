using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GaiaOnline
{
	//TODO: Document
	[Table("GameSessions")]
	public class GameSessionModel
	{
		/// <summary>
		/// This is the primary key that is mapped to <see cref="GaiaNameEntryModel"/>'s UserId key.
		/// It's a unique key that is manually generated.
		/// </summary>
		[Key]
		[Required]
		[ForeignKey(nameof(User))]
		public int UserId { get; private set; }

		//TODO: Determine if lazy loading is better than immediate loading
		/// <summary>
		/// The foreign table entry for the user associated with the session.
		/// </summary>
		[Required]
		public virtual GaiaNameEntryModel User { get; private set; }

		[Required]
		public Guid SessionGuid { get; private set; }

		[Required]
		public string SessionIp { get; private set; }

		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime SessionCreationTime { get; private set; }

		public GameSessionModel(int userId, Guid sessionGuid, string sessionIp)
		{
			if(sessionIp == null) throw new ArgumentNullException(nameof(sessionIp));
			if(userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

			UserId = userId;
			SessionGuid = sessionGuid;
			SessionIp = sessionIp;
		}

		protected GameSessionModel()
		{
			
		}
	}
}
