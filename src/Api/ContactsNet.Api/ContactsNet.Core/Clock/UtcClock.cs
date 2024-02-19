namespace ContactsNet.Core.Clock;

internal class UtcClock : IUtcClock
{
    public DateTime CurrentDate() => DateTime.Now;
}