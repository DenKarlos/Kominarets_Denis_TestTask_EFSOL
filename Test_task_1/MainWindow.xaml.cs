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
using System.Globalization;

namespace Test_task_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int fnt = 30; // размер шрифта
        public MainWindow()
        {
            InitializeComponent();
        }
               
        private void TxBx1_KeyDown(object sender, KeyEventArgs e)
        {
            // блокирует ввод недопустимых символов, но всё равно некоторые вводятся!
            // как сделать чтобы не вводились - пока не знаю
            Key ch = e.Key;
            if ((ch < Key.D0 || ch > Key.D9) && (ch != Key.OemComma || txBx1.Text.Contains(",")) && ch != Key.Back)
                e.Handled = true; 
        }

        private void TxBx1_TextChanged(object sender, TextChangedEventArgs e)
        {
            // если в полях находятся значения, которые невозможно преобразовать в числа,
            // то кнопка, производящая вычисления, блокируется
            double x;
            if (txBx1 != null && txBx2 != null)
            {
                bool isParse1 = double.TryParse(txBx1.Text, out x);
                bool isParse2 = double.TryParse(txBx2.Text, out x);
                if (btnCalc != null)
                    if (isParse1 && isParse2)
                        btnCalc.IsEnabled = true;
                    else
                        btnCalc.IsEnabled = false;
            }
            
        }

        private void TxBx2_TextChanged(object sender, TextChangedEventArgs e)
        {
            // если в полях находятся значения, которые невозможно преобразовать в числа,
            // то кнопка, производящая вычисления, блокируется
            double x;
            if (txBx1 != null && txBx2 != null)
            {
                bool isParse1 = double.TryParse(txBx1.Text, out x);
                bool isParse2 = double.TryParse(txBx2.Text, out x);
                if (btnCalc != null)
                    if (isParse1 && isParse2)
                        btnCalc.IsEnabled = true;
                    else
                        btnCalc.IsEnabled = false;
            }
        }

        private void TxBx2_KeyDown(object sender, KeyEventArgs e)
        {
            // блокирует ввод недопустимых символов, но всё равно некоторые вводятся!
            // как сделать чтобы не вводились - пока не знаю
            var ch = e.Key;
            if (ch < Key.D0 || ch > Key.D9 && ch != Key.LeftShift && (ch != Key.OemComma || txBx2.Text.Contains(",")) && ch != Key.Back)
            {
                e.Handled = true;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxBx1_GotFocus(object sender, RoutedEventArgs e)
        {
            // реализует так называемый "placeholder"
            if (txBx1.Text == "введите дробное число ...")
            {
                txBx1.Text = "";
                txBx1.Foreground = new SolidColorBrush(Colors.Black);
                txBx1.FontSize = 24;
            }
        }

        private void TxBx1_LostFocus(object sender, RoutedEventArgs e)
        {
            // реализует так называемый "placeholder"
            if (txBx1.Text == "")
            {
                txBx1.Text = "введите дробное число ...";
                txBx1.Foreground = new SolidColorBrush(Colors.Silver);
                txBx1.FontSize = 12;
            }
        }

        private void TxBx2_GotFocus(object sender, RoutedEventArgs e)
        {
            // реализует так называемый "placeholder"
            if (txBx2.Text == "введите дробное число ...")
            {
                txBx2.Text = "";
                txBx2.Foreground = new SolidColorBrush(Colors.Black);
                txBx2.FontSize = 24;
            }
        }

        private void TxBx2_LostFocus(object sender, RoutedEventArgs e)
        {
            // реализует так называемый "placeholder"
            if (txBx2.Text == "")
            {
                txBx2.Text = "введите дробное число ...";
                txBx2.FontSize = 12;
                txBx2.Foreground = new SolidColorBrush(Colors.Silver);
            }
        }

        public void Draw_Line(int pad, int depth, int length)
        {
            // рисует линию на элементе Grid, где pad - это отступ слева, depth - отступ вниз, а length - это длина линии
            Line divLine = new Line
            { X1 = pad,
              X2 = pad + length,
              Y1 = depth,
              Y2 = depth,
              Stroke = new SolidColorBrush(Colors.Black),
              StrokeThickness = 3,
            };

            Cnvs.Children.Add(divLine);
        }

        public void Draw_Text(string text, int x, int y)
        {
            // рисует текст из переменной text на элементе Grid, x и y - координаты на элементе Grid
            Label txt = new Label
            {
                Content = text,
                FontFamily = new FontFamily("Courier new"), // выбран такой стиль, т.к. каждый символ занимает одинаковое количество места
                FontSize = fnt,
                FontWeight = FontWeights.Bold,
                Padding = new Thickness(0, 0, 0, 0),
            };
            Canvas.SetTop(txt, y);
            Canvas.SetLeft(txt, x);
            Cnvs.Children.Add(txt);
        }

        public int MaxIntArray(int[] arr)
        {
            // возвращает максимальное целое число из массива arr
            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                max = Math.Max(max, arr[i]);
            }

            return max;
        }

        public void DrawPlusMinus(string a, string b, double c)
        {
            // реализует рисование на элементе Grid всех необходимых элементов,
            // чтобы отобразить арифметические действия (сложение, вычитание) в столбик

            // разделяем целую и дробные части на элементы массивов, т.к.
            // при выводе значений необходимо отображать элементы, чтобы
            // разряды чисел находились друг под другом
            string[] sep1 = a.Split(',');
            string[] sep2 = b.Split(',');
            string res = Convert.ToString(c);
            string[] sepres = res.Split(',');

            // если числа не содержат дробных частей, то массивы, где хранятся целые части,
            // необходимо расширить и присвоить пустые строковые значения
            if (sep1.Length == 1)
            {
                Array.Resize(ref sep1, 2);
                sep1[1] = "";
            }
            if (sep2.Length == 1)
            {
                Array.Resize(ref sep2, 2);
                sep2[1] = "";
            }
            if (sepres.Length == 1)
            {
                Array.Resize(ref sepres, 2);
                sepres[1] = "";
            } 
               
            // находим самую длинную целую и дробную части
            int maxint = MaxIntArray(new int[] { sep1[0].Length, sep2[0].Length, sepres[0].Length} );
            int maxfact = MaxIntArray(new int[] { sep1[1].Length, sep2[1].Length, sepres[1].Length} );

            // чтобы запятая не отображалась, если дробные части отсутствуют
            sep1[1] = (sep1[1]) == "" ? sep1[1] : sep1[1] = "," + sep1[1];
            sep2[1] = (sep2[1]) == "" ? sep2[1] : sep2[1] = "," + sep2[1];
            sepres[1] = (sepres[1]) == "" ? sepres[1] : sepres[1] = "," + sepres[1];

            Cnvs.Children.Clear(); // очищаем холст, если до этого там было что-то изображено
            Draw_Text(sep1[0], 200 + fnt * (maxint - sep1[0].Length) * 6 / 10, 1 * fnt); // выводим целую часть 1-ого числа
            Draw_Text(sep1[1], 200 + fnt * maxint * 6 / 10, 1 * fnt); // выводим дробную часть 1-ого числа
            Draw_Text(sep2[0], 200 + fnt * (maxint - sep2[0].Length) * 6 / 10, 2 * fnt); // выводим целую часть 2-ого числа
            Draw_Text(sep2[1], 200 + fnt * maxint * 6 / 10, 2 * fnt); // выводим дробную часть 2 - ого числа
            Draw_Text(cb1.Text, 200 - fnt * 6 / 10, 3 * fnt / 2); // выводим арифметический знак между числами
            Draw_Line(200, 3 * fnt , (maxint + maxfact + 1) * fnt * 6 / 10 ); // выводим линию, под которой производятся вычисления
            Draw_Text(sepres[0], 200 + fnt * (maxint - sepres[0].Length) * 6 / 10, 3 * fnt); // выводим целую часть результата
            Draw_Text(sepres[1], 200 + fnt * maxint * 6 / 10, 3 * fnt); // выводим дробную часть результата
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {

            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "," }; // разделитель дробной части - ','
            // преобразуем значения в TextBox в действительные числа
            double a = double.Parse(txBx1.Text, formatter);
            double b = double.Parse(txBx2.Text, formatter);
            double res = 0;
            // в зависимости от выбора арифметического действия в ComboBox производятся вычисления
            // и отображаются в виде решения столбиком
            switch (cb1.Text)
            {
                case "+":
                    res = a + b;
                    DrawPlusMinus(txBx1.Text, txBx2.Text, res);
                    break;
                case "-":
                    res = a - b;
                    DrawPlusMinus(txBx1.Text, txBx2.Text, res);
                    break;
                case "*":
                    res = a * b;
                    // здесь должно быть другое, но я не успел
                    DrawPlusMinus(txBx1.Text, txBx2.Text, res);
                    break;
                case "/":
                    res = a / b;
                    // здесь должно быть другое, но я не успел
                    DrawPlusMinus(txBx1.Text, txBx2.Text, res);
                    break;
            }

            lblResult.Content = Convert.ToString(res); // вывод результата после знака "="
        }

    }
}
