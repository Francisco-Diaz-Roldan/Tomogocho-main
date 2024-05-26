using UnityEngine;

public class PlayerSleep : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerDead _playerDead;
    [SerializeField] GameObject carita_happy;
    [SerializeField] GameObject carita_comida;
    [SerializeField] private Egg _egg;

    private bool _isSleeping = false;
    
    public bool IsSleeping => _isSleeping; //Hago un Get de la propiedad privada y no puede editarse porque voy a poder acceder desde cualquier script y que por seguridad solo se pueda activar desde aquí

    public void ChangeSleepState(bool isSleeping)
    {
        if (_playerDead != null && !_playerDead.IsDead)
        {
            if (_egg == null || !_egg.IsEgg())
            {
                _isSleeping = isSleeping;
            }
        }
        else
        {
            isSleeping = false; // Fuerzo el estado de sueño a falso si el jugador está muerto
        }
        animator.SetBool("SleepTime", isSleeping);
        if (_isSleeping)
        {
            carita_happy.SetActive(false);
            carita_comida.SetActive(false);
        }
    }
}