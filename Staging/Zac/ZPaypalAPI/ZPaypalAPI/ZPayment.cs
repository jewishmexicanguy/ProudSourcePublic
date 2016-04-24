using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using PayPal.Api; //PM> Install-Package PayPal

namespace ZPaypalAPI
{
    public class ZPayment
    {
        private Payment payment;
        private Dictionary<string, string> config;
        private APIContext apiContext;
        private string accessToken;

        public string itemName, itemDescription, currency, cardNumber, cardType, cvv, firstName, lastName, address, city, countryCode, postalCode, state;
        public double subTotal;
        public string paymentID;
        public string saleID;

        public int accountID, expMonth, expYear;

        public string approvalUrl;

        public ZPayment(int accountID, string itemName, string itemDescription, string currency, double subTotal, string cardNumber, string cardType, int expMonth, int expYear, string cvv, string firstName, string lastName, string address, string city, string countryCode, string postalCode, string state)
        {
            setupConfig();

            this.accountID = accountID;
            this.itemName = itemName;
            this.itemDescription = itemDescription;
            this.currency = currency;
            this.subTotal = subTotal;
            this.cardNumber = cardNumber;
            this.cardType = cardType;
            this.expMonth = expMonth;
            this.expYear = expYear;
            this.cvv = cvv;
            this.firstName = firstName;
            this.address = address;
            this.city = city;
            this.countryCode = countryCode;
            this.postalCode = postalCode;
            this.state = state;
        }

        public ZPayment()
        {
            setupConfig();
        }

        public void setupConfig()
        {
            // Authenticate with PayPal and get token
            config = ConfigManager.Instance.GetProperties();
            accessToken = new OAuthTokenCredential(config).GetAccessToken();
            apiContext = new APIContext(accessToken);
        }

        public bool executePayment()
        {
            //don't create the payment again
            if (this.payment != null)
                return false;

            //TODO - Remove randomness
            Random rnd = new Random();

            Address billingAddress = new Address();
            billingAddress.line1 = address;
            billingAddress.city = city;
            billingAddress.country_code = countryCode;
            billingAddress.postal_code = postalCode;
            billingAddress.state = state;

            CreditCard creditCard = new CreditCard();
            creditCard.number = cardNumber;
            creditCard.type = cardType;
            creditCard.expire_month = expMonth;
            creditCard.expire_year = expYear;
            creditCard.cvv2 = cvv;
            creditCard.first_name = firstName;
            creditCard.last_name = lastName;
            creditCard.billing_address = billingAddress;

            Details amountDetails = new Details();
            amountDetails.subtotal = subTotal.ToString();
            amountDetails.tax = "0.00";
            amountDetails.shipping = "0.00";

            Amount amount = new Amount();
            amount.total = subTotal.ToString();
            amount.currency = currency;
            amount.details = amountDetails;

            Transaction transaction = new Transaction();
            transaction.amount = amount;
            transaction.description = itemDescription;

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(transaction);

            FundingInstrument fundingInstrument = new FundingInstrument();
            fundingInstrument.credit_card = creditCard;

            List<FundingInstrument> fundingInstruments = new List<FundingInstrument>();
            fundingInstruments.Add(fundingInstrument);

            Payer payer = new Payer();
            payer.funding_instruments = fundingInstruments;
            payer.payment_method = "credit_card";

            payment = new Payment();
            payment.intent = "sale";
            payment.payer = payer;
            payment.transactions = transactions;


            try
            {
                //attempt to create the payment
                payment = payment.Create(apiContext);
                //if payment was approved
                if (payment.state.ToLower() == "approved")
                {
                    //Send the query to add transaction
                    ZDatabaseManager.sendTransactionQuery(accountID, subTotal, 2, 3, 1);
                } else
                {
                    return false;
                }              
            } catch (PayPal.PaymentsException e)
            {
                Console.WriteLine(e);
                return false;
            }

            saleID = payment.transactions[0].related_resources[0].sale.id;
            paymentID = payment.id;
            return true;

        }
    }
}
