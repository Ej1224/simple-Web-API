using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTesting
{
    [TestClass]
    public class ProgramTest
    {
        private static WebApplicationFactory<Program> _application;

        public static HttpClient NewClient
        {
            get
            {
                return _application.CreateClient();
            }
        }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext _)
        {
            _application = new WebApplicationFactory<Program>();

        }
    }
}
