using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerManager, _UIManager;
    private NS_PlayerManager.PlayerManager _playerManagerComponent;
    private NS_UIManager.UIManager _UIManagerComponent;

    // Start is called before the first frame update
    void Start()
    {
        _playerManagerComponent = _playerManager.GetComponent<NS_PlayerManager.PlayerManager>();
        _UIManagerComponent = _UIManager.GetComponent<NS_UIManager.UIManager>();

        SetupPlayer();
    }

    void SetupPlayer()
    {
        _playerManagerComponent.SpawnPlayer();
        _UIManagerComponent.SetCurrentPlayer(_playerManagerComponent.GetCurrentPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerManagerComponent.GetPlayerCount() <= 0)
        {
            SetupPlayer();
        }
    }
}
