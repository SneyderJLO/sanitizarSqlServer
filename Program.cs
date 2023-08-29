using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ejercicioSanitizar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            sanitizarIdentificacion();
        }

        static void sanitizarIdentificacion()
        {


            SqlConnectionStringBuilder sc = new SqlConnectionStringBuilder();
            try
            {


                sc.DataSource = "localhost";
                sc.InitialCatalog = "nts_clientes";
                sc.UserID = "sa";
                sc.Password = "135678";
                SqlConnection conexion = new SqlConnection(sc.ConnectionString.ToString());
                conexion.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "select * from ts_clientes";
                command.Connection = conexion;
                SqlDataReader reader = command.ExecuteReader();
                Random random = new Random();
                Mod10 mod10 = new Mod10();

                int cod = 0;
                string codIdent = "";
                string strSec = "";
                //  mod10.CheckDigit.ToString();

                while (reader.Read())
                {

                    cod = random.Next(1, 24);
                    if (cod < 10)
                    {
                        codIdent = "0" + cod.ToString();

                    }
                    codIdent = cod.ToString();
                    cod = random.Next(1, 9999999);
                    if (cod < 9999999)
                    {
                        strSec = cod.ToString().PadLeft(7 - codIdent.Length, '0');



                    }
                    Console.WriteLine(reader[1].ToString() + "\n" + "\n" + strSec);
                }

                reader.Close();
                conexion.Close();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
    }
}
