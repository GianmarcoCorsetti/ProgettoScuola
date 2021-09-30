using System;

namespace ScufflodDB {
    class Program {
        static void Main(string[] args)
        {
            dotnet ef dbcontext scaffold "Data Source = Server = localhost; User = sa; Password = 1Secure* Password; Database = gestione_corsi"--context ScoolContext --context -dir ScuffoldDB --output -dir Entities Microsoft.EntityFrameworkCore.SqlServer
        }
    }
}
