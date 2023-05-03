using Bank.API.Models.DTOs.BranchDTOs;
using Bank.API.Models;

namespace Bank.API.Services
{
    public interface IBranchService
    {
        Task<Branch> CreateBranchAsync(CreateBranchDTO branchDTO);
        Task UpdateBranchAsync(int id, UpdateBranchDTO branchDTO);
    }
}
