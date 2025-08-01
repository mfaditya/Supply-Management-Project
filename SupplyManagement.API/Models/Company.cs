namespace SupplyManagement.API.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LogoPath { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public bool IsApprovedByManager { get; set; }
        public DateTime CreatedAt { get; set; }


        public Vendor Vendor { get; set; }
    }
}
