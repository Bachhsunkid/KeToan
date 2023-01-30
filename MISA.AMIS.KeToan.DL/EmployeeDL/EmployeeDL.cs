using Dapper;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        /// <summary>
        /// API lấy danh sách nhân viên theo bộ lọc và phân trang
        /// </summary>
        /// <returns>Danh sách nhân viên đã lọc</returns>
        /// Create by: TXBACH 17/02/2023
        public PagingResult GetEmployeeByFilterAndPagging(string? keyword, string? sortBy, int limit = 10, int offset = 0)
        {
            //Khởi tạo kết nối với DB Mysql
            var connectionString = "Server=localhost;Port=3333;Database=misa.web09.tcdn.bach;Uid=root;Pwd=1234;";
            var mySqlConnection = new MySqlConnection(connectionString);
            //Chuẩn bị câu lệnh sql
            string storedProcedureName = "Proc_employee_GetPaging";
            string whereClause = $"EmployeeName like '%{keyword}%'";


            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("v_Offset", offset);
            parameters.Add("v_Limit", limit);
            parameters.Add("v_Sort", sortBy);
            parameters.Add("v_Where", whereClause);

            //Thực hiện gọi vào DB
            var employees = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            var records = employees.Read<Employee>().ToList();
            var count = employees.Read<int>().Single();

            return new PagingResult(records, count);
        }

        /// <summary>
        /// Xoa nhieu nhan vien
        /// </summary>
        /// <param name="listEmployee"></param>
        /// <returns></returns>
        public ListEmployeeID DeleteMultipleEmployees(ListEmployeeID listEmployee)
        {
            //Khởi tạo kết nối với DB Mysql
            var connectionString = "Server=localhost;Port=3333;Database=misa.web09.tcdn.bach;Uid=root;Pwd=1234;";
            var mySqlConnection = new MySqlConnection(connectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = "Proc_employee_DeleteMultiple";

            //Xử lý string đầu vào proc về dạng "A,B,C"
            string inputProc = String.Join(",", listEmployee.EmployeeIDs);

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("v_EmployeeIDs", inputProc);

            //Thực hiện gọi vào DB
            var employee = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            if (employee > 0)
            {
                return listEmployee;
            }
            return new ListEmployeeID();
        }
    }
}
