using UnityEngine;

public class PlayerTimeLife : MonoBehaviour
{
    [SerializeField] private PlayerDead _playerDead;
    [SerializeField] private PlayerData _playerData;

    void Update()
    {
        if (_playerData != null && (_playerDead != null && !_playerDead.IsDead)) {
            _playerData.LifeTimeInSeconds += Time.deltaTime;
        }
    }
}