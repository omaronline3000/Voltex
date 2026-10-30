using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voltex.Core.Interfaces;

namespace Voltex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
