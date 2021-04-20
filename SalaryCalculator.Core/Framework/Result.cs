using System;

namespace SalaryCalculator.Core.Framework
{
	public class Result<T>
	{
		public bool Failure
		{
			get => !Success;
		}

		public bool Success { get; }
		public T Data { get; }
		public Error Error { get; }

		private Result(T data, bool success, Error error)
		{
			Data = data;
			Success = success;
			Error = error;
		}

		public static Result<T> Ok(T data) => new Result<T>(data, true, default);
		public static Result<T> Fail(Error err) => new Result<T>(default, false, err);

		public static implicit operator Result<T>(Error err) => new Result<T>(default, false, err);
	}
}