

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class User
    {
        public User()
        {
                    Users = new HashSet<User>();
                }

        
		[Key]
				public System.Int64? ID { get; set; }
				public System.String Login { get; set; }
				public System.String PwdHash { get; set; }
				public System.String Salt { get; set; }
				public System.String FirstName { get; set; }
				public System.String MiddleName { get; set; }
				public System.String LastName { get; set; }
				public System.String FriendlyName { get; set; }
				public System.DateTime CreatedDate { get; set; }
				public System.DateTime? ModifiedDate { get; set; }
		
		[ForeignKey("FK_User_User")]
				public System.Int64? ModifiedByID { get; set; }
			


                public virtual User ModifiedBy { get; set; }
        
                public virtual ICollection<User> Users { get; set; }
            }
}