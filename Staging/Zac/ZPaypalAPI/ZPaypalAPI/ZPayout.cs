using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPal.Api;

namespace ZPaypalAPI
{
    public class ZPayout
    {
        private Dictionary<string, string> config;
        private string accessToken;
        private APIContext apiContext;

        public string receiver, currency;
        public int accountID;
        public double amount;

        public ZPayout(int accountID, string receiver, double amount, string currency)
        {
            config = ConfigManager.Instance.GetProperties();
            accessToken = new OAuthTokenCredential(config).GetAccessToken();
            apiContext = new APIContext(accessToken);

            this.accountID = accountID;
            this.receiver = receiver;
            this.amount = amount;
            this.currency = currency;

        }

        public bool execute()
        {

            // ### Initialize `Payout` Object tests
            // Initialize a new `Payout` object with details of the batch payout to be created.
            var payout = new Payout
            {
                // #### items
                // The `items` array contains the list of payout items to be included in this payout.
                // If `syncMode` is set to `true` when calling `Payout.Create()`, then the `items` array must only
                // contain **one** item.  If `syncMode` is set to `false` when calling `Payout.Create()`, then the `items`
                // array can contain more than one item.
                items = new List<PayoutItem>
                {
                    new PayoutItem
                    {
                        recipient_type = PayoutRecipientType.EMAIL,
                        amount = new Currency
                        {
                            value = amount.ToString(),
                            currency = currency
                        },
                        receiver = receiver,
                        note = "Thank you.",
                        sender_item_id = "item_1"
                    },
                }
            };

            // ### Payout.Create()
            // Creates the batch payout resource.
            // `syncMode = false` indicates that this call will be performed **asynchronously**,
            // and will return a `payout_batch_id` that can be used to check the status of the payouts in the batch.
            // `syncMode = true` indicates that this call will be performed **synchronously** and will return once the payout has been processed.
            // > **NOTE**: The `items` array can only have **one** item if `syncMode` is set to `true`.
            var createdPayout = payout.Create(apiContext, true);

            ZDatabaseManager.sendTransactionQuery(this.accountID, -amount, 2, 6, 2);

            return true;
        }

    }
}
