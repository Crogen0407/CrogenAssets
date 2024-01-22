using System;
using UnityEngine;

namespace Crogen.EffectExtension.LineRendererExtension
{
    public class LineRendererExtension : MonoBehaviour
    {
        [SerializeField] private Transform[] _verticeTransforms;
        private Vector3[] _verticePositions = new Vector3[250];
        
        //Components
        private LineRenderer _lineRenderer;

        private void Init()
        {
            _lineRenderer.positionCount = _verticeTransforms.Length;
            for (int i = 0; i < _lineRenderer.positionCount; i++)
            {
                _verticePositions[i] = _verticeTransforms[i].position;
            }
        }
        
        private void OnEnable()
        {
            if (_lineRenderer == null)
                _lineRenderer = GetComponent<LineRenderer>();
        }

        private void FixedUpdate()
        {
            Init();
            _lineRenderer.SetPositions(_verticePositions);
        }
    }
}