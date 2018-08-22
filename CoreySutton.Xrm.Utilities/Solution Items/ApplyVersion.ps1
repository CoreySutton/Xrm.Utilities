param([string]$version)

Write-Host "*************"; 
Write-Host "Apply Version";
Write-Host "*************"; 

Write-Host "Set variables"
$env:BUILD_SOURCESDIRECTORY="../CoreySutton.Xrm.Utilities";
$env:BUILD_BUILDNUMBER="Build CoreySutton.Xrm.Utilities_$version"; 

Write-Host "Apply version"
.\ApplyVersionToAssemblies.ps1 

Write-Host "Clean up"
Remove-Item env:\BUILD_SOURCESDIRECTORY; 
Remove-Item env:\BUILD_BUILDNUMBER;

Write-Host "Success"