using System.Collections;
using UnityEngine;

public class PlayerEating : MonoBehaviour
{
    [SerializeField] GameObject carita_comida;
    [SerializeField] GameObject carita_happy;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void ActivateEatingFace(bool isEating)
    {
        if (isEating)
        {
            _playerMovement.IncrementActiveFaceCount();
            carita_happy.SetActive(false);
            StartCoroutine(DisactivateEatingFace());
        }
        else
        {
            _playerMovement.DecrementActiveFaceCount();
        }
        carita_comida.SetActive(isEating);
        _playerMovement.SetHappyFace(isEating);
    }

    private IEnumerator DisactivateEatingFace()
    {
        yield return new WaitForSeconds(1.5f);
        ActivateEatingFace(false);
    }
}