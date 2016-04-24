using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleDataBaseExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting routine");
            int count = 45;
            Console.WriteLine("Press the [Enter] key to continue.");
            Console.ReadLine();
            ManagementActions managementObject = new ManagementActions();
            // Uncomment the following lines to enable creating user tes emails.
            //Console.WriteLine(string.Format("Generating {0} user Email messages.", count));
            //managementObject.make_userTestMessages(count);
            //Console.WriteLine("Generation has finished, press the [Enter] key to read out some Emails.");
            //Console.ReadLine();
            SelectQueries selection = new SelectQueries();
            foreach (System.Data.DataRow i in selection.get_Outbound_UserMessages().Tables[0].Rows)
            {
                Console.WriteLine("***********************************");
                Console.WriteLine(string.Format("|| Email Origin: {0}", i[2]));
                Console.WriteLine(string.Format("|| Email Destination: {0}", i[1]));
                Console.WriteLine(string.Format("|| Email Index ID: {0}", i[0]));
                Console.WriteLine(string.Format("|| Email Subject: {0}", i[3]));
                Console.WriteLine(string.Format("|| Email Body: {0}", i[4]));
                Console.WriteLine("***********************************");
            }
            Console.WriteLine("Reading User Email messages has concluded, press the [Enter] key to quit.");
            Console.ReadLine();
        }
    }
}
