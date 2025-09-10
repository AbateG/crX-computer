# Kill all dotnet and API processes to resolve DLL lock issues
# Run this script as Administrator if needed
Get-Process -Name dotnet -ErrorAction SilentlyContinue | ForEach-Object { Write-Host "Killing PID $($_.Id) ($($_.ProcessName))"; Stop-Process -Id $_.Id -Force }

# Optionally, kill any process locking DLLs in the build output
# You can use Sysinternals Handle.exe for advanced DLL unlocks
Write-Host "All dotnet processes killed. If you still see lock issues, restart your machine."
