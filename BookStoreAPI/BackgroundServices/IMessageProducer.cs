namespace BookStoreAPI.BackgroundServices
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);

    }
}
