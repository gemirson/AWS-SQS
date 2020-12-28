namespace AWS.SQS.Publish.Model
{
    public class Adress
    {
        public string Name { get; private set; }
        public string Cod { get; private set; }
        public string Complement { get; private set; }
        public string City { get; private set; }

        public string Country { get; private set; }

        public Adress(string name, string cod, string complement, string city, string country)
        {
            Name = name;
            Cod = cod;
            Complement = complement;
            City = city;
            Country = country;
        }
    }
}