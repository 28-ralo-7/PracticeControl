using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeControl.WpfClient.Helpers
{
    public static class ValidationTools
    {
        //Зачистка пробелов
        public static void ClearWhiteSpace(object sender)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            string text = textBox.Text;
            text = text.Replace(" ", string.Empty);
            textBox.Text = text; 
            textBox.CaretIndex = text.Length;

        }

        //Пропуск только для букв
        public static void AllowOnlyCharacter(System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0)) // запрещаем ввод любых символов кроме букв
            {
                e.Text.Replace(" ", string.Empty);
                e.Handled = true;
            }
        }

        //Пропуск только для цифр
        public static void AllowOnlyNumber(System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsNumber(e.Text, 0)) // запрещаем ввод любых символов кроме цифр
            {
                e.Text.Replace(" ", string.Empty);
                e.Handled = true;
            }
        }
    }
}
