using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerTargetEmptyMove : MonoBehaviour
    {
        private PlayerMove playerMove;

        [SerializeField] private Transform targetEmty;
        [SerializeField] private float targetDistance = 0.5f;

        private void Awake()
        {
            playerMove = GetComponent<PlayerMove>();
        }

        private void Update()
        {
            if (playerMove == null || targetEmty == null) return;

            Vector2 movement = playerMove.Movement;

            if (movement.x > 0)
                targetEmty.localPosition = new Vector2(targetDistance, 0);
            else if (movement.x < 0)
                targetEmty.localPosition = new Vector2(-targetDistance, 0);
            else if (movement.y > 0)
                targetEmty.localPosition = new Vector2(0, targetDistance);
            else if (movement.y < 0)
                targetEmty.localPosition = new Vector2(0, -targetDistance);
        }
    }
}
