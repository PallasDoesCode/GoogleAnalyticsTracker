namespace GoogleAnalyticsTracker
{
    public interface IReporter
    {
        void Track(string url);
        void TrackAsync(string url);
    }
}