namespace Stations.Models.DTOs
{
    public class CardImportDto
    {
        public CardImportDto()
        {
            this.CardType = CardType.Normal;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public CardType CardType { get; set; }
    }
}
