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
        public Peca VulneravelEnPassant { get; private set; }

        public MecanicaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
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

            // #JogadaEspecial roque pequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementarQuantidadeDeMovimento();
                Tab.ColocarPeca(T, destinoT);
            }

            // #JogadaEspecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementarQuantidadeDeMovimento();
                Tab.ColocarPeca(T, destinoT);
            }

            // #JogadaEspecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && PecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branco)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    PecaCapturada = Tab.RetirarPeca(posP);
                    Capturadas.Add(PecaCapturada);
                }
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

            Peca p = Tab.Peca(destino);

            // #JogadaEspecial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2) || destino.Linha == origem.Linha + 2)
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
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

            // #JogadaEspecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarQuantidadeDeMovimento();
                Tab.ColocarPeca(T, origemT);
            }

            // #JogadaEspecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarQuantidadeDeMovimento();
                Tab.ColocarPeca(T, origemT);
            }

            // #JogadaEspecial en passant
            if(p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posP;
                    if(p.Cor == Cor.Branco)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tab.ColocarPeca(peao, posP);
                } 
            }
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
            InserirNovaPeca('c', 1, new Bispo(Cor.Branco, Tab));
            InserirNovaPeca('f', 1, new Bispo(Cor.Branco, Tab));
            InserirNovaPeca('a', 1, new Torre(Cor.Branco, Tab));
            InserirNovaPeca('h', 1, new Torre(Cor.Branco, Tab));
            InserirNovaPeca('b', 1, new Cavalo(Cor.Branco, Tab));
            InserirNovaPeca('g', 1, new Cavalo(Cor.Branco, Tab));
            InserirNovaPeca('d', 1, new Rainha(Cor.Branco, Tab));
            InserirNovaPeca('e', 1, new Rei(Cor.Branco, Tab,this));
            InserirNovaPeca('a', 2, new Peao(Cor.Branco, Tab, this));
            InserirNovaPeca('b', 2, new Peao(Cor.Branco, Tab, this));
            InserirNovaPeca('c', 2, new Peao(Cor.Branco, Tab, this));
            InserirNovaPeca('d', 2, new Peao(Cor.Branco, Tab, this));
            InserirNovaPeca('e', 2, new Peao(Cor.Branco, Tab, this));
            InserirNovaPeca('f', 2, new Peao(Cor.Branco, Tab, this));
            InserirNovaPeca('g', 2, new Peao(Cor.Branco, Tab, this));
            InserirNovaPeca('h', 2, new Peao(Cor.Branco, Tab, this));


            InserirNovaPeca('c', 8, new Bispo(Cor.Preto, Tab));
            InserirNovaPeca('f', 8, new Bispo(Cor.Preto, Tab));
            InserirNovaPeca('a', 8, new Torre(Cor.Preto, Tab));
            InserirNovaPeca('h', 8, new Torre(Cor.Preto, Tab));
            InserirNovaPeca('b', 8, new Cavalo(Cor.Preto, Tab));
            InserirNovaPeca('g', 8, new Cavalo(Cor.Preto, Tab));
            InserirNovaPeca('d', 8, new Rainha(Cor.Preto, Tab));
            InserirNovaPeca('e', 8, new Rei(Cor.Preto, Tab,this));
            InserirNovaPeca('a', 7, new Peao(Cor.Preto, Tab, this));
            InserirNovaPeca('b', 7, new Peao(Cor.Preto, Tab, this));
            InserirNovaPeca('c', 7, new Peao(Cor.Preto, Tab, this));
            InserirNovaPeca('d', 7, new Peao(Cor.Preto, Tab, this));
            InserirNovaPeca('e', 7, new Peao(Cor.Preto, Tab, this));
            InserirNovaPeca('f', 7, new Peao(Cor.Preto, Tab, this));
            InserirNovaPeca('g', 7, new Peao(Cor.Preto, Tab, this));
            InserirNovaPeca('h', 7, new Peao(Cor.Preto, Tab, this));


        }
    }
}
