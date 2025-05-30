namespace Customer.Service
{
    using Customer.Data;

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerData _customerData;

        public CustomerService(ICustomerData customerData)
        {
            this._customerData = customerData ?? throw new ArgumentNullException(nameof(customerData));
        }

        public async Task<CustomerResponse> CreateCustomer(CustomerRequest customerRequest)
        {
            (bool isValidObj, string error) = IsValidCustomerObj(customerRequest);

            if (!isValidObj)
            {
                throw new ArgumentException(error);
            }

            var customerDto = Transform(customerRequest);
            var customerDtoResponse = await _customerData.CreateCustomer(customerDto).ConfigureAwait(false);
            return Transform(customerDtoResponse);
        }

        public async Task DeleteCustomer(Guid id)
        {
            (bool isValid, string errorMessage) = IsValidKey(id);

            if (!isValid)
            {
                throw new ArgumentException(errorMessage);
            }

            await _customerData.DeleteCustomer(id).ConfigureAwait(false);
        }

        public async Task<CustomerResponse> GetCustomer(Guid id)
        {
            (bool isValid, string errorMessage) = IsValidKey(id);

            if (!isValid)
            {
                throw new ArgumentException(errorMessage);
            }

            var customerDataResponse = await _customerData.GetCustomerById(id).ConfigureAwait(false);
            return Transform(customerDataResponse);
        }

        public async Task<CustomerResponse> UpdateCustomer(CustomerRequest customerRequest)
        {
            (bool isValidObj, string error) = IsValidKey(customerRequest.Id);

            if (!isValidObj)
            {
                throw new ArgumentException(error);
            }

            var customerDto = Transform(customerRequest);
            var customerDataResponse = await _customerData.UpdateCustomer(customerDto).ConfigureAwait(false);
            return Transform(customerDataResponse);
        }

        private CustomerDto Transform(CustomerRequest customerRequest)
        {
            if (customerRequest == null)
            {
                throw new ArgumentNullException(nameof(customerRequest));
            }

            return new CustomerDto()
            {
                Id = customerRequest.Id,
                FirstName = customerRequest.FirstName,
                LastName = customerRequest.LastName,
                MiddleName = customerRequest.MiddleName,
                EmailAddress = customerRequest.EmailAddress,
                PhoneNumber = customerRequest.PhoneNumber
            };
        }

        private CustomerResponse Transform(CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                throw new ArgumentNullException(nameof(customerDto));
            }

            return new CustomerResponse()
            {
                FirstName = customerDto.FirstName,
                MiddleName = customerDto.MiddleName,
                LastName = customerDto.LastName,
                EmailAddress = customerDto.EmailAddress,
                PhoneNumber = customerDto.PhoneNumber,
                Id = customerDto.Id
            };
        }

        private (bool, string) IsValidCustomerObj(CustomerRequest customerRequest)
        {
            if (customerRequest == null)
            {
                return (false, "Customer information is null");
            }

            if (string.IsNullOrEmpty(customerRequest.FirstName))
            {
                return (false, $"Customer {nameof(CustomerDto.FirstName)} is null");
            }

            if (string.IsNullOrEmpty(customerRequest.LastName))
            {
                return (false, $"Customer {nameof(CustomerDto.LastName)} is null");
            }

            if (string.IsNullOrEmpty(customerRequest.EmailAddress))
            {
                return (false, $"Customer {nameof(CustomerDto.EmailAddress)} is null");
            }

            return (true, string.Empty);
        }

        private (bool, string) IsValidKey(Guid id)
        {
            if (id == Guid.Empty)
            {
                return (false, "Customer Id is not valid");
            }

            return (true, string.Empty);
        }

    }
}
