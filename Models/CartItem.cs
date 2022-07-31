namespace BookStore.Models
{
    public class CartItem
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }
        public string Image { get; set; }

        public CartItem()
        {
        }

        public CartItem(ProductModel productModel)
        {
            ItemId = productModel.ProductID;
            ItemName = productModel.Title;
            Price = productModel.Price;
            Quantity = 1;
            Image = productModel.Cover;
        }
    }
}
