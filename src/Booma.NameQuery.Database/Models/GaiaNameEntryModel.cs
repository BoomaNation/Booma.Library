using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Model represents a Gaia Online avatar entry.
	/// Containing the Avatar username and the userid.
	/// </summary>
	[Table("AvatarEntries")]
	public sealed class GaiaNameEntryModel
	{
		/// <summary>
		/// The primary key.
		/// Represents the GaiaOnline UserId they associate accounts with by name.
		/// </summary>
		[Key]
		public int UserId { get; private set; }

		[Required]
		public string AvatarUsername { get; private set; }

		public GaiaNameEntryModel(int userId, string avatarUsername)
		{
			if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

			UserId = userId;
			AvatarUsername = avatarUsername;
		}

		protected GaiaNameEntryModel()
		{
			
		}
	}
}
