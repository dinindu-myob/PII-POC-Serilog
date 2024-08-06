
using Serilog;
using Serilog.Enrichers.Sensitive;
using test_serilog.CustomEnrichers;
using test_serilog.Models;

Customer customer = new Customer
{
    Id = 1,
    Name = "John Doe",
    Email = "testcomp@example.com",
    SerialNumber = "123456789012"
};


Log.Logger = new LoggerConfiguration()
    //Using Custom enricher
    //.Enrich.With(new SensitiveDataEnricher())
    
    //Using Serilog.Enrichers.Sensitive Default masking
    //.Enrich.WithSensitiveDataMasking()

    //Using Serilog.Enrichers.Sensitive Custom masking
    .Enrich.WithSensitiveDataMasking(
        options =>
        {
            options.MaskValue = "*";
            options.MaskingOperators = new List<IMaskingOperator>
            {
                new CustomizedEmailAddressMaskingOperator(),
                new SerialNumberMaskingOperator()
            };
        }
        )
    .WriteTo.Console()
    .CreateLogger();

Console.WriteLine("Logging Customer Object directly");
Log.Information(@"Customer Object {@Customer}", customer);

Console.WriteLine("Logging Customer Info as a formatted string");
Log.Information($"Customer Info: ID: {customer.Id}, Name: {customer.Name}, " +
                $"Email: {customer.Email}, Serial Number: {customer.SerialNumber}");

