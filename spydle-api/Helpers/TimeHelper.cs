namespace spydle_api.Helpers
{
    public static class TimeHelper
    {
        public static long GetTimeSinceUnixEpochInMilisecondsFromDateTime(DateTime dateTime)
        {
            return (long)dateTime.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
        }
    }
}
