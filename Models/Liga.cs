namespace TP08.Models;

public class Liga{
    private int _IdLiga;
    private string _Nombre;
    private string _Logo;
    private List<Equipo> _Equipos = new List<Equipo>();

    private List<Fecha> _Fixture = new List<Fecha>();

    public Liga(int idLiga, string nombre, string logo){
        _IdLiga = idLiga;
        _Nombre = nombre;
        _Logo = logo;
    }

    public Liga(){

    }

    public int IdLiga{
        get{
            return _IdLiga;
        } set{
            _IdLiga = value;
        }
    }
    public string Nombre{
        get{
            return _Nombre;
        } set{
            _Nombre = value;
        }
    }
    public string Logo{
        get{
            return _Logo;
        } set{
            _Logo = value;
        }
    }
    public List<Fecha> Fixture{
        get{
            return _Fixture;
        } set{
            _Fixture = value;
        }
    }

    public List<Equipo> Equipos { get => _Equipos; set => _Equipos = value; }

    public void AgregarEquipo(string nombre, string escudo)
    {
        _Equipos.Add(new Equipo(nombre, escudo, _IdLiga));
    }

    public void ArmarFixture(){

        _Fixture = new List<Fecha>();
        DateTime fechaInicial = new DateTime(2022,1,1);
        Console.WriteLine(_Equipos.Count());
        for (int i = 0; i < _Equipos.Count() - 1; i++)
        {
            _Fixture.Add(new Fecha(i));
            _Fixture[i].AgregarPartido(new Partido(_Equipos[i], _Equipos[_Equipos.Count()-1],  fechaInicial.AddDays(i*7)));
            for (int j = 1; j <= (_Equipos.Count()-1)/2; j++)
            {
                if(((i + j) % (_Equipos.Count()-1)) != (((_Equipos.Count()-1) - j + i) % (_Equipos.Count()-1))){
                    _Fixture[i].AgregarPartido(new Partido(_Equipos[(i + j) % (_Equipos.Count()-1)], _Equipos[((_Equipos.Count()-1) - j + i) % (_Equipos.Count()-1)],  fechaInicial.AddDays(i*7)));
                }
               
            }
            
        }
    }

} 