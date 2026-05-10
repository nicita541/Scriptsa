using Assets.Scripts.Core;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterStats))]
    public class EnemyMove : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 movement;
        private Animator animator;
        private CharacterStats enemyStats;
        private SpriteRenderer spriteRenderer;

        public Vector2 Movement => movement;

        [Header("Animation")]
        [SerializeField] private bool sideAnimationFacesRight = true;

        [Header("Wander")]
        [SerializeField] private WanderZoneBuilder2D wanderZone;
        [SerializeField] private float reachDistance = 0.1f;
        [SerializeField] private float waitTime = 1f;

        private Vector2 targetPoint;
        private float waitTimer;
        private bool waiting;
        private bool movementLocked;

        private float LastX;
        private float LastY = -1;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            enemyStats = GetComponent<CharacterStats>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            PickNewTarget();
        }

        private void Update()
        {
            if (GameManager.Instance.gamestate != GameState.Playing)
            {
                movement = Vector2.zero;
                UpdateAnimator();
                return;
            }

            if (movementLocked)
            {
                movement = Vector2.zero;
                UpdateAnimator();
                UpdateSpriteFlip();
                return;
            }

            if (waiting)
            {
                movement = Vector2.zero;
                waitTimer -= Time.deltaTime;

                if (waitTimer <= 0f)
                {
                    waiting = false;
                    PickNewTarget();
                }

                UpdateAnimator();
                UpdateSpriteFlip();
                return;
            }

            Vector2 currentPosition = rb.position;
            Vector2 direction = targetPoint - currentPosition;

            if (direction.magnitude <= reachDistance)
            {
                movement = Vector2.zero;
                waiting = true;
                waitTimer = waitTime;

                UpdateAnimator();
                UpdateSpriteFlip();
                return;
            }

            movement = direction.normalized;

            if (movement.x != 0 || movement.y != 0)
            {
                LastX = movement.x;
                LastY = movement.y;
            }

            UpdateAnimator();
            UpdateSpriteFlip();
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.gamestate != GameState.Playing)
                return;

            if (movementLocked)
                return;

            rb.MovePosition(rb.position + movement * enemyStats.MoveSpeed * Time.fixedDeltaTime);
        }

        public void StopMove()
        {
            movementLocked = true;
            movement = Vector2.zero;
            UpdateAnimator();
        }

        public void ResumeMove()
        {
            movementLocked = false;
            PickNewTarget();
        }

        private void PickNewTarget()
        {
            if (wanderZone == null)
            {
                Debug.LogWarning("WanderZone is not assigned on " + gameObject.name);
                targetPoint = transform.position;
                return;
            }

            targetPoint = wanderZone.GetRandomPoint();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            animator.SetFloat("LastX", LastX);
            animator.SetFloat("LastY", LastY);
        }

        private void UpdateSpriteFlip()
        {
            if (spriteRenderer == null)
                return;

            if (Mathf.Abs(movement.y) > 0.01f)
            {
                spriteRenderer.flipX = false;
                return;
            }

            if (Mathf.Abs(movement.x) <= 0.01f)
                return;

            spriteRenderer.flipX = sideAnimationFacesRight ? movement.x < 0 : movement.x > 0;
        }
    }
}