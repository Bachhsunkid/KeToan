using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class EmployeeInserted
    {
        #region prop
        public Guid _NewEmployeeID { get; set; }

        public int _NumberRowsAffected { get; set; }
        #endregion

        #region Constructor
        public EmployeeInserted() { }

        public EmployeeInserted(Guid employeeID, int numberRowsAffected)
        {
            _EmployeeID = employeeID;
            _NumberRowsAffected = numberRowsAffected;
        } 
        #endregion
    }
}
