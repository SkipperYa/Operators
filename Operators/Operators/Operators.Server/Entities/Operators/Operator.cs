using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Server.Entities.Operators
{
	public class Operator
	{
		public string Name { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Code { get; set; }
	}
}
