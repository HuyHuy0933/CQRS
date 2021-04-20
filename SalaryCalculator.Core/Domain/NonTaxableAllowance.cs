namespace SalaryCalculator.Core.Domain
{
	public class NonTaxableAllowance
	{
		public NonTaxableAllowance(
			Money amount, 
			string name)
		{
			Amount = amount;
			Name = name;
		}

		public Money Amount { get; }
		public string Name { get; }
	}
}