namespace E_commerce.DTO.OrderHistory
{
    public class GetOrderHistoryDto
    {
        public int id { get; set; }
        public int itmeSizeId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
