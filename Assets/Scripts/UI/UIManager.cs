using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NS_UIManager
{
    public class UIManager : MonoBehaviour
    {
        private GameObject _canvas;
        private GameObject _powerBarFill;
        private Image _powerBarFillImage;
        static private bool _readPower = false;
        private NS_Player.Player _player;

        // Start is called before the first frame update
        void Start()
        {
            _canvas = transform.Find("Canvas").gameObject;
            _powerBarFill = _canvas.transform.Find("PowerBar_Interior").gameObject;

            if (_powerBarFill == null)
            {
                Debug.Log("powerBarInterior Not Found!");
            }

            _powerBarFillImage = _powerBarFill.GetComponent<Image>();

            _player = GameObject.Find("Player").GetComponent<NS_Player.Player>();

            if (_canvas == null)
            {
                Debug.Log("No Canvas in UI!");
            }

            if (_player == null)
            {
                Debug.Log("Player Not Found!");
            }
        }
       
        void Update()
        {
            if (_readPower)
            {
                var jumpForce = ReadPowerFromPlayer();
                _powerBarFillImage.fillAmount = jumpForce;
            }
        }

        float ReadPowerFromPlayer()
        {
            return _player.GetJumpPowerNormalized();
        }

        static public void ActivatePowerBar()
        {
            _readPower = true;
        }
    }
}
