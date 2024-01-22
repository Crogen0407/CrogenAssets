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
        [SerializeField] protected float maxHp = 100.0f;
        [SerializeField] protected float minHpRange = 0.05f;
        
        protected float Hp
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
                    
                    if (_hp <= minHpRange)
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