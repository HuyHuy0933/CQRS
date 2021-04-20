using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Domain;

namespace SalaryCalculator.Application.Common.Helpers
{
	public interface IFileHelper
	{
		Task<byte[]> CreateCsvByteArrayAsync(IEnumerable<ExportEmpMonthlySalary> records,
			SalaryConfig salaryConfig, string yearMonth);
	}

	public class FileHelper : IFileHelper
	{
		public async Task<byte[]> CreateCsvByteArrayAsync(
			IEnumerable<ExportEmpMonthlySalary> records,
			SalaryConfig salaryConfig,
			string yearMonth)
		{
			using (var memoryStream = new MemoryStream())
			using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
			using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
			{
				csvWriter.Configuration.RegisterClassMap<ExportEmpMonthlySalaryMap>();
				WriteHeader(csvWriter, records);
				csvWriter.WriteRecords(records);

				await streamWriter.FlushAsync();
				return memoryStream.ToArray();
			}
		}

		private static void WriteHeader(CsvWriter csvWriter, IEnumerable<object> records)
		{
			// Need this otherwise no header when having no data
			var recordType = records.GetType().GetGenericArguments()[0];
			csvWriter.WriteHeader(recordType);
			csvWriter.NextRecord();
		}
	}
}
