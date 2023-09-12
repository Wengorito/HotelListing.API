namespace HotelListing.API.Models
{
    public class QueryParameters
    {
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 15;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
    }
}
