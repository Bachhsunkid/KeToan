using MimeKit.Text;
using MimeKit;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace MISA.AMIS.KeToan.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field
        private IEmployeeDL _employeeDL;

        private readonly IConfiguration _config;
        #endregion

        #region Constructor
        public EmployeeBL(IEmployeeDL employeeDL, IConfiguration config) : base(employeeDL)
        {
            _employeeDL = employeeDL;
            _config = config;
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

        /// <summary>
        /// Gửi email đến nhiều nhân viên
        /// </summary>
        /// Created by: Txbach 13/02/2023
        /// <returns>New employeecode</returns>
        public bool SendMail(EmailDTO request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailAddress").Value));
                foreach (var to in request.To)
                {
                    email.To.Add(MailboxAddress.Parse(to));
                }
                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailAddress").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Dispose();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
