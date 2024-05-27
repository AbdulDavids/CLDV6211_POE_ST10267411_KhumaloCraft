namespace CLDV6211_POE_ST10267411_KhumaloCraft.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }

        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
