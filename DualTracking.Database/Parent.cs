using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualTracking.Database
{
	public class Parent : IdentityUser
	{
		public Parent()
		{
			this.Children = new HashSet<Child>();
		}
		
		public string? Address { get; set; }

		public virtual ICollection<Child> Children { get; set; }
	}
}
