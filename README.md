# LicensePurchaseCalculator

## Overview

This application calculates the minimum number of software licenses a company must purchase based on user device installation data. It is designed to be extensible, testable, and scalable to handle large datasets efficiently.
The utility processes installation records and determines the required license count for a specified Application ID (defaults to 374).

### Each license allows:

1. Installation on one desktop, OR
2. Installation on two computers if at least one is a laptop
   The solution is designed as a production-ready, streaming, memory-efficient utility, capable of handling large CSV files (e.g., 1GB+).

### Design Principles

1. Single Responsibility Principle
2. Separation of Concerns
3. Streaming (Deferred Execution via yield)
4. Unit-testable components
5. Structured logging
6. Defensive entry-point error handling
7. O(n) time complexity

### Notes

1. Processes CSV using streaming (yield) for memory efficiency.
2. Removes duplicates using composite key: (ApplicationID, UserID, ComputerID)
3. License logic implemented and tested separately.
4. Follows separation of concerns and production-quality structure.

## Running the Application

### From Terminal

Prerequisites: .NET 8 SDK or Runtime must be installed

Ensure you run the command from the directory containing the .csproj file.

A sample CSV file is included in the data folder.
If using a large file, place it inside the data folder and update the command accordingly.
If no Application ID is provided, the application defaults to 374.
A sample configuration: dotnet run -- ..\\data\\sample-small.csv 374

### From Visual Studio

1. Open launchSettings.json
2. Update the commandLineArgs value as needed.

A sample CSV file is included in the data folder.
If using a large file, place it inside the data folder and update the path accordingly.
If no Application ID is provided, the application defaults to 374.
A sample configuration: "commandLineArgs": "..\\data\\sample-small.csv 374"

### Logging Output Example

* License calculation started.
* Reading: ..\\data\\sample-small.csv | ApplicationID: 374
* License calculation completed successfully.
* Minimum licenses required: 190

### Running Tests

### From Terminal

From the solution root directory, run:
dotnet test

This will automatically discover and execute all test projects.

### From Visual Studio

1. Open the solution in Visual Studio
2. Go to Test → Test Explorer
3. Click Run All
