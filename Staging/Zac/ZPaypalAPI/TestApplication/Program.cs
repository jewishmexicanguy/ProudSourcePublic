using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPaypalAPI;
using System.Data.SqlClient;
using System.Configuration;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ZPayout payout = new ZPayout(1, "test@notanemail.com", 1.00, "USD");
            payout.execute();

            Console.WriteLine("Executed Payout!");

            ZPayment newPayment = new ZPayment(
                accountID: 1,
                itemName: "Add Funds",
                itemDescription: "Test Item",
                currency: "USD",
                subTotal: 1.00,
                cardNumber: "4417119669820331",
                cardType: "visa",
                expMonth: 11,
                expYear: 2018,
                cvv: "874",
                firstName: "Joe",
                lastName: "Shopper",
                address: "52 N Main ST",
                city: "Johnstown",
                countryCode: "US",
                postalCode: "43210",
                state: "OH"
            );

            bool paymentReturn = newPayment.executePayment();

            Console.WriteLine("Payment Status: " + paymentReturn);
            Console.WriteLine("Payment ID: " + newPayment.paymentID);
            Console.WriteLine("Sale ID: " + newPayment.saleID);

            if (paymentReturn)
            {
                //ZPayout payout = new ZPayout("test@notanemail.com", "1.00", "USD");
            }


            Console.WriteLine("Done");

            Console.ReadKey();
        }
    }
}
