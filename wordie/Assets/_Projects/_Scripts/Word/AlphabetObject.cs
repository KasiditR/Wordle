using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Wordle.Word
{
    public class AlphabetObject : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _alphabetText;
        [SerializeField] private GameObject _imageIdle;
        
        public void InitAlphabet(Color color,char word)
        {
            _imageIdle.SetActive(false);
            _background.color = color;
            _alphabetText.text = word.ToString();
        }
        public void InitAlphabet(char word)
        {
            _alphabetText.text = word.ToString();
        }
        public void IdleAlphabetObject()
        {
            _imageIdle.SetActive(true);
            _background.color = Color.white;
            _alphabetText.text = string.Empty;
        }
        public string GetAlphabet()
        {
            return _alphabetText.text;
        }
        public void SetFontSize(int fontSize)
        {
            _alphabetText.fontSize = fontSize;
        }
        
    }
}
