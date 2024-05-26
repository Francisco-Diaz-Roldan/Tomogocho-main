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
    [SerializeField] private AudioClip _victorySound;
    [SerializeField] private AudioClip _defeatSound;

    private Color colorPuntosTomogocho = new Color32(255, 0, 61, 255);
    private Color colorPuntosJugador = new Color32(34, 177, 76, 255);
    private AudioSource _audioSource;

    public bool partidaTerminada = false;
    private int rondasGanadasJugador = 0;
    private int rondasGanadasTomogocho = 0;
    private int partidasGanadas = 0;
    private int partidasPerdidas = 0;
    private int numeroRonda = 1;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Cargar las partidas ganadas y perdidas al iniciar el juego
        partidasGanadas = PlayerPrefs.GetInt("PartidasGanadas", 0);
        partidasPerdidas = PlayerPrefs.GetInt("PartidasPerdidas", 0);
        ActualizarContadoresUI();
        OcultarJugadaTomogocho();
        EstablecerAnimaciónJugador();
    }

    public void SeleccionarJugadaJugador(string jugadaJugador)
    {
        _bocadilloJugadaTomogocho.SetActive(true);
        string jugadaTomogocho = DeterminarJugadaTomogocho();
        MostrarJugadaTomogocho(jugadaTomogocho);
        string resultado = DeterminarResultado(jugadaJugador, jugadaTomogocho);
        _resultadoText.text = "Ronda " + numeroRonda + ": " + resultado;
        numeroRonda++;

        // Comprobamos si alguien ha ganado al mejor de 3
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
            partidaTerminada = true;
            if (rondasGanadasJugador >= 2)
            {
                _resultadoText.text = "¡Enhorabuena, has ganado!";
                partidasGanadas++;
                _audioSource.PlayOneShot(_victorySound);
            }
            else
            {
                _resultadoText.text = "El todopoderoso Tomogocho te ha derrotado";
                partidasPerdidas++;
                _audioSource.PlayOneShot(_defeatSound);
            }

            GuardarPartida();
            numeroRonda++;
            ActualizarContadoresUI();
            OcultarJugadaTomogocho();
            _panelResultado.SetActive(true);

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

        if (palabrasResultado.Length < 2) // Si el texto está en otro formato distinto al esperado
        {
            return "Piedra";
        }

        return palabrasResultado[1]; // Se obtiene la última jugada del jugador
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
        OcultarJugadaTomogocho();
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
        _imagenJugadaTomogocho.gameObject.SetActive(true);
    }

    void OcultarJugadaTomogocho()
    {
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
        // Guardo las partidas ganadas y perdidas antes de resetear
        PlayerPrefs.SetInt("PartidasGanadas", partidasGanadas);
        PlayerPrefs.SetInt("PartidasPerdidas", partidasPerdidas);
        PlayerPrefs.Save();

        rondasGanadasJugador = 0;
        rondasGanadasTomogocho = 0;
        partidasGanadas = 0;
        partidasPerdidas = 0;
        numeroRonda = 1;

        _resultadoText.text = "Elige: Piedra, Papel o Tijeras";

        ActualizarContadoresUI();

        if (_panelResultado.activeSelf)
        {
            _panelResultado.SetActive(false);
        }

        OcultarJugadaTomogocho();
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