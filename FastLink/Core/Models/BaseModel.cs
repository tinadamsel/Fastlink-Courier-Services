﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class BaseModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public DateTime DateCreated { get; set; }
		public bool Active { get; set; }
		public bool Deleted { get; set; }

	}
}
