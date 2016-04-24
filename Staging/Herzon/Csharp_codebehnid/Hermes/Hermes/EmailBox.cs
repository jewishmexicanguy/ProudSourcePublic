using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System.Net.Mail;

namespace Hermes
{
    interface EmailBox
    {
        int getNumberOfEmails();
        MailMessage getEmail(String ID);
        IList<MailMessage> getEmails();
        IList<MailMessage> getUnreadEmails();
        
    }
}
