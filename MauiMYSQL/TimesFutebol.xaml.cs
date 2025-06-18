using MauiMYSQL.Models;

namespace MauiMYSQL;

// O NOME DA CLASSE 'TimesFutebol' FOI MANTIDO
public partial class TimesFutebol : ContentPage
{
    // OS NOMES DAS VARIÁVEIS 'banco' e 'times' FORAM MANTIDOS
    Conecta banco = new Conecta();
    Times times = new Times(); 

    public TimesFutebol()
    {
        InitializeComponent();
        if (banco.Conexao())
        {
            lblStatus.Text = "Banco conectado com Sucesso !";
            if (times.Times_Consulta()) 
            {
                // O ListView 'lstTimesFutebol' agora recebe a lista de Pokémon
                lstTimesFutebol.ItemsSource = times.listaTimes;
            }
        }
        else
        {
            lblStatus.Text = banco.conexao_status;
        }
    }

    // O NOME DO MÉTODO 'btnAdicionar' FOI MANTIDO
    private void btnAdicionar(object sender, EventArgs e)
    {
        // --- MUDANÇA PRINCIPAL AQUI DENTRO ---

        // 1. Validação simples para os novos campos
        if (string.IsNullOrEmpty(txtNome.Text) ||
            string.IsNullOrEmpty(txtTipo.Text) ||
            string.IsNullOrEmpty(txtLevel.Text) ||
            string.IsNullOrEmpty(txtAtaque.Text) ||
            string.IsNullOrEmpty(txtDefesa.Text))
        {
            DisplayAlert("Atenção", "Preencha todos os campos do Pokémon", "OK");
            return;
        }

        // Tenta converter os campos numéricos, validando a entrada
        if (!int.TryParse(txtLevel.Text, out int level) ||
            !int.TryParse(txtAtaque.Text, out int ataque) ||
            !int.TryParse(txtDefesa.Text, out int defesa))
        {
            DisplayAlert("Erro", "Level, Ataque e Defesa devem ser números válidos.", "OK");
            return;
        }

        // 2. Chama o método 'Times_Add' com os dados do Pokémon
        // A função ainda se chama Times_Add, mas agora salva um Pokémon
        times.Times_Add(txtNome.Text, txtTipo.Text, level, ataque, defesa);

        // 3. Limpa os campos após adicionar
        txtNome.Text = string.Empty;
        txtTipo.Text = string.Empty;
        txtLevel.Text = string.Empty;
        txtAtaque.Text = string.Empty;
        txtDefesa.Text = string.Empty;

        // 4. Atualiza a lista na tela 
        if (times.Times_Consulta())
        { // Este método agora consulta e atualiza a lista de Pokémon
            lstTimesFutebol.ItemsSource = null;
            lstTimesFutebol.ItemsSource = times.listaTimes;
        }
    }
}