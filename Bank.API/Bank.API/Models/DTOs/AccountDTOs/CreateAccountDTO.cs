namespace Bank.API.Models.DTOs.AccountDTOs
{
    public class CreateAccountDTO
    {
        public int AccountNo { get; set; }
        public decimal Balance { get; set; }
        public DateTime OpenDate { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
    }
}
