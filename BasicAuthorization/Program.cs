using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicAuthorization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Username:");
            string username = Console.ReadLine();

            Console.WriteLine("Password:");
            string password = Console.ReadLine();

            var builder = new BasicAuthenticationBuilder(username, password);
            Console.WriteLine(builder.GetHeader());
            Console.ReadLine();
        }
    }

    public class BasicAuthenticationBuilder
    {
        public BasicAuthenticationBuilder(string username, string password)
        {
            Username = username;
            Password = password;
        }

        string Username { get; }
        string Password { get; }

        public string GetHeader()
        {
            var encoding = new ASCIIEncoding();
            byte[] auth = encoding.GetBytes(String.Format("{0}:{1}", Username, Password));
            return "Authorization: Basic " + Convert.ToBase64String(auth);
        }
    }
}
