using Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

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
                sc.DataSource = "192.168.1.124";
                sc.InitialCatalog = "nts_clientes";
                sc.UserID = "sa";
                sc.Password = "123*abc*456";
                // sc.IntegratedSecurity = true;
                SqlConnection conexion = new SqlConnection(sc.ToString());
                conexion.Open();



                SqlCommand command = new SqlCommand();
                command.CommandText = "select * from cl_clientes";
                command.Connection = conexion;

                SqlDataReader reader = command.ExecuteReader();
                Random random = new Random();

                int cod = 0;
                string codIdent = "";
                string strSec = "";


                while (reader.Read())
                {
                    string primerNombre = Faker.Name.First();
                    string segundoNombre = Faker.Name.First();
                    string primerApellido = Faker.Name.Last();
                    string segundoApellido = Faker.Name.Last();
                    string fullName = $"{primerNombre} {segundoNombre} {primerApellido} {segundoApellido}";


                    cod = random.Next(1, 24);

                    if (cod < 10)
                        codIdent = "0" + cod.ToString();
                    else
                        codIdent = cod.ToString();
                    cod = random.Next(1, 9999999);
                    if (cod < 9999999)
                    {
                        strSec = cod.ToString().PadLeft(codIdent.Length, '0');

                    }
                    var mod = new Mod10(reader[1].ToString());
                    var digit = mod.CheckDigit.ToString();
                    Console.WriteLine("Cedula:\t\t" + reader[1].ToString());
                    Console.WriteLine("sanitizado:\t" + codIdent + strSec + digit);
                    Console.WriteLine($"Nombre\t\t{primerNombre}\nApellido\t{primerApellido}\nfullName\t{fullName}");
                    Console.WriteLine("--------------------------------------------");
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

        static void sanitizarNombres()
        {


        }

    }
}
