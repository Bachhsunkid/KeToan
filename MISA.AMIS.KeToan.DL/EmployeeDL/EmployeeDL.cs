using Dapper;
using MISA.AMIS.KeToan.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public class EmployeeDL : IEmployeeDL
    {
        /// <summary>
        /// Lấy tất cả danh sách nhân viên
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// Create by: TXBACH 17/02/2023
        public IEnumerable<dynamic> GetAllEmployees()
        {
            //Khởi tạo kết nối với DB Mysql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = "Proc_employee_GetAll";

            //Thực hiện gọi vào DB
            var employees = mySqlConnection.Query(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

            //Xử lí kết quả trả về
            return employees;
        }

        /// <summary>
        /// Lấy thông tin 1 nhân viên theo ID
        /// </summary>
        /// <returns>Thông tin 1 nhân viên theo ID</returns>
        /// Create by: TXBACH 17/02/2023
        public Employee GetEmployeeByID(Guid employeeID)
        {
            //Khởi tạo kết nối với DB Mysql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            //Chuẩn bị câu lệnh sql
            string storedProcedureName = "Proc_employee_GetByID";

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("v_EmployeeID", employeeID);

            //Thực hiện gọi vào DB
            var employee = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return employee;
        }

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
        /// Them moi 1 nhan vien
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// /// Create by: TXBACH 17/02/2023
        public Guid InsertEmployee(Employee employee)
        {
            //Khởi tạo kết nối đến DB
            var connectionString = "Server=localhost;Port=3333;Database=misa.web09.tcdn.bach;Uid=root;Pwd=1234;";
            var mySqlConnection = new MySqlConnection(connectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = "Proc_employee_InsertOne";

            var parameters = new DynamicParameters();
            var newEmployeeID = Guid.NewGuid();

            parameters.Add("v_EmployeeID", newEmployeeID);
            parameters.Add("v_EmployeeCode", employee.EmployeeCode);
            parameters.Add("v_EmployeeName", employee.EmployeeName);
            parameters.Add("v_DepartmentID", employee.DepartmentID);
            parameters.Add("v_PositionName", employee.PositionName);
            parameters.Add("v_DateOfBirth", employee.DateOfBirth);
            parameters.Add("v_Gender", employee.Gender);
            parameters.Add("v_IdentityNumber", employee.IdentityNumber);
            parameters.Add("v_IdentityDate", employee.IdentityDate);
            parameters.Add("v_IdentityPlace", employee.IdentityPlace);
            parameters.Add("v_Address", employee.Address);
            parameters.Add("v_TelephoneNumber", employee.TelephoneNumber);
            parameters.Add("v_PhoneNumber", employee.PhoneNumber);
            parameters.Add("v_Email", employee.Email);
            parameters.Add("v_BankAccountNumber", employee.BankAccountNumber);
            parameters.Add("v_BankName", employee.BankName);
            parameters.Add("v_BankBranchName", employee.BankBranchName);
            parameters.Add("v_CreatedBy", employee.CreatedBy);


            //Thực hiện gọi vào DB
            int numberRowsAffected = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            if (numberRowsAffected > 0) 
            {
                return newEmployeeID;
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Sua thong tin 1 nhan vien
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid UpdateEmployee(Guid employeeID, Employee employee)
        {
            //Khởi tạo kết nối đến DB
            var connectionString = "Server=localhost;Port=3333;Database=misa.web09.tcdn.bach;Uid=root;Pwd=1234;";
            var mySqlConnection = new MySqlConnection(connectionString);

            //Chuẩn bị câu lệnh sql
            string storedProcedureName = "Proc_employee_UpdateOne";

            var parameters = new DynamicParameters();

            parameters.Add("v_EmployeeID", employeeID);
            parameters.Add("v_EmployeeName", employee.EmployeeName);
            parameters.Add("v_DepartmentID", employee.DepartmentID);
            parameters.Add("v_PositionName", employee.PositionName);
            parameters.Add("v_DateOfBirth", employee.DateOfBirth);
            parameters.Add("v_Gender", employee.Gender);
            parameters.Add("v_IdentityNumber", employee.IdentityNumber);
            parameters.Add("v_IdentityDate", employee.IdentityDate);
            parameters.Add("v_IdentityPlace", employee.IdentityPlace);
            parameters.Add("v_Address", employee.Address);
            parameters.Add("v_TelephoneNumber", employee.TelephoneNumber);
            parameters.Add("v_PhoneNumber", employee.PhoneNumber);
            parameters.Add("v_Email", employee.Email);
            parameters.Add("v_BankAccountNumber", employee.BankAccountNumber);
            parameters.Add("v_BankName", employee.BankName);
            parameters.Add("v_BankBranchName", employee.BankBranchName);
            parameters.Add("v_CreatedBy", employee.CreatedBy);
            parameters.Add("v_CreatedDate", employee.CreatedDate);
            parameters.Add("v_ModifiedBy", employee.ModifiedBy);
            parameters.Add("v_ModifiedDate", employee.ModifiedDate);


            //Thực hiện gọi vào DB
            int numberRowsAffected = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            if (numberRowsAffected > 0)
            {
                return employeeID;
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid DeleteEmployee(Guid employeeID)
        {
            //Khởi tạo kết nối với DB Mysql
            var connectionString = "Server=localhost;Port=3333;Database=misa.web09.tcdn.bach;Uid=root;Pwd=1234;";
            var mySqlConnection = new MySqlConnection(connectionString);
            //Chuẩn bị câu lệnh sql
            string storedProcedureName = "Proc_employee_DeleteOne";

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("v_EmployeeID", employeeID);

            //Thực hiện gọi vào DB
            var employee = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            if(employee > 0)
            {
                return employeeID;
            }
            return Guid.Empty;
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
