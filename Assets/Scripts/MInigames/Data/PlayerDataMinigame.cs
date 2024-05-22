using UnityEngine;

public class PlayerDataMinigame : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;

    private void Update()
    {
        _playerData.LifeTimeInSeconds += Time.deltaTime;
    }

}
