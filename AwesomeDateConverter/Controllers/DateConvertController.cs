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
        public IHttpActionResult Post(Date timeToConvert)
        {
            try
            {
                Date dateResponse = new Date();
                var parsedDate = DateTimeOffset.Parse(timeToConvert.posted);
                dateResponse.posted = DateTime.Parse(parsedDate.ToString()).ToString("yyyy-MM-ddTHH:mm:ss");
                dateResponse.converted = DateTime.Parse(timeToConvert.posted).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
                return this.Ok(dateResponse);
            }
            catch(Exception ex)
            {
                return this.BadRequest("There was a problem converting the local time to UTC.  The error is: " + ex.Message);
            }   
        }
    }
}
