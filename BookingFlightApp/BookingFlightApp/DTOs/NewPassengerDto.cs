using System.ComponentModel.DataAnnotations;

namespace BookingFlightApp.DTOs
{
    public record NewPassengerDto(
        [Required][EmailAddress][StringLength(100, MinimumLength = 6)] string Email,

        [Required][MinLength(2)][MaxLength(35)] string Password,

        [Required][MinLength(2)][MaxLength(35)] string Name,

        [Required] bool Gender);
}
