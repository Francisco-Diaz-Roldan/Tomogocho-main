using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHappy : MonoBehaviour
{
    [SerializeField] GameObject carita;
    [SerializeField] GameObject contenedorCorazones;
    [SerializeField] Image porcentajeFelicidad;
    private PlayerMovement _playerMovement;

    public void ActivateHappyFace(bool isHappy)
    {
        carita.SetActive(isHappy);
        contenedorCorazones.SetActive(isHappy);
        _playerMovement.SetHappyFace(isHappy);
        if (isHappy){
            StartCoroutine(SetToUnhappy());
            porcentajeFelicidad.fillAmount = Mathf.Min(1, porcentajeFelicidad.fillAmount + 0.05f); //La barra de porcentaje no pasa de 1
        }
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private IEnumerator SetToUnhappy()
    {
        yield return new WaitForSeconds(2f);
        ActivateHappyFace(false);
    }
}
