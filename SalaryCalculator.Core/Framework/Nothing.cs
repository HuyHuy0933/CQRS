﻿namespace SalaryCalculator.Core.Framework
{
	public class Nothing
	{
		public static Nothing Value => new Nothing();

		private Nothing()
		{
		}

		public override string ToString()
		{
			return "Nothing";
		}
	}
}