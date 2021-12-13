using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appv1.Configuration
{
    public class SwaggerOptions
    {
        public string JsonRoute = "swagger/{documentName}/swagger.json";
        public string UIEndpoint = "v1/swagger.json";
        public string Description = "Demo API";
    }
}
