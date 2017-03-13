using System.ComponentModel.DataAnnotations;

namespace _11_Banking_System.Models
{
    public class SavingAccount
    {
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public int UserId { get; set; }         // Problem 12

        public virtual User User { get; set; }  // Problem 12

        public decimal DepositMoney(decimal money)
        {
            return this.Balance += money;
        }

        public decimal WithdrawMoney(decimal money)
        {
            return this.Balance -= money;
        }

        public decimal AddInterest()
        {
            return this.Balance *= (1 + this.InterestRate / 100);
        }
    }
}
