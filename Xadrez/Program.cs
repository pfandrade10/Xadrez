using System;
using tabuleiro;
using JogoXadrez;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.ColocarPeca(new Torre(Cor.Preto, tab), new Posicao(0, 0));
            tab.ColocarPeca(new Torre(Cor.Preto, tab), new Posicao(1, 3));
            tab.ColocarPeca(new Rei(Cor.Preto, tab), new Posicao(2, 4));

            Tela.ImprimirTabuleiro(tab);
        }
    }
}
