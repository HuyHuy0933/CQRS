using System;

namespace SalaryCalculator.Core.Framework
{
	public class Error : IEquatable<Error>
	{
		internal Error(string code, string message)
		{
			Code = code;
			Message = message;
		}

		public string Code { get; }
		public string Message { get; }

		public bool Equals(Error other)
		{
			if (other is null)
			{
				return false;
			}

			return Code == other.Code;
		}

		public override bool Equals(object obj)
		{
			if (obj is Error)
			{
				return Equals(obj);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return (Code.GetHashCode() + Message.GetHashCode()) ^ 2;
		}
	}
}