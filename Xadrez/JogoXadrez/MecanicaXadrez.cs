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
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }

        public MecanicaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            InserirPecas();
        }       

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQuantidadeDeMovimento();
            Peca PecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (PecaCapturada != null)
            {
                Capturadas.Add(PecaCapturada);
            }
            return PecaCapturada;
        }

        public void ExecutarJogada(Posicao origem, Posicao destino)
        {
            Peca PecaCapturada = ExecutarMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, PecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            if (EstaEmXeque(Adversario(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }      
            if (XequeMate(Adversario(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudarJogador();
            }
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQuantidadeDeMovimento();
            if(pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);

            }
            Tab.ColocarPeca(p, origem);
        }


        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) is null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem informada!");
            }
            if(JogadorAtual != Tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("Peça escolhida não é do jogador atual!");
            }
            if (!Tab.Peca(pos).ExisteMovimentoPossivel())
            {
                throw new TabuleiroException("A peça escolhida nao tem movimentos possíveis!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posiçõ de destino inválida!");
            }
        }

        private void MudarJogador()
        {
            if( JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach(Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca r = Rei(cor);
            if (r is null)
            {
                throw new TabuleiroException("Não há rei da cor" + cor + " no tabuleiro");
            }
            foreach( Peca x in PecasEmJogo(Adversario(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool XequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCaptudara = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCaptudara);
                            if (testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversario(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }

        public void InserirNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void InserirPecas()
        {
            InserirNovaPeca('c', 1,new Bispo(Cor.Branco, Tab));
            InserirNovaPeca('f', 1, new Bispo(Cor.Branco, Tab));
            InserirNovaPeca('a', 1, new Torre(Cor.Branco, Tab));
            InserirNovaPeca('h', 1, new Torre(Cor.Branco, Tab));
            InserirNovaPeca('d', 1, new Rainha(Cor.Branco, Tab));
            InserirNovaPeca('e', 1, new Rei(Cor.Branco, Tab));
            InserirNovaPeca('e', 2, new Torre(Cor.Branco, Tab));


            InserirNovaPeca('b', 8, new Torre(Cor.Preto, Tab));
 
            InserirNovaPeca('a', 8, new Rei(Cor.Preto, Tab));



        }
    }
}
