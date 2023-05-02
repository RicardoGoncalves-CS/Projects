namespace BankApp.API.Models.DTOs
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public int Balance { get; set; } = 0;
        public DateTime OpenDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
    }
}
