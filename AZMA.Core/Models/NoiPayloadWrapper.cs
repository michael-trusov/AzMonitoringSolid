using AZMA.Core.Exceptions;
using System.Collections.Generic;

namespace AZMA.Core.Models
{
    public class NoiPayloadWrapper
    {
        private readonly NoiPayload _noiPayload;

        public NoiPayloadWrapper(NoiPayload noiPayload)
        {
            _noiPayload = noiPayload;
        }

        public NoiPayloadWrapper AppId(string appId)
        {
            _noiPayload.AppId = appId;

            return this;
        }

        public NoiPayloadWrapper AlertGroup(string alertGroup)
        {
            _noiPayload.AlertGroup = alertGroup;

            return this;
        }

        public NoiPayloadWrapper Status(string status)
        {
            _noiPayload.Status = status;

            return this;
        }

        public NoiPayloadWrapper OriginalAlertPayload(string originalAlertPayload)
        {
            _noiPayload.OriginalAlertPayload = originalAlertPayload;

            return this;
        }

        public NoiPayloadWrapper CmdbClasses(Dictionary<string, string> cmdbClasses)
        {
            if (string.IsNullOrWhiteSpace(_noiPayload.Node))
            {
                throw new InvalidNoiPayloadException("'Node' attribute should be specified for NoiPayload.");
            }

            string cmdbClass;
            if (!cmdbClasses.TryGetValue(_noiPayload.Node, out cmdbClass))
            {
                throw new NotFoundCmdbClassException($"CmdbClass '{_noiPayload.Node}' was not found.");
            }

            _noiPayload.ServiceNowCmdbClass = cmdbClass;

            return this;
        }

        public NoiPayloadWrapper Validate()
        {
            if (_noiPayload == null ||
                string.IsNullOrWhiteSpace(_noiPayload.AppId) ||
                string.IsNullOrWhiteSpace(_noiPayload.AlertId) ||
                string.IsNullOrWhiteSpace(_noiPayload.AlertName) ||
                string.IsNullOrWhiteSpace(_noiPayload.AlertGroup) ||
                string.IsNullOrWhiteSpace(_noiPayload.Severity) ||
                string.IsNullOrWhiteSpace(_noiPayload.Node) ||
                string.IsNullOrWhiteSpace(_noiPayload.Status) ||
                string.IsNullOrWhiteSpace(_noiPayload.Summary) ||
                string.IsNullOrWhiteSpace(_noiPayload.OriginalAlertPayload) ||
                string.IsNullOrWhiteSpace(_noiPayload.ServiceNowCi) ||
                string.IsNullOrWhiteSpace(_noiPayload.ServiceNowCmdbClass))
            {
                throw new InvalidNoiPayloadException("NoiPayload model is not valid.");
            }

            return this;
        }

        public static implicit operator NoiPayload(NoiPayloadWrapper wrapper) => wrapper._noiPayload;
    }
}
