namespace HalloweenApp.Domain.Events
{
    public class VisitorAlreadySignedInException : Exception
    {
        public VisitorAlreadySignedInException(string message) : base(message) { }
    }
}
