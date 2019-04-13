using System;
using tabuleiro;
using JogoXadrez;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                MecanicaXadrez partida = new MecanicaXadrez();


                Tela.ImprimirTabuleiro(partida.Tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
