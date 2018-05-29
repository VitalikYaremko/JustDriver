using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDriver
{
    class Program
    {

        static List<int> position = new List<int>();//сиписок для позицій символів (:)

        static void Main(string[] args)
        {
            bool packageFound = true;
            while (true)
            {
                try
                {
                    bool endRead = false;
                    int count = 0;
                    //буфер для запису введеного рядка
                    StringBuilder S = new StringBuilder();
                    ConsoleKeyInfo key;

                    while (endRead is false) // доки виконується умова
                    {
                        key = Console.ReadKey(true);
                        char c = key.KeyChar; // нажата клавиша
                        Console.Write(c); // виводим на консоль
                        S.Append(c); // додаємо до буферу

                        if (c == 58)//якщо зустрівся симлол :
                        {
                            count++;
                        }
                        if (count > 1)// якщо після другої : є символ Е
                        {
                            if (S[S.Length - 2] == 58 && c == 69)
                            {
                                endRead = true;
                            }
                        }

                    }
                    Console.WriteLine(); // перевід рядка

                    char[] arr = new char[S.Length];
                    S.CopyTo(0, arr, 0, S.Length);//конвертація в масив 

                    if (arr[0] != 80)//чи перший символ Р
                    {
                        packageFound = false;
                        throw new Exception("first symbol is not P");
                    }
                    if (arr[arr.Length - 1] != 69)//чи останній символ Р
                    {
                        packageFound = false;
                        throw new Exception("last symbol is not E");
                    }

                    for (int i = 0; i < arr.Length; i++)// перевіряємо си входить пакет в наш діапазон та записуємо позиції розділюючого символу
                    {
                        if (arr[i] <= 32 || arr[i] >= 127)
                        {
                            packageFound = false;
                            throw new Exception("The package does not fall into the range (32-127) ASCII");
                        }
                        if (arr[i] == 58)
                        {
                            position.Add(i);//додаємо позицію символу(:)
                        }
                    }

                    if (packageFound is true)
                    {
                        Console.WriteLine("ACK");
                    }
                    else { Console.WriteLine("package not found"); }

                    int command = arr[position[0] - 1];//витягуємо команду 

                    switch (command)
                    {
                        case 84:
                            Console.WriteLine(Text(arr));
                            break;
                        case 83:
                            Sound(arr);
                            break;
                    }
                    position.Clear();
                    Console.Write("write next package:");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadKey();
                }

            }

        }
        static public string Text(char[] arr)//метод для виведення тексту
        {
            try
            {
                String text = null;
                for (int i = position[0] + 1; i < position[1]; i++)// шукаємо що між :
                {
                    text += Convert.ToString(arr[i]);// записуємо текст який був між :
                }
                return text;//повертаємо
            }
            catch (Exception e)
            {
                return Convert.ToString(e);//якщо щось пішло не так 
            }
           
        }
        static public void Sound(char[] arr)// метод для бібкання :) 
        {
            try
            {
                String freq = null;
                String duration = null;
                bool next = false;
                for (int i = position[0] + 1; i < position[1]-1; i++)//шукаємо що між :
                {
                    if (arr[i] == 44)//якзщо знайшли кому 
                    {
                        next = true;
                    }
                    if (next is true) // після коми записуємо в іншу строку тривалість
                    {
                        duration += Convert.ToString(arr[i+1]);
                    }
                    else
                    {
                        freq += Convert.ToString(arr[i]);// записуємо частоту 
                    }
                }
                Console.Beep(Convert.ToInt32(freq), Convert.ToInt32(duration));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
