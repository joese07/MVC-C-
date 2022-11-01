using System;
namespace MVC.Base
{
    public class BaseModel
    {

        public string CreatedBy { get; set; }

        public DateTime CreateDate
        {
            get
            {
                return CreateDate;
            }
            set
            {
                CreateDate = DateTime.Now.ToLocalTime();
            }
        }
       
    }
}

