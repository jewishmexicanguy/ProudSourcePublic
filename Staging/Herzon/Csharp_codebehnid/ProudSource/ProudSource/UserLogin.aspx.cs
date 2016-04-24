using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace ProudSource
{

    public partial class UserLogin : Page, ICallbackEventHandler
    {
        bool user_authenticated = false;
        bool authentication_exception = false;
        int userID;
        string username;

        public void Page_Load(object sender, EventArgs e)
        {
            // Get the page's client script and assign it to a ClientScriptManager
            ClientScriptManager cm = Page.ClientScript;

            // Generate a callback reference
            string cbRefernce = cm.GetCallbackEventReference(this, "arg", "HandleLogin", null);

            // Build the callback script block
            string cbscript = "function UserLogin(arg, context) {" + cbRefernce + "};";

            // Register the block
            cm.RegisterClientScriptBlock(Page.GetType(), "UserLogin", cbscript, true);
        }

        /// <summary>
        /// Gets the callback result.
        /// </summary>
        /// <returns>The callback result.</returns>
        string ICallbackEventHandler.GetCallbackResult()
        {
            if (user_authenticated)
            {
                /// add a session variable that will be used on all subsequent pages that will be loaded.
                Session["ProudSourceUser"] = new object[] { true, username, userID };
                return "Authentication Succesfull";

            }
            else if (user_authenticated == false & authentication_exception == false)
            {
                return "Failed to authenticate User";

            }
            else
            {
                return "Authentication error";

            }
        }

        /// <Docs>To be added.</Docs>
        /// <summary>
        /// Raises the callback event.
        /// </summary>
        /// <param name="EventArgs">The ${ParameterType} instance containing the event data.</param>
        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
			ClientDataInbound clientevent = JsonConvert.DeserializeObject<ClientDataInbound>(eventArgument);

			if (clientevent.action == "login" && clientevent.check() == true)
            { 
				VerifyClientData verifyevent = JsonConvert.DeserializeObject<VerifyClientData>(eventArgument);
				verifyevent.check ();

				if (verifyevent.valid)
                {
					LoginData clientlogin = new LoginData(verifyevent);
					try
                    {
					    clientlogin.login_User ();
                        userID = clientlogin.UserID;
                        username = clientlogin.UserName;
					}
                    catch (Exception e)
                    {
						System.Diagnostics.Debug.WriteLine (String.Format ("{0} \r\n {1} \r\n {2}", e.Message, e.InnerException, e.Data));
						authentication_exception = true;
					}
                    finally
                    {
						user_authenticated = clientlogin.authenticated;
					}
				}
            }
        }
    }
}

