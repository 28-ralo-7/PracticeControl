using PracticeControl.WebAPI.Database;
using PracticeControl.WebAPI.Views.View;
using PracticeControl.WebAPI.Views.ViewCreate;

namespace PracticeControl.WebAPI.Converters
{
    public static class PracticeConverter
    {
        //Из бд в View
        public static PracticeView ConvertToPracticeView(Practice practice)
        {
            return new PracticeView
            {
                Id = Convert.ToInt32(practice.Id),
                Abbreviation = practice.Abbreviation,
                PracticeModule = practice.Practicemodule,
                Specialty = practice.Specialty 
            };
        }

        //Из View в бд
        public static Practice ConvertToPractice(PracticeView practiceView)
        {
            return new Practice
            {
                Id = practiceView.Id,
                Abbreviation = practiceView.Abbreviation,
                Practicemodule = practiceView.PracticeModule,
                Specialty = practiceView.Specialty
            };
        }


        //Из CreateView в бд
        public static Practice ConvertToPractice(CreatePracticeView practiceView)
        {
            return new Practice
            {
                Abbreviation = practiceView.Abbreviation,
                Practicemodule = practiceView.PracticeModule,
                Specialty = practiceView.Specialty
            };
        }
    }
}
