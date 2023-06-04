using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDMysql
{
    static class Conn
    {
        private const string servidor = "localhost";
        private const string bancoDados = "livros";
        private const string usuario = "root";
        private const string senha = "";

        public static string strConn = $"server={servidor};User Id={usuario};database={bancoDados};password={senha}";
    }
}
