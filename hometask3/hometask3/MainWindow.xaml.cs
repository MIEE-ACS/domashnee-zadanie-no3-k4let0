using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hometask3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder text = new StringBuilder(textUC.Text);
            StringBuilder key = new StringBuilder (KeyWord.Text);
            if (text.Length == 0) MessageBox.Show("Введите шифруемый текст");
            else if (key.Length == 0) MessageBox.Show("Введите ключ");
            else
            {
                int n = key.Length;
                bool IsCorrect = true;
                bool Lang = true; // true=rus false=eng
                for (int i = 0; i < n; ++i) if (!char.IsLetter(key[i])) IsCorrect = false; // проверка на отсутствие символов в ключе
                if (IsCorrect == true) // проверка на единственность языка ключа
                {
                    Lang = ((key[0] <= 'я' && key[0] >= 'а') || (key[0] >= 'А' && key[0] <= 'Я'));
                    for(int i = 0; i < n; ++i)
                    {
                        if (((key[i] <= 'z' && key[i] >= 'a') || (key[i] >= 'A' && key[i] <= 'Z')) && Lang == true) IsCorrect = false;
                        if (((key[0] <= 'я' && key[0] >= 'а') || (key[0] >= 'А' && key[0] <= 'Я')) && Lang==false) IsCorrect = false;
                    }
                } 
                if (IsCorrect == false) MessageBox.Show("Ключ должен содержать только символы кириллицы или только символы латиницы");
                else
                {
                    for (int i = 0; i < n; ++i) char.ToLower(key[i]); 
                    int k = 0; // счетчик букв
                    for (int i = 0; i < text.Length; ++i)
                    {
                        if (char.IsLetter(text[i]))
                        {
                            if (Lang == true && ((text[0] <= 'я' && text[0] >= 'а') || (text[0] >= 'А' && text[0] <= 'Я')))
                            {
                                if (char.IsLower(text[i]))
                                {
                                    int letter = text[i] - 'а'; //сдвиг текущей буквы от 'а'(рус)
                                    letter += key[k % n] - 'а'; //сдвиг текущей буквы после шифровки (может быть больше 33)
                                    letter %= 33; //сдвиг текущей буквы после шифровки
                                    text[i] = 'а';
                                    for (int j = 0; j < letter; ++j) ++text[i];
                                    ++k;
                                }
                                else
                                {
                                    int letter = text[i] - 'А'; //сдвиг текущей буквы от 'А'(рус)
                                    letter += key[k % n] - 'а'; //сдвиг текущей буквы после шифровки (может быть больше 33)
                                    letter %= 33; //сдвиг текущей буквы после шифровки
                                    text[i] = 'А';
                                    for (int j = 0; j < letter; ++j) ++text[i];
                                    ++k;
                                }
                            }
                            if (Lang==false && ((text[i] <= 'z' && text[i] >= 'a') || (text[i] >= 'A' && text[i] <= 'Z')))
                            { 
                                if (char.IsLower(text[i]))
                                {
                                    int letter = text[i] - 'a'; //сдвиг текущей буквы от 'a'(eng)
                                    letter += key[k % n] - 'a'; //сдвиг текущей буквы после шифровки (может быть больше 26)
                                    letter %= 26; //сдвиг текущей буквы после шифровки
                                    text[i] = 'a';
                                    for (int j = 0; j < letter; ++j) ++text[i];
                                    ++k;
                                }
                                else
                                {
                                    int letter = text[i] - 'A'; //сдвиг текущей буквы от 'A'(eng)
                                    letter += key[k % n] - 'a'; //сдвиг текущей буквы после шифровки (может быть больше 26)
                                    letter %= 26; //сдвиг текущей буквы после шифровки
                                    text[i] = 'A';
                                    for (int j = 0; j < letter; ++j) ++text[i];
                                    ++k;
                                }
                            }
                        }
                    }
                    textC.Text = text.ToString();
                }
            }
        }
    }
}
