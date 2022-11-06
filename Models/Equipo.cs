namespace TP08.Models;

public class Equipo{
    private int _IdEquipo;
    private string _Nombre;
    private string _Escudo;
    private int _IdLiga;

    public Equipo(string nombre, string escudo, int idLiga){
        _Nombre = nombre;
        _Escudo = escudo;
        _IdLiga = idLiga;
    }
    public Equipo(int idEquipo, string nombre, string escudo, int idLiga){
        _IdEquipo = idEquipo;
        _Nombre = nombre;
        _Escudo = escudo;
        _IdLiga = idLiga;
    }

    public Equipo(){
        
    }

    public int IdEquipo{
        get{
            return _IdEquipo;
        } set{
            _IdEquipo = value;
        }
    }

    public string Nombre{
        get{
            return _Nombre;
        } set{
            _Nombre = value;
        }
    }
    public string Escudo{
        get{
            return _Escudo;
        } set{
            _Escudo = value;
        }
    }

    public int IdLiga{
        get{
            return _IdLiga;
        } set{
            _IdLiga = value;
        }
    }
} 