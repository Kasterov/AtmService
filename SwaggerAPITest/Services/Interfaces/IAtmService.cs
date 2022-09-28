namespace SwaggerAPITest.Services.Interfaces
{
    public interface IAtmService
    {
        public bool IsCardExist(string cardNumber);
        public bool VerifyPassword(string cardNumber, string cardPassword);

        public void Withdraw(string cardNumber, decimal amount);
        public decimal GetCardBalance(string cardNumber);

        public void AddAmount(string cardNumber, decimal amount);
        public void Tranzaction(string cardNumberSender, string cardNumberReceiver, decimal amount);
    }
}
