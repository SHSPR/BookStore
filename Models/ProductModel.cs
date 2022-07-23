namespace BookStore.Models
{
    public class ProductModel
    {
        public Guid ID { get; set; }
        public string Cover { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int ProductID { get; set; }
    }
}
