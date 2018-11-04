
using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class SimpleTradeValidator : ITradeValidator
    {
        private readonly ILogger logger;

        public SimpleTradeValidator(ILogger logger)
        {
            this.logger = logger;
        }

        public bool Validate(string[] tradeData)
        {
            if (tradeData.Length != 3)
            {
                logger.LogWarning("Line malformed. Only {0} field(s) found.", tradeData.Length);
                return false;
            }

            if (tradeData[0].Length != 6)
            {
                logger.LogWarning("Trade currencies malformed: '{0}'", tradeData[0]);
                return false;
            }

            int tradeAmount;
            if (!int.TryParse(tradeData[1], out tradeAmount))
            {
                logger.LogWarning("Trade not a valid integer: '{0}'", tradeData[1]);
                return false;
            }
            else if(int.Parse(tradeData[1]) > 100000) // Solves Request 403 - "As a Trader, I want to prevent dangerous trades so that one typo doesn't ruin the company." 
            {
                logger.LogWarning("Trade amount cannot exceed 100,000 : '{0}'", tradeData[1]);
                return false;
            }
            else if (int.Parse(tradeData[1]) < 1000) // Solves Request 403 - "As a Trader, I want to prevent dangerous trades so that one typo doesn't ruin the company." 
            {
                logger.LogWarning("Trade amount cannot be less than 10,000 : '{0}'", tradeData[1]);
                return false;
            }

            decimal tradePrice;
            if (!decimal.TryParse(tradeData[2], out tradePrice))
            {
                logger.LogWarning("Trade price not a valid decimal: '{0}'", tradeData[2]);
                return false;
            }

            return true;
        }
    }
}
