namespace Demo.Models
{
    public class Dog
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person Owner { get; set; }
        public override string ToString()
        {
            return $"{Name} {Age} (Owner {Owner})";
        }
    }
}
