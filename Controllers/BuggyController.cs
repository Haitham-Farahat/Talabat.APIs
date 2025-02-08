using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{

    public class BuggyController : APIBaseController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("NotFound")]
        //baseUrl/api/Byggy/NotFound
        public ActionResult GetNotFoundRequest()
        {
            var Product = _dbContext.Products.Find(100);
            if (Product is null) return NotFound(new ApiResponse(404));
            return Ok(Product);
        }


        [HttpGet("ServerError")]
        //baseUrl/api/Byggy/ServerError
        public ActionResult GetServerError()
        {
            var Product = _dbContext.Products.Find(100);
            var ProductToReturn = Product.ToString();// Error
            // Will Throw Exception [Null Refernce Exception]
            return Ok(ProductToReturn);
        }


        [HttpGet("badRequest")]
        public ActionResult GetbadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }


        [HttpGet("badRequest/{id}")]
        public ActionResult GetbadRequest(int id)
        {
            return Ok();
        }
    }
}
