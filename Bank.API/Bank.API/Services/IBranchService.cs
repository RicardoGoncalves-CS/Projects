using Bank.API.Models.DTOs.BranchDTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bank.API.Services
{
    public interface IBranchService : IBankService<CreateBranchDTO, GetBranchDTO, UpdateBranchDTO>
    {
        Task<bool> AddCustomerToBranchAsync(int branchId, int customerId);
    }
}
