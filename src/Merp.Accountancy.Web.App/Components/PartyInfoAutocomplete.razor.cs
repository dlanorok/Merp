﻿using Merp.Registry.Web.Api.Internal;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq.Expressions;

namespace Merp.Accountancy.Web.App.Components
{
    public partial class PartyInfoAutocomplete
    {
        [Inject] public IPartyApiServices PartyApi { get; set; } = default!;

        [Inject] public IDialogService Dialog { get; set; } = default!;

        [Parameter] public string Label { get; set; } = string.Empty;

        [Parameter] public int DebounceInterval { get; set; } = 300;

        [Parameter] public Func<ViewModel?, string> DisplayValueTemplate { get; set; } = x => x?.Name ?? string.Empty;

        [Parameter] public ViewModel? Value { get; set; }

        [Parameter] public EventCallback<ViewModel> ValueChanged { get; set; }

        [Parameter] public Expression<Func<ViewModel?>> ValueExpression { get; set; }

        private async Task<IEnumerable<ViewModel>> SearchPartyByTextAsync(string text)
        {
            var parties = await PartyApi.GetPartyInfoByPatternAsync(text);
            return parties.Select(p => new ViewModel { Id = p.Id, OriginalId = p.OriginalId, Name = p.Name });
        }

        private async Task RegisterNewPartyAsync()
        {

        }

        public class ViewModel
        {
            public int Id { get; set; }

            public Guid OriginalId { get; set; }

            public string Name { get; set; } = string.Empty;
        }
    }
}