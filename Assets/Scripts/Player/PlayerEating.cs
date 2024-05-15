using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEating : MonoBehaviour
{
    [SerializeField] GameObject carita_comida;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void ActivateEatingFace(bool isHappy)
    {
        carita_comida.SetActive(isHappy);
        _playerMovement.SetHappyFace(isHappy);
    }
    private IEnumerator DisctivateEatingFace()
    {
        yield return new WaitForSeconds(1.5f);
        ActivateEatingFace(false);
    }
}
