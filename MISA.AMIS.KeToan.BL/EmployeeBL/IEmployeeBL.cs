using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
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
        /// Xoa nhieu nhan vien
        /// </summary>
        /// <param name="listEmployee"></param>
        /// <returns></returns>
        public ListEmployeeID DeleteMultipleEmployees(ListEmployeeID listEmployee);

        /// <summary>
        /// Tạo mã code lớn hơn 1 so với hiện tại (để không lặp)
        /// </summary>
        /// Created by: Txbach 13/02/2023
        /// <returns>New employeecode</returns>
        public string GetNewEmployeeCode();

        /// <summary>
        /// Gửi email đến nhiều nhân viên
        /// </summary>
        /// Created by: Txbach 13/02/2023
        /// <returns>New employeecode</returns>
        public bool SendMail(EmailDTO request);
    }
}
