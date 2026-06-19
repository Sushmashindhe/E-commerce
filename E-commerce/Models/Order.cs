namespace E_commerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
