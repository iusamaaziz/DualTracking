using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualTracking.Database
{
	public class Child
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public string? ParentId { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? Gender { get; set; }

		//public Parent? Parent { get; set; }
	}
}
