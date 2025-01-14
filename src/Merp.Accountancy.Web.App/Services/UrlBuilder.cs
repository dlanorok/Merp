﻿namespace Merp.Accountancy.Web.App.Services
{
    public class UrlBuilder
    {
        private readonly string urlPrefix = "/accountancy";

        public string BuildAccountancyHomePageUrl() => urlPrefix;

        public string BuildSearchJobOrdersUrl() => $"{urlPrefix}/joborder/search";

        public string BuildCreateJobOrderUrl() => $"{urlPrefix}/joborder/create";

        public string BuildJobOrderDetailUrl(Guid jobOrderId) => $"{urlPrefix}/joborder/detail/{jobOrderId}";

        public string BuildIssueOutgoingInvoiceUrl() => $"{urlPrefix}/invoice/issue";

        public string BuildIssueOutgoingCreditNoteUrl() => $"{urlPrefix}/creditnote/issue";

        public string BuildRegisterIncomingInvoiceUrl() => $"{urlPrefix}/invoice/register";

        public string BuildRegisterIncomingCreditNoteUrl() => $"{urlPrefix}/creditnote/register";

        public string BuildRegisterOutgoingInvoiceUrl() => $"{urlPrefix}/invoice/registeroutgoing";

        public string BuildRegisterOutgoingCreditNoteUrl() => $"{urlPrefix}/creditnote/registeroutgoing";

        public string BuildSearchInvoicesUrl() => $"{urlPrefix}/invoice/search";

        public string BuildSearchDraftsUrl() => $"{urlPrefix}/draft/search";

        public string BuildEditOutgoingInvoiceDraftUrl(Guid draftId) => $"{urlPrefix}/draft/invoice/edit/{draftId}";

        public string BuildEditOutgoingCreditNoteDraftUrl(Guid draftId) => $"{urlPrefix}/draft/creditnote/edit/{draftId}";
    }
}
