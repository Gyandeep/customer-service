namespace Customer.Data
{
    using Microsoft.Extensions.Caching.Memory;

    using Customer.Shared;

    public class CustomerData : ICustomerData
    {
        /*
         * Methods are kept async to imitate actual db calls.
         */
        private readonly IMemoryCache _memoryCache;

        public CustomerData(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<CustomerDto> CreateCustomer(CustomerDto customerDto)
        {
            if (_memoryCache.TryGetValue(customerDto.EmailAddress, out _))
            {
                throw new ResourceAlreadyExistsException("A customer with same email address already exists!");
            }

            customerDto.Id = Guid.NewGuid();
            _memoryCache.Set(customerDto.Id, customerDto);
            _memoryCache.Set(customerDto.EmailAddress, customerDto.Id);
            
            return await Task.FromResult(customerDto);
        } 

        public async Task DeleteCustomer(Guid id)
        {
            if (!_memoryCache.TryGetValue(id, out _)) 
            {
                throw new ResourceNotExistException($"A customer with id : {id} not found!");
            }

            var customerData = (CustomerDto)_memoryCache.Get(id);
            _memoryCache.Remove(customerData.EmailAddress);
            _memoryCache.Remove(customerData.Id);

            await Task.CompletedTask;
        }

        public async Task<CustomerDto> GetCustomerById(Guid id)
        {
            if (!_memoryCache.TryGetValue(id, out _))
            {
                throw new ResourceNotExistException($"A customer with id : {id} not found!");
            }

            return await Task.FromResult((CustomerDto)_memoryCache.Get(id));
        }

        public async Task<CustomerDto> UpdateCustomer(CustomerDto customerDto)
        {
            Guid id = customerDto != null ? customerDto.Id : Guid.Empty;

            if (!_memoryCache.TryGetValue(id, out _))
            {
                throw new ResourceNotExistException($"A customer with id : {id} not found!");
            }

            var customerData = (CustomerDto)_memoryCache.Get(id);
            customerData.FirstName = customerDto.FirstName;
            customerData.LastName = customerDto.LastName;
            customerData.MiddleName = customerDto.MiddleName;
            customerData.PhoneNumber = customerDto.PhoneNumber;

            _memoryCache.Set<CustomerDto>(id, customerData);
            return await Task.FromResult(customerData);
        }
    }
}
