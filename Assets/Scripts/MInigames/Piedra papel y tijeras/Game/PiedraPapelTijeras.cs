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

    private int rondasGanadasJugador = 0;
    private int rondasGanadasTomogocho = 0;
    private int partidasGanadas = 0;
    private int partidasPerdidas = 0;

    private void Start()
    {
        // Cargar las partidas ganadas y perdidas al iniciar el juego
        partidasGanadas = PlayerPrefs.GetInt("PartidasGanadas", 0);
        partidasPerdidas = PlayerPrefs.GetInt("PartidasPerdidas", 0);
        ActualizarContadoresUI();
        OcultarJugadaTomogocho();
        EstablecerAnimaciónJugador();
    }

    private void AsignarJugadaJugador(string jugadaJugador)
    {
        Debug.Log("El jugador seleccionó: " + jugadaJugador);
    }

    public void SeleccionarJugada(string jugadaJugador)
    {
        AsignarJugadaJugador(jugadaJugador);

        // Determinar la jugada de Tomogocho basada en una estrategia simple
        string jugadaTomogocho = DeterminarJugadaTomogocho();

        // Mostrar la jugada de Tomogocho
        MostrarJugadaTomogocho(jugadaTomogocho);
        Debug.Log("Tomogocho seleccionó: " + jugadaTomogocho);

        // Determinar el resultado del juego
        string resultado = DeterminarResultado(jugadaJugador, jugadaTomogocho);

        // Mostrar el resultado de la ronda en el texto
        _resultadoText.text = "Ronda: " + resultado;

        // Determinar si alguien ha ganado al mejor de 3
        if (resultado == "¡Ganaste!")
        {
            rondasGanadasJugador++;
        }
        else if (resultado == "¡Perdiste!")
        {
            rondasGanadasTomogocho++;
        }

        if (rondasGanadasJugador >= 2 || rondasGanadasTomogocho >= 2)
        {
            // Determinar al ganador al mejor de 3
            if (rondasGanadasJugador >= 2)
            {
                _resultadoText.text = "¡Has ganado al mejor de 3!";
                partidasGanadas++;
            }
            else
            {
                _resultadoText.text = "El todopoderoso Tomogocho te ha vencido al mejor de 3.";
                partidasPerdidas++;
            }

            // Guardar las partidas ganadas y perdidas
            GuardarPartida();

            // Actualizar los contadores de partidas en la UI
            ActualizarContadoresUI();

            // Ocultar la jugada de Tomogocho
            OcultarJugadaTomogocho();

            // Mostrar el panel de resultados
            _panelResultado.SetActive(true);

            // Reiniciar los contadores para una nueva partida
            rondasGanadasJugador = 0;
            rondasGanadasTomogocho = 0;
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
        if (_resultadoText.text == "" || !_resultadoText.text.Contains(" "))
        {
            int indiceJugadaInicial = Random.Range(0, jugadasPosibles.Length);
            return jugadasPosibles[indiceJugadaInicial];
        }
        else
        {
            // Intenta vencer la última jugada del jugador
            string[] palabrasResultado = _resultadoText.text.Split(' ');

            if (palabrasResultado.Length < 2)
            {
                // Si el texto no está en el formato esperado, elige al azar
                int indiceJugadaInicial = Random.Range(0, jugadasPosibles.Length);
                return jugadasPosibles[indiceJugadaInicial];
            }

            string jugadaJugadorAnterior = palabrasResultado[1]; // Obtener la última jugada del jugador
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
            return "¡Ganaste!";
        }
        else
        {
            return "¡Perdiste!";
        }
    }

    public void AsignarPiedra()
    {
        if (!_panelResultado.activeSelf) SeleccionarJugada("Piedra");
    }

    public void AsignarPapel()
    {
        if (!_panelResultado.activeSelf) SeleccionarJugada("Papel");
    }

    public void AsignarTijeras()
    {
        if (!_panelResultado.activeSelf) SeleccionarJugada("Tijeras");
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
    }
    private void EstablecerAnimaciónJugador()
    {
        _animator.SetFloat("Y", -1f);
        _animator.SetFloat("X", 0f);
        _animator.SetFloat("Speed", 0f);
    }
}