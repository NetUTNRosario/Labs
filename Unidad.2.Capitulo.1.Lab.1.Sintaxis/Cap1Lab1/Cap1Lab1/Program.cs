using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap1Lab1
{
    class Program
    {
        private static bool Menu(string arg)
        {
            
                Console.Clear();
                Console.WriteLine("1. Pasar a mayúsculas ");
                Console.WriteLine("2. Pasar a minúsculas ");
                Console.WriteLine("3. Contar caracteres");
                Console.WriteLine("0. Salir");
                Console.WriteLine("Ingrese una opción: ");
                ConsoleKeyInfo a = Console.ReadKey();
                Console.Clear();
                if (a.Key==ConsoleKey.D0)
                {
                    return false;
                }
                else
                {
                    switch (a.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine(arg.ToUpper());
                            break;
                        case ConsoleKey.D2:
                            Console.WriteLine(arg.ToLower());
                            break;
                        case ConsoleKey.D3:
                            Console.WriteLine("El largo del texto es: {0}", arg.Length);
                            break;

                    }
                Console.WriteLine("Presione una tecla para continuar. ");
                Console.ReadLine();
                return true;

                }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese un texto");
            string inputTexto=Console.ReadLine();
            Console.Clear();
            int ban = 0;
            foreach(char ca in inputTexto)
            {
                if (!(Char.IsLetterOrDigit(ca) || Char.IsWhiteSpace(ca)||Char.IsSeparator(ca)))
                {
                    Console.WriteLine("Error. Lo ingresado incluye caracteres inválidos");
                    ban = 1;
                    Console.ReadLine();
                    break;
                }    
            }
            if (ban == 0) 
            {
                bool mostrarMenu = true;
                while (mostrarMenu)
                {
                    mostrarMenu=Menu(inputTexto);
                }
            }

        }
    }
}
