using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PiedraPapelTijeras : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resultadoText;
    [SerializeField] private TextMeshProUGUI _partidasText;
    [SerializeField] private GameObject _panelResultado;
    [SerializeField] private GameObject _bocadilloJugadaTomogocho;
    [SerializeField] private Image _imagenJugadaTomogocho;
    [SerializeField] private List<Sprite> _spriteJugadaTomogocho;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _puntoTomogocho1;
    [SerializeField] private Image _puntoTomogocho2;
    [SerializeField] private Image _puntoJugador1;
    [SerializeField] private Image _puntoJugador2;

    private int rondasGanadasJugador = 0;
    private int rondasGanadasTomogocho = 0;
    private int partidasGanadas = 0;
    private int partidasPerdidas = 0;
    private int numeroRonda = 1;
    private Color colorPuntosTomogocho = new Color32(255, 0, 61, 255);
    private Color colorPuntosJugador = new Color32(34, 177, 76, 255);


    private void Start()
    {
        // Cargar las partidas ganadas y perdidas al iniciar el juego
        partidasGanadas = PlayerPrefs.GetInt("PartidasGanadas", 0);
        partidasPerdidas = PlayerPrefs.GetInt("PartidasPerdidas", 0);
        ActualizarContadoresUI();
        OcultarJugadaTomogocho();
        EstablecerAnimaciónJugador();
    }

    public void SeleccionarJugadaJugador(string jugadaJugador)
    {
        // Mostrar el bocadillo de la jugada de Tomogocho
        _bocadilloJugadaTomogocho.SetActive(true);

        // Determinar la jugada de Tomogocho basada en una estrategia simple
        string jugadaTomogocho = DeterminarJugadaTomogocho();

        // Mostrar la jugada de Tomogocho
        MostrarJugadaTomogocho(jugadaTomogocho);

        // Determinar el resultado del juego
        string resultado = DeterminarResultado(jugadaJugador, jugadaTomogocho);

        // Mostrar el resultado de la ronda en el texto
        _resultadoText.text = "Ronda " + numeroRonda + ": " + resultado;

        // Incrementar el número de ronda
        numeroRonda++;

        // Determinar si alguien ha ganado al mejor de 3
        if (resultado == "¡Has ganado la ronda!")
        {
            rondasGanadasJugador++;
            CambiarColorPuntosJugador(rondasGanadasJugador);
        }
        else if (resultado == "Has perdido la ronda")
        {
            rondasGanadasTomogocho++;
            CambiarColorPuntosTomogocho(rondasGanadasTomogocho);
        }

        if (rondasGanadasJugador >= 2 || rondasGanadasTomogocho >= 2)
        {
            // Determinar al ganador al mejor de 3
            if (rondasGanadasJugador >= 2)
            {
                _resultadoText.text = "¡Enhorabuena, has ganado!";
                partidasGanadas++;
            }
            else
            {
                _resultadoText.text = "El todopoderoso Tomogocho te ha derrotado";
                partidasPerdidas++;
            }

            // Guardar las partidas ganadas y perdidas
            GuardarPartida();

            // Incrementar el número de ronda
            numeroRonda++;

            // Actualizar los contadores de partidas en la UI
            ActualizarContadoresUI();

            // Ocultar la jugada de Tomogocho
            OcultarJugadaTomogocho();

            // Mostrar el panel de resultados
            _panelResultado.SetActive(true);

            // Reiniciar los contadores para una nueva partida
            rondasGanadasJugador = 0;
            rondasGanadasTomogocho = 0;
            ReiniciarColoresPuntosJugador();
            ReiniciarColoresPuntosTomogocho();
        }
    }

    void ActualizarContadoresUI()
    {
        _partidasText.text = "Partidas\nGanadas: " + partidasGanadas + "\nPerdidas: " + partidasPerdidas;
    }

    void GuardarPartida()
    {
        PlayerPrefs.SetInt("PartidasGanadas", partidasGanadas);
        PlayerPrefs.SetInt("PartidasPerdidas", partidasPerdidas);
        PlayerPrefs.Save();
    }

    string DeterminarJugadaTomogocho()
    {
        string[] jugadasPosibles = { "Piedra", "Papel", "Tijeras" };

        // Si es la primera jugada, elige al azar
        if (numeroRonda == 1)
        {
            int indiceJugadaInicial = Random.Range(0, jugadasPosibles.Length);
            return jugadasPosibles[indiceJugadaInicial];
        }
        else
        {
            // Intenta vencer la última jugada del jugador
            string jugadaJugadorAnterior = ObtenerUltimaJugadaJugador();

            switch (jugadaJugadorAnterior)
            {
                case "Piedra":
                    return "Papel";
                case "Papel":
                    return "Tijeras";
                case "Tijeras":
                    return "Piedra";
                default:
                    return jugadasPosibles[Random.Range(0, jugadasPosibles.Length)]; // En caso de error, elige al azar
            }
        }
    }

    string ObtenerUltimaJugadaJugador()
    {
        string[] palabrasResultado = _resultadoText.text.Split(' ');

        if (palabrasResultado.Length < 2)
        {
            // Si el texto no está en el formato esperado, devuelve una jugada aleatoria
            return "Piedra"; // O cualquier otra jugada por defecto
        }

        return palabrasResultado[1]; // Obtener la última jugada del jugador
    }

    string DeterminarResultado(string jugadaJugador, string jugadaTomogocho)
    {
        if (jugadaJugador == jugadaTomogocho)
        {
            return "Empate";
        }
        else if ((jugadaJugador == "Piedra" && jugadaTomogocho == "Tijeras") ||
                 (jugadaJugador == "Papel" && jugadaTomogocho == "Piedra") ||
                 (jugadaJugador == "Tijeras" && jugadaTomogocho == "Papel"))
        {
            return "¡Has ganado la ronda!";
        }
        else
        {
            return "Has perdido la ronda";
        }
    }

    public void AsignarPiedra()
    {
        if (!_panelResultado.activeSelf) SeleccionarJugadaJugador("Piedra");
    }

    public void AsignarPapel()
    {
        if (!_panelResultado.activeSelf) SeleccionarJugadaJugador("Papel");
    }

    public void AsignarTijeras()
    {
        if (!_panelResultado.activeSelf) SeleccionarJugadaJugador("Tijeras");
    }

    void MostrarJugadaTomogocho(string jugadaTomogocho)
    {
        _bocadilloJugadaTomogocho.SetActive(true);
        OcultarJugadaTomogocho(); // Ocultar la imagen antes de mostrar la nueva
        switch (jugadaTomogocho)
        {
            case "Piedra":
                _imagenJugadaTomogocho.sprite = _spriteJugadaTomogocho[0];
                break;
            case "Papel":
                _imagenJugadaTomogocho.sprite = _spriteJugadaTomogocho[1];
                break;
            case "Tijeras":
                _imagenJugadaTomogocho.sprite = _spriteJugadaTomogocho[2];
                break;
        }
        _imagenJugadaTomogocho.gameObject.SetActive(true); // Mostrar la imagen seleccionada
    }

    void OcultarJugadaTomogocho()
    {
        //_bocadilloJugadaTomogocho.SetActive(false);
        _imagenJugadaTomogocho.gameObject.SetActive(false);
    }

    public void ReiniciarRondas()
    {
        rondasGanadasJugador = 0;
        rondasGanadasTomogocho = 0;
        _resultadoText.text = "Elige: Piedra, Papel o Tijeras";
        ReiniciarColoresPuntosTomogocho();
    }

    public void ResetData()
    {
        // Guardar las partidas ganadas y perdidas antes de resetear
        PlayerPrefs.SetInt("PartidasGanadas", partidasGanadas);
        PlayerPrefs.SetInt("PartidasPerdidas", partidasPerdidas);
        PlayerPrefs.Save();

        // Reiniciar todas las variables
        rondasGanadasJugador = 0;
        rondasGanadasTomogocho = 0;
        partidasGanadas = 0;
        partidasPerdidas = 0;
        numeroRonda = 1;

        // Reiniciar el texto de resultado
        _resultadoText.text = "Elige: Piedra, Papel o Tijeras";

        // Actualizar los contadores en la UI
        ActualizarContadoresUI();

        // Ocultar el panel de resultados si está activo
        if (_panelResultado.activeSelf)
        {
            _panelResultado.SetActive(false);
        }

        // Ocultar la jugada de Tomogocho
        OcultarJugadaTomogocho();

        // Reiniciar los colores de los puntos de Tomogocho
        ReiniciarColoresPuntosTomogocho();
    }

    private void EstablecerAnimaciónJugador()
    {
        _animator.SetFloat("Y", -1f);
        _animator.SetFloat("X", 0f);
        _animator.SetFloat("Speed", 0f);
    }

    private void CambiarColorPuntosTomogocho(int rondasGanadas)
    {
        switch (rondasGanadas)
        {
            case 1:
                _puntoTomogocho1.color = colorPuntosTomogocho;
                break;
            case 2:
                _puntoTomogocho2.color = colorPuntosTomogocho;
                break;
        }
    }
    private void CambiarColorPuntosJugador(int rondasGanadas)
    {
        switch (rondasGanadas)
        {
            case 1:
                _puntoJugador1.color = colorPuntosJugador;
                break;
            case 2:
                _puntoJugador2.color = colorPuntosJugador;
                break;
        }
    }

    private void ReiniciarColoresPuntosTomogocho()
    {
        _puntoTomogocho1.color = Color.white;
        _puntoTomogocho2.color = Color.white;
    }
    private void ReiniciarColoresPuntosJugador()
    {
        _puntoJugador1.color = Color.white;
        _puntoJugador2.color = Color.white;
    }
}
