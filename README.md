## PII POC Serilog
This is a smiple POC done to try out implementing Serilog custom enrichers using Serilogs ILogEventEnricher and also nuget package https://github.com/serilog-contrib/Serilog.Enrichers.Sensitive

### Prerequisites
- .NET 8.0

### How to run

Run the follwoing command in the root directory of the project
```
dotnet run
```

Sample Output:
```
Logging Customer Object directly
[14:33:37 INF] Customer Object {"Id": 1, "Name": "John Doe", "Email": "t***p@example.com", "SerialNumber": "1**********2"}
Logging Customer Info as a formatted string
[14:33:37 INF] Customer Info: ID: 1, Name: John Doe, Email: t***p@example.com, Serial Number: 1**********2

```

