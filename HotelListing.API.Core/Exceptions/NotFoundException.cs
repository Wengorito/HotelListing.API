namespace HotelListing.API.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        // TODO teraz zmienić na 
        public NotFoundException(string name, object key) : base($"{name} with id ({key}) was not found")
        {

        }
    }
}
