namespace OnlineStore.API.Helpers
{
	public class PaginationResponse<T>
	{

		public int PageSize {  get; set; }
		public int PageIndex { get; set; }

		// public int Conut { get; set; }
		public IReadOnlyList<T> Data { get; set; }

        public PaginationResponse(int pgSize , int pgIndex , IReadOnlyList<T> data )
        {
            PageSize = pgSize;
            PageIndex = pgIndex;
            Data = data;
        }

    }
}
