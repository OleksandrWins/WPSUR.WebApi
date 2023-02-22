using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Widgets;

namespace WPSUR.Services.Services
{
    public sealed class OccupantNumbersService : IOccupantNumbersService
    {
        private readonly IConfiguration _configuration;

        public OccupantNumbersService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<KilledRussiansModel> GetFreshNumbers()
        {
            string result;

            int retryCount = 5;
            int retryInterval = 1000;

            var identifiers = _configuration.GetSection("Widgets:RussianIdentifiers").Get<string[]>();

            int iternation = 0;
            do
            {
                HtmlWeb web = new();
                var htmlDoc = web.Load("https://www.pravda.com.ua/");
                var node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div/div[1]/div[2]");
                result = node?.InnerText;

                if (string.IsNullOrWhiteSpace(result))
                {
                    await Task.Delay(retryInterval);
                    iternation++;
                }
            } while (retryCount >= iternation && string.IsNullOrWhiteSpace(result));

            if (result == null)
            {
                throw new NullReferenceException();
            }

            string[] splittedResult = result.Split("&#43;");
            KilledRussiansModel model = new()
            {
                Identifier = GetFreshIdentifier(identifiers),
                TotalKilled = splittedResult[0],
                DailyKilled = splittedResult[1],
            };

            return model;
        }

        private string GetFreshIdentifier(string[] identifiers)
        {
            var result = identifiers[new Random().Next(0, identifiers.Length)];
            return result;
        }
    }
}
