using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Braintree;

namespace ProudSourceAccountingLibrary.BrainTree
{

    public class BrainTreeBase : AccountingEdits
    {
        /// <summary>
        /// merchant id for brain tree.
        /// </summary>
        private const string merchant_ID = "h9vwr53gf26m9c8v";

        /// <summary>
        /// public key used for signing requests with braintree.
        /// </summary>
        private const string public_key = "kd9jp9qvdny8xkcv";

        /// <summary>
        /// privae key used for signing requests with braintree
        /// </summary>
        private const string private_key = "4cc5cc1ed3ffc1f92715ae026ca72f96";

        /// <summary>
        /// Class resident that is a gatewat object whcih opens connections to BrainTrees financail processing.
        /// </summary>
        protected BraintreeGateway gateway;
        /// <summary>
        /// Constructor
        /// </summary>
        public BrainTreeBase()
        {
            gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = merchant_ID,
                PublicKey = public_key,
                PrivateKey = private_key
            };
        }

        /// <summary>
        /// Method that returns a client token as a string to be used on the client side for payment transactions using braintree.
        /// </summary>
        /// <returns></returns>
        public string get_ClientToken()
        {
            return gateway.ClientToken.generate();
        }
    }

    /// <summary>
    /// This will get called inside of a web app controler method that passes back an Nonce BrainTree object.
    /// </summary>
    public class BTInboundPayment : BrainTreeBase
    {
        /// <summary>
        /// private resident that will house a braintree request
        /// </summary>
        private TransactionRequest request;
        /// <summary>
        /// Accessor that harbours whether or not the transaction was successfully submited or not.
        /// </summary>
        public bool Transaction_Success { get; private set; }

        /// <summary>
        /// Class constructor used for accepting incoming payments using braintree.
        /// </summary>
        /// <param name="nonce">The Nonce instance passed back from the client.</param>
        /// <param name="amount">The amount of money to be transacted.</param>
        public BTInboundPayment(string nonce, decimal amount, int financial_account_id) : base()
        {
            request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);
            Transaction_Success = result.IsSuccess();
            if(Transaction_Success)
            {
                Insert_USDTransaction(financial_account_id, amount, true);
            }
        }
    }

    public class BTOutboundPayment : BrainTreeBase
    {
        private TransactionRequest request;

        public bool Transaction_Success { get; private set; }

        public BTOutboundPayment(string nonce, decimal amount, int financial_account_id) : base()
        {
            request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Credit(request);
            Transaction_Success = result.IsSuccess();
            if(Transaction_Success)
            {
                Insert_USDTransaction(financial_account_id, amount, false);
            }
        }
    }
}