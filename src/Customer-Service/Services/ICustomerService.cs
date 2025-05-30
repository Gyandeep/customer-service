namespace Customer.Service
{
    /// <summary>
    /// An interface to interact with CustomerData
    /// </summary>
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateCustomer(CustomerRequest customerRequest);

        Task<CustomerResponse> UpdateCustomer(CustomerRequest customerRequest);

        Task DeleteCustomer(Guid id);

        Task<CustomerResponse> GetCustomer(Guid id);
    }
}
