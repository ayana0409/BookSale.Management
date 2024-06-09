namespace BookSale.Management.Application.DTOs.ViewModal
{
    public class ExternalLoginModel
    {
        public string Provider { get; set; }
        public string ReturnUrl { get; set; }
        public string Email { get; set; }
        public string Fullname {  get; set; }
    }
}
