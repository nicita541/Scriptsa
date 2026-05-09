using Assets.Scripts.Core;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterStats))]
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D rigidbody2D;
        private Vector2 movement;
        private Animator animator;
        private CharacterStats playerStats;
        private SpriteRenderer spriteRenderer;

        public Vector2 Movement => movement;

        [SerializeField] private bool sideAnimationFacesRight = true;

        private float LastX;
        private float LastY = -1;


        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            playerStats = GetComponent<CharacterStats>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            movement = movement.normalized;

            if (movement.x != 0 || movement.y != 0)
            {
                LastX = movement.x;
                LastY = movement.y;
            }

            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            animator.SetFloat("LastX", LastX);
            animator.SetFloat("LastY", LastY);

            UpdateSpriteFlip();
        }

        private void UpdateSpriteFlip()
        {
            if (spriteRenderer == null) return;

            if (Mathf.Abs(movement.y) > 0.01f)
            {
                spriteRenderer.flipX = false;
                return;
            }

            if (Mathf.Abs(movement.x) <= 0.01f) return;

            spriteRenderer.flipX = sideAnimationFacesRight ? movement.x < 0 : movement.x > 0;
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.gamestate != GameState.Playing) return;

            rigidbody2D.MovePosition(rigidbody2D.position + movement * playerStats.MoveSpeed * Time.fixedDeltaTime);
        }
    }
}
