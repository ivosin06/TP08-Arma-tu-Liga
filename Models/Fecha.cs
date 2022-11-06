namespace TP08.Models;

public class Fecha{
    private int _Numero;

    private List<Partido> _Partidos = new List<Partido>();

    public Fecha (int numero){
        _Numero = numero + 1;
    }

    public int Numero { get => _Numero; set => _Numero = value; }
    public List<Partido> Partidos { get => _Partidos; set => _Partidos = value; }

    public void AgregarPartido(Partido partido) {
        _Partidos.Add(partido);
    }
} 