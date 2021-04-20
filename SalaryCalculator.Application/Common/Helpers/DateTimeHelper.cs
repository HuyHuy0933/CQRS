using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalaryCalculator.Application.Common.Helpers
{
	public static class DateTimeHelper
	{
		public static DateTime ToDateTime(this long timestamp)
		{
			return CalendarFirstDate().AddMilliseconds(timestamp);
		}

		public static DateTime? ToDateTimeOrNull(this long timestamp)
		{
			if (timestamp == 0) return null;
			return CalendarFirstDate().AddMilliseconds(timestamp);
		}

		public static DateTime[] ToDateTimeArray(this long[] timestamps)
		{
			if (timestamps.Length == 0) return Array.Empty<DateTime>();
			return timestamps.Select(x => x == -1 ? DateTime.MinValue : CalendarFirstDate().AddMilliseconds(x)).ToArray();
		}

		public static DateTime CalendarFirstDate()
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0);
		}
	}
}
