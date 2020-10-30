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
            string abcrus = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string abceng = "abcdefghijklmnopqrstuvwxyz";

            StringBuilder text = new StringBuilder(textUC.Text);
            string key = KeyWord.Text;
            if (text.Length == 0) MessageBox.Show("Введите текст");
            else if (key.Length == 0) MessageBox.Show("Введите ключ");
            else if (operation.SelectedIndex == -1) MessageBox.Show("Выберите операцию");
            else
            {
                bool Lang = true; // true=rus falce=eng
                bool IsCorrect = true;
                int n = key.Length;
                for (int i = 0; i < n; ++i) if (!char.IsLetter(key[i])) IsCorrect = false; // проверка на отсутствие символов в ключе
                if (IsCorrect == true)
                {
                    key = key.ToLower();
                    //задаем язык ключа по первому символу
                    for (int i = 0; i < 33; ++i) if (key[0] == abcrus[i]) Lang = true;
                    for (int i = 0; i < 26; ++i) if (key[0] == abceng[i]) Lang = false;
                    // проверка на единственность языка
                    for (int i = 0; i < n; ++i)
                    {
                        for (int j = 0; j < 33; j++) if (key[i] == abcrus[j] && Lang == false) IsCorrect = false;
                        for (int j = 0; j < 26; j++) if (key[i] == abceng[j] && Lang == true) IsCorrect = false;
                    }
                }

                if (IsCorrect == true)
                {
                    int k = 0;
                    for (int i = 0; i < text.Length; ++i)
                    {
                        if (char.IsLetter(text[i]))
                        {
                            bool IsRus = true;
                            if (operation.SelectedIndex == 0) // шифровка
                            {
                                if (char.IsLower(text[i]))
                                {
                                    int letter;
                                    for (int j = 0; j < 33; ++j) if (text[i] == abcrus[j]) IsRus = true;
                                    for (int j = 0; j < 26; ++j) if (text[i] == abceng[j]) IsRus = false;
                                    if (IsRus == true && Lang == true)
                                    {
                                        for (letter = 0; abcrus[letter] != text[i]; ++letter) ; // сдвиг текущей буквы от 'а'(рус)
                                        for (int j = 0; abcrus[j] != key[k % n]; ++j) ++letter; // сдвиг после шифровки (может быть больше 33)
                                        letter %= 33;
                                        text[i] = abcrus[letter];
                                        ++k;
                                    }
                                    if (IsRus == false && Lang == false)
                                    {
                                        for (letter = 0; abceng[letter] != text[i]; ++letter) ; // сдвиг текущей буквы от 'a'(eng)
                                        for (int j = 0; abceng[j] != key[k % n]; ++j) ++letter; // сдвиг после шифровки (может быть больше 26)
                                        letter %= 26;
                                        text[i] = abceng[letter];
                                        ++k;
                                    }
                                }
                                else
                                {
                                    text[i] = char.ToLower(text[i]);
                                    int letter;
                                    if (IsRus == true && Lang == true)
                                    {
                                        for (letter = 0; abcrus[letter] != text[i]; ++letter) ; // сдвиг текущей буквы от 'а'(рус)
                                        for (int j = 0; abcrus[j] != key[k % n]; ++j) ++letter; // сдвиг после шифровки (может быть больше 33)
                                        letter %= 33;
                                        text[i] = abcrus[letter];
                                        ++k;
                                    }
                                    if (IsRus == false && Lang == false)
                                    {
                                        for (letter = 0; abceng[letter] != text[i]; ++letter) ; // сдвиг текущей буквы от 'a'(eng)
                                        for (int j = 0; abceng[j] != key[k % n]; ++j) ++letter; // сдвиг после шифровки (может быть больше 26)
                                        letter %= 26;
                                        text[i] = abceng[letter];
                                        ++k;
                                    }
                                    text[i] = char.ToUpper(text[i]);
                                }
                            }
                            else // дешифровка
                            {
                                if (char.IsLower(text[i]))
                                {
                                    int letter;
                                    for (int j = 0; j < 33; ++j) if (text[i] == abcrus[j]) IsRus = true;
                                    for (int j = 0; j < 26; ++j) if (text[i] == abceng[j]) IsRus = false;
                                    if (IsRus == true && Lang == true)
                                    {
                                        for (letter = 0; abcrus[letter] != text[i]; ++letter) ; // сдвиг текущей буквы от 'а'(рус)
                                        for (int j = 0; abcrus[j] != key[k % n]; ++j) --letter; // сдвиг после шифровки (может быть меньше 0)
                                        letter += 33;
                                        letter %= 33;
                                        text[i] = abcrus[letter];
                                        ++k;
                                    }
                                    if (IsRus == false && Lang == false)
                                    {
                                        for (letter = 0; abceng[letter] != text[i]; ++letter) ; // сдвиг текущей буквы от 'a'(eng)
                                        for (int j = 0; abceng[j] != key[k % n]; ++j) --letter; // сдвиг после шифровки (может быть меньше 0)
                                        letter += 26;
                                        letter %= 26;
                                        text[i] = abceng[letter];
                                        ++k;
                                    }
                                }
                                else
                                {
                                    text[i] = char.ToLower(text[i]);
                                    int letter;
                                    if (IsRus == true && Lang == true)
                                    {
                                        for (letter = 0; abcrus[letter] != text[i]; ++letter) ; // сдвиг текущей буквы от 'а'(рус)
                                        for (int j = 0; abcrus[j] != key[k % n]; ++j) --letter; // сдвиг после шифровки (может быть меньше 0)
                                        letter += 33;
                                        letter %= 33;
                                        text[i] = abcrus[letter];
                                        ++k;
                                    }
                                    if (IsRus == false && Lang == false)
                                    {
                                        for (letter = 0; abceng[letter] != text[i]; ++letter) ; // сдвиг текущей буквы от 'a'(eng)
                                        for (int j = 0; abceng[j] != key[k % n]; ++j) --letter; // сдвиг после шифровки (может быть меньше 0)
                                        letter += 26;
                                        letter %= 26;
                                        text[i] = abceng[letter];
                                        ++k;
                                    }
                                    text[i] = char.ToUpper(text[i]);
                                }
                            }
                        }
                    }

                    textC.Text = text.ToString();
                }
                else MessageBox.Show("Ключ должен содержать только символы кириллицы или только символы латиницы");
            }
        }
    }
}
