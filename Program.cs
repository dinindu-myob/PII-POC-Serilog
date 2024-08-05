
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
    //
    //Using Serilog.Enrichers.Sensitive
    //.Enrich.WithSensitiveDataMasking()
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


Log.Information(@"Customer Object {@Customer}", customer);

Log.Information($"Customer Info: ID: {customer.Id}, Name: {customer.Name}, " +
                $"Email: {customer.Email}, Serial Number: {customer.SerialNumber}");

