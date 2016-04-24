using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace ProudSource
{
    public partial class CreateEntreprenuer : Page, ICallbackEventHandler
    {
        public string UserName;
        public int UserID, EntrepreneurID;
        bool EntrepreneurCreated = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // This a page where the user using it must be identifed and logged in so that we know what master user ID investor accounts created are associated with.
            // This if ... else statement will check to make sure that a session object exists with the information that we are looking for.
            if (Session["ProudSourceUser"] != null)
            {
                UserSessionAccessor Token = new UserSessionAccessor((object[])Session["ProudSourceUser"]);
                {
                    if (Token.UserAuthenticated)
                    {
                        UserName = Token.UserName;
                        UserID = Token.UserID;
                        Token.Dispose();
                    }
                    else
                    {
                        Response.Redirect("UserLogin.aspx");
                    }
                }
            }
            else
            {
                if (!Page.IsCallback)
                {
                    Server.Transfer("UserLogin.aspx");
                }
                else
                {
                    Response.Redirect("UserLogin.aspx");
                }
            }

            // Add the scripts that allow accessing resources from our server only after the user can be authenticated

            // Get the page's client script and assign it to a ClientScriptManager
            ClientScriptManager cm = Page.ClientScript;

            // Generate a callback reference
            string cbRefernce = cm.GetCallbackEventReference(this, "arg", "EntrepreneurCreated", null);

            // Build the callback script block
            string cbscript = "function CreateEntrepreneur(arg, context) {" + cbRefernce + "};";

            // Register the block
            cm.RegisterClientScriptBlock(Page.GetType(), "CreateEntrepreneur", cbscript, true);
        }

        /// <summary>
        /// This method is responsible for giving the client user a return message after AJAX processing. 
        /// </summary>
        /// <returns></returns>
        string ICallbackEventHandler.GetCallbackResult()
        {
            if (EntrepreneurCreated)
            {
                return "Entrepreneur Profile Created";
            }
            else
            {
                return "Entrepreneur Profile failed to be created";
            }
        }

        /// <summary>
        /// This method is what accepts an incoming stringified JSON object from the client user.
        /// </summary>
        /// <param name="eventArgument"></param>
        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            ClientDataInbound clientevent = JsonConvert.DeserializeObject<ClientDataInbound>(eventArgument);

            if (clientevent.action == "CreateEntrepreneur")
            {
                VerifyClientData verifyevent = JsonConvert.DeserializeObject<VerifyClientData>(eventArgument);
                verifyevent.check();

                if (verifyevent.valid)
                {
                    CreateEntreprenuerProfile newprofile = new CreateEntreprenuerProfile(verifyevent);

                    try
                    {
                        EntrepreneurID = newprofile.create_entreprenuer(UserID, verifyevent.EntrepreneurProfile);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("{0}\r\n{1}\r\n{2}", e.Message, e.InnerException, e.Data));
                        EntrepreneurCreated = false;
                    }
                    finally
                    {
                        EntrepreneurCreated = newprofile.valid;
                    }
                }
            }
        }
    }
}