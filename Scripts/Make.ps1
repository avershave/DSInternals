<#
.SYNOPSIS
Builds the solution from scratch and ZIPs the resulting module.
#>

.\Build-Solution
.\Run-Tests
.\Pack-PSModule
.\Pack-NuGetPackages