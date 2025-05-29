namespace Customer.Data
{
    public interface ICustomerData
    {
        Task<CustomerDto> GetCustomerById(Guid id);

        Task<CustomerDto> CreateCustomer(CustomerDto customerDto);

        Task<CustomerDto> UpdateCustomer(CustomerDto customerDto);

        Task DeleteCustomer(Guid id);
    }
}