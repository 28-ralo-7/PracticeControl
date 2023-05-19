using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.blanks;

namespace PracticeControl.WebAPI.Converters
{
    public static class AttendanceConverter
    {
        public static AttendanceView ConvertToView(Attendance attendance)
        {
            if (attendance!=null)
            {
                return new AttendanceView
                {
                    AttendanceID = Convert.ToInt32(attendance.Id),
                    Date = Convert.ToString(attendance.Date),
                    IsPresent = attendance.Ispresent
                };
            }
            return null;
        }
    }
}
