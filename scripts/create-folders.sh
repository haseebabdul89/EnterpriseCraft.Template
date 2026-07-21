#!/usr/bin/env bash
set -euo pipefail

folders=(
  "src/Api"
  "src/Shared"
  "src/Shared/Behaviors"
  "src/Shared/Persistence"
  "src/Shared/Authentication"
  "src/Shared/Authorization"
  "src/Shared/Common"
  "src/Shared/Exceptions"
  "src/Shared/Logging"
  "src/Shared/Contracts"
  "src/Modules"
  "src/Modules/Customers"
  "src/Modules/Orders"
  "src/Modules/Products"
  "src/Modules/Inventory"
  "src/Modules/Identity"
  "src/Tests"
)

for f in "${folders[@]}"; do
  mkdir -p "$f"
done

echo "Created folder structure under $(pwd)/src"