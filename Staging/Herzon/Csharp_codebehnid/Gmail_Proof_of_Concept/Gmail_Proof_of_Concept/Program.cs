using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmail_Proof_of_Concept
{
    class Program
    {
        static void Main(string[] args)
        {
            GmailBox inbox = new GmailBox();

            int GmailCount = inbox.get_number_of_Gmails();

            Console.WriteLine(String.Format(" {0} {1} {2}", "There are", GmailCount, "Gmails in this mailbox :D"));

            Console.WriteLine(" Press the [Enter] key to quit.");
            Console.ReadLine();
        }
    }
}
