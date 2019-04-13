using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace JogoXadrez
{
    class MecanicaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }

        public MecanicaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            InserirPecas();
        }       

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQuantidadeDeMovimento();
            Peca PecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
        }

        private void InserirPecas()
        {
            Tab.ColocarPeca(new Bispo(Cor.Branco, Tab), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.ColocarPeca(new Bispo(Cor.Branco, Tab), new PosicaoXadrez('f', 1).ToPosicao());
            Tab.ColocarPeca(new Cavalo(Cor.Branco, Tab), new PosicaoXadrez('g', 1).ToPosicao());
            Tab.ColocarPeca(new Cavalo(Cor.Branco, Tab), new PosicaoXadrez('b', 1).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branco, Tab), new PosicaoXadrez('a', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branco, Tab), new PosicaoXadrez('b', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branco, Tab), new PosicaoXadrez('c', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branco, Tab), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branco, Tab), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branco, Tab), new PosicaoXadrez('f', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branco, Tab), new PosicaoXadrez('g', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branco, Tab), new PosicaoXadrez('h', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('a', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('h', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Branco, Tab), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.ColocarPeca(new Rainha(Cor.Branco, Tab), new PosicaoXadrez('d', 1).ToPosicao());

            Tab.ColocarPeca(new Bispo(Cor.Preto, Tab), new PosicaoXadrez('c', 8).ToPosicao());
            Tab.ColocarPeca(new Bispo(Cor.Preto, Tab), new PosicaoXadrez('f', 8).ToPosicao());
            Tab.ColocarPeca(new Cavalo(Cor.Preto, Tab), new PosicaoXadrez('g', 8).ToPosicao());
            Tab.ColocarPeca(new Cavalo(Cor.Preto, Tab), new PosicaoXadrez('b', 8).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preto, Tab), new PosicaoXadrez('a', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preto, Tab), new PosicaoXadrez('b', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preto, Tab), new PosicaoXadrez('c', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preto, Tab), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preto, Tab), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preto, Tab), new PosicaoXadrez('f', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preto, Tab), new PosicaoXadrez('g', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preto, Tab), new PosicaoXadrez('h', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('a', 8).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('h', 8).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Preto, Tab), new PosicaoXadrez('d', 8).ToPosicao());
            Tab.ColocarPeca(new Rainha(Cor.Preto, Tab), new PosicaoXadrez('e', 8).ToPosicao());


        }
    }
}
