using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProudSourcePrime.Models
{
    public class ProjectIndexModel
    {
        public ServiceReference1.ProjectIndexComposite projectModel;

        public ProjectIndexModel(string User_id, int Entrepreneur_id, int Project_id)
        {
            projectModel = new ServiceReference1.Service1Client().get_ProjectIndexData(User_id, Entrepreneur_id, Project_id);
        }
    }

    public class ProjectDetailsModel
    {
        public ServiceReference1.ProjectDetailsComposite project;

        public ProjectDetailsModel(int project_id)
        {
            project = new ServiceReference1.Service1Client().get_ProjectDetails_Data(project_id);
        }
    }
}