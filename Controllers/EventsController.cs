using MeetSport.Business.Events;
using MeetSport.Dbo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventBusiness<Event> _business;

        public EventsController(IEventBusiness<Event> business, ILogger<UsersController> logger) : base(logger)
        {
            _business = business;
        }
    }
}
