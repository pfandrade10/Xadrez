using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro Tab)
        {
            for ( int i=0; i < Tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < Tab.Colunas; j++)
                {                
                    if (Tab.Peca(i, j) is null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(Tab.Peca(i, j));
                        Console.Write(" ");
                    }                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if  (peca.Cor == Cor.Branco)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
