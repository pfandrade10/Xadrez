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
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Torre(Cor.Preto, tab), new Posicao(0,0));
                tab.ColocarPeca(new Torre(Cor.Preto, tab), new Posicao(1,3));
                tab.ColocarPeca(new Torre(Cor.Preto, tab), new Posicao(0,2));
                tab.ColocarPeca(new Torre(Cor.Branco, tab), new Posicao(0, 7));
                tab.ColocarPeca(new Torre(Cor.Branco, tab), new Posicao(4, 2));
                tab.ColocarPeca(new Torre(Cor.Branco, tab), new Posicao(5, 3));

                Tela.ImprimirTabuleiro(tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
