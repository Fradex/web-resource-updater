using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPackUpdater.Model;
using WebPackUpdater.Repositories.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebPackUpdater.Controllers
{
    [Route("api/[controller]")]
    public class BuildController : Controller
    {
        private IBuildRepository BuildRepository { get; set; }

        public BuildController(IBuildRepository buildRepository)
        {
            BuildRepository = buildRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public Task<IEnumerable<Build>> Get()
        {
            return BuildRepository.GetAllAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Task<Build> Get(Guid? id)
        {
            return BuildRepository.GetAsync(id);
        }
    }
}