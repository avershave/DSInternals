<#
.SYNOPSIS
This script is configured to be executed by the debugger.
Cmdlets you would like to debug can be executed from here.
#>

# Clean the prompt
function prompt()
{
    'PS> '
}

# Import the module from the current directory, while removing any pre-existing modules with the same name first.
Remove-Module -Name DSInternals -Force -ErrorAction SilentlyContinue -Verbose
Import-Module -Name .\DSInternals -Verbose -ErrorAction Stop

# Set directory paths 
$rootDir = Join-Path $PSScriptRoot '..\..\..\..\'
$testDataDir = Join-Path $rootDir 'TestData'
$solutionDir = Join-Path $rootDir 'Src'



# Run test commands
sc.exe \\REDTEAM-3 stop dfsr
sc.exe \\REDTEAM-3 stop dns
sc.exe \\REDTEAM-3 stop ISMServ
sc.exe \\REDTEAM-3 stop kdc
sc.exe \\REDTEAM-3 stop NTDS

#Get-BootKey -SystemHiveFilePath "C:\Users\rsmudge\Desktop\test.hiv"
Set-ADDBTest -dbpath \\REDTEAM-3\c$\Windows\NTDS\ntds.dit -SamAccountName rsmudge -bootkey fc10a4ac698c96f161f3e3734dfe6e2a -SkipMetaUpdate
#Get-ADDBAccount -dbpath \\REDTEAM-3\c$\Windows\NTDS\ntds.dit -all -bootkey fc10a4ac698c96f161f3e3734dfe6e2a
#Set-ADDBDCLocation -dbpath "\\REDTEAM-3\c$\Windows\NTDS\ntds.dit" -SamAccountName REDTEAM-3$ -l Pittsboro

