using Less5_DZ_Viktor_Vill;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServer.Present;

namespace WebServer.Controllers
{
    public class DepartamentController : ApiController
    {
        PDepartament d = new PDepartament();

        [Route("departament")]
        public IEnumerable<Departament> GetAllDepartament()
        {
            return d.GetDepartaments();
        }

        [Route("adddepart")]
        public HttpResponseMessage Post([FromBody] Departament departament)
        {
            if (d.AddDepartament(departament))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
