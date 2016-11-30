using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudSourceBusinessProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Repeat some code for some time interval.
            ///
            ///
            do
            {
                OnTimedEvent();
                System.Threading.Thread.Sleep(600000);
            } while (true);
        }

        private static void OnTimedEvent()
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Main update routine.
            ///
            ///
            Console.WriteLine(string.Format("Update Routine has benn run at {0}", DateTime.Now.ToString()));
            DataSet transactions_needing_update = new ProudSourceAccountingLibrary.ProudSourceAccountingProcesses().get_pendingTransactions();
            foreach (DataRow transaction in transactions_needing_update.Tables[0].Rows)
            {
                Dictionary<string, string> updateDictionary = new Dictionary<string, string>();
                string status = new ProudSourceAccountingLibrary.ProudSourceBrainTree(int.Parse(transaction["Account_Id"].ToString())).get_BrainTree_Transaction_Status(transaction["Outside_Transaction_Id"].ToString());
                if (status == "settled")
                {
                    updateDictionary.Add("Transaction_State", "PROCESSED");
                    updateDictionary.Add("DateTime_Processed", DateTime.Now.ToString());
                }
                else if (status == "failed" || status == "processor_declined" || status == "settlement_declined")
                {
                    updateDictionary.Add("Transaction_State", "DECLINED");
                    updateDictionary.Add("DateTime_Processed", DateTime.Now.ToString());
                }
                else if (!status.Equals(transaction["Transaction_State"].ToString()))
                {
                    updateDictionary.Add("Transaction_State", status);
                }
                if (updateDictionary.Keys.Count > 0)
                {
                    new ProudSourceAccountingLibrary.ProudSourceFinancialAccount(int.Parse(transaction["Account_Id"].ToString())).update_Transaction(int.Parse(transaction["Id"].ToString()), updateDictionary);
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }
}
