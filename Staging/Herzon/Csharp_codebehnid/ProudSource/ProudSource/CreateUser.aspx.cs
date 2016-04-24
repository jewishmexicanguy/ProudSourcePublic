using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace ProudSource
{
	public partial class CreateUser : Page, ICallbackEventHandler
    {
		bool usercreation_success = false;

		public void Page_Load(object sender, EventArgs e)
		{
			// Get the page's client script and assign it to a ClientScriptManager
			ClientScriptManager cm = Page.ClientScript;

			// Generate a callback reference
			string cbRefernce = cm.GetCallbackEventReference(this, "arg", "UserCreated", null);

			// Build the callback script block
			string cbscript = "function SubmitUser(arg, context) {" + cbRefernce + "};";

			// Register the block
			cm.RegisterClientScriptBlock(Page.GetType(), "SubmitUser", cbscript, true);
		}

        /// <summary>
        /// Method that gets called at the end of a page callback after server processing has happened. 
        /// This controls what gets returned to the clients machine.
        /// </summary>
        /// <returns></returns>
        string ICallbackEventHandler.GetCallbackResult()
        {
            if (usercreation_success)
            {
                return "User Registered";
            }
            else
            {
                return "Your User account has been created";
            }
        }

        /// <summary>
        /// This method is what gets called when a callback is initated from the clients AJAX call.
        /// </summary>
        /// <param name="eventArgument"></param>
        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			ClientDataInbound clientevent = JsonConvert.DeserializeObject<ClientDataInbound>(eventArgument);

			if (clientevent.action == "CreateUser" && clientevent.check() == true)
            {
				VerifyClientData verifyevent = JsonConvert.DeserializeObject<VerifyClientData>(eventArgument);
				verifyevent.check ();

				if (verifyevent.valid)
                {
					CreateClientUser usercreation = new CreateClientUser(verifyevent);

					if (usercreation.valid)
                    {
						try
                        {
						    usercreation.create_user();
						}
                        catch (Exception e)
                        {
							System.Diagnostics.Debug.WriteLine (String.Format ("{0}\r\n{1}\r\n{2}", e.Message, e.InnerException, e.Data));
						}
                        finally
                        {
                            usercreation_success = usercreation.usercreated;
						}
					}
				}
			}
		}
	}
}