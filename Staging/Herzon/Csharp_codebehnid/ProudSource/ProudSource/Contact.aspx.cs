using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace ProudSource
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Submit_Click(object sender, EventArgs e)
        {
            // check that all text boxes are not null
            if (Tbox_Email.Text == string.Empty)
            {
                return;
            }

            if (TBox_Message.Text == string.Empty)
            {
                return;
            }

            if (Tbox_Name.Text == string.Empty)
            {
                return;
            }

            if (TBox_Subject.Text == string.Empty)
            {
                return;
            }
            try
            {
                send_Email(Tbox_Email.Text, TBox_Subject.Text, TBox_Subject.Text, TBox_Message.Text);
            }
            catch (Exception k)
            {

            }
        }

        private void send_Email(string Email, string subject, string name, string message)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.google.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("joe@proudsource.us", "Proud2bproud");

            MailMessage mail = new MailMessage(Email, "joe@proudsource.us", subject, message);
            //client.Send(mail);
        }
    }
}