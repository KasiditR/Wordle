using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Wordle.Manager;
namespace Wordle.Word
{
    public enum KeyboardType
    {
        Alphabet,
        Delete,
        Enter,
    }
    public class KeyboardObject : MonoBehaviour
    {
        [SerializeField] private char _keyboardAlphabet;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _keyboardText;
        [SerializeField] private Button _keyboardButton;
        [SerializeField] private KeyboardType _keyboardType;
        private GameManager _gameManager;

        public char KeyboardAlphabet { get => _keyboardAlphabet;}
        
        private void Start()
        {
            _gameManager = GameManager.Instance;
        }
        private void OnEnable()
        {
            _keyboardButton.onClick.AddListener(() => {Keyboard_OnClick();});
        }
        private void OnDisable()
        {
            _keyboardButton.onClick.RemoveListener(() => {Keyboard_OnClick();});
        }
        private void Keyboard_OnClick()
        {
            _gameManager?.KeyboardInput(GetKeyboard().Item1,GetKeyboard().Item2);
        }
        private (KeyboardType,char) GetKeyboard()
        {
            return (_keyboardType,_keyboardText.text.ToCharArray()[0]);
        }
        public void SetColorKeyboardObject(Color color)
        {
            _background.color = color;
        }
        public void IdleKeyboard()
        {
            _background.color = Color.gray;
        }
    }
}
