namespace File_Interface_Simulator.Models
{
    public class MessageFieldDetailViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Optional { get; set; }
        public string Value { get; set; }
        public string Datatype { get; set; }
        public int Size { get; set; }
        public string Format { get; set; } = "-";
        public string Group { get; set; }
        public string Level { get; set; }
    }
}