using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace ProudSource
{
    /// <summary>
    /// Notes: There exists a variable InvestorID that holds the integer ID value of the investor account that gets created.
    /// </summary>
    public partial class CreateInvestor : Page, ICallbackEventHandler
    {
        public string UserName;
        private int UserID, InvestorID;
        bool InvestorCreated = false;

        /// <summary>
        /// This method will acomplish tasks that must fire everytime that this page is shown to the user.
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
            string cbRefernce = cm.GetCallbackEventReference(this, "arg", "InvestotrCreated", null);

            // Build the callback script block
            string cbscript = "function CreateInvestor(arg, context) {" + cbRefernce + "};";

            // Register the block
            cm.RegisterClientScriptBlock(Page.GetType(), "CreateInvestor", cbscript, true);
        }

        /// <summary>
        /// Controls what gets passed back to the page to the user
        /// </summary>
        /// <returns></returns>
        string ICallbackEventHandler.GetCallbackResult()
        {
            if (InvestorCreated)
            {
                return "Investor Profile Created";
            }
            else
            {
                return "Investor Profile Not Created!";
            }
        }

        /// <summary>
        /// When a page callback is made by the client this method is what will activate. 
        /// eventArgument is the stringified data that is passed in by the the callback from the client side javascript function.
        /// </summary>
        /// <param name="eventArgument"></param>
        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            ClientDataInbound clientevent = JsonConvert.DeserializeObject<ClientDataInbound>(eventArgument);

            if (clientevent.action == "CreateInvestor")
            {
                VerifyClientData verifyevent = JsonConvert.DeserializeObject<VerifyClientData>(eventArgument);
                verifyevent.check();

                if (verifyevent.valid)
                {
                    CreateInvestorProfile newprofile = new CreateInvestorProfile(verifyevent);

                    try
                    {
                        InvestorID = newprofile.create_investor(UserID, verifyevent.InvestorProfile);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("{0}\r\n{1}\r\n{2}", e.Message, e.InnerException, e.Data));
                        InvestorCreated = false;
                    }
                    finally
                    {
                        InvestorCreated = newprofile.valid;
                    }
                }
            }
        }
    }
}