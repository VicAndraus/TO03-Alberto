using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure;
using Newtonsoft.Json;

namespace JsonAPPTeste
{
    public partial class Form1 : Form
    {
        public List<voo> v2 = new List<voo>();
        public Form1()
        {
            InitializeComponent();

            string jsonPath = "C:\\voosjson\\voos.json";
            string retorno = string.Empty;

            string entrada;

            using (StreamReader lido = new StreamReader(jsonPath))
            {
                while (true)
                {
                    entrada = lido.ReadLine();

                    if (entrada == null)
                        break;

                    v2.Add(JsonConvert.DeserializeObject<voo>(entrada));
                }
            }

            foreach (voo v in v2)
            {
                richTextBox1.Text += "\n" + v.codigo + " - " + v.origem + " - " + v.destino + " - " + v.horario + " - " + v.compania + " - " + v.operando;
                comboBox1.Items.Add(v.codigo);
            }
        }

        public class voo
        {
            public string codigo { set; get; }
            public string origem { set; get; }
            public string destino { set; get; }
            public string horario { set; get; }
            public string compania { set; get; }
            public char operando { set; get; }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Verifica se algo está selecionado no ComboBox
            if (comboBox1.SelectedIndex != -1)
            {
                // Pega o código do voo selecionado no ComboBox
                string codigoSelecionado = comboBox1.SelectedItem.ToString();

                // Encontra o voo correspondente na lista 'v2'
                voo vooSelecionado = v2.FirstOrDefault(v => v.codigo == codigoSelecionado);

                if (vooSelecionado != null)
                {
                    // Serializa o objeto voo para JSON
                    string jsonVoo = JsonConvert.SerializeObject(vooSelecionado, Formatting.Indented);


                    // Se quiser salvar o JSON em um arquivo, descomente as linhas abaixo:

                    string outputPath = "C:\\voosjson\\output.json";
                    File.WriteAllText(outputPath, jsonVoo);
                    MessageBox.Show($"JSON salvo em {outputPath}");

                }
                else
                {
                    MessageBox.Show("Voo não encontrado!");
                }
            }
            else
            {
                MessageBox.Show("Selecione um voo no ComboBox.");
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
