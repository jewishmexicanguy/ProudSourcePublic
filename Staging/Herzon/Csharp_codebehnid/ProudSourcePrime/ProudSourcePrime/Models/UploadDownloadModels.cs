using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProudSourcePrime.Models
{
    /// <summary>
    /// Class that will handle the modeling and uploading of documents for proud source.
    /// </summary>
    public class DocumentFileModel
    {
        /// <summary>
        /// Private resident that houses client object to interact with our WCF Data service.
        /// </summary>
        private ServiceReference1.Service1Client client;
        /// <summary>
        /// Public accessor exposing functionality to set the document filename in question.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Public accessor exposing ability to set the description of this document. 
        /// </summary>
        [Display(Name = "What is this document about?")]
        public string Description { get; set; }
        /// <summary>
        /// Public accesor exposing functionality to set or get the mime file type of this document.
        /// </summary>
        public string MimeType { get; set; }
        /// <summary>
        /// Accessor exposing th Id of a document in the event of a document update.
        /// </summary>
        public int Document_Id { get; set; }
        /// <summary>
        /// Accessor that exposes functionality to get or set the byte array contents that will interact with our WCF service to place the document into our data base.
        /// </summary>
        [Display(Name = "Choose a document")]
        public byte[] DocumentData { get; set; }
        /// <summary>
        /// Accessor that exposes functionality to assign whether or not this document belongs to a project or a normal profile.
        /// </summary>
        public bool IsProject { get; set; }
        /// <summary>
        /// Accessor that exposes functionality to assign what profile this document will be associated too.
        /// </summary>
        public int Profile_Id { get; set; }
        /// <summary>
        /// Class constructor
        /// </summary>
        public DocumentFileModel()
        {
            client = new ServiceReference1.Service1Client();
        }
        /// <summary>
        /// Method that passes in an integer that represents the file to be retrived by our WCF data service.
        /// 
        /// Will return a dictionary object caontaing the pertinant information for giving this docuemnt to the end user.
        /// </summary>
        /// <param name="document_Id"></param>
        /// <returns></returns>
        public Dictionary<string, string> DownloadFile(int document_Id)
        {
            return client.get_Document(document_Id);
        }
        /// <summary>
        /// Model method used for requesting that a new file be uploaded via our WCF service.
        /// </summary>
        /// <param name="docData"></param>
        /// <param name="fileName"></param>
        /// <param name="mimeType"></param>
        /// <param name="IsProject"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool UploadFile()
        {
            bool result = false;
            Dictionary<string, object> docDict = new Dictionary<string, object>();
            docDict.Add("int,profile_Id", Profile_Id);
            docDict.Add("byte[],docData", DocumentData);
            docDict.Add("string,fileName", FileName);
            docDict.Add("string,mimeType", MimeType);
            docDict.Add("string,description", Description);
            docDict.Add("bool,IsProject", IsProject);
            result = client.upload_Document(new ServiceReference1.DocumentFileComposite() { DocumentDict = docDict });
            return result;
        }
        /// <summary>
        /// Model method used for requestion an update of an image given the document id.
        /// </summary>
        /// <param name="document_Id"></param>
        /// <param name="docData"></param>
        /// <param name="fileName"></param>
        /// <param name="mimeType"></param>
        /// <param name="IsProject"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool UpdateFile()
        {
            bool result = false;
            Dictionary<string, object> docDict = new Dictionary<string, object>();
            docDict.Add("int,document_Id", Document_Id);
            docDict.Add("byte[],docData", DocumentData);
            docDict.Add("string,fileName", FileName);
            docDict.Add("string,mimeType", MimeType);
            docDict.Add("string,description", Description);
            result = client.update_Document(new ServiceReference1.DocumentFileComposite() { DocumentDict = docDict });
            return result;
        }
        /// <summary>
        /// Model method used to request that a given document is to be deleted given it's id.
        /// </summary>
        /// <param name="document_Id"></param>
        /// <returns></returns>
        public bool DeleteFile(int document_Id)
        {
            bool result = false;
            // client, do thing
            return result;
        }
    }
    /// <summary>
    /// Class that will handle the modeling of uploading Images for proud source prime.
    /// </summary>
    public class ImageFileModel
    {
        /// <summary>
        /// Private resident that will house a client object too our WCF service for use by methods of this class.
        /// </summary>
        private ServiceReference1.Service1Client client;
        /// <summary>
        /// Public accessor exposing ability to set or get the image Id of the image in question.
        /// </summary>
        public int Image_Id { get; set; }
        /// <summary>
        /// Public accessor that exposes ability to get or set the byte[] for the image in question.
        /// </summary>
        [Display(Name = "Choose an image.")]
        public byte[] ImageData { get; set; }
        /// <summary>
        /// Public accessor that exposes ability tp get or set the Profile if od the profile to add an image too.
        /// </summary>
        public int Profile_Id { get; set; }
        /// <summary>
        /// Public accessor that exposes ability to tell model whether this an image for a project profile or a normal type profile.
        /// 
        /// This effectivley controls what XREF table will end up deing used to store the relation between images and profiles.
        /// </summary>
        public bool IsProject { get; set; }
        /// <summary>
        /// Class constructor
        /// </summary>
        public ImageFileModel()
        {
            client = new ServiceReference1.Service1Client();
        }
        /// <summary>
        /// Model method for structuring and sending the upload request to our WCF service.
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="profile_Id"></param>
        /// <param name="IsProject"></param>
        /// <returns></returns>
        public bool UploadImage()
        {
            bool result = false;
            Dictionary<string, object> imageDict = new Dictionary<string, object>();
            imageDict.Add("byte[],imageData", ImageData);
            imageDict.Add("int,profile_Id", Profile_Id);
            imageDict.Add("bool,IsProject", IsProject);
            result = client.upload_Image(new ServiceReference1.ImageFileComposite() { ImageDict = imageDict });
            return result;
        }
        /// <summary>
        /// Model method for sending an update request for a given image to ur WCF service.
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="image_Id"></param>
        /// <returns></returns>
        public bool UpdateImage()
        {
            bool result = false;
            Dictionary<string, object> imageDict = new Dictionary<string, object>();
            imageDict.Add("byte[],imageData", ImageData);
            imageDict.Add("int,image_Id", Image_Id);
            result = client.update_Image(new ServiceReference1.ImageFileComposite() { ImageDict = imageDict });
            return result;
        }
        /// <summary>
        /// Model method to reques that our WCF service delete an image given an image id.
        /// </summary>
        /// <returns></returns>
        public bool DeleteImage()
        {
            bool result = false;
            // client, do thing
            return result;
        }
    }
    /// <summary>
    /// Class that will handle the modeling and uploading of links for proud source prime.
    /// </summary>
    public class LinkModel
    {
        /// <summary>
        /// Publically accessible client resident
        /// </summary>
        public ServiceReference1.Service1Client client;
        /// <summary>
        /// Publically accessible profile id integer
        /// </summary>
        public int Profile_Id { get; set; }
        /// <summary>
        /// Publically accessible boolean denoting whether this is a projct or not
        /// </summary>
        public bool Is_Project { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public LinkModel()
        {
            client = new ServiceReference1.Service1Client();
            SelectOptions = new[]
            {
                new SelectListItem { Value = "YouTube", Text = "YouTube" },
                new SelectListItem { Value = "FaceBook", Text = "FaceBook" },
                new SelectListItem { Value = "Twitter", Text = "Twitter" },
                new SelectListItem { Value = "Vimeo", Text = "Vimeo" },
                new SelectListItem { Value = "Web site", Text = "Web site" },
                new SelectListItem { Value = "Blog site", Text = "Blog site" }
            };
        }
        /// <summary>
        /// String Accessor
        /// </summary>
        public string SelectedOption { get; set; }
        /// <summary>
        /// SelectListItem IEnumerable Accessor
        /// </summary>
        public IEnumerable<SelectListItem> SelectOptions { get; set; }
    }
    /// <summary>
    /// Class that will handle the uploading of embelishments for proud source prime.
    /// </summary>
    public class EmbelishmentsModel
    {
        /// <summary>
        /// Client resident
        /// </summary>
        private ServiceReference1.Service1Client _Client;
        /// <summary>
        /// Ordered Dictionary resident
        /// </summary>
        private OrderedDictionary embelishments;
        /// <summary>
        /// Class constructor
        /// </summary>
        public EmbelishmentsModel()
        {
            embelishments = new OrderedDictionary();
            Client = new ServiceReference1.Service1Client();
        }
        /// <summary>
        /// Client Accessor
        /// </summary>
        public ServiceReference1.Service1Client Client
        {
            get { return _Client; }
            set { _Client = value; }
        }
    }
}