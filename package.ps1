$archiveName = ".\webpackage\phoneorder.zip"

if (Test-Path ".\Health\bin\Release") {
    Remove-Item ".\Health\bin\Release\*"  -ErrorAction Ignore -Force
}

msbuild /t:Rebuild /p:Configuration=Release

New-Item -Name "webpackage" -ItemType "directory" -Force
Remove-Item $archiveName -ErrorAction Ignore

Write-Output "Renaming files"

Get-ChildItem .\Health\bin\Release\Health.* | Rename-Item -NewName { $_.Name -replace 'Health\.','hello-world-dotnet.' } -Force

Write-Output "Creating Archive: $archiveName"

$Command=" & 'C:\Program Files\7-Zip\7z.exe' a $archiveName .\phoneorder\WebSite\index.html .\phoneorder\WebSite .\phoneorder\bin .\Health\bin\Release"
Write-Output "Running: $Command"
$Command | Invoke-Expression

Move-Item $archiveName -Destination .\hello-world-dotnet.zip -Force

if(-Not(Test-Path ".\hello-world-dotnet.zip")){
    Write-Output "WARNING: zip file was not created"
}
