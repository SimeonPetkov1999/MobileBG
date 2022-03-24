namespace MobileBG.Web.Areas.Administration.Controllers;
using MobileBG.Common;
using MobileBG.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
[Area("Administration")]
public class AdministrationController : BaseController
{
}
