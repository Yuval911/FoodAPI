using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodModel;

namespace FoodAPI.Controllers
{
    public class FoodController : ApiController
    {
        static DataManager dataManager = new DataManager();

        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<Food> foods = dataManager.GetAll();

            if (foods.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            
            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            Food food = dataManager.GetByID(id);

            if (food == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, food);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Food food)
        {
            try
            {
                dataManager.Add(food);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse("Application threw an exception: " + ex.ToString());
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Food food)
        {
            try
            {
                dataManager.Update(food);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse("Application threw an exception: " + ex.ToString());
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                dataManager.Remove(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse("Application threw an exception: " + ex.ToString());
            }
        }

        // Special functions:

        [Route("api/food/getbyname/{name}")]
        [HttpGet]
        public HttpResponseMessage GetByName([FromUri]string name)
        {
            Food food = dataManager.GetByName(name);

            if (food == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, food);
        }

        [Route("api/food/getbymincal/{mincal}")]
        [HttpGet]
        public HttpResponseMessage GetByMinCal([FromUri]int minCal)
        {
            List<Food> foods = dataManager.GetByMinCal(minCal);

            if (foods.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }

        [Route("api/food/search")]
        [HttpGet]
        public HttpResponseMessage GetByCategories(string name = "", int grade = 0, int minCal = 0, int maxCal = 0)
        {
            List<Food> foods = dataManager.GetByCategories(name, grade, minCal, maxCal);

            if (foods.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }
    }
}
