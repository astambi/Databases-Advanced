namespace Demo.Models
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Dog Dog { get; set; }
        public override string ToString()
        {
            return $"{Name} {Age} (Dog {Dog})";
        }
    }
}
