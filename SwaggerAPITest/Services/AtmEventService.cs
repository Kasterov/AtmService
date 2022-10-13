using SwaggerAPITest.Models.Events;
using SwaggerAPITest.Services.Interfaces;

namespace SwaggerAPITest.Services;

public sealed class AtmEventService : IAtmService
{
    private readonly IAtmService _atm;
    private readonly IAtmEventBroker _broker;

    public AtmEventService(IAtmService atm, IAtmEventBroker broker) => (_atm, _broker) = (atm, broker);

    public decimal GetCardBalance(string cardNumber)
    {
        if (_broker.GetLastEvent(cardNumber) is not AuthorizeEvent)
        {
            throw new UnauthorizedAccessException("Pass identification and authorization!");
        }

        _broker.AppendEvent(cardNumber, new BalanceEvent());
        return _atm.GetCardBalance(cardNumber);
    }

    public void AddAmount(string cardNumber, decimal amount)
    {
        if (_broker.GetLastEvent(cardNumber) is not AuthorizeEvent)
        {
            throw new UnauthorizedAccessException("Pass identification and authorization!"); 
        }

         _atm.AddAmount(cardNumber, amount);
        _broker.AppendEvent(cardNumber, new AddAmountEvent());
    }

    public bool IsCardExist(string cardNumber)
    {
        if (_atm.IsCardExist(cardNumber))
        {
            _broker.StartStream(cardNumber, new AtmEvent());
            _broker.AppendEvent(cardNumber, new InitEvent());

            return true;
        }
        throw new UnauthorizedAccessException("Pass identification and authorization!");
    }

    public void Tranzaction(string cardNumberSender, string cardNumberReceiver, decimal amount)
    {
        if (_broker.GetLastEvent(cardNumberSender) is not AuthorizeEvent)
        {
            throw new UnauthorizedAccessException("Pass identification and authorization!");
        }

        _atm.Tranzaction(cardNumberSender, cardNumberReceiver, amount);
        _broker.AppendEvent(cardNumberSender, new TranzactionEvent());
    }

    public bool VerifyPassword(string cardNumber, string cardPassword)
    {
        if (_broker.FindEvent<InitEvent>(cardNumber) is { }
               && _atm.VerifyPassword(cardNumber, cardPassword))
        {
            _broker.AppendEvent(cardNumber, new AuthorizeEvent());
            return true;
        }

        throw new UnauthorizedAccessException("Pass identification and authorization!");
    }
    
    public void Withdraw(string cardNumber, decimal amount)
    {
        if (_broker.GetLastEvent(cardNumber) is not AuthorizeEvent)
        {
            throw new UnauthorizedAccessException("Pass identification and authorization!");
        }

        _broker.AppendEvent(cardNumber, new WithdrawEvent());

        _atm.Withdraw(cardNumber, amount);
    }
    
}
