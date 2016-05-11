namespace File_Interface_Simulator.Models
{
    public class MessageGroupDetailViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int AmountOfFields { get; set; }
        public string Level { get; set; }
        public string ErrorMessage { get; set; }
    }
}