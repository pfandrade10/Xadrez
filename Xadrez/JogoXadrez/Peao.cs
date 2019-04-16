using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace JogoXadrez
{
    class Peao : Peca
    {
        private MecanicaXadrez partida;
        public Peao(Cor cor, Tabuleiro tab, MecanicaXadrez partida) : base(cor, tab)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p != null && p.Cor != Cor;
        }

            private bool Livre(Posicao pos)
        {
            return Tab.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branco)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(p2) && Livre(p2) && Tab.PosicaoValida(pos) && Livre(pos) && QuantidadeDeMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // #JogadaEspecial en passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if(Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == partida.VulneravelEnPassant)
                    {
                        mat[esquerda.Linha -1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == partida.VulneravelEnPassant)
                    {
                        mat[direita.Linha -1, direita.Coluna] = true;
                    }     
                }
            }
            else
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(p2) && Livre(p2) && Tab.PosicaoValida(pos) && Livre(pos) && QuantidadeDeMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // #JogadaEspecial en passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == partida.VulneravelEnPassant)
                    {
                        mat[esquerda.Linha +1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == partida.VulneravelEnPassant)
                    {
                        mat[direita.Linha +1, direita.Coluna] = true;
                    }
                }
            }
            return mat;
        }
    }
}
