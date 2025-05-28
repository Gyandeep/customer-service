namespace Customer.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using System.Net;
    using Customer.Shared;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer([FromRoute]Guid guid)
        {
            try
            {
                var customerData = await _customerService.GetCustomer(guid);
                return Ok(customerData);
            }
            catch (ResourceNotExistException resEx)
            {
                return NotFound(resEx.Message);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }

                //logger.LogCritical(ex, "");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error has occurred while retrieving customer data id: {guid}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerRequest customerRequest)
        {
            try
            {
                var customerData = await _customerService.CreateCustomer(customerRequest);
                return Created(customerData.Id.ToString(), customerData);
            }
            catch (ResourceAlreadyExistsException resAEx)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, resAEx);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }

                return StatusCode((int)HttpStatusCode.InternalServerError, "An error has occurred while creating Customer"); 
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCustomer([FromBody] CustomerRequest customerRequest)
        {
            try
            {
                var customerData = await _customerService.UpdateCustomer(customerRequest);
                return Ok(customerData);
            }
            catch (ResourceNotExistException resEx)
            {
                return NotFound(resEx.Message);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, $"An error has occurred while updating customer data id: {customerRequest.Id}");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCustomer([FromRoute] Guid guid)
        {
            try
            {
                await _customerService.DeleteCustomer(guid);
                return Ok();
            }
            catch (ResourceNotExistException resEx)
            {
                return NotFound(resEx.Message);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, $"An error has occurred while deleting customer data id: {guid}");
        }
    }
}
