namespace SalaryCalculator.Core.Domain
{
	public class TaxableAllowance
	{
		public TaxableAllowance(
			string name, 
			Money amount)
		{
			Name = name;
			Amount = amount;
		}

		public Money Amount { get; }
		public string Name { get; }
	}
}