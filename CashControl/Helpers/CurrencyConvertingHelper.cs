using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Enums;
using static CashControl.Enums.Currency;

namespace CashControl.Helpers
{
    public static class CurrencyConvertingHelper
    {
        public static double Convert(Currency From, Currency To, double Amount)
        {
            var fromRelation = GetRelationFor(From);
            var fromDollars = fromRelation * Amount;
            var toRelation = GetRelationFor(To);
            var result = fromDollars / toRelation;

            return result;
        }

        private static double GetRelationFor(Currency Currency)
        {
            switch (Currency)
            {
                case BYN:
                    return 0.47;
                case USD:
                    return 1;
                case EUR:
                    return 1.13;
                case UAH:
                    return 0.036;
                case RUB:
                    return 0.015;
                default: return 1;
            }
        }

        public static Currency ToCurrency(this string value)
        {
            return (Currency)Enum.Parse(typeof(Currency), value, true);
        }
    }
}
