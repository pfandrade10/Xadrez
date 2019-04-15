using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeDeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QuantidadeDeMovimentos = 0;
        }

        public void IncrementarQuantidadeDeMovimento()
        {
            QuantidadeDeMovimentos++;
        }

        public void DecrementarQuantidadeDeMovimento()
        {
            QuantidadeDeMovimentos--;
        }

        public bool ExisteMovimentoPossivel()
        {
            bool[,] mat = MovimentosPossiveis();
            for (int i = 0; i< Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if(mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
