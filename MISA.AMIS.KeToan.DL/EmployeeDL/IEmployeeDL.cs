using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public interface IEmployeeDL
    {
        /// <summary>
        /// Lấy tất cả danh sách nhân viên
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// Create by: TXBACH 17/02/2023
        public IEnumerable<dynamic> GetAllEmployees();

        /// <summary>
        /// Lấy thông tin 1 nhân viên theo ID
        /// </summary>
        /// <returns>Thông tin 1 nhân viên theo ID</returns>
        /// Create by: TXBACH 17/02/2023
        public Employee GetEmployeeByID(Guid employeeID);

        /// <summary>
        /// API lấy danh sách nhân viên theo bộ lọc và phân trang
        /// </summary>
        /// <returns>Danh sách nhân viên đã lọc</returns>
        /// Create by: TXBACH 17/02/2023
        public PagingResult GetEmployeeByFilterAndPagging(string? keyword,
            string? sortBy,
            int limit = 10,
            int offset = 0
        );

        /// <summary>
        /// Them moi 1 nhan vien
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid InsertEmployee(Employee employee);


        /// <summary>
        /// Sua thong tin 1 nhan vien
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid UpdateEmployee(Guid employeeID, Employee employee);

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// Create by: TXBACH 17/02/2023
        public Guid DeleteEmployee(Guid employeeID);

        /// <summary>
        /// Xoa nhieu nhan vien
        /// </summary>
        /// <param name="listEmployee"></param>
        /// <returns></returns>
        public ListEmployeeID DeleteMultipleEmployees(ListEmployeeID listEmployee);
    }
}
