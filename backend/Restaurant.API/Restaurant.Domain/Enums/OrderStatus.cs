namespace Restaurant.Domain.Enums;

public enum OrderStatus : byte
{
    Pending = 0,      // Order is newly created
    Processing = 1,   // Order is being processed
    Completed = 2,    // Order has been completed
    Canceled = 3      // Order was canceled
}
