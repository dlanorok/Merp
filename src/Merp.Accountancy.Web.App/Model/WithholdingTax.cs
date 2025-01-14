﻿namespace Merp.Accountancy.Web.App.Model
{
    public class WithholdingTax
    {
        public string Description { get; set; } = string.Empty;

        public decimal Rate { get; set; }

        public decimal TaxableAmountRate { get; set; }

        public decimal Amount { get; set; }
    }
}
