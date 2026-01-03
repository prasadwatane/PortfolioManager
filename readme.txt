The application follows Clean Architecture principles with the following layers:

PrasadWatanePortfolioManager/
├── Domain/           # Business entities and logic
├── Application/      # Application services and commands
├── Infra/           # Infrastructure (file system, data access)
└── Presentation/    # Console interface

- **Domain Layer**: `Fund`, `Portfolio`, `OverlapResult` entities
- **Application Layer**: `PortfolioManager`, `ApplicationService`, Command pattern
- **Infrastructure Layer**: `FundRepository`, `FileReader`
- **Commands**: `CurrentPortfolioCommand`, `CalculateOverlapCommand`, `AddStockCommand`

## 🛠️ Technologies Used

- .NET 9.0
- C#
- xUnit
- Moq
- JSON
- Clean Architecture

 How to Run

open powershell in PrasadWatanePortfolioManager\ (project folder) and execute
	.\run_prasad_watane.bat
