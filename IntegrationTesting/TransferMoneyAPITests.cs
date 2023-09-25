using ejbanking.ViewModel;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTesting
{
    [TestClass]
    public class TransferMoneyAPITests
    {
        [TestMethod]
        public async Task sameTransaction_testConcurrency()
        {
            var client = ProgramTest.NewClient;
            var firstTransfer = TransferMoneyAsync(client, "202309211556581", "202309211556582", 500.00M);

            var secondTransfer = TransferMoneyAsync(client, "202309211556581", "202309211556582", 500.00M);

            var alltask = await Task.WhenAll(firstTransfer, secondTransfer);

            var transaction1_output = firstTransfer.Result.Content.ReadFromJsonAsync<BankTransactionVM>();
            var transaction2_output = secondTransfer.Result.Content.ReadFromJsonAsync<BankTransactionVM>();

            var err_thrown = "";

            if(transaction1_output?.Result?.ErrorThrown != "")
            {
                err_thrown = transaction1_output?.Result?.ErrorThrown;
            }
            else if (transaction2_output?.Result?.ErrorThrown != "")
            {
                err_thrown = transaction2_output?.Result?.ErrorThrown;
            }

            Assert.AreEqual("Concurrency Exception", err_thrown);
        }

        [TestMethod]
        public async Task viceVersaTransactions_testConcurrency()
        {
            var client = ProgramTest.NewClient;
            var transfer_A = TransferMoneyAsync(client, "202309211556581", "202309211556582", 500.00M);
            var transfer_B = TransferMoneyAsync(client, "202309211556582", "202309211556581", 500.00M);

            var alltask = await Task.WhenAll(transfer_A, transfer_B);

            var transactionA_output = transfer_A.Result.Content.ReadFromJsonAsync<BankTransactionVM>();
            var transactionB_output = transfer_B.Result.Content.ReadFromJsonAsync<BankTransactionVM>();

            var err_thrown = "";

            if (transactionA_output?.Result?.ErrorThrown != "")
            {
                err_thrown = transactionA_output?.Result?.ErrorThrown;
            }
            else if (transactionB_output?.Result?.ErrorThrown != "")
            {
                err_thrown = transactionB_output?.Result?.ErrorThrown;
            }

            Assert.AreEqual("Concurrency Exception", err_thrown);
        }

        private async Task<HttpResponseMessage> TransferMoneyAsync(HttpClient client, string source, string dest, decimal amount)
        {
            //return await client.PostAsync("api/TransferMoneyAPI?sourceAcctId=202309211556581&destAcctId=202309211556582&amount=500", null);
            return await client.PostAsync("api/TransferMoneyAPI?sourceAcctId=" + source + "&destAcctId=" + dest + "&amount=" + amount, null);
        }
    }
}
