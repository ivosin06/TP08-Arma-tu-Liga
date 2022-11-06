namespace TP08.Models;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public class BD{
    private static string _connectionString = @"Server=DESKTOP-KILBPAV\SQLEXPRESS;DataBase=TP08;Trusted_Connection=True";

    public static List<Equipo> ListarEquipos(int IdLiga)
    {
        List<Equipo> lista = new List<Equipo>();
        string sql = "SELECT IdEquipo, nombre, escudo, IdLiga FROM Equipos WHERE Equipos.IdLiga = @IdL ";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            lista = db.Query<Equipo>(sql, new { IdL = IdLiga }).ToList();
        }        
        return lista;
    }
    public static List<Partido> VerInfoPartidos(int IdLiga){
        List<Partido> lista = new List<Partido>();
        string sql = "SELECT IdEquipoLocal, IdEquipoVisitante, golesEquipoLocal, golesEquipoVisitante, fechaPartido FROM Ligas INNER JOIN Fechas on Fechas.IdLiga = Ligas.IdLiga INNER JOIN Partidos on Partidos.IdFecha = Fechas.IdFecha WHERE Ligas.IdLiga = @IdL";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            lista = db.Query<Partido>(sql, new { IdL = IdLiga }).ToList();
        }        
        return lista;
    }
    public static Equipo VerInfoEquipo(int IdEquipo){
        Equipo E;
        string sql = "SELECT * FROM Equipos WHERE Equipos.IdEquipo = @IdE";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            E = db.QueryFirstOrDefault<Equipo>(sql, new { IdE = IdEquipo });
        }
        return E;
    }
    //HACER VERINFOPARTIDOS que traiga todos los datos de los partidos de una liga menos idpartido y los datos de equipolocal y visitante
    public static List<Liga> ListarLigas()
    {
        List<Liga> lista = new List<Liga>();
        string sql = "SELECT * FROM Ligas";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            lista = db.Query<Liga>(sql).ToList();
        }
        return lista;
    }
    public static void AgregarEquipo(Equipo E)
    {
        string sql = "INSERT INTO Equipos VALUES (@pNombre, @pEscudo, @pIdLiga)";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new { pNombre = E.Nombre, pEscudo = E.Escudo, pIdLiga = E.IdLiga });
        }
    }
    public static void AgregarLiga(Liga L)
    {
        string sql = "INSERT INTO Ligas VALUES (@pNombre, @pLogo)";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new { pNombre = L.Nombre, pLogo = L.Logo});
        }
    }
 
    public static void AgregarFecha(Fecha F, int idLiga)
    {
        string sql = "INSERT INTO Fechas VALUES (@pNumero, @pIdLiga)";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new { pNumero = F.Numero, pIdLiga = idLiga});
        }
        foreach (Partido partido in F.Partidos)
        {
            AgregarPartidoSinJugar(partido, MaxIdF());
        }
    }
    private static void AgregarPartidoSinJugar(Partido P, int idFecha)
    {
        string sql = "INSERT INTO Partidos VALUES (@pIdEquipoLocal, @pIdEquipoVisitante, @pGolesEquipoLocal, @pGolesEquipoVisitante, @pFechaPartido, @pIdFecha)";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new { pIdEquipoLocal = P.EquipoLocal.IdEquipo, pIdEquipoVisitante = P.EquipoVisitante.IdEquipo, pGolesEquipoLocal = P.GolesEquipoLocal, pGolesEquipoVisitante = P.GolesEquipoVisitante,  pFechaPartido = P.FechaPartido, pIdFecha = idFecha});
        }
    }
    
    public static Liga verInfoLiga(int IdLiga){
        Liga L;
        string sql = "SELECT * FROM Ligas WHERE Ligas.IdLiga = @IdL";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            L = db.QueryFirstOrDefault<Liga>(sql, new { IdL = IdLiga });
        }
        return L;
    }
    public static void ModificarResultado(int IdEquipoLocal, int IdEquipoVisitante, int golesEquipoLocal, int golesEquipoVisitante){
        
        string sql = "UPDATE Partidos SET golesEquipoLocal = @pGolesEquipoLocal, golesEquipoVisitante = @pGolesEquipoVisitante WHERE Partidos.IdEquipoLocal = @IdEL and Partidos.IdEquipoVisitante = @IdEV ;";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new { pGolesEquipoLocal = golesEquipoLocal, pGolesEquipoVisitante = golesEquipoVisitante,  IdEL = IdEquipoLocal, IdEV = IdEquipoVisitante });
        }
    }
    public static void BorrarLiga(int IdLiga){
        string sql = "Delete From Partidos where Partidos.IdPartido in (Select Partidos.IdPartido from Partidos where Partidos.IdFecha in (select Fechas.IdFecha from Fechas where Fechas.IdLiga = @IdL)); Delete from Fechas where Fechas.IdLiga = @IdL; delete from Equipos where Equipos.IdLiga = @IdL; delete from Ligas where Ligas.IdLiga = @IdL;";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new { IdL = IdLiga });
        }
    }
    public static int MaxIdL()
    {
        int a = 0;
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connectionString);

        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {

            connection.Open();

            String sql = "SELECT MAX(IdLiga) FROM Ligas";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        a = Convert.ToInt32(reader[0]);
                    }
                }
            }
        }
        return a;
    }
    public static int MaxIdF()
    {
        int a = 0;
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connectionString);

        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {

            connection.Open();

            String sql = "SELECT MAX(IdFecha) FROM Fechas";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        a = Convert.ToInt32(reader[0]);
                    }
                }
            }
        }
        return a;
    }
    public BD(){
    }
} 