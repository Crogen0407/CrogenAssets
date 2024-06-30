using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.HealthSystem
{
    public abstract class HealthSystem : MonoBehaviour
    {
        [Header("Hp Option")]
        [SerializeField] private float _hp = 100.0f;
        public float maxHp = 100.0f;

        private void Awake()
        {
            _hp = maxHp;
        }

        public float Hp
        {
            get => _hp;
            set
            {
                OnHpChange();
                if (gameObject.activeSelf == true)
                {
                    if(_hp < value)
                    {
                        OnHpUp();
                    }
                    else if (_hp > value)
                    {
                        OnHpDown();
                    }
            
                    _hp = value;
                    
                    if (_hp <= 0.1f)
                    {
                        OnDie();
                    }                
                }
            }
        }

        protected abstract void OnHpChange();
        protected abstract void OnHpUp();
        protected abstract void OnHpDown();
        protected abstract void OnDie();
    }    
}