using BookingFlightApp.Error;
using BookingFlightApp.ReadModels;

namespace BookingFlightApp.Domain.Entities
{
    public record Flight
    {
        public Guid Id { get; set; }
        public string Airline { get; set; }
        public string Price { get; set; }
        public TimePlace Departure { get; set; }
        public TimePlace Arrival { get; set; }
        public int RemainingNumberOfSeats { get; set; }

        public IList<Book> Books = new List<Book>();

        public Flight()
        {
            
        }

        public Flight(Guid id,
            string airline,
            string price,
            TimePlace departure,
            TimePlace arrival,
            int remainingNumberOfSeats
            )
        {
            Id = id;
            Airline = airline;
            Price = price;
            Departure = departure;
            Arrival = arrival;
            RemainingNumberOfSeats = remainingNumberOfSeats;
        }


        public object? MakingBook(string passengerEmail, byte numberOfSeats)
        {
            var flight = this;
            if (flight.RemainingNumberOfSeats < numberOfSeats)
            {
                return new OverBooking();
            }

            var book = new Book(passengerEmail, numberOfSeats);

            flight.Books.Add(book);
            flight.RemainingNumberOfSeats -= numberOfSeats;

            return null;
        }
    }
}
