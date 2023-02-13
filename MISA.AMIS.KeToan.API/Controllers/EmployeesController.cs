using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Enum;
using MISA.AMIS.KeToan.DL;
using MySqlConnector;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        #region Field
        private IEmployeeBL _employeeBL;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion

        /// <summary>
        /// Tạo mã code lớn hơn 1 so với hiện tại (để không lặp)
        /// </summary>
        /// Created by: Txbach 13/02/2023
        /// <returns>New employeecode</returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var newEmployeeCode = _employeeBL.GetNewEmployeeCode();

                //Xử lí kết quả trả về //Opps: dung employee > 0 trả về errorcode 500 dù DB đã chạy đúng
                if (newEmployeeCode != null)
                {
                    return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
                }

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //Try catch Exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = AMISKeToanErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = Resource.MoreInfor_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API lấy danh sách nhân viên theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="departmentID">ID phòng ban muốn lọc</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="offset">Vị trí bản ghi bắt đầu lấy</param>
        /// <returns>Danh sách nhân viên đã lọc</returns>
        /// Create by: TXBACH 17/02/2023
        [HttpGet("filter")]
        public IActionResult GetEmployeeByFilterAndPagging(
            [FromQuery] string? keyword,
            [FromQuery] string? sortBy,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0
            )
        {
            try
            {
                var pagingResult = _employeeBL.GetEmployeeByFilterAndPagging(keyword, sortBy, limit, offset);

                //Xử lí kết quả trả về
                if (pagingResult._TotalCount > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, pagingResult);
                }

                return StatusCode(StatusCodes.Status200OK, new PagingResult());
            }
            //Try catch Exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catch an exception",
                    UserMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        [HttpPost("SendMail")]
        public IActionResult SendMail([FromBody] EmailDTO request)
        {
            try
            {
                var isSent = _employeeBL.SendMail(request);

                if (isSent)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = AMISKeToanErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = Resource.MoreInfor_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// Xoa nhieu nhan vien
        /// </summary>
        /// <param name="listEmployee"></param>
        /// <returns></returns>
        [HttpDelete("DeleteBatch")]
        public IActionResult DeleteMultipleEmployees([FromBody] ListEmployeeID listEmployee)
        {
            try
            {
                var listEmployeeIDs = _employeeBL.DeleteMultipleEmployees(listEmployee);

                //Xử lí kết quả trả về //Opps: dung employee > 0 trả về errorcode 500 dù DB đã chạy đúng
                if (listEmployee.EmployeeIDs != null)
                {
                    return StatusCode(StatusCodes.Status200OK, listEmployeeIDs.EmployeeIDs);
                }

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //Try catch Exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = AMISKeToanErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = Resource.MoreInfor_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }
    }
}