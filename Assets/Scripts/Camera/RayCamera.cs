using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCamera : MonoBehaviour
{
    [SerializeField] private GameObject _panelGameOver;
    [SerializeField] private GameObject _panelHome;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Si se hace clic izquierdo
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);  // Se lanza un rayo desde la posición del ratón en la pantalla

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                GameObject player = hit.collider.gameObject;
                PlayerSleep playerSleep = player.GetComponent<PlayerSleep>();
                PlayerHappy playerHappy = player.GetComponent<PlayerHappy>();
                PlayerDead playerDead = player.GetComponent<PlayerDead>();
                if (playerSleep.IsSleeping) { return; }
                if (playerDead.IsDead) {
                    _panelGameOver.SetActive(true);
                    return;
                }

                if (!_panelHome.activeSelf)
                {
                    playerHappy.ActivateHappyFace(true);
                }

                Hapiness hapiness = FindObjectOfType<Hapiness>(); // Obtengo el componente Hapiness del objeto que tiene el script Hapiness ya que busca el primer objeto con el script Hapiness en la escena

                if (hapiness != null && !_panelHome.activeSelf)
                {
                    hapiness.MakeFeelHappyCreature();  // Aumenta la barra de felicidad
                }
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Poo"))
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }
}
