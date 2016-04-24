using System;
using Newtonsoft.Json;

namespace ProudSource
{
	/// <summary>
	/// This class will used with a JsonConvert.DeserializeObject() call that will result in this object 
	/// being filled with the data telling it what kind of server command has been passed from the client.
	/// </summary>
	public class ClientDataInbound
	{
		[JsonProperty(PropertyName = "action")]
		public string action { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProudSource.ClientDataInbound"/> class.
		/// </summary>
		public ClientDataInbound()
		{

		}

        public bool check()
        {
            if (action.Length < 255 && action.Length > 0)
                return true;
            else
                return false;
        }
	}
}

