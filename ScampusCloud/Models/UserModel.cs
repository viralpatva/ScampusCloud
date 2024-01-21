using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScampusCloud.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string StaffId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Guid ParentUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public string DeviceId { get; set; }
    }
}