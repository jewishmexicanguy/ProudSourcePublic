using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProudSource
{
    /// <summary>
    /// Verify data action.
    /// This class at instantiation will try to find what kind of server command this data is
    /// communicating on behalf of the client and as such what th user is trying to do. 
    /// All data actions need to be defined in this class.
    /// </summary>
    public class VerifyDataAction : UserDataAction
    {
        public bool valid;
        public string data_role;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProudSource.VerifyDataAction"/> class.
        /// In doing so it checks to make sure that server command is a pre defined action that
        /// we are worrying about accounting for. Define all actions or data tied events that we
        /// are expecting to recive from our clients from the web.
        /// </summary>
        public VerifyDataAction()
        {
            valid = false;

            switch (action)
            {

                case "login":
                    valid = true;
                    data_role = "login";
                    return;
                case "create-user":
                    valid = true;
                    data_role = "create-user";
                    return;
                case "create-project":
                    valid = true;
                    data_role = "create-project";
                    return;
                case "update-user-data":
                    valid = true;
                    data_role = "update-user-data";
                    return;
                case "create-investor":
                    valid = true;
                    data_role = "create-investor";
                    return;
                case "create-entreprenuer":
                    valid = true;
                    data_role = "create-entreprenuer";
                    return;
                default:
                    valid = false;
                    return;
            }
        }

        /// <summary>
        /// Abstract method that is not defined here but will be defined in by types but it will essentially Execute the action.
        /// </summary>
        public override void execute_action()
        {
            throw new NotImplementedException();
        }
    }
}