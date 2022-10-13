namespace SwaggerAPITest.Services.Interfaces
{
    public interface IAtmService
    {
        public bool IsCardExist(string cardNumber);
        public bool VerifyPassword(string cardNumber, string cardPassword);

        public Task Withdraw(string cardNumber, decimal amount);
        public decimal GetCardBalance(string cardNumber);

        public Task AddAmount(string cardNumber, decimal amount);
        public Task Tranzaction(string cardNumberSender, string cardNumberReceiver, decimal amount);
    }
}
