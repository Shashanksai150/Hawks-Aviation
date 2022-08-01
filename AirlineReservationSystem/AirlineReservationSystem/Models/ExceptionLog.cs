using System.ComponentModel.DataAnnotations;

namespace AirlineReservationSystem.Models
{
    public class ExceptionLog
    {
//create table ExceptionLog
//(
//    Id int not null primary key IDENTITY(11101,3),
//	[DataTime] DateTime default CURRENT_TIMESTAMP,
//	ErrorDescription varchar(max) default '',
//	Data varchar(100) default '',
//	StackTrace varchar(max) default ''
//)

        [Key]
        public int Id { get; set; }
        public DateTime DataTime { get; set; }
        public string ErrorDescription { get; set; }
        public string Data { get; set; }
        public string StackTrace { get; set; }
    }
}
