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
                switch (a) 
                {
                    case "1":
                        Console.WriteLine(arg.ToUpper());
                        return true;
                    case "2":
                        Console.WriteLine(arg.ToLower());
                        return true;
                    case "3":
                        Console.WriteLine("El largo del texto es: {0}", arg.Length);
                        return true;
                    case "0":
                        return false;

                }
                Console.WriteLine("Presione una tecla para continuar. ");
                Console.ReadLine();

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
                    Menu(inputTexto);
                }
            }

        }
    }
}
