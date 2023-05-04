using Bank.API.Models.DTOs.BranchDTOs;
using Bank.API.Models;

namespace Bank.API.Services
{
    public interface oldIBranchService
    {
        Task<GetBranchDTO> GetBranchById(int id);
        Task<Branch> CreateBranchAsync(CreateBranchDTO branchDTO);
        Task UpdateBranchAsync(int id, UpdateBranchDTO branchDTO);
    }
}
