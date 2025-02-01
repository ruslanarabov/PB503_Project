using System;
namespace PB503Project.Models
{
	public class BaseEntity
	{
		public int Id { get; set; }

		public DateTime CreateAt { get; set; }

		public DateTime UpdateAt { get; set; }

		public bool IsDeleted { get; set; }

	}


}

