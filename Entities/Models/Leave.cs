using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
   public class Leave
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public string Description { get; set; }

        public virtual LeaveTypeMaster  LeaveType { get; set; }
        [ForeignKey("FK_Leave_LeaveType")]
        public int LeaveTypeId { get; set; }

     
        public int Employee_UserId { get; set; }

        [ForeignKey("Employee_UserId")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("FK_Leave_LeaveStatus")]
        public int LeaveStatusId { get; set; }
        public LeaveStatusMaster LeaveStatus { get; set; }

    }
}
