using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BisoftMobileWCFService.HelpClasses
{
    public static class TimeDateFunctions
    {
        public static DateTime GetNextDate(DateTime start, string unit, int nr)
        {
            DateTime next;

            switch (unit.ToLower())
            {
                case "dag":
                    next = start.AddDays(nr);
                    break;
                case "dagar":
                     next = start.AddDays(nr);
                    break;
                case "vecka":
                    next = start.AddDays(nr * 7);
                    break;
                case "veckor":
                    next = start.AddDays(nr * 7);
                    break;
                case "månad":
                    next = start.AddMonths(nr);
                    break;
                case "månader":
                    next = start.AddMonths(nr);
                    break;
                case "år":
                    next = start.AddYears(nr);
                    break;
                default:
                    next = DateTime.Now;
                    break;

            }

            return next;
        }
    }
}