using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _heartMaxCount;
    [SerializeField] private int _heartCount;
    [SerializeField] private TMP_Text _heartText;
    [SerializeField] private TMP_Text _text;
    [Space(10)]
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _hearts;
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _cardOne;
    [SerializeField] private GameObject _cardTwo;
    [SerializeField] private GameObject _menu;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _playerStartPosition;
    [Space(10)]
    [SerializeField] private AudioClip _pickUpSFX;
    [SerializeField] private AudioClip _buttonSFX;
    [SerializeField] private AudioClip _winSFX;
    [SerializeField] private AudioClip _startAudio;
    [SerializeField] private AudioClip _winAudio;
    [Space(10)] 
    [SerializeField] private GameObject _winVFX;
    

    private AudioSource _audioSource;
    private bool _isMenuOpen = false;
    
    public bool IsCardShown;

    public static GameManager Instanсe;

    private void Awake()
    {
        Instanсe = this;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.PlayOneShot(_startAudio);
        IsCardShown = false;
    }

    private void Update()
    {
        _heartText.GetComponentInParent<Transform>().rotation = _player.rotation;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu();
        }
    }

    public void ActivateHearts()
    {
        _text.text = "Найди и собери все сердечки!";
        _heartText.text = $"{_heartCount}/{_heartMaxCount}";
        _audioSource.PlayOneShot(_buttonSFX);
        _buttons.SetActive(false);
        _hearts.SetActive(true);
    }

    public void AddHeart()
    {
        _heartCount++;
        _heartText.text = $"{_heartCount}/{_heartMaxCount}";
        _audioSource.PlayOneShot(_pickUpSFX);
        if (_heartCount == _heartMaxCount)
        {
            Win();
        }
    }

    public void ShowCardOne()
    {
        _cardOne.SetActive(true);
        IsCardShown = true;

    }

    public void ShowCardTwo()
    {
        _cardOne.SetActive(false);
        _cardTwo.SetActive(true);
    }

    public void CloseCards()
    {
        _cardTwo.SetActive(false);
        IsCardShown = false;
    }

    private void Menu()
    {
        _isMenuOpen = !_isMenuOpen;
        _menu.SetActive(_isMenuOpen);
        IsCardShown = _isMenuOpen;
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Win()
    {
        _win.SetActive(true);
        _heartText.text = " ";
        _text.text = "Ты нашел все сердечки\nпотому что ты\n10/10 :3";
        _player.position = _playerStartPosition.position;
        Instantiate(_winVFX, _player.position, Quaternion.identity);
        _audioSource.Stop();
        _audioSource.Play();
        _audioSource.PlayOneShot(_winSFX);
        _audioSource.PlayOneShot(_winAudio);
        _audioSource.loop = true;
    }
    
    
    
    
}
