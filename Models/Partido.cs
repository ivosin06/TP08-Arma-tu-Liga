namespace TP08.Models;

public class Partido{
    private int _IdPartido;
    private Equipo _EquipoLocal;
    private Equipo _EquipoVisitante;
    private int _GolesEquipoLocal;
    private int _GolesEquipoVisitante;
    private DateTime _FechaPartido;

    public Partido (Equipo equipoLocal, Equipo equipoVisitante, DateTime fechaPartido){
        _EquipoLocal = equipoLocal;
        _EquipoVisitante = equipoVisitante;
        _FechaPartido = fechaPartido;
    }

    public Partido(int idEquipoLocal, int idEquipoVisitante, int golesEquipoLocal, int golesEquipoVisitante, DateTime fechaPartido){
        _GolesEquipoLocal = golesEquipoLocal;
        _GolesEquipoVisitante = golesEquipoVisitante;
        _FechaPartido = fechaPartido;
        _EquipoLocal = BD.VerInfoEquipo(idEquipoLocal);
        _EquipoVisitante = BD.VerInfoEquipo(idEquipoVisitante);
    }

    public int IdPartido{
        get{return _IdPartido;
        }set{ 
            _IdPartido = value;
        }
    }
    public Equipo EquipoLocal{ 
        get{return _EquipoLocal;
        }set{
            _EquipoLocal = value;
        }    
    } 
        public Equipo EquipoVisitante{ 
        get{return _EquipoVisitante;
        }set{
            _EquipoVisitante = value;
        }    
    } 
    public int GolesEquipoLocal{ 
        get{return _GolesEquipoLocal;
        }set{
            _GolesEquipoLocal = value;
        }    
    } 
        public int GolesEquipoVisitante{ 
        get{return _GolesEquipoVisitante;
        }set{
            _GolesEquipoVisitante = value;
        }    
    }     
    public DateTime FechaPartido{ 
        get{return _FechaPartido;
        }set{
            _FechaPartido = value;
        }    
    } 
} 