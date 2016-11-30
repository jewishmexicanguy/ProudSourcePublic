using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ProudSourceWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        #region Document Operations

        [OperationContract]
        bool upload_Document(DocumentFileComposite document);

        [OperationContract]
        bool update_Document(DocumentFileComposite document);

        [OperationContract]
        bool delete_Document(int document_id);

        [OperationContract]
        Dictionary<string, string> get_Document(int document_Id);

        #endregion

        #region Image Operations

        [OperationContract]
        bool upload_Image(ImageFileComposite image);

        [OperationContract]
        bool update_Image(ImageFileComposite image);

        [OperationContract]
        bool delete_Image(int image_id);

        [OperationContract]
        string get_Image(int image_id);

        #endregion

        #region User opertaions

        [OperationContract]
        UserRecordComposite get_UserById(string Id);

        [OperationContract]
        UserRecordComposite get_UserByUserName(string UserName);

        [OperationContract]
        string get_PasswordHash(string UserId);

        [OperationContract]
        bool set_PasswordHash(string userId, string passwordHash, string userName, string name);

        [OperationContract]
        UserIndexComposite get_UserIndexData(string userId);

        [OperationContract]
        bool update_ProfileData(string UserId, Dictionary<string, string> userdata);

        #endregion

        #region Investor operations

        [OperationContract]
        InvestorIndexComposite get_InvestorIndexData(string UserId, int investor_id);

        [OperationContract]
        InvestorDetailsComposite get_InvestorDetails_Data(int Investor_Id);

        #endregion

        #region Entrepreneur operations

        [OperationContract]
        EntrepreneurIndexComposite get_EntrepreneurIndexData(string UserId, int entrepreneur_id);

        [OperationContract]
        EntrepreneurDetailsComposite get_EntrepreneurDetails_Data(int Entrepreneur_Id);

        #endregion

        #region Project operations

        [OperationContract]
        int create_Project(string UserId, int Entrepreneur_Id, Dictionary<string, string> projectdata);

        [OperationContract]
        ProjectIndexComposite get_ProjectIndexData(string UserId, int entrepreneur_id, int project_id);

        [OperationContract]
        ProjectDetailsComposite get_ProjectDetails_Data(int Project_Id);

        [OperationContract]
        bool update_ProjectProfileData(string UserId, int Entrepreneur_Id, int Project_Id, Dictionary<string, string> projectdata);

        #endregion

        #region PROC operations

        [OperationContract]
        int create_PROC(string UserId, int Investor_Id, int Project_Id, Dictionary<string, string> PROCdata);

        [OperationContract]
        PROCComposite get_PROC(int proc_id, int entrepreneur_id = 0, int investor_id = 0);

        [OperationContract]
        bool update_PROC(int proc_id, Dictionary<string, string> updateDictionary, int entrepreneur_id = 0, int investor_id = 0);

        [OperationContract]
        bool alter_PROC_MutualyAccepted(int proc_id, bool investor_mutualy_accepts_PROC, int entrepreneur_id = 0, int investor_id = 0);

        [OperationContract]
        bool recant_PROC_MutualAcceptance(int proc_id, bool mutual_acceptance, int entrepreneur_id, int investor_id);

        #endregion

        #region Link operations

        [OperationContract]
        bool upload_Link(Dictionary<string, string> link);

        [OperationContract]
        bool delete_Link(int link_id, int profile_id, int profile_type_id);

        #endregion

        #region Embelishment operations

        // TODO
        //[OperationContract]
        //bool upload_Embelishment(Dictionary<string, string> embelishment);

        // TODO
        //[OperationContract]
        //bool delete_Embelishment(int embelishment_id, int profile_id, int profile_type);

        #endregion

        [OperationContract]
        FinancialAccountComposite get_FinancialAccountData(string UserId, int account_id);

        [OperationContract]
        bool create_new_BT_Transactions(string UserId, int account_Id, string nonce, decimal amount);

        [OperationContract]
        List<Dictionary<string, string>> ajax_SearchResults(string keyArg);
    }

    [DataContract]
    public class UserRecordComposite
    {
        private string _Id, _Email, _PasswordHash, _SecurityStamp, _PhoneNumber, _UserName, _Name;
        private bool _EmailConfirmed, _PhoneNumberConfirmed, _TwoFactorEnabled, _LockoutEneabled;
        private int _AccessFailedCount;
        private DateTime _LockoutEndDateUtc;

        [DataMember]
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        [DataMember]
        public string PasswordHash
        {
            get { return _PasswordHash; }
            set { _PasswordHash = value; }
        }

        [DataMember]
        public string SecurityStamp
        {
            get { return _SecurityStamp; }
            set { _SecurityStamp = value; }
        }

        [DataMember]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        [DataMember]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        [DataMember]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [DataMember]
        public bool EmailConfirmed
        {
            get { return _EmailConfirmed; }
            set { _EmailConfirmed = value; }
        }

        [DataMember]
        public bool PhoneNumberConfirmed
        {
            get { return _PhoneNumberConfirmed; }
            set { _PhoneNumberConfirmed = value; }
        }

        [DataMember]
        public bool TwoFactorEnabled
        {
            get { return _TwoFactorEnabled; }
            set { _TwoFactorEnabled = value; }
        }

        [DataMember]
        public bool LockoutEnabled
        {
            get { return _LockoutEneabled; }
            set { _LockoutEneabled = value; }
        }

        [DataMember]
        public int AccessFailedCount
        {
            get { return _AccessFailedCount; }
            set { _AccessFailedCount = value; }
        }

        [DataMember]
        public DateTime LockoutEndDateUtc
        {
            get { return _LockoutEndDateUtc; }
            set { _LockoutEndDateUtc = value; }
        }
    }

    [DataContract]
    public class UserIndexComposite
    {
        private Dictionary<string, string> _UserProfile, _EntrepreneurProfile, _InvestorProfile, _InvestorAccount;
        private Dictionary<int, Dictionary<string, string>> _EntrepreneurDocuments, _EntrepreneurLinks, _EntrepreneurEmbelishments, _EntrepreneurAccounts, _InvestorDocuments, _InvestorLinks, _InvestorEmbelishments;
        private int _Investor_Financial_Account_Id;

        [DataMember]
        public Dictionary<string, string> UserProfile
        {
            get { return _UserProfile; }
            set { _UserProfile = value; }
        }

        [DataMember]
        public Dictionary<string, string> EntrepreneurProfile
        {
            get { return _EntrepreneurProfile; }
            set { _EntrepreneurProfile = value; }
        }

        [DataMember]
        public Dictionary<string, string> InvestorProfile
        {
            get { return _InvestorProfile; }
            set { _InvestorProfile = value; }
        }

        [DataMember]
        public Dictionary<string, string> InvestorAccount
        {
            get { return _InvestorAccount; }
            set { _InvestorAccount = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> EntrepreneurDocuments
        {
            get { return _EntrepreneurDocuments; }
            set { _EntrepreneurDocuments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> EntrepreneurLinks
        {
            get { return _EntrepreneurLinks; }
            set { _EntrepreneurLinks = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> EntrepreneurEmbelishments
        {
            get { return _EntrepreneurEmbelishments; }
            set { _EntrepreneurEmbelishments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> EntrepreneurAccounts
        {
            get { return _EntrepreneurAccounts; }
            set { _EntrepreneurAccounts = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorDocuments
        {
            get { return _InvestorDocuments; }
            set { _InvestorDocuments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorLinks
        {
            get { return _InvestorLinks; }
            set { _InvestorLinks = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorEmbelishments
        {
            get { return _InvestorEmbelishments; }
            set { _InvestorEmbelishments = value; }
        }

        [DataMember]
        public int Investor_Financial_Account_Id
        {
            get { return _Investor_Financial_Account_Id; }
            set { _Investor_Financial_Account_Id = value; }
        }
    }

    [DataContract]
    public class ImageFileComposite
    {
        private Dictionary<string, object> _ImageDict;

        [DataMember]
        public Dictionary<string, object> ImageDict
        {
            get { return _ImageDict; }
            set { _ImageDict = value; }
        }
    }

    [DataContract]
    public class DocumentFileComposite
    {
        private Dictionary<string, object> _DocumentDict;

        [DataMember]
        public Dictionary<string, object> DocumentDict
        {
            get { return _DocumentDict; }
            set { _DocumentDict = value; }
        }
    }

    [DataContract]
    public class EntrepreneurIndexComposite
    {
        // entrepreneur profile details.
        private Dictionary<string, string> _EntrepreneurProfile;

        // projects owned details, Links that the entrepreneur has added to thier account, embelishments for the entrepreneur's account, docuemtns for the entrepreneurs account.
        private Dictionary<int, Dictionary<string, string>> _EntrepreneurProjects, _EntrepreneurLinks, _EntrepreneurEmbelishments, _EntrepreneurDocuments;

        [DataMember]
        public Dictionary<string,string> EntrepreneurProfile
        {
            get { return _EntrepreneurProfile; }
            set { _EntrepreneurProfile = value; }
        }

        [DataMember]
        public Dictionary<int,Dictionary<string,string>> EntrepreneurProjects
        {
            get { return _EntrepreneurProjects; }
            set { _EntrepreneurProjects = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string,string>> EntrepreneurDocuments
        {
            get { return _EntrepreneurDocuments; }
            set { _EntrepreneurDocuments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string,string>> EntrepreneurEmbelishments
        {
            get { return _EntrepreneurEmbelishments; }
            set { _EntrepreneurEmbelishments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string,string>> EntrepreneurLinks
        {
            get { return _EntrepreneurLinks; }
            set { _EntrepreneurLinks = value; }
        }
    }

    [DataContract]
    public class InvestorIndexComposite
    {
        private Dictionary<string, string> _InvestorProfile, _InvestorAccount;

        private Dictionary<int, Dictionary<string, string>> _InvestorPROCs, _InvestorLinks, _InvestorEmbelishments, _InvestorDocuments;

        private int _InvestorAccount_Id;

        [DataMember]
        public Dictionary<string,string> InvestorProfile
        {
            get { return _InvestorProfile; }
            set { _InvestorProfile = value; }
        }

        [DataMember]
        public Dictionary<string, string> InvestorAccount
        {
            get { return _InvestorAccount; }
            set { _InvestorAccount = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorePROCs
        {
            get { return _InvestorPROCs; }
            set { _InvestorPROCs = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorLinks
        {
            get { return _InvestorLinks; }
            set { _InvestorLinks = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorEmbelishments
        {
            get { return _InvestorEmbelishments; }
            set { _InvestorEmbelishments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorDocuments
        {
            get { return _InvestorDocuments; }
            set { _InvestorDocuments = value; }
        }

        [DataMember]
        public int InvestorAccount_Id
        {
            get { return _InvestorAccount_Id; }
            set { _InvestorAccount_Id = value; }
        }
    }

    [DataContract]
    public class ProjectIndexComposite
    {
        private Dictionary<string, string> _ProjectProfile, _ProjectAccount;

        private Dictionary<int, Dictionary<string, string>> _ProjectDocuments, _ProjectImages, _ProjectPROCs, _ProjectLinks, _ProjectEmbelishments;

        private int _ProjectAccount_Id;

        [DataMember]
        public Dictionary<string,string> ProjectProfile
        {
            get { return _ProjectProfile; }
            set { _ProjectProfile = value; }
        }

        [DataMember]
        public Dictionary<string,string> ProjectAccount
        {
            get { return _ProjectAccount; }
            set { _ProjectAccount = value; }
        }

        [DataMember]
        public Dictionary<int,Dictionary<string,string>> ProjectDocuments
        {
            get { return _ProjectDocuments; }
            set { _ProjectDocuments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string,string>> ProjectImages
        {
            get { return _ProjectImages; }
            set { _ProjectImages = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string,string>> ProjectPROCs
        {
            get { return _ProjectPROCs; }
            set { _ProjectPROCs = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string,string>> ProjectLinks
        {
            get { return _ProjectLinks; }
            set { _ProjectLinks = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> ProjectEmbelishments
        {
            get { return _ProjectEmbelishments; }
            set { _ProjectEmbelishments = value; }
        }

        [DataMember]
        public int ProjectAccount_Int
        {
            get { return _ProjectAccount_Id; }
            set { _ProjectAccount_Id = value; }
        }
    }

    [DataContract]
    public class FinancialAccountComposite
    {
        private Dictionary<string, string> _FinancialAccount;

        private Dictionary<int, Dictionary<string, string>> _FinancialTransactions_Processed, _FinancialTransactions_Pending, _FinancialTransactions_Failed;

        private string _Client_BrainTree_Token;

        [DataMember]
        public Dictionary<string,string> FinancialAccount
        {
            get { return _FinancialAccount; }
            set { _FinancialAccount = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> FinancialTransactions_Processed
        {
            get { return _FinancialTransactions_Processed; }
            set { _FinancialTransactions_Processed = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> FinancialTransactions_Pending
        {
            get { return _FinancialTransactions_Pending; }
            set { _FinancialTransactions_Pending = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string,string>> FinancialTransactions_Failed
        {
            get { return _FinancialTransactions_Failed; }
            set { _FinancialTransactions_Failed = value; }
        }

        [DataMember]
        public string Client_Braintree_Token
        {
            get { return _Client_BrainTree_Token; }
            set { _Client_BrainTree_Token = value; }
        }
    }

    [DataContract]
    public class FinancialTransaction
    {
        private Dictionary<string, string> _Transaction;

        [DataMember]
        public Dictionary<string, string> Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }
    }

    [DataContract]
    public class PROCComposite
    {
        private Dictionary<string, string> _PROC;

        private bool _EntrepreneurOwner, _InvestorOwner;

        [DataMember]
        public Dictionary<string,string> PROC
        {
            get { return _PROC; }
            set { _PROC = value; }
        }

        [DataMember]
        public bool EntrepreneurOwner
        {
            get { return _EntrepreneurOwner; }
            set { _EntrepreneurOwner = value; }
        }

        [DataMember]
        public bool InvestorOwner
        {
            get { return _InvestorOwner; }
            set { _InvestorOwner = value; }
        }
    }

    [DataContract]
    public class EntrepreneurDetailsComposite
    {
        private Dictionary<string, string> _EntrepreneurDetails;

        private Dictionary<int, Dictionary<string, string>> _EntrepreneurDocuments, _EntrepreneurLinks, _EntrepreneurEmbelishments, _EntrepreneurProjects, _EntrepreneurPROCs;

        [DataMember]
        public Dictionary<string,string> EntrepreneurDetails
        {
            get { return _EntrepreneurDetails; }
            set { _EntrepreneurDetails = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string,string>> EntrepreneurDocuments
        {
            get { return _EntrepreneurDocuments; }
            set { _EntrepreneurDocuments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> EntrepreneurLinks
        {
            get { return _EntrepreneurLinks; }
            set { _EntrepreneurLinks = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string,string>> EntrepreneurEmbelishments
        {
            get { return _EntrepreneurEmbelishments; }
            set { _EntrepreneurEmbelishments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> EntrepreneurProjects
        {
            get { return _EntrepreneurProjects; }
            set { _EntrepreneurProjects = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string,string>> EntrepreneurPROCS
        {
            get { return _EntrepreneurPROCs; }
            set { _EntrepreneurPROCs = value; }
        }
    }

    [DataContract]
    public class InvestorDetailsComposite
    {
        private Dictionary<string, string> _InvestorDetails;

        private Dictionary<int, Dictionary<string, string>> _InvestorDocuments, _InvestorLinks, _InvestorEmbelishments, _InvestorPROCs;

        [DataMember]
        public Dictionary<string, string> InvestorDetails
        {
            get { return _InvestorDetails; }
            set { _InvestorDetails = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorDocuments
        {
            get { return _InvestorDocuments; }
            set { _InvestorDocuments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorLinks
        {
            get { return _InvestorLinks; }
            set { _InvestorLinks = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorEmbelishments
        {
            get { return _InvestorEmbelishments; }
            set { _InvestorEmbelishments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> InvestorPROCs
        {
            get { return _InvestorPROCs; }
            set { _InvestorPROCs = value; }
        }
    }

    [DataContract]
    public class ProjectDetailsComposite
    {
        private Dictionary<string, string> _ProjectDetails;

        private Dictionary<int, Dictionary<string, string>> _ProjectDocuments, _ProjectImages, _ProjectPROCs, _ProjectLinks, _ProjectEmbelishments;

        [DataMember]
        public Dictionary<string, string> ProjectDetails
        {
            get { return _ProjectDetails; }
            set { _ProjectDetails = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> ProjectDocuments
        {
            get { return _ProjectDocuments; }
            set { _ProjectDocuments = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> ProjectImages
        {
            get { return _ProjectImages; }
            set { _ProjectImages = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> ProjectPROCs
        {
            get { return _ProjectPROCs; }
            set { _ProjectPROCs = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> ProjectLinks
        {
            get { return _ProjectLinks; }
            set { _ProjectLinks = value; }
        }

        [DataMember]
        public Dictionary<int, Dictionary<string, string>> ProjectEmbelishments
        {
            get { return _ProjectEmbelishments; }
            set { _ProjectEmbelishments = value; }
        }
    }

    [DataContract]
    public class LinkComposite
    {
        private List<string> _Link_Types;

        private Dictionary<string, string> _Link_KeyValues;

        private bool _Is_Project;

        [DataMember]
        public List<string> Link_Types
        {
            get { return _Link_Types; }
            set { _Link_Types = value; }
        }

        [DataMember]
        public Dictionary<string, string> Link_KeyValues
        {
            get { return _Link_KeyValues; }
            set { _Link_KeyValues = value; }
        }

        [DataMember]
        public bool Is_Project
        {
            get { return _Is_Project; }
            set { _Is_Project = value; }
        }
    }

    [DataContract]
    public class EmbelishmentComposite
    {
        private OrderedDictionary _od;

        [DataMember]
        public OrderedDictionary Embelishment_OrderedDictionary
        {
            get { return _od; }
            set { _od = value; }
        }
    }
}
