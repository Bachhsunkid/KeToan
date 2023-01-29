using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MISA.AMIS.KeToan.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: TXBACH 17/02/2023
        public IEnumerable<T> GetAllRecords()
        {
            //Khởi tạo kết nối với DB Mysql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = String.Format(Procedure.GET_ALL, typeof(T).Name);

            //Thực hiện gọi vào DB
            var records = mySqlConnection.Query<T>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

            //Xử lí kết quả trả về
            return records;
        }

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <returns>Thông tin 1 bản ghi theo ID</returns>
        /// Create by: TXBACH 17/02/2023
        public T GetRecordByID(Guid recordID)
        {
            //Khởi tạo kết nối với DB Mysql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = $"Proc_{typeof(T).Name}_GetByID";

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(T).Name}ID", recordID);

            //Thực hiện gọi vào DB
            var result = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }
        

        /// <summary>
        /// Them moi 1 bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid InsertRecord(T record)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid DeleteRecord(Guid recordID)
        {
            throw new NotImplementedException();
        }
        
    }
}
