namespace ReviewApp.Configuration
{
    public interface IConfigurationContext
    {
        string FacebookVerificationToken { get; }

        string FacebookMessengerSendAPIAccessToken { get; }

        string FacebookAppSecret { get; }
    }
}
