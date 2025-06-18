using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Java.Util;
using MauiMYSQL.Models;
using MySqlConnector;

namespace MauiMYSQL.Models
{
    // O NOME DA CLASSE FOI MANTIDO COMO "Times"
    public class Times : Conecta
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string tipo { get; set; }
        public int level { get; set; }
        public int ataque { get; set; }
        public int defesa { get; set; }


        public List<Times> listaTimes = new List<Times>();

        public Times() { }

        public bool Times_Add(string nome, string tipo, int level, int ataque, int defesa)
        {
            if (!Conexao())
            {
                return false;
            }

            StrQuery = "INSERT INTO pokemon (nome, tipo, level, ataque, defesa) VALUES (@nome, @tipo, @level, @ataque, @defesa)";
            Cmd = new MySqlCommand(StrQuery, Conn);

            Cmd.Parameters.AddWithValue("@nome", nome);
            Cmd.Parameters.AddWithValue("@tipo", tipo);
            Cmd.Parameters.AddWithValue("@level", level);
            Cmd.Parameters.AddWithValue("@ataque", ataque);
            Cmd.Parameters.AddWithValue("@defesa", defesa);
            try
            {
                Cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Times_Consulta()
        {
            if (!Conexao())
            {
                return false;
            }

            StrQuery = "SELECT * FROM pokemon order by nome";
            MySqlCommand cmd = new MySqlCommand(StrQuery, Conn);
            cmd.CommandType = System.Data.CommandType.Text;
            Dr = cmd.ExecuteReader();
            listaTimes.Clear();
            while (Dr.Read())
            {
                listaTimes.Add(new Times
                {
                    id = int.Parse(Dr["id"].ToString()),
                    nome = Dr["nome"].ToString(),
                    tipo = Dr["tipo"].ToString(),
                    level = int.Parse(Dr["level"].ToString()),
                    ataque = int.Parse(Dr["ataque"].ToString()),
                    defesa = int.Parse(Dr["defesa"].ToString())
                });
            }

            return true;
        }
    }
}