namespace SupplyManagement.API.Data
{
    public class CompanyRegisterData
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile File { get; set; }
    }
}
