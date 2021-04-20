namespace SalaryCalculator.Core.Domain
{
	public class ActualGrossSalary
	{
		public Money Amount { get; }

		public ActualGrossSalary(
			GrossContractedSalary grossContractedSalary, 
			int standardWorkingDays, 
			int probationWorkingDays,
			int workingDays)
		{
			Amount = (grossContractedSalary.Amount / standardWorkingDays * workingDays).Round();

			if (probationWorkingDays > 0)
			{
				Amount = ((grossContractedSalary.Amount / standardWorkingDays *
					probationWorkingDays)).Round();
			}
		}
	}
}