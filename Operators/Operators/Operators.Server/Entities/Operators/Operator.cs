using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Server.Entities.Operators
{
	public class Operator
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Code { get; set; }
		public string Name { get; set; }
	}
}
