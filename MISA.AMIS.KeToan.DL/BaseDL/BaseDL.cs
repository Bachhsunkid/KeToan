using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMIS.KeToan.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: TXBACH 17/02/2023
        public IEnumerable<T> GetAllRecords(string? keyword)
        {
            //Khởi tạo kết nối với DB Mysql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = String.Format(Procedure.GET_ALL, typeof(T).Name);

            if(keyword == null)
            {
                keyword = "";
            }

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(T).Name}Name", keyword);

            //Thực hiện gọi vào DB
            var records = mySqlConnection.Query<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            //Xử lí kết quả trả về
            return records;


            
        }

        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Create by: TXBACH 17/02/2023
        public IEnumerable<T> GetRecordsByKeyword(string keyword)
        {
            //Khởi tạo kết nối với DB Mysql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = $"Proc_{typeof(T).Name}_GetByKeyword";

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(T).Name}Name", keyword);

            //Thực hiện gọi vào DB
            var result = mySqlConnection.Query<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return result;
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
            //Khởi tạo kết nối đến DB
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = $"Proc_{typeof(T).Name}_InsertOne";

            var parameters = new DynamicParameters();
            Guid newRecordID = Guid.NewGuid();
            var props = record.GetType().GetProperties();

            parameters.Add($"v_{typeof(T).Name}ID", newRecordID); //Add ID bằng GUID mới
            //parameters.Add($"v_{typeof(T).Name}Code", newRecordCode()); // Add code bằng mã code lớn hơn mã code lớn nhất hiện thời 1 đơn vị

            for (int i = 1; i < props.Length; i++)
            {
                var value = props[i].GetValue(record);
                parameters.Add($"@v_{props[i].Name}", value);
            }

            //Thực hiện gọi vào DB
            int numberRowsAffected = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            if (numberRowsAffected > 0)
            {
                return newRecordID;
            }
            return Guid.Empty;
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
            //Khởi tạo kết nối đến DB
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = $"Proc_{typeof(T).Name}_UpdateOne";

            var parameters = new DynamicParameters();
            var props = record.GetType().GetProperties();

            parameters.Add($"v_{typeof(T).Name}ID", recordID); //Add ID bằng GUID truyền vào

            for (int i = 2; i < props.Length; i++)
            {
                if (props[i].Name == "CreatedBy")
                {
                    continue;
                }
                var value = props[i].GetValue(record);
                parameters.Add($"@v_{props[i].Name}", value);
            }

            //Thực hiện gọi vào DB
            int numberRowsAffected = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            if (numberRowsAffected > 0)
            {
                return recordID;
            }
            return Guid.Empty;
        }
        

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid DeleteRecord(Guid recordID)
        {
            //Khởi tạo kết nối với DB Mysql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh sql 
            string storedProcedureName = $"Proc_{typeof(T).Name}_DeleteOne";

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(T).Name}ID", recordID);

            //Thực hiện gọi vào DB
            var employee = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            if (employee > 0)
            {
                return recordID;
            }
            return Guid.Empty;
        }

        public Guid ConvertCodeToID(string employeeCode)
        {
            //Khởi tạo kết nối với DB Mysql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = $"Proc_{typeof(T).Name}_CodeToID";

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(T).Name}Code", employeeCode);

            //Thực hiện gọi vào DB
            var result = mySqlConnection.QueryFirstOrDefault(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).employeeID;

            return result;
        }
    }
}
