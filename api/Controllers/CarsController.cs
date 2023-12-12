using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        // GET: api/TTVM
        [HttpGet]
        public List<Cars> Get()
        {
            CarUtilities utility = new CarUtilities();
            return utility.GetAllCars();
        }


        // GET: api/TTVM/5
        [HttpGet("{id}", Name = "GetCar")]
        public Cars Get(int id)
        {
        
        CarUtilities utility = new CarUtilities();
        List<Cars> myCars = utility.GetAllCars();
            foreach (Cars car in myCars)
            {
             if (car.CarID == id)
                {
                    return car;
                }
            }
        return new Cars();
}

        // POST: api/TTVM
        [HttpPost]
        public void Post([FromBody] Cars value)
        {
            CarUtilities utility = new CarUtilities();
            utility.CreateCar(value);
        }

        // PUT: api/TTVM/5
        [HttpPut("{id}")]
        public void Put([FromBody] Cars value)
        {
            CarUtilities utility = new CarUtilities();
            utility.UpdateCars(value);
        }

        // DELETE: api/TTVM/5
        [HttpDelete("{id}")]
        public void Delete(Cars value)
        {
            CarUtilities utility = new CarUtilities();
            utility.DeleteCar(value);
        }
    }
}
