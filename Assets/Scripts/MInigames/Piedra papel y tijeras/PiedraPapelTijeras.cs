using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PiedraPapelTijeras : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI resultadoText;

    public void SeleccionarJugada(string jugadaJugador)
    {
        // Determinar la jugada de Tomogocho basada en una estrategia simple
        string jugadaTomogocho = DeterminarJugadaTomogocho();

        // Mostrar la jugada de Tomogocho
        Debug.Log("Tomogocho seleccionó: " + jugadaTomogocho);

        // Determinar el resultado del juego
        string resultado = DeterminarResultado(jugadaJugador, jugadaTomogocho);

        // Mostrar el resultado en el texto
        resultadoText.text = resultado;
    }

    string DeterminarJugadaTomogocho()
    {
        // Estrategia simple: Tomogocho elige una jugada al azar la primera vez,
        // luego trata de vencer la última jugada del jugador
        string[] jugadasPosibles = { "Piedra", "Papel", "Tijeras" };

        // Si es la primera jugada, elige al azar
        if (resultadoText.text == "")
        {
            int indiceJugadaInicial = Random.Range(0, jugadasPosibles.Length);
            return jugadasPosibles[indiceJugadaInicial];
        }
        else
        {
            // Intenta vencer la última jugada del jugador
            string jugadaJugadorAnterior = resultadoText.text.Split(' ')[1]; // Obtener la última jugada del jugador
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
}