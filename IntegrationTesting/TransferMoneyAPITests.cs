using ejbanking.ViewModel;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTesting
{
    [TestClass]
    public class TransferMoneyAPITests
    {
        [TestMethod]
        public async Task TestConcurrency()
        {
            var client = ProgramTest.NewClient;
            var firstTransfer = TransferMoneyAsync(client);

            var secondTransfer = TransferMoneyAsync(client);

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

        private async Task<HttpResponseMessage> TransferMoneyAsync(HttpClient client)
        {
            return await client.PostAsync("api/TransferMoneyAPI?sourceAcctId=202309211556581&destAcctId=202309211556582&amount=500", null);
        }
    }
}
