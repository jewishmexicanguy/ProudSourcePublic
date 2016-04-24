using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPal.Api;

namespace ZPaypalAPI
{
    public class ZRefund
    {
        private Dictionary<string, string> config;
        private string accessToken;
        private APIContext apiContext;

        string transactionPaypalID;
        int transactionDBID;

        //TODO
        //EDIT this refund function so that instead of taking in a transactionDBID it
        //finds it via a query searching for a transactionPaypalID.

        public ZRefund(int transactionDBID, string transactionPaypalID)
        {
            //config
            config = ConfigManager.Instance.GetProperties();
            accessToken = new OAuthTokenCredential(config).GetAccessToken();
            apiContext = new APIContext(accessToken);

            this.transactionPaypalID = transactionPaypalID;
            this.transactionDBID = transactionDBID;
        }

        public void execute()
        {
            Refund refund = Refund.Get(apiContext, transactionPaypalID);
            ZDatabaseManager.editTransactionState(transactionDBID, "Reversed");
        }
    }
}
