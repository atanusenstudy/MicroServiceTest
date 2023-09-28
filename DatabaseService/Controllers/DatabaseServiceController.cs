using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using DatabaseService.DAL.Data;
using DatabaseService.Core.Model;

namespace DatabaseService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseService : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
       // private readonly ApplicationDbContext dbContxt;


        public DatabaseService(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Hi")]
        public async Task<IActionResult> GetHi()
        {
            return Ok("Hi");
        }

        [HttpGet("all-unique-strings")]
        public IActionResult GetUnique()
        {
            return Ok(this._unitOfWork.UniqueString.GetAll().Select(x => x.Data).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> StoreUniqueStrings(List<string> uniqueStrings)
        {
            try
            {
                int count = 0;
                var savedStrings = new Dictionary<string, string>();
                savedStrings = _unitOfWork.UniqueString.GetAll().ToDictionary(s => s.Data, s => s.Data);
               // savedStrings = this.dbContxt.UniqueStrings.ToDictionary(s => s.Data, s => s.Data);

                // Store unique strings in the database
                foreach (var ele in uniqueStrings)
                {
                    // Check for duplicates and store only if it's unique
                    if (!savedStrings.ContainsKey(ele))
                    {
                        savedStrings.Add(ele, ele);
                        _unitOfWork.UniqueString.Add(new UniqueString() { Data = ele });
                    }
                    else
                    {
                        count++;
                    }
                }
                if (count == 0)
                    _unitOfWork.Save();//  this.dbContxt.SaveChangesAsync();
                else
                {
                    return BadRequest("Some Repeated data found");
                }


                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}



