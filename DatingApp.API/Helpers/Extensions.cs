using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        public static HttpResponse AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Application-Error", message);

            return response;
        }

        public static int CalculateAge(this DateTime datetime)
        {
            var age = DateTime.Today.Year - datetime.Year;
            if (datetime.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}