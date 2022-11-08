using Phonebook.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Shared.Messages
{
    public class CreateReportMessageCommand
    {
        public string ReportId { get; set; }
        public ReportTypes ReportType { get; set; }
    }
}
