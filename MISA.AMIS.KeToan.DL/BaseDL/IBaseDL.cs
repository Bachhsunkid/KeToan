using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: TXBACH 17/02/2023
        public IEnumerable<T> GetAllRecords(string? keyword);

        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: TXBACH 17/02/2023
        public IEnumerable<T> GetRecordsByKeyword(string keyword);

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <returns>Thông tin 1 bản ghi theo ID</returns>
        /// Create by: TXBACH 17/02/2023
        public T GetRecordByID(Guid recordID);

        /// <summary>
        /// Them moi 1 bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid InsertRecord(T record);

        /// <summary>
        /// Sua thong tin 1 bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid UpdateRecord(Guid recordID, T record);

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid DeleteRecord(Guid recordID);

        public Guid ConvertCodeToID(string employeeCode);
    }
}
