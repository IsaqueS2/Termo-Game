namespace Termo_Game
{
    public partial class Form1 : Form
    {

        private Termo jogoPalavras;

        public Form1()
        {
            InitializeComponent();
            IniciarPartida();
            ConfiguracaoDosBotoes();

        }
        private void ConfiguracaoDosBotoes()
        {
            btnEnter.Click += Enter_Click;
            btnReiniciar.Click += NovoJogo;

            foreach (Button botao in pnlBotoes.Controls)
            {
                if (botao.Text != "Enter")
                {
                    botao.Click += SelecionarLetraPorBotao;
                }
            }
        }
        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            IniciarPartida();
        }
        private void Enter_Click(object sender, EventArgs e)
        {
            ValidacaoPalavra();
        }
        private void IniciarPartida()
        {
            jogoPalavras = new Termo();
            btnReiniciar.Visible = true;

            pnlBotoes.Enabled = true;

            RecomecarPaineis();

            ReiniciarRodada();
        }
        private void SelecionarLetraPorBotao(object? sender, EventArgs e)
        {
            Button botaoClicado = (Button)sender;
            PassarLetraBotaoParaTextBox(Convert.ToChar(botaoClicado.Text[0]));
        }
        private void ReceberPalavraInteira()
        {
            jogoPalavras.palavraDigitada = "";

            foreach (Control txtLetra in pnlMostrarLetra.Controls)
            {
                if (pnlMostrarLetra.GetRow(txtLetra) == jogoPalavras.rodada)
                {
                    jogoPalavras.palavraDigitada += txtLetra.Text;
                }
            }
        }
        private void PassarLetraBotaoParaTextBox(char letraTeclado)
        {
            foreach (Control txtLetra in pnlMostrarLetra.Controls)
            {
                if (txtLetra is TextBox && pnlMostrarLetra.GetRow(txtLetra) == jogoPalavras.rodada && txtLetra.Text == "")
                {
                    txtLetra.Text = letraTeclado.ToString();
                    break;
                }
            }
        }
        private void ValidacaoPalavra()
        {
            ReceberPalavraInteira();

            if (jogoPalavras.VerificaPalavraCompleta())

                FinalizarRodada();
        }
        private void FinalizarRodada()
        {
            VerificarLetrasNaoExistentesNaPalavra();
            VerificarLetraExistenteNaPalavra();
            VerificarPosicaoDaLetraNaPalavra();
       

            if (jogoPalavras.VerificaSeJogadorGanhou())
                VencerPartida();

            else if (jogoPalavras.VerificaSeJogadorPerdeu())
                PerderPartida();

            jogoPalavras.rodada++;

            ReiniciarRodada();
        }
        private void VerificarLetrasNaoExistentesNaPalavra()
        {

            foreach (Control txtLetra in pnlMostrarLetra.Controls)
            {
                if (pnlMostrarLetra.GetRow(txtLetra) != jogoPalavras.rodada)
                    continue;

                txtLetra.BackColor = Color.Red;

                foreach (Control btnTeclado in pnlBotoes.Controls)
                {
                    if (btnTeclado.Text == "Enter")
                        continue;

                    if (jogoPalavras.CompararLetrasIguais(Convert.ToChar(btnTeclado.Text[0]), Convert.ToChar(txtLetra.Text[0])))
                        btnTeclado.BackColor = Color.Red;
                }
            }
        }
        private void VerificarLetraExistenteNaPalavra()
        {
            foreach (char letraPalavraSecreta in jogoPalavras.palavraSecreta)
            {
                foreach (Control txtLetra in pnlMostrarLetra.Controls)
                {
                    if (pnlMostrarLetra.GetRow(txtLetra) != jogoPalavras.rodada)
                        continue;

                    if (jogoPalavras.CompararLetrasIguais(Convert.ToChar(txtLetra.Text), letraPalavraSecreta))
                    {
                        txtLetra.BackColor = Color.Yellow;

                        foreach (Control btnTeclado in pnlBotoes.Controls)
                        {
                            if (btnTeclado.Text == txtLetra.Text && btnTeclado.BackColor != Color.Green)
                            {
                                btnTeclado.BackColor = Color.Yellow;
                            }
                        }
                    }
                }
            }
        }
        private void VerificarPosicaoDaLetraNaPalavra()
        {
            List<Control> txtListaLetras = new List<Control>();

            foreach (Control txtBox in pnlMostrarLetra.Controls)
            {
                if (pnlMostrarLetra.GetRow(txtBox) == jogoPalavras.rodada)
                    txtListaLetras.Add(txtBox);
            }

            for (int letra = 0; letra < jogoPalavras.palavraSecreta.Length && letra < txtListaLetras.Count; letra++)
            {
                char letraNoTxt = Convert.ToChar(txtListaLetras[letra].Text);
                char letraSecreta = jogoPalavras.palavraSecreta[letra];

                if (jogoPalavras.CompararLetrasIguais(letraNoTxt, letraSecreta))
                {
                    txtListaLetras[letra].BackColor = Color.Green;
                    txtListaLetras[letra].Text = letraSecreta.ToString();

                    foreach (Control btnTeclado in pnlBotoes.Controls)
                    {
                        if (btnTeclado.Text == txtListaLetras[letra].Text)
                            btnTeclado.BackColor = Color.Green;
                    }
                }
            }
        }
        private void ReiniciarRodada()
        {
            foreach (Control txtLetra in pnlMostrarLetra.Controls)
            {
                if (pnlMostrarLetra.GetRow(txtLetra) == jogoPalavras.rodada)
                {
                    txtLetra.BackColor = Color.AliceBlue;
                }
            }
        }
        private void VencerPartida()
        {
            MessageBox.Show("Voce venceu, Parabéns!");

            btnReiniciar.Visible = true;
            pnlBotoes.Enabled = false;
        }
        private void PerderPartida()
        {
            MessageBox.Show("Você perdeu!");

            btnReiniciar.Visible = true;
            pnlBotoes.Enabled = false;
        }
        private void RecomecarPaineis()
        {
            foreach (Control txtTabelaLetra in pnlMostrarLetra.Controls)
            {
                txtTabelaLetra.Text = "";
                txtTabelaLetra.BackColor = Color.White;

            }

            foreach (Control btnTeclado in pnlBotoes.Controls)
            {
                btnTeclado.BackColor = Color.Transparent;
            }
        }
        private void NovoJogo(object sender, EventArgs e)
        {
            IniciarPartida();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }



}




