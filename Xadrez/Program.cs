using System;
using tabuleiro;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(23, 23);

            Tela.ImprimirTabuleiro(tab);
        }
    }
}
