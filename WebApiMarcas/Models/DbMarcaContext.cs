using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiMarcas.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DbMarcaContext : DbContext  //Herda de DbContext
    {
        //Subscreve o construtor padrão
        public DbMarcaContext() : base("DbMarcas"){ //nome da string de conexão que fica no web.config

        }

        //Uma representação da tabela no banco de dados
        public DbSet<Marca> Marcas { get; set; }
    }
}                                               