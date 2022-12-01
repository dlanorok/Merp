﻿namespace Merp.Registry.Web.Api.Internal.Models
{
    public class PartyLegalInfo
    {
        public string? VatIndex { get; set; }

        public string? NationalIdentificationNumber { get; set; }

        public PostalAddress? Address { get; set; }
    }
}