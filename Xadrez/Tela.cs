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
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if (Tab.Peca(i,j) is null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(Tab.Peca(i, j) + " ");
                    }                    
                }
                Console.WriteLine();
            }
        }
    }
}
