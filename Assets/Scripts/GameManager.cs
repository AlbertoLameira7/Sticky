using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NS_GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerManager, _UIManager;
        private NS_PlayerManager.PlayerManager _playerManagerComponent;
        private NS_UIManager.UIManager _UIManagerComponent;
        static private int _currentLevel;

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

        static public void currentLevelFinished()
        {
            // add scoring points to player and change scene to next level or end title
            // TODO: add level select menu
            if (_currentLevel < SceneManager.sceneCountInBuildSettings) // needs a better solution, no difference in "menu scene" and "level scene"
            {
                _currentLevel++;
                LoadNextLevel();
            }
            else if (_currentLevel >= SceneManager.sceneCountInBuildSettings)
            {
                LoadEndGame();
            }
        }

        static void LoadEndGame()
        {
            SceneManager.LoadScene("EndGame");
        }

        static void LoadNextLevel()
        {
            SceneManager.LoadScene(_currentLevel);
        }
    }
}

