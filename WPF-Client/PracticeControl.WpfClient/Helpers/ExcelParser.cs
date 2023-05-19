using ClosedXML.Excel;
using PracticeControl.WpfClient.Model.ViewCreate;
using PracticeControl.WpfClient.Windows.Pages;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.Helpers
{
    public class ExcelParser
    {
        private static List<CreateStudentView> Students { get; set; } = new List<CreateStudentView>();

        public static List<CreateStudentView> LoadFile(string filePath)
        {
            using (var book = new XLWorkbook(filePath))
            {
                var sheet = book.Worksheet("Студенты");

                var student = new CreateStudentView();

                int counterA = 1;
                int counterB = 1;
                int counterC = 1;
                int counterD = 1;
                int counterE = 1;

                while (true)
                {
                    string cellAID = "A" + counterA++;
                    string cellBID = "B" + counterB++;
                    string cellCID = "C" + counterC++;
                    string cellDID = "D" + counterD++;
                    string cellEID = "E" + counterE++;

                    var cellLastName = sheet.Cell(cellAID);
                    var cellFirstName = sheet.Cell(cellBID);
                    var cellMiddleName = sheet.Cell(cellCID);
                    var cellLogin = sheet.Cell(cellDID);
                    var cellPassword = sheet.Cell(cellEID);

                    if (!cellLastName.Value.IsBlank &&
                        !cellFirstName.Value.IsBlank &&
                        !cellLogin.Value.IsBlank &&
                        !cellPassword.Value.IsBlank)
                    {


                        Students.Add(new CreateStudentView
                        {
                            FirstName = cellFirstName.Value.ToString(),
                            LastName = cellLastName.Value.ToString(),
                            MiddleName = cellMiddleName.Value.ToString(),
                            Login = cellLogin.Value.ToString(),
                            Password = cellPassword.Value.ToString(),
                        });
                    }
                    else
                    {
                        break;
                    }
                }

                return Students;
            }

        }
    }
}
