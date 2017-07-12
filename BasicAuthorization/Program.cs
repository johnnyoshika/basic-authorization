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

            Console.WriteLine();

            Console.WriteLine("Value to Parse:");
            string value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var parser = new BasicAuthenticationParser(value);
                if (parser.TryParse(out string result))
                    Console.WriteLine($"Parsed result wrapped in singlel quotes: '{result}'");
                else
                    Console.WriteLine("Parsing error!");
            }

            Console.WriteLine("End!");
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

    public class BasicAuthenticationParser
    {
        public BasicAuthenticationParser(string value)
        {
            Value = value;
        }

        string Value { get; }

        string NormalizedValue =>
            Value.StartsWith("Basic ")
            ? Value.Substring("Basic ".Length).Trim()
            : Value;

        public string Parse() =>
            TryParse(out string result)
                ? result
                : throw new ArgumentException();

        public bool TryParse(out string result)
        {
            result = null;
            try
            {
                var encoding = new ASCIIEncoding();
                result = encoding.GetString(Convert.FromBase64String(NormalizedValue));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
