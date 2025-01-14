﻿using MementoFX.Persistence;
using Merp.Registry.QueryStack;
using Merp.Registry.Web.App.Components;
using Merp.Registry.Web.App.Model;
using Microsoft.AspNetCore.Components;
using Rebus.Bus;
using System.ComponentModel.DataAnnotations;

namespace Merp.Registry.Web.App.Pages
{
    public partial class EditPerson
    {
        [Inject] IDatabase Database { get; set; }
        [Inject] IRepository Repository { get; set; }
        [Inject] IBus Bus { get; set; }

        [Parameter]
        public Guid PersonId { get; set; }

        public ViewModel Model = new();

        private void VatNumberLookup(VatNumber.PartyInfo partyInfo)
        {
            Model.FirstName = partyInfo.FirstName;
            Model.LastName = partyInfo.LastName;
            Model.LegalAddress.Address = partyInfo.Address;
            Model.LegalAddress.City = partyInfo.City;
            Model.LegalAddress.Country = partyInfo.Country;
            Model.LegalAddress.PostalCode = partyInfo.PostalCode;
            Model.LegalAddress.Province = partyInfo.Province;
        }

        protected override Task OnInitializedAsync()
        {
            return Task.Run(() =>
            {
                var person = Repository.GetById<CommandStack.Model.Person>(PersonId);
                Model.FirstName = person.FirstName;
                Model.LastName = person.LastName;
                Model.NationalIdentificationNumber = person.NationalIdentificationNumber;
                Model.VatNumber = person.VatNumber;
            });


        }

        private async Task Submit()
        {

        }

        public class ViewModel
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            public string NationalIdentificationNumber { get; set; }
            public string VatNumber { get; set; }

            public PostalAddress LegalAddress { get; set; } = new Model.PostalAddress();
            public bool UseLegalAddressAsBillingAddress { get; set; } = true;
            public PostalAddress BillingAddress { get; set; } = new Model.PostalAddress();
            public bool UseLegalAddressAsShippingAddress { get; set; } = true;
            public PostalAddress ShippingAddress { get; set; } = new Model.PostalAddress();

            [Phone]
            public string PhoneNumber { get; set; }
            [Phone]
            public string MobilePhoneNumber { get; set; }
            [Phone]
            public string FaxNumber { get; set; }
            public string WebSiteUrl { get; set; }
            [EmailAddress]
            public string EmailAddress { get; set; }
            public string InstantMessaging { get; set; }
        }
    }
}
