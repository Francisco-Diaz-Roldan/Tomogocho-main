using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCamera : MonoBehaviour
{
    void Update()
    {
        // Si se hace clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            // Lanzar un rayo desde la posición del ratón en la pantalla
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                GameObject player = hit.collider.gameObject;
                PlayerSleep playerSleep = player.GetComponent<PlayerSleep>();
                if (playerSleep.IsSleeping) { return; }
                PlayerHappy playerHappy = player.GetComponent<PlayerHappy>();
                playerHappy.ActivateHappyFace(true);

                // Obtengo el componente Hapiness del objeto que tiene el script Hapiness
                Hapiness hapiness = FindObjectOfType<Hapiness>(); // Esto busca el primer objeto con el script Hapiness en la escena

                if (hapiness != null)
                {
                    // Aumentar la barra de felicidad
                    hapiness.MakeFeelHappyCreature();
                }

                //Debug.Log("He tocado al moñeco");
                //TODO hacer que la barra de felicidad suba y se le ponga cara de uwu mirando a cámara (para esto ultimo hacer animación nueva), para lo de 
                //las particulas corazones hacer unas partículas o un sprite
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Poo"))
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }
}
