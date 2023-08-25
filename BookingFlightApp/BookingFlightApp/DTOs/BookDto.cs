using System.ComponentModel.DataAnnotations;

namespace BookingFlightApp.DTOs
{
    public record BookDto(
        [Required] Guid FlightId,

        [Required][EmailAddress][StringLength(100, MinimumLength = 6)] string PassengerEmail,

        [Required][Range(1, 254)] byte NumberOfSeats);
}
