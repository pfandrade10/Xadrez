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

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQuantidadeDeMovimento();
            Peca PecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (PecaCapturada != null)
            {
                Capturadas.Add(PecaCapturada);
            }
        }

        public void ExecutarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudarJogador();
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

            InserirNovaPeca('c', 8, new Bispo(Cor.Preto, Tab));
            InserirNovaPeca('f', 8, new Bispo(Cor.Preto, Tab));
            InserirNovaPeca('a', 8, new Torre(Cor.Preto, Tab));
            InserirNovaPeca('h', 8, new Torre(Cor.Preto, Tab));
            InserirNovaPeca('e', 8, new Rainha(Cor.Preto, Tab));
            InserirNovaPeca('d', 8, new Rei(Cor.Preto, Tab));
            

        }
    }
}
