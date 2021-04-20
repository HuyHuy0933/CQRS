namespace SalaryCalculator.Core.Domain
{
	public class TotalSalaryCost
	{
		public TotalSalaryCost(
			TotalMonthlyIncome totalMonthlyIncome,
			EmployerSocialInsurance employerSocialInsurance,
			EmployerHealthcareInsurance employerHealthcareInsurance,
			EmployerUnemploymentInsurance employerUnemploymentInsurance,
			EmployerUnionFee employerUnionFee)
		{
			Amount = totalMonthlyIncome.Amount +
			         employerSocialInsurance.Amount +
			         employerHealthcareInsurance.Amount +
			         employerUnemploymentInsurance.Amount +
			         employerUnionFee.Amount;
		}

		public Money Amount { get; }
	}
}