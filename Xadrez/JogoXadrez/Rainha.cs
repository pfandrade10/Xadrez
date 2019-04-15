﻿using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace JogoXadrez
{
    class Rainha : Peca
    {
        public Rainha(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            // norte
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                while (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Linha = pos.Linha - 1;
                }

            // nordeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                while (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Coluna = pos.Coluna + 1;
                    pos.Linha = pos.Linha - 1;
                }

            // leste
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                while (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Coluna = pos.Coluna + 1;
                }

            // sudeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                while (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Coluna = pos.Coluna + 1;
                    pos.Linha = pos.Linha + 1;
                }

            // sul
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                while (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Linha = pos.Linha + 1;
                }

            // sudoeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                while (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Coluna = pos.Coluna - 1;
                    pos.Linha = pos.Linha + 1;
                }

            // oeste
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                while (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Coluna = pos.Coluna - 1;
                }

            // noroeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                while (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                    {
                        break;
                    }
                    pos.Coluna = pos.Coluna - 1;
                    pos.Linha = pos.Linha - 1;
                }           
            return mat;

        }
    }
}
