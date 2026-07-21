$folders = @(
  "src\Api",
  "src\Shared",
  "src\Shared\Behaviors",
  "src\Shared\Persistence",
  "src\Shared\Authentication",
  "src\Shared\Authorization",
  "src\Shared\Common",
  "src\Shared\Exceptions",
  "src\Shared\Logging",
  "src\Shared\Contracts",
  "src\Modules",
  "src\Modules\Customers",
  "src\Modules\Orders",
  "src\Modules\Products",
  "src\Modules\Inventory",
  "src\Modules\Identity",
  "src\Tests"
)

foreach ($f in $folders) {
  if (-not (Test-Path -Path $f)) {
    New-Item -ItemType Directory -Path $f | Out-Null
  }
}
Write-Output "Created folder structure under $(Get-Location)\src"   
