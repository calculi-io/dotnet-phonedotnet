Set-PSDebug -Trace 2

Get-ChildItem Env:

Write-Output "Add-Type -assembly 'system.io.compression.filesystem'"
Add-Type -assembly "system.io.compression.filesystem"

$wwwroot = "C:\inetpub\wwwroot\"
$wwwrootRemoveFiles = $wwwroot + "*"
$archiveLocation = "$env:PKG_BASE_DIR\$env:APPLICATION_NAME\hello-world-dotnet.zip"

Get-Date

Write-Output "Removing root files - Remove-Item $wwwrootRemoveFiles -Recurse"
Remove-Item $wwwrootRemoveFiles -Recurse

Get-Date

Write-Output "Extracting Archive: $archiveLocation to $wwwroot"
[io.compression.zipfile]::ExtractToDirectory($archiveLocation, $wwwroot)

Get-Date

Write-Output "Starting IIS - Set-Service W3SVC -StartupType Automatic"
Set-Service W3SVC -StartupType Automatic
iisreset /start

Get-Date

Write-Output "Starting site on port 80 C:\windows\system32\inetsrv\appcmd start site 'Default Web Site'"
C:\windows\system32\inetsrv\appcmd start site "Default Web Site"

Get-Date

Write-Output "Starting health monitor"
$Command = $wwwroot + "Release\hello-world-dotnet.exe -Uri http://localhost -Seconds 30"
Write-Output "Launching: $Command"
Invoke-Expression $Command

Get-Date

Write-Output "Looking for process..."

$procName = 'hello-world-dotnet'
$sec = 10
$endTime = [System.DateTime]::Now.AddSeconds($sec)
while ($endTime -gt [System.DateTime]::Now) {
	$proc = @(Get-Process | Select-Object -Property ProcessName, Id, WS | Where-Object {$_.ProcessName -eq $procName})
	if ($proc.length -gt 0) {
		Write-Output "SUCCESS: $procName started at $(Get-Date)"
		exit
	}
	Start-Sleep -Milliseconds 200
}
Write-Output "ERROR: $procName did not start after $sec seconds."

$tl = @(tasklist)
Write-Output "Task List"
foreach ($p in $tl) { Write-Output "$p" }
