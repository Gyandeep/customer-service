namespace Customer_Service
{
    /// <summary>
    /// An interface to interact with CustomerData
    /// </summary>
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomer(CustomerRequest customerRequest);

        Task<CustomerDto> UpdateCustomer(CustomerRequest customerRequest);

        Task DeleteCustomer(CustomerRequest customerRequest);

        Task<CustomerDto> GetCustomer(Guid id);
    }
}
