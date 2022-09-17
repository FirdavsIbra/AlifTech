namespace TaskOfAlifTech.Domain.Configurations
{
    public class PaginationParams
    {
        private const uint maxPageSize = 20;
        public PaginationParams()
        {

        }

        private uint pageSize;

        public uint PageSize
        {   
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : value; }
        }

        private uint pageIndex;

        public uint PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
    }
}
