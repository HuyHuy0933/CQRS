using System.Linq;

namespace SalaryCalculator.Core.Domain
{
	public class PersonalIncomeTax
	{
		public PersonalIncomeTax(
			Money amount)
		{
			Amount = amount;
		}

		public Money Amount { get; }
	}
}