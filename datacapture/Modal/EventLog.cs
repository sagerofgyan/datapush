using System;
namespace datacapture.Modal
{

    public class EventLog
    {


        public class RequestEventLog
        {

            public string ModuleName { get; set; }
            public string Action { get; set; }
            public string Description { get; set; }
            public int UserId { get; set; }
            public string CreatedBy { get; set; }
        }

        public class RequestEventLogFilter
        {

            public string ModuleName { get; set; }
            public string Action { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }

        }

        public class ResponseEventLog
        {
            public int EventLogId { get; set; }
            public string ModuleName { get; set; }
            public string Action { get; set; }
            public string Description { get; set; }
            public int UserId { get; set; }
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
        }

        public class RequestRecordlock
        {
            public int RecordId { get; set; }
            public int UserId { get; set; }
            //public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            //public string SessionId { get; set; }
            public int MenuId { get; set; }
            public string ModuleName { get; set; }
            public string Activity { get; set; }


        }

        public class GetRequestRecordlock
        {

            public int RecordId { get; set; }
            public int MenuId { get; set; }
            public string ModuleName { get; set; }



        }


        public class RequestUpdateRecordlock
        {
            public int RecordlockId { get; set; }
            public int RecordId { get; set; }
            public bool IsActive { get; set; }

        }

        public class ResponseRecordlock
        {
            public int RecordlockId { get; set; }
            public int RecordId { get; set; }
            public int UserId { get; set; }
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            //public string SessionId { get; set; }
            public int MenuId { get; set; }
            public string ModuleName { get; set; }
            public string Activity { get; set; }
            public bool IsActive { get; set; }

        }
    }
}
