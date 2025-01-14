﻿using MementoFX;
using Merp.Registry.CommandStack.Commands;
using Merp.Registry.QueryStack;
using Merp.Registry.Web.Api.Internal.Models;
using Merp.Web;
using Microsoft.EntityFrameworkCore;
using Rebus.Bus;

namespace Merp.Registry.Web.Api.Internal
{
    public class PartyApiServices : IPartyApiServices
    {
        public IDatabase Database { get; }
        public IBus Bus { get; }
        public IAppContext AppContext { get; }

        public PartyApiServices(IDatabase database, IBus bus, IAppContext appContext)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
            Bus = bus ?? throw new ArgumentNullException(nameof(bus));
            AppContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        }

        public async Task<IEnumerable<PartyInfoModel>> GetPartyInfoByPatternAsync(string query)
        {
            var model = await Database.Parties
                .Where(p => p.DisplayName.StartsWith(query))
                .OrderBy(p => p.DisplayName)
                .Select(p => new PartyInfoModel { Id = p.Id, OriginalId = p.OriginalId, Name = p.DisplayName })
                .ToArrayAsync();

            return model;
        }

        public async Task<PartyInfoModel?> GetPartyInfoByIdAsync(Guid partyId)
        {
            var party = await Database.Parties.SingleOrDefaultAsync(p => p.OriginalId == partyId);
            if (party is null)
            {
                return null;
            }

            var model = new PartyInfoModel { Id = party.Id, OriginalId = party.OriginalId, Name = party.DisplayName };
            return model;
        }

        public async Task RegisterPartyAsync(RegisterPartyModel model)
        {
            Command command;
            var userId = AppContext.UserId;
            var nationalIdentificationNumber = string.IsNullOrWhiteSpace(model.NationalIdentificationNumber) ? default : model.NationalIdentificationNumber.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                command = new RegisterCompanyCommand(
                    userId,
                    model.LastNameOrCompanyName,
                    nationalIdentificationNumber,
                    model.VatNumber,
                    model.Address.Address,
                    model.Address.PostalCode,
                    model.Address.City,
                    model.Address.Province,
                    model.Address.Country,
                    model.Address.Address,
                    model.Address.PostalCode,
                    model.Address.City,
                    model.Address.Province,
                    model.Address.Country,
                    model.Address.Address,
                    model.Address.PostalCode,
                    model.Address.City,
                    model.Address.Province,
                    model.Address.Country,
                    null, null, null, null, null, null);
            }
            else
            {
                command = new RegisterPersonCommand(
                    userId,
                    model.FirstName,
                    model.LastNameOrCompanyName,
                    nationalIdentificationNumber,
                    model.VatNumber,
                    model.Address.Address,
                    model.Address.PostalCode,
                    model.Address.City,
                    model.Address.Province,
                    model.Address.Country,
                    model.Address.Address,
                    model.Address.PostalCode,
                    model.Address.City,
                    model.Address.Province,
                    model.Address.Country,
                    model.Address.Address,
                    model.Address.PostalCode,
                    model.Address.City,
                    model.Address.Province,
                    model.Address.Country,
                    null, null, null, null, null, null);
            }

            await Bus.Send(command);
        }

        public async Task<PartyBillingInfo?> GetPartyBillingInfoByPartyIdAsync(Guid partyId)
        {
            var party = await Database.Parties.SingleOrDefaultAsync(p => p.OriginalId == partyId);
            if (party is null)
            {
                return null;
            }

            var address = party.BillingAddress is null ?
                null :
                new PostalAddress
                {
                    Address = party.LegalAddress.Address,
                    City = party.LegalAddress.City,
                    Province = party.LegalAddress.Province,
                    Country = party.LegalAddress.Country,
                    PostalCode = party.LegalAddress.PostalCode
                };

            var billingInfo = new PartyBillingInfo
            {
                VatIndex = party.VatIndex,
                NationalIdentificationNumber = party.NationalIdentificationNumber,
                Address = address
            };

            return billingInfo;
        }
    }
}
