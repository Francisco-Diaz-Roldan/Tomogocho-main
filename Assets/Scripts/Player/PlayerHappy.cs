using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHappy : MonoBehaviour
{
    [SerializeField] GameObject carita;
    [SerializeField] GameObject carita_carita_comida;
    [SerializeField] GameObject contenedorCorazones;
    [SerializeField] Image porcentajeFelicidad;
    [SerializeField] private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void ActivateHappyFace(bool isHappy)
    {
        carita.SetActive(isHappy);
        contenedorCorazones.SetActive(isHappy);
        _playerMovement.SetHappyFace(isHappy);

        if (isHappy)
        {
            _playerMovement.IncrementActiveFaceCount();
            StartCoroutine(SetToUnhappy());
            carita_carita_comida.SetActive(false);
            porcentajeFelicidad.fillAmount = Mathf.Min(1, porcentajeFelicidad.fillAmount + 0.05f);
        }
        else
        {
            _playerMovement.DecrementActiveFaceCount();
        }
    }

    private IEnumerator SetToUnhappy()
    {
        yield return new WaitForSeconds(1.5f);
        ActivateHappyFace(false);
    }
}