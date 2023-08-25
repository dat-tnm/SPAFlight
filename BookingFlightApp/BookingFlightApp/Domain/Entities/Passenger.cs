namespace BookingFlightApp.Domain.Entities
{
    public record Passenger(string Email,
        string Password,
        string Name,
        bool Gender);
}
