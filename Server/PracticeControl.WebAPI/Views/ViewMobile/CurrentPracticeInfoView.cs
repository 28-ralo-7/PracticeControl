namespace PracticeControl.WebAPI.Views.ViewMobile
{
    public class CurrentPracticeInfoView //Information current practice for mobile
    {
        public string PracticeName { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string PracticeLead { get; set; }
        public byte[] Photo { get; set; }
        public string Comment { get; set; }
        public bool IsPresent { get; set; }
    }
}
