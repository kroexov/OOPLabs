namespace Banks.Entities
{
    public interface IClientBuilder
    {
        public abstract void SetName(string name);
        public abstract void SetAddress(string address);
        public abstract void SetPassport(string passport);
        public abstract Client GetResult();
    }
}