using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Application.Common.Converters
{
	public class SalaryConverter : DefaultTypeConverter
	{
		public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
		{
			if(Int32.TryParse(value.ToString(), out int result1))
			{
				return result1 == 0 ? "-" : $"{result1}";
			}

			Decimal.TryParse(value.ToString(), out decimal result2);
			return result2 == 0 ? "-"  : $"{result2}";
		}
	}
}
