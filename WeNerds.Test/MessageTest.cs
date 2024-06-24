namespace WeNerds.Test;

public class MessageTest
{
        public MessageTest(Guid addresId, string value = "")
        {
            AddresId = addresId;
            if(!string.IsNullOrWhiteSpace(value))
                Value = value;
        else
        {
            Value = NewRandomValue();
        }
        }
        public Guid AddresId { get; set; }
        public string Value { get; set; }

        private string NewRandomValue()
        {
            var random = new Random();
            return (random.Next(0, 10001)).ToString();
        }
    
}
