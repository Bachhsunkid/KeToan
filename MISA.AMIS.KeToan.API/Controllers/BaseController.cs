using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Enum;
using MISA.AMIS.KeToan.Common;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")] // "Users" sẽ được mapping với "[controller]"
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Field
        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        /// <summary>
        /// API lay danh sach tat ca bản ghi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllRecords([FromQuery] string? keyword)
        {
            try
            {
                var records = _baseBL.GetAllRecords(keyword);

                //Xử lí kết quả trả về
                if (records != null)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }

                return StatusCode(StatusCodes.Status200OK, new List<T>());
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

        //[HttpGet]
        //public IActionResult GetRecordsByKeyword([FromQuery] string? keyword)
        //{
        //    try
        //    {
        //        var records = _baseBL.GetRecordsByKeyword(keyword);

        //        //Xử lí kết quả trả về
        //        if (records != null)
        //        {
        //            return StatusCode(StatusCodes.Status200OK, records);
        //        }

        //        return StatusCode(StatusCodes.Status200OK, new List<T>());
        //    }
        //    //Try catch Exception
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, new
        //        {
        //            ErrorCode = AMISKeToanErrorCode.Exception,
        //            DevMsg = Resource.DevMsg_Exception,
        //            UserMsg = Resource.UserMsg_Exception,
        //            MoreInfo = Resource.MoreInfor_Exception,
        //            TraceId = HttpContext.TraceIdentifier
        //        });
        //    }
        //}

        /// <summary>
        /// Lay thong tin 1 bản ghi theo id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {
            try
            {
                var record = _baseBL.GetRecordByID(recordID);

                //Xử lí kết quả trả về
                if (record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
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
        /// Them moi 1 ban ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                var result = _baseBL.InsertRecord(record);

                //Xử lý kết quả trả về
                if (result != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status201Created, result);
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
        /// Sua thong tin 1 bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpPut("{recordID}")]
        public IActionResult UpdateRecord(
            [FromRoute] Guid recordID,
            [FromBody] T record
            )
        {
            try
            {
                var employeeEditedID = _baseBL.UpdateRecord(recordID, record);

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
        /// <param name="recordID"></param>
        /// <returns></returns>
        [HttpDelete("{recordID}")]
        public IActionResult DeleteEmployee([FromRoute] Guid recordID)
        {
            try
            {
                var result = _baseBL.DeleteRecord(recordID);

                //Xử lí kết quả trả về
                if (result != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
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
    }
}
