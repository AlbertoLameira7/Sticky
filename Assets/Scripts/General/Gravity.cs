using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NS_Gravity
{
    public class Gravity : MonoBehaviour
    {
        [SerializeField]
        private float _gravityMultiplier = 1.0f;
        private float _acceleration = 9.8f;
        private bool _isGrounded;
        private Rigidbody2D _rigidBody;
        private bool _isGravityActive = true;

        // Start is called before the first frame update
        void Start()
        {
            _rigidBody = gameObject.GetComponent<Rigidbody2D>();

            if (_rigidBody == null)

            {
                Debug.Log("No RigidBody Attached to GameObject");
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Apply gravity if object isn't on ground and gravity is enabled
            if (_isGravityActive && !_isGrounded)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y + (_acceleration * Time.deltaTime * _gravityMultiplier) * -1);
            }
        }

        public void Deactivate()
        {
            _isGravityActive = false;
        }

        public void Activate()
        {
            _isGravityActive = true;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag == "Ground" && _isGravityActive)
            {
                _isGrounded = true;
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.tag == "Ground" && _isGravityActive)
            {
                _isGrounded = false;
            }
        }
    }
}
