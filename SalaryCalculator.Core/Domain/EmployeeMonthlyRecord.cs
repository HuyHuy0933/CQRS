using SalaryCalculator.Core.Common;
using System.Linq;

namespace SalaryCalculator.Core.Domain
{
	public class EmployeeMonthlyRecord
	{
		public EmployeeMonthlyRecord(
			string name,
			string email,
			int workingDays,
			string employeeType,
			int numberOfDependants,
			int probationWorkingDays, 
			int standardWorkingDays, 
			bool inUnion, 
			GrossContractedSalary grossContractedSalary, 
			TaxableAllowance[] taxableAllowances,
			NonTaxableAllowance[] nonTaxableAllowances,
			PaymentAdvance paymentAdvance,
			AdjustmentAddition[] adjustmentAdditions,
			AdjustmentDeduction[] adjustmentDeduction,
			string position,
			bool isForeigner)
		{
			Name = name;
			Email = email;
			WorkingDays = workingDays;
			EmployeeType = employeeType;
			NumberOfDependants = numberOfDependants;
			ProbationWorkingDays = probationWorkingDays;
			StandardWorkingDays = standardWorkingDays;
			InUnion = inUnion;
			GrossContractedSalary = grossContractedSalary;
			TaxableAllowances = taxableAllowances;
			NonTaxableAllowances = nonTaxableAllowances;
			PaymentAdvance = paymentAdvance;
			AdjustmentAdditions = adjustmentAdditions;
			AdjustmentDeduction = adjustmentDeduction;
			Position = position;
			IsForeigner = isForeigner;
		}

		public string Name { get; }
		public string Email { get; }
		public int WorkingDays { get; set; }
		public bool InUnion { get; }
		public string Position { get; }
		public bool IsForeigner { get; set; }
		public GrossContractedSalary GrossContractedSalary { get; }
		public TaxableAllowance[] TaxableAllowances { get; }
		public NonTaxableAllowance[] NonTaxableAllowances { get; }
		public PaymentAdvance PaymentAdvance { get; }
		public AdjustmentAddition[] AdjustmentAdditions { get; }
		public AdjustmentDeduction[] AdjustmentDeduction { get; }
		public string EmployeeType { get; }
		public int ProbationWorkingDays { get; }
		public int StandardWorkingDays { get; }
		public int NumberOfDependants { get; }

		public bool IsOnProbation()
		{
			return EmployeeType == nameof(EmployeeTypeEnum.Probation);
		}

		public bool IsEligibleForSocialInsurance()
		{
			return WorkingDays >= 14;
		}
	}
}