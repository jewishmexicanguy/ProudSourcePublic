using Newtonsoft.Json;

namespace ProudSource
{
    public class VerifyClientData
	{
		[JsonProperty(PropertyName = "action")]
		public string action { get; set; }

		[JsonProperty(PropertyName = "UserName")]
		public string UserName { get; set; }

		[JsonProperty(PropertyName = "Password")]
		public string Password { get; set; }

		[JsonProperty(PropertyName = "E-mail")]
		public string Email { get; set; }

        [JsonProperty(PropertyName = "InvestorProfile")]
        public string InvestorProfile { get; set; }

        [JsonProperty(PropertyName = "EntrepreneurProfile")]
        public string EntrepreneurProfile { get; set; }

        /// <summary>
        /// This property will get calls from the client that use Investor IDs
        /// </summary>
        [JsonProperty(PropertyName = "InvestorID")]
        public string InvestorID { get; set; }

        /// <summary>
        /// This property will get calls from the client that use Entrepreneur IDs
        /// </summary>
        [JsonProperty(PropertyName = "EntrepreneurID")]
        public string EntrepreneurID { get; set; }

		public bool valid = false;

		/* <TODO>
		 * Define all data values that can be possibly passed through by client side callbacks.
		 * </TODO>
		 */

		public VerifyClientData ()
		{

		}

		public void check()
		{
            if (Email != null)
            {
                if (Email.Length >= 255 | Email.Length <= 0)
                {
                    valid = false;
                    return;
                }
            }
            if (EntrepreneurProfile != null)
            {
                if (EntrepreneurProfile.Length >= 255 | EntrepreneurProfile.Length <= 0)
                {
                    valid = false;
                    return;
                }
            }
            if (InvestorProfile != null)
            {
                if (InvestorProfile.Length >= 255 | InvestorProfile.Length <= 0)
                {
                    valid = false;
                    return;
                }
            }
            if (Password != null)
            {
                if (Password.Length >= 255 | Password.Length <= 0)
                {
                    valid = false;
                    return;
                }
            }
            if (UserName != null)
            {
                if (UserName.Length >= 255 | UserName.Length <= 0)
                {
                    valid = false;
                    return;
                }
            }
            // if we have not exited out by the time we have checked all non null acessors then we know that at the very least these accessors are not going to cause buffer overflows.
            valid = true;
        }
	}
}

