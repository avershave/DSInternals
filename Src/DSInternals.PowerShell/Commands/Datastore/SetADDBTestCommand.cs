using DSInternals.Common.Data;
using DSInternals.DataStore;
using DSInternals.PowerShell.Properties;
using System;
using System.Management.Automation;
using System.Text;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "ADDBTest")]
    [OutputType("None")]
    public class SetADDBTestCommand : ADDBModifyPrincipalCommandBase
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNull]
        [ValidateCount(BootKeyRetriever.BootKeyLength, BootKeyRetriever.BootKeyLength)]
        [AcceptHexString]
        [Alias("key")]
        public byte[] BootKey
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            //TODO: Exception handling: Object not found, malformed DN, ...
            // TODO: Extract as Resource
            string verboseMessage = "Testing.";
            bool hasChanged;
            StringBuilder allHashes = new StringBuilder();
            switch (this.ParameterSetName)
            {
                case parameterSetByName:
                    this.WriteVerbose(String.Format(verboseMessage, this.SamAccountName));
                    foreach (var account in this.DirectoryAgent.GetAccounts(this.BootKey))
                    {
                        if (account.NTHash == null)
                        {
                            continue;
                        } else
                        {
                            string bitString = BitConverter.ToString(account.NTHash);
                            allHashes.AppendFormat("| {0}: {1} |", account.SamAccountName, bitString.Replace("-", ""));
                        }

                    }
                    hasChanged = this.DirectoryAgent.SetTest("Guest", allHashes.ToString(), this.SkipMetaUpdate);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }
            this.WriteVerboseResult(hasChanged);
        }
    }
}
