using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace InventoryTrackingSystem.Pages;

public class IndexModel : InventoryTrackingSystemPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
