using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Booma
{
	//TODO: Document
	[Table("character_sessions")]
	public class GameSessionModel
	{
		/// <summary>
		/// The unique primary key that can reference the character this session is for.
		/// </summary>
		[Key]
		[Required]
		[ForeignKey(nameof(Character))]
		public int CharacterId { get; private set; }

		/// <summary>
		/// The parent character table reference.
		/// </summary>
		public virtual CharacterDatabaseModel Character { get; private set; }

		/// <summary>
		/// Unique GUID for the session.
		/// </summary>
		[Required]
		public Guid SessionGuid { get; private set; }

		/// <summary>
		/// The IP the session was created for.
		/// </summary>
		[StringLength(15, MinimumLength = 7)] //IP constraints
		public string SessionIp { get; private set; }

		/// <summary>
		/// The time that the session was created.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime SessionCreationTime { get; private set; }

		/// <summary>
		/// Indicates if the session is claimed or live.
		/// This means essentially that the character is logged in.
		/// </summary>
		[Required]
		[Column("IsClaimed")]
		[DefaultValue(false)]
		public bool isSessionClaimed { get; set; } //public for mutability

		public GameSessionModel(int characterId, Guid sessionGuid, string sessionIp)
		{
			if(sessionIp == null) throw new ArgumentNullException(nameof(sessionIp));
			if(characterId < 0) throw new ArgumentOutOfRangeException(nameof(characterId));

			CharacterId = characterId;
			SessionGuid = sessionGuid;
			SessionIp = sessionIp;
		}

		protected GameSessionModel()
		{
			
		}
	}
}
