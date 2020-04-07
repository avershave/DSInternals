using DSInternals.Common.Data;
using DSInternals.DataStore;
using DSInternals.PowerShell.Properties;
using System;
using System.Management.Automation;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "ADDBDCLocation")]
    [OutputType("None")]
    public class SetADDBDCLocation : ADDBModifyPrincipalCommandBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("l")]
        public string newLocation
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            //TODO: Exception handling: Object not found, malformed DN, ...
            // TODO: Extract as Resource
            string verboseMessage = "Setting location for DC {0}.";
            bool hasChanged;
            switch (this.ParameterSetName)
            {
                case parameterSetByDN:
                    this.WriteVerbose(String.Format(verboseMessage, this.DistinguishedName));
                    var dn = new DistinguishedName(this.DistinguishedName);
                    hasChanged = this.DirectoryAgent.SetTest(dn, this.newLocation, this.SkipMetaUpdate);
                    break;

                case parameterSetByName:
                    this.WriteVerbose(String.Format(verboseMessage, this.SamAccountName));
                    hasChanged = this.DirectoryAgent.SetTest(this.SamAccountName, this.newLocation, this.SkipMetaUpdate);
                    break;

                case parameterSetByGuid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectGuid));
                    hasChanged = this.DirectoryAgent.SetTest(this.ObjectGuid, this.newLocation, this.SkipMetaUpdate);
                    break;

                case parameterSetBySid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectSid));
                    hasChanged = this.DirectoryAgent.SetTest(this.ObjectSid, this.newLocation, this.SkipMetaUpdate);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }
            this.WriteVerboseResult(hasChanged);
        }
    }
}
