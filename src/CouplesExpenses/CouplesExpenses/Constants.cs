using System;
using System.Collections.Generic;
using System.Text;

namespace CouplesExpenses
{
    public static class Constants
    {
        // Replace strings with your Azure Mobile App endpoint.
        public static string ApplicationURL = @"https://couplesexpenses.azurewebsites.net";
        // public static string ApplicationURL = @"http://localhost:50015";

        public static Guid member1 = Guid.Parse("E47874F0-D181-462A-8EB6-4B91A37A1D0A");
        public static Guid member2 = Guid.Parse("7670e053-502d-4bf9-8999-52c29e316ef5");
        public static Guid CoupeId = Guid.Parse("33f35658-7406-40c2-8d57-e4f7bf51fd09");
    }
}