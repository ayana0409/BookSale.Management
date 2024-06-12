using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application.Abtracts
{
    public interface IRoleService
    {
        Task<IEnumerable<SelectListItem>> GetRoleForDropdownListAsync();
    }
}