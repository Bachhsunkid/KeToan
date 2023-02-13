using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class EmailDTO
    {
        /// <summary>
        /// Danh sách những email muốn gửi
        /// </summary>
        public List<string> To { get; set; }

        /// <summary>
        /// Tiêu đề email
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Nội dung email
        /// </summary>
        public string Body { get; set; }
    }
}
