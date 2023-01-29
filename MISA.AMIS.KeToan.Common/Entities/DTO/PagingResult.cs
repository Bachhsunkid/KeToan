namespace MISA.AMIS.KeToan.Common.Entities
{
    public class PagingResult
    {
        /// <summary>
        /// Kết quả trả về của API lấy danh sách nhân viên và phân trang
        /// </summary>
        /// 

        public List<Employee> _Data { get; set; }

        public long _TotalCount { get; set; }


        public PagingResult(List<Employee> Data, long TotalCount)
        {
            _Data = Data;
            _TotalCount = TotalCount;
        }

        public PagingResult()
        {
            _Data = null;
            _TotalCount = 0;
        }
    }
}
