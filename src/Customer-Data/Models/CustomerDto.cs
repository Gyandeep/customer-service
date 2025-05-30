namespace Customer.Data
{
    /// <summary>
    /// A customer dto.
    /// </summary>
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }
    }
}
