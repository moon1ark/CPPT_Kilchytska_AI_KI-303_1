namespace DormitoryApp
{
    public class RoomModel
    {
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int Occupied { get; set; }

        public int AvailableSeats { get { return Capacity - Occupied; } }

        public string Floor
        {
            get
            {
                if (!string.IsNullOrEmpty(RoomNumber) && RoomNumber.Length > 0)
                {
                    return RoomNumber[0].ToString();
                }
                return "?";
            }
        }
    }
}