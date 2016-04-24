using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: Move over to the new .json way of authenticating, which is recommended


namespace Hermes
{
    class Hermes
    {
        static void Main(String[] args)
        {
            keyFile = "Hermes-232edbf43f3e.p12";

            GmailBox inbox = new GmailBox(keyFile);

            Console.Read();
        }
    }
}
