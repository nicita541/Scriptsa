using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class UpdateHealsBar : MonoBehaviour
    {
        [SerializeField] private GameObject gOHealsBar;
        [SerializeField] private float healsBarMaxWidth = 0.5f;
        [SerializeField] private float healsBarMinWidth = 0f;

        private SpriteRenderer healsBar;
        private float timeOff;

        private void Awake()
        {
            healsBar = gOHealsBar.GetComponent<SpriteRenderer>();
            timeOff = 0f;
        }

        private void Start()
        {
            gOHealsBar.SetActive(false);

        }

        private void Update()
        {
            if (timeOff > 0)
            {
                timeOff -= Time.deltaTime;
            }
            else
            {

                gOHealsBar.SetActive(false);
            }
        }

        public void WidthHealsBar(float maxHp, float currentHp)
        {
            timeOff = 5f;
            gOHealsBar.SetActive(true);
            float HpInt = healsBarMaxWidth / maxHp;
            float newWidth = Mathf.Clamp(HpInt * currentHp, healsBarMinWidth, healsBarMaxWidth);
            healsBar.size = new Vector2(newWidth, healsBar.size.y);
        }
    }
}
