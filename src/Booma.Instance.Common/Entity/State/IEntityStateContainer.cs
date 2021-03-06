﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booma
{
	public interface IEntityStateContainer
	{
		byte State { get; set; }
	}

	public interface IEntityStateContainer<TEntityStateType> : IEntityStateContainer
	{
		new TEntityStateType State { get; set; }
	}
}
