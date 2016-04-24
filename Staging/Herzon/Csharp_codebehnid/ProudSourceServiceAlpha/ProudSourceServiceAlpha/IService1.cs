using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ProudSourceServiceAlpha
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        [OperationContract]
        void foo();

        #region Operation Contracts that deal with Microsoft Identity user actions
        // Need to implement service contracts to allow for authentication mechanism of Microsoft's identity stuffs, WIF.
        #endregion

        #region Operation Contracts that deal with User profile actions
        // Create a new User profile for ProudSource front end
        #endregion

        #region Operation Contracts that deal with Entrepreneur profile actions
        // Create a new Entrepreneur profile for ProudSource front end
        #endregion

        #region Operation Contracts that deal with Investor profile actions and financial account actions
        // Create a new Investor profile for ProudSource front end, with financial account
        #endregion

        #region Operation Contracts that deal with Project profile actions and financial account actions
        // Create a new Project profile for ProudSource front end, with financial account
        #endregion

        #region Operation Contracts that deal with PROC actions
        // Create a new PROC
        #endregion

        #region Operation Contracts that deal with search view queries
        #endregion
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
