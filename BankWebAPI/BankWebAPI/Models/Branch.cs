﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebAPI.Models;

public class Branch
{
    public int Id { get; set; }
    public string BranchName { get; set; }
    public int AddressId { get; set; }

    [ForeignKey("AddressId")]
    public Address Address { get; set; }

    [NotMapped]
    public IEnumerable<int> AccountIds { get; set; }

    [JsonIgnore]
    public ICollection<Account> Accounts { get; set; }
}
