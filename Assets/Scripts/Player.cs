using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace NS_Player
{
    public class Player : MonoBehaviour
    {
        private GameObject _arrow;
        private bool _isReadyToJump;
        public float _jumpForce;
        private float _lerp;
        private bool _isSticky;
        private Vector2 _positionSticky;
        private Rigidbody2D _rb;
        [SerializeField]
        private float _minimumJumpForce = 200.0f, _maximumJumpForce = 1500.0f;
        private AudioSource _audio;
        private NS_Gravity.Gravity _gravity;
        private float _angle;

        // Start is called before the first frame update
        void Start()
        {
            _arrow = transform.Find("Arrow_Container").gameObject;
            _rb = transform.GetComponent<Rigidbody2D>();
            _audio = transform.GetComponent<AudioSource>();
            _gravity = transform.GetComponent<NS_Gravity.Gravity>();

            if (_arrow == null)
            {
                Debug.Log("No Arrow Container attached!");
            }

            if (_rb == null)
            {
                Debug.Log("No Rigidbody attached to player!");
            }

            if (_gravity == null)
            {
                Debug.Log("No Gravity Component attached!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            ArrowContainer();

            if (Input.GetButton("Fire1"))
            {
                ChargeJump();
            }

            if (_isReadyToJump && Input.GetButtonUp("Fire1"))
            {
                Jump();
            }

            if (_isSticky)
            {
                transform.position = _positionSticky;
            }
        }

        void ChargeJump()
        {
            NS_UIManager.UIManager.ActivatePowerBar();
            _jumpForce = Mathf.Lerp(_minimumJumpForce, _maximumJumpForce, _lerp);
            _lerp += 0.3f * Time.deltaTime;
        }

        public float GetJumpPowerNormalized()
        {
            //return jumpPower from 0 to 1, 0 being minimumJumpForce and 1 maximumJumpForce
            return (_jumpForce - _minimumJumpForce) / (_maximumJumpForce - _minimumJumpForce);
        }

        void Jump()
        {
            _gravity.Activate();
            _isSticky = false;
            Vector2 direction = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad));
            direction.Normalize();
            gameObject.GetComponent<Rigidbody2D>().AddForce(direction * _jumpForce);
            _lerp = 0;
            _audio.Play();
        }

        void ArrowContainer()
        {
            var mouse = Input.mousePosition;
            var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
            _angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            _arrow.transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag == "Sticky" || other.collider.tag == "Ground")
            {
                _isReadyToJump = true;
                _isSticky = true;
                _positionSticky = transform.position;
                _gravity.Deactivate();
                _rb.velocity = new Vector2(0, 0);
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.tag == "Ground")
            {
                _isReadyToJump = false;
            }
        }
    }
}

