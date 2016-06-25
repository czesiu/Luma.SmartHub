$Location = Get-Location
$Configuration = "Release"
$SolutionFile = "src\Luma.SmartHub.sln"
$NugetSources = "https://www.nuget.org/api/v2;https://ci.appveyor.com/nuget/luma-smarthub;https://ci.appveyor.com/nuget/luma-smarthub-plugins-youtube"

# Restore NuGet packages
NuGet Restore $SolutionFile -Source $NugetSources

# Run the build
MSBuild $SolutionFile /property:Configuration=$Configuration

# Search for all nuspec files
Get-ChildItem -r *.nuspec | ForEach-Object {

    # Finding csproj files in directory with nuspec file
    Get-ChildItem $_.Directory -r *.csproj| ForEach-Object {

        # Packaging project to nupkg
        Write-Host Packaging $_.FullName
        NuGet Pack $_.FullName -Prop Configuration=$Configuration
    }
}

Set-Location $Location

# tests
# dnvm use 1.0.0-rc1-update2 -a x64 -r clr
# dnx -p tests\Luma.SmartHub.Tests test -xml xunit-results.xml

# upload results to AppVeyor
# $wc = New-Object 'System.Net.WebClient'
# $wc.UploadFile("https://ci.appveyor.com/api/testresults/xunit/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\xunit-results.xml))