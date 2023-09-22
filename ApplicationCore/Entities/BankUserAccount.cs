using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class BankUserAccount
    {
        public int Id { get; set; }
        public string? AccountId { get; set; }
        public string? Username { get; set; }
        public decimal? AccountBalance { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public BankUserAccount(string _username, decimal _initialBal)
        {
            Username = _username;
            AccountBalance = _initialBal;
        }
        private BankUserAccount()
        {
            
        }
    }
}