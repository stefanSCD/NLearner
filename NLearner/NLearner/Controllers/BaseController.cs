using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NLearner.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string CurrentUserId
        {
            get
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrWhiteSpace(userId))
                    throw new InvalidOperationException("User ID claim (NameIdentifier) was not found in the security token.");
                return userId;
            }
        }
    }
}
