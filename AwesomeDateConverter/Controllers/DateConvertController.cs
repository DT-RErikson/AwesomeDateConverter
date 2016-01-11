using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AwesomeDateConverter.Interfaces;
using AwesomeDateConverter.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Description;

namespace AwesomeDateConverter.Controllers
{
    public class DateConvertController : ApiController
    {

        [ResponseType(typeof(Date))]
        public IHttpActionResult Post([FromBody]string timeToConvert)
        {
            try
            {
                Date dateResponse = new Date();

                var parsedDate = DateTime.Parse(timeToConvert);
                var datePostedOffset = DateTimeOffset.Parse(parsedDate.ToString(), null);
                dateResponse.posted = datePostedOffset.DateTime;
                dateResponse.converted = DateTime.Parse(timeToConvert).ToUniversalTime();
                return this.Ok(dateResponse);
            }
            catch(Exception ex)
            {
                return this.BadRequest("There was a problem converting the local time to UTC.  The error is: " + ex.Message);
            }   
        }
    }
}
