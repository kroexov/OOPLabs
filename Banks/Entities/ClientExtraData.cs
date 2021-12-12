namespace Banks.Entities
{
    public class ClientExtraData
    {
        public ClientExtraData(string address, string passportId)
        {
            Address = address;
            PassportID = passportId;
        }

        public string Address { get; }
        public string PassportID { get; }
        public bool IsSuspect => PassportID == null || Address == null;
    }
}