using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field
        private IEmployeeDL _employeeDL;
        #endregion

        #region Constructor
        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }
        #endregion

        /// <summary>
        /// API lấy danh sách nhân viên theo bộ lọc và phân trang
        /// </summary>
        /// <returns>Danh sách nhân viên đã lọc</returns>
        /// Create by: TXBACH 17/02/2023
        public PagingResult GetEmployeeByFilterAndPagging(string? keyword, string? sortBy, int limit, int offset)
        {
            return _employeeDL.GetEmployeeByFilterAndPagging(keyword, sortBy, limit, offset);
        }

        /// <summary>
        /// Xoa nhieu nhan vien
        /// </summary>
        /// <param name="listEmployee"></param>
        /// <returns></returns>
        public ListEmployeeID DeleteMultipleEmployees(ListEmployeeID listEmployee)
        {
            return _employeeDL.DeleteMultipleEmployees(listEmployee);
        }
        /// <summary>
        /// Tạo mã code lớn hơn 1 so với hiện tại (để không lặp)
        /// </summary>
        /// Created by: Txbach 13/02/2023
        /// <returns>New employeecode</returns>
        public string GetNewEmployeeCode()
        {
            return _employeeDL.GetNewEmployeeCode();
        }
    }
}
