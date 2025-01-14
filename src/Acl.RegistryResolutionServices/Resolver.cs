﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Acl.RegistryResolutionServices
{
    public class Resolver
    {
        private static IEnumerable<string> ViesCountryCodes = new string[] { "AT", "BE", "BG", "CY", "CZ", "DE", "DK", "EE", "EL", "ES", "FI", "FR", "GB", "HR", "HU", "IE", "IT", "LT", "LU", "LV", "MT", "NL", "PL", "PT", "RO", "SE", "SI", "SK" };

        private readonly CompanyInformationMapperFactory _companyInformationMapperFactory;

        private readonly PersonInformationMapperFactory _personInformationMapperFactory;

        public Resolver()
        {
            _companyInformationMapperFactory = new CompanyInformationMapperFactory();
            _personInformationMapperFactory = new PersonInformationMapperFactory();
        }

        public async Task<CompanyInformation> LookupCompanyInfoByVatNumberAsync(string countryCode, string vatNumber)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                throw new ArgumentException("countryCode must be provided", nameof(countryCode));
            }

            if (!ViesCountryCodes.Contains(countryCode))
            {
                throw new ArgumentOutOfRangeException("The provided country code is not accepted by VIES service");
            }

            if (string.IsNullOrWhiteSpace(vatNumber))
            {
                throw new ArgumentException("vatNumber must be provided", nameof(vatNumber));
            }

            var clientRequest = new Vies.checkVatRequest(countryCode, vatNumber);
            var channel = new Vies.checkVatPortTypeClient();              
            var checkVatResponse = await channel.checkVatAsync(clientRequest);
            var mapper = _companyInformationMapperFactory.CreateMapper(countryCode);

            if (checkVatResponse == null || !mapper.CanMap(checkVatResponse))
            {
                return null;
            }
                
            var companyInformation = mapper.Map(checkVatResponse);

            return companyInformation;                
                
        }

        public async Task<PersonInformation> LookupPersonInfoByVatNumberAsync(string countryCode, string vatNumber)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                throw new ArgumentException("countryCode must be provided", nameof(countryCode));
            }

            if (!ViesCountryCodes.Contains(countryCode))
            {
                throw new ArgumentOutOfRangeException("The provided country code is not accepted by VIES service");
            }

            if (string.IsNullOrWhiteSpace(vatNumber))
            {
                throw new ArgumentException("vatNumber must be provided", nameof(vatNumber));
            }

            var clientRequest = new Vies.checkVatRequest(countryCode, vatNumber);
            var channel = new Vies.checkVatPortTypeClient();
            var checkVatResponse = await channel.checkVatAsync(clientRequest);
            var mapper = _personInformationMapperFactory.CreateMapper(countryCode);

            if (checkVatResponse == null || !mapper.CanMap(checkVatResponse))
            {
                return null;
            }

            var personInformation = mapper.Map(checkVatResponse);

            return personInformation;

        }
    }
}
