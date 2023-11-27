namespace ShoppingKart.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public byte[] ProductImage { get; set; }

    }
}
