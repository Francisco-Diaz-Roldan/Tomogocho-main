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

    private Color _colorPuntosTomogocho = new Color32(255, 0, 61, 255);
    private Color _colorPuntosJugador = new Color32(34, 177, 76, 255);
    private Color _colorEmpate = new Color32(255, 148, 0, 255);

    private AudioSource _audioSource;

    public bool _partidaTerminada = false;
    private bool _esEmpate = false;
    private int _rondasGanadasJugador = 0;
    private int _rondasGanadasTomogocho = 0;
    private int _partidasGanadas = 0;
    private int _partidasPerdidas = 0;
    private int _numeroRonda = 1;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Cargar las partidas ganadas y perdidas al iniciar el juego
        _partidasGanadas = PlayerPrefs.GetInt("PartidasGanadas", 0);
        _partidasPerdidas = PlayerPrefs.GetInt("PartidasPerdidas", 0);
        ActualizarContadoresUI();
        OcultarJugadaTomogocho();
        EstablecerAnimaciónJugador();
    }

    public void SeleccionarJugadaJugador(string jugadaJugador)
    {
        if (_esEmpate)
        {
            ReiniciarColoresEmpate();
            _esEmpate = false;
        }

        _bocadilloJugadaTomogocho.SetActive(true);
        string jugadaTomogocho = DeterminarJugadaTomogocho();
        MostrarJugadaTomogocho(jugadaTomogocho);
        string resultado = DeterminarResultado(jugadaJugador, jugadaTomogocho);
        _resultadoText.text = "Ronda " + _numeroRonda + ": " + resultado;
        _numeroRonda++;

        // Comprobamos si alguien ha ganado al mejor de 3
        if (resultado == "¡Has ganado la ronda!")
        {
            _rondasGanadasJugador++;
            CambiarColorPuntosJugador(_rondasGanadasJugador);
        }
        else if (resultado == "Has perdido la ronda")
        {
            _rondasGanadasTomogocho++;
            CambiarColorPuntosTomogocho(_rondasGanadasTomogocho);
        }
        else if (resultado == "Empate")
        {
            _esEmpate = true;
            CambiarColorPuntosEmpate();
        }

        if (_rondasGanadasJugador >= 2 || _rondasGanadasTomogocho >= 2)
        {
            _partidaTerminada = true;
            if (_rondasGanadasJugador >= 2)
            {
                _resultadoText.text = "¡Enhorabuena, has ganado!";
                _partidasGanadas++;
                _audioSource.PlayOneShot(_victorySound);
            }
            else
            {
                _resultadoText.text = "El todopoderoso Tomogocho te ha derrotado";
                _partidasPerdidas++;
                _audioSource.PlayOneShot(_defeatSound);
            }

            GuardarPartida();
            _numeroRonda++;
            ActualizarContadoresUI();
            OcultarJugadaTomogocho();
            _panelResultado.SetActive(true);

            _rondasGanadasJugador = 0;
            _rondasGanadasTomogocho = 0;
            ReiniciarColoresPuntosJugador();
            ReiniciarColoresPuntosTomogocho();
        }
    }

    void ActualizarContadoresUI()
    {
        _partidasText.text = "Partidas\nGanadas: " + _partidasGanadas + "\nPerdidas: " + _partidasPerdidas;
    }

    void GuardarPartida()
    {
        PlayerPrefs.SetInt("PartidasGanadas", _partidasGanadas);
        PlayerPrefs.SetInt("PartidasPerdidas", _partidasPerdidas);
        PlayerPrefs.Save();
    }

    string DeterminarJugadaTomogocho()
    {
        string[] jugadasPosibles = { "Piedra", "Papel", "Tijeras" };

        // Si es la primera jugada, elige al azar
        if (_numeroRonda == 1)
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
        _rondasGanadasJugador = 0;
        _rondasGanadasTomogocho = 0;
        _resultadoText.text = "Elige: Piedra, Papel o Tijeras";
        ReiniciarColoresPuntosTomogocho();
    }

    public void ResetData()
    {
        // Guardo las partidas ganadas y perdidas antes de resetear
        PlayerPrefs.SetInt("PartidasGanadas", _partidasGanadas);
        PlayerPrefs.SetInt("PartidasPerdidas", _partidasPerdidas);
        PlayerPrefs.Save();

        _rondasGanadasJugador = 0;
        _rondasGanadasTomogocho = 0;
        _partidasGanadas = 0;
        _partidasPerdidas = 0;
        _numeroRonda = 1;

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
                _puntoTomogocho1.color = _colorPuntosTomogocho;
                break;
            case 2:
                _puntoTomogocho2.color = _colorPuntosTomogocho;
                break;
        }
    }
    private void CambiarColorPuntosJugador(int rondasGanadas)
    {
        switch (rondasGanadas)
        {
            case 1:
                _puntoJugador1.color = _colorPuntosJugador;
                break;
            case 2:
                _puntoJugador2.color = _colorPuntosJugador;
                break;
        }
    }

    private void CambiarColorPuntosEmpate()
    {
        _puntoJugador1.color = _colorEmpate;
        _puntoTomogocho1.color = _colorEmpate;
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

    private void ReiniciarColoresEmpate()
    {
        ReiniciarColoresPuntosJugador();
        ReiniciarColoresPuntosTomogocho();
    }
}