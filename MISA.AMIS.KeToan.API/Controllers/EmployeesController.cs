using System.Linq;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Enum;
using MISA.AMIS.KeToan.DL;
using MySqlConnector;


namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")] // "Users" sẽ được mapping với "[controller]"
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region Field
        private IEmployeeBL _employeeBL;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion

        /// <summary>
        /// API lay danh sach tat ca nhan vien
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employees = _employeeBL.GetAllEmployees();

                //Xử lí kết quả trả về
                if (employees != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employees);
                }

                return StatusCode(StatusCodes.Status200OK, new List<Employee>());
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
        /// Lay thong tin 1 nhan vien theo id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{employeeID}")]
        public IActionResult GetEmployeeByID([FromRoute] Guid employeeID)
        {
            try
            {
                var employee = _employeeBL.GetEmployeeByID(employeeID);

                //Xử lí kết quả trả về
                if (employee != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employee);
                }

                return StatusCode(StatusCodes.Status404NotFound);
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

        /// <summary>
        /// Them moi 1 nhan vien
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                var employeeInserted = _employeeBL.InsertEmployee(employee);

                //Xử lý kết quả trả về
                if (employeeInserted != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status201Created, employeeInserted);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 2,
                    DevMsg = "Database insert fail.",
                    UserMsg = "Thêm mới nhân viên thất bại.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
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
        /// Sua thong tin 1 nhan vien
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("{employeeID}")]
        public IActionResult UpdateEmployee(
            [FromRoute] Guid employeeID,
            [FromBody] Employee employee
            )
        {
            try
            {
                var employeeEditedID = _employeeBL.UpdateEmployee(employeeID, employee);

                //Xử lý kết quả trả về
                if (employeeEditedID != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status201Created, employeeEditedID);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 2,
                    DevMsg = "Database update fail.",
                    UserMsg = "Sửa thông tin nhân viên thất bại.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
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
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpDelete("{employeeID}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeID)
        {
            try
            {
                var employeeDeletedID = _employeeBL.DeleteEmployee(employeeID);

                //Xử lí kết quả trả về
                if (employeeDeletedID != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeDeletedID);
                }

                return StatusCode(StatusCodes.Status404NotFound);
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
                if (listEmployeeIDs.EmployeeIDs.Count > 0)
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


        //public string newEmployeeCode()
        //{
        //    string query = "select EmployeeCode from Employee e order by e.EmployeeCode desc";
        //    using (conn = new MySqlConnection(connString))
        //    {
        //        var code = conn.QueryFirstOrDefault<string>(query, null);
        //        int number = 1;
        //        if (!string.IsNullOrEmpty(code))
        //            number = Int32.Parse(($"{code}").Substring(2)) + 1;
        //        return "NV" + number;
        //    }
        //}

    }
}