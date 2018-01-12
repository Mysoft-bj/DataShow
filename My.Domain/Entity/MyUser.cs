using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace My.Domain
{
    public partial class MyUser
    {
        public MyUser()
		{
		}

        /// <summary>
        ///ID(主键)
        /// </summary>
        [Key]
        public string id { get; set; }

        public string UserName { get; set; }

        public string UserCode { get; set; }

        public string MobilePhone { get; set; }


    }
}
