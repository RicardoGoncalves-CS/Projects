﻿namespace BankApp.API.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}