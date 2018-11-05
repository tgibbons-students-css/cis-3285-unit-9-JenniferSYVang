using System.Collections.Generic;
using System.Linq;

using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple.AdoNet
{
    public class AdoNetTradeStorage : ITradeStorage
    {
        private readonly ILogger logger;

        public AdoNetTradeStorage(ILogger logger)
        {
            this.logger = logger;
        }

        public void Persist(IEnumerable<TradeRecord> trades)
        {
            logger.LogInfo("Connecting to Database");
            // solves Request 409 - "As a trader, I need all trades to be shared among traders and stored on a centralized database."
            string connectSqlServer = "Data Source = athena.css.edu; Initial Catalog = CIS3285; Persist Security Info = True; User ID = tgibbons; Password = Data Source = athena.css.edu; Initial Catalog = CIS3285; Persist Security Info = True; User ID = tgibbons; Password = Saints4CSS";
            using (var connection = new System.Data.SqlClient.SqlConnection(connectSqlServer))
            //using (var connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\tradedatabase.mdf;Integrated Security=True;Connect Timeout=30;"))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var trade in trades)
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "dbo.insert_trade";
                        command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                        command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                        command.Parameters.AddWithValue("@lots", trade.Lots);
                        command.Parameters.AddWithValue("@price", trade.Price);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                connection.Close();
            }

            logger.LogInfo("{0} trades processed", trades.Count());
        }
    }
}
