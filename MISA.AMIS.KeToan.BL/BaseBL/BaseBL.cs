using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field
        private IBaseDL<T> _baseDL;
        #endregion

        #region Constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }
        #endregion

        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: TXBACH 17/02/2023
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <returns>Thông tin 1 bản ghi theo ID</returns>
        /// Create by: TXBACH 17/02/2023
        public T GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// Them moi 1 bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid InsertRecord(T record)
        {
            return _baseDL.InsertRecord(record);
        }

        /// <summary>
        /// Sua thong tin 1 bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid UpdateRecord(Guid recordID, T record)
        {
            return _baseDL.UpdateRecord(recordID, record);
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid DeleteRecord(Guid recordID)
        {
            return _baseDL.DeleteRecord(recordID);
        }
    }
}
