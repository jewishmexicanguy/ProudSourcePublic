using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPal.Api;

namespace ZPaypalAPI
{
    public class ZSale
    {

        private Dictionary<string, string> config;
        private APIContext apiContext;
        private string accessToken;

        public string id;
        public Sale sale;

        public ZSale(string id)
        {
            config = ConfigManager.Instance.GetProperties();
            accessToken = new OAuthTokenCredential(config).GetAccessToken();
            apiContext = new APIContext(accessToken);

            this.id = id;
        }

        public bool loadSale()
        {
            try
            {
                this.sale = Sale.Get(apiContext, id); // PAY-34629814WL663112AKEE3AWQ
            }
            catch (PayPal.HttpException e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }
}
