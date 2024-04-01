using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] private Transform _playerPlacement;
    [SerializeField] private GameObject _player;
    [SerializeField] bool _isLanded;
    private bool _isStart;

    private void Start()
    {
        _isLanded = false;
        _isStart = true;
    }

    private void Update()
    {
        if (!_isLanded && !_isStart)
        {
            _player.transform.position = _playerPlacement.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _isLanded)
        {
            _isLanded = true;
            _isStart = false;

        }
        else
        {
            _isLanded = false;
        }
    }

    
}
