using Less5_DZ_Viktor_Vill;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServer.Present;

namespace WebServer.Controllers
{
    public class EmployeesController : ApiController
    {
        PEmployees p = new PEmployees();


        [Route("worker")]
        public IEnumerable<Employee> GetAllEmplyees()
        {
            return p.GetEmployees();
        }

        [Route("worker/{id?}")]
        public Employee GetEmployById(int id)
        {
            return p.GetEmployeeById(id);
        }

        [Route("addworker")]
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            if (p.AddEmployee(employee))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
