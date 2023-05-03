using Bank.API.Data.Repository;
using Bank.API.Models;
using Bank.API.Models.DTOs.BranchDTOs;

namespace Bank.API.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IAddressRepository _addressRepository;

        public BranchService(IBranchRepository branchRepository, IAddressRepository addressRepository)
        {
            _branchRepository = branchRepository;
            _addressRepository = addressRepository;
        }

        public async Task<GetBranchDTO> GetBranchById(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);

            if(branch == null)
            {
                return null;
            }

            var customerIds = branch.Customers?.Select(c => c.Id);

            var branchDTO = new GetBranchDTO
            {
                Id = branch.Id,
                BranchName = branch.BranchName,
                Address = branch.Address,
                CustomerIds = customerIds
            };

            return branchDTO;
        }

        public async Task<Branch> CreateBranchAsync(CreateBranchDTO branchDTO)
        {
            var address = await _addressRepository.GetByIdAsync(branchDTO.AddressId);

            var branch = new Branch
            {
                BranchName = branchDTO.BranchName,
                Address = address
            };

            await _branchRepository.AddAsync(branch);

            return branch;
        }

        public async Task UpdateBranchAsync(int id, UpdateBranchDTO branchDTO)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
            {
                throw new ArgumentException($"Branch with id {id} not found");
            }

            if (branchDTO.AddressId != 0)
            {
                var address = await _addressRepository.GetByIdAsync(branchDTO.AddressId);
                branch.Address = address;
            }

            branch.BranchName = branchDTO.BranchName;

            await _branchRepository.UpdateAsync(branch);
        }
    }
}
