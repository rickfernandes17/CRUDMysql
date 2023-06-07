using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDMysql
{
    class Livro
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string autores { get; set; }
        public decimal unitario { get; set; }
        public char ativo { get; set; }
        public DateTime dataCriacao { get; set; }

        public static DataTable GetLivros(string filtro = "")
        {
            var dt = new DataTable();
            string sql;
            if (filtro == "") { 
                sql = "select id as Codigo,titulo as Titulo,autores,unitario, data_criacao from livros WHERE livros.ativo=\"s\"";
            }
            else
            {   
                sql = $"select id as Codigo,titulo as Titulo,autores,unitario, ativo, data_criacao from livros WHERE livros.autores LIKE '%{filtro}%' OR livros.titulo LIKE '%{filtro}%'";
            }
            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var da = new MySqlDataAdapter(sql, cn))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dt;
        }

        public void GetLivro(int id) {
            var sql = $"select id,titulo,autores,unitario, data_criacao,ativo from livros WHERE livros.id={id}";
            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var cmd = new MySqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    this.id = Convert.ToInt32(dr["id"]);
                                    this.titulo = dr["titulo"].ToString();
                                    this.autores = dr["autores"].ToString();
                                    this.unitario = Convert.ToDecimal(dr["unitario"]);
                                    this.dataCriacao = Convert.ToDateTime(dr["data_criacao"]);
                                    this.ativo = Convert.ToChar(dr["ativo"]);

                                }
                            }
                        }
                    }
                }
                
            }catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public void SalvarLivro() {
            var sql = "";
            if (this.id == 0)
            {
                sql = "insert into livros (titulo, autores, unitario, ativo) Values (@titulo, @autores, @unitario, @ativo)";
            }
            else
            {
                sql = $"update livros set titulo=@titulo, autores=@autores, unitario=@unitario, ativo=@ativo where id={this.id}";
                
            }
            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@titulo", this.titulo);
                        cmd.Parameters.AddWithValue("@autores", this.autores);
                        cmd.Parameters.AddWithValue("@unitario", this.unitario);
                        cmd.Parameters.AddWithValue("@ativo", this.ativo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
