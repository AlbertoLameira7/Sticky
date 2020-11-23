using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace NS_PlayerManager
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerPrefab;
        static private int _playerCount;
        [SerializeField]
        private Vector2 _defaultPlayerRespawnPos;
        [SerializeField]
        private GameObject _playerRespawnPos;
        private GameObject _currentPlayer;

        public int GetPlayerCount()
        {
            return _playerCount;
        }

        public GameObject GetCurrentPlayer()
        {
            return _currentPlayer;
        }

        public void SpawnPlayer()
        {
            Vector2 spawnLocation = _defaultPlayerRespawnPos;

            if (_playerRespawnPos != null)
            {
                spawnLocation = _playerRespawnPos.transform.position;
            }

            _currentPlayer = Instantiate(_playerPrefab, spawnLocation, Quaternion.identity);

            if (_currentPlayer == null)
            {
                return;
            }

            _playerCount++;
        }

        static public void KillCurrentPlayer(GameObject currentPlayer)
        {
            Destroy(currentPlayer);
            _playerCount--;
        }
    }
}
