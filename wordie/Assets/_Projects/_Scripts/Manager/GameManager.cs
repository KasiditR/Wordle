using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wordle.Word;
using Random = UnityEngine.Random;
using TMPro;

namespace Wordle.Manager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance 
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Game Manager Is NULL");
                }
                return _instance;
            }
        }
        [SerializeField] private string _answer;
        [SerializeField] private string _targetAnswer;
        [SerializeField] private Color _correctColorWord;
        [SerializeField] private Color _haveColorWord;
        [SerializeField] private Color _dontHaveColorWord;
        [SerializeField] private GameObject _reStartPanel;
        [SerializeField] private TMP_Text _answerText;
        [SerializeField] private Button _reStartButton;
        [SerializeField] private CollectAnswerWord[] _collectAnswerWords;
        [SerializeField] private AlphabetObject[] _answerAlphabetObject;
        [SerializeField] private KeyboardObject[] _keyboardObjects;
        private Dictionary<char,KeyboardObject> _keyboardData = new Dictionary<char, KeyboardObject>();
        private AnswerData[] _answerDatas = new AnswerData[5];
        private string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private int _countAnswerIndex;
        private int _countCollectIndex;
        private void Awake()
        {
            _instance = this;
            for (var i = 0; i < _keyboardObjects.Length; i++)
            {
                _keyboardData.Add(_keyboardObjects[i].KeyboardAlphabet,_keyboardObjects[i]);
            } 
            GenerateAnswer();
        }
        private void OnEnable()
        {
            _reStartButton.onClick.AddListener(() => ReStart_OnClick());
        }
        private void OnDisable()
        {
            _reStartButton.onClick.RemoveListener(() => ReStart_OnClick());
        }
        public void KeyboardInput(KeyboardType keyboardType,char alphabet)
        {
            switch (keyboardType)
            {
                case KeyboardType.Alphabet:
                    Alphabet(alphabet);
                    break;
                case KeyboardType.Enter:
                    Enter();
                    break;
                case KeyboardType.Delete:
                    Delete();
                    break;
            }
        }
        private void Alphabet(char word)
        {
            if (_countAnswerIndex >= 5)
            {
                return;
            }
            _answerAlphabetObject[_countAnswerIndex].InitAlphabet(word);
            _countAnswerIndex++;
        }
        private void Enter()
        {
            for (var i = 0; i < _answerAlphabetObject.Length; i++)
            {
                if (_answerAlphabetObject[i].GetAlphabet() == string.Empty)
                {
                    return;
                }
            }
            _answer = string.Empty;
            _countAnswerIndex = 0;
            for (int i = 0; i < _answerAlphabetObject.Length; i++)
            {
                char character = _answerAlphabetObject[i].GetAlphabet().ToCharArray()[0];
                if (_targetAnswer.Contains(character.ToString()))
                {
                    _answerDatas[i].InitAnswerData(_haveColorWord,character);
                }
                else
                {
                    _answerDatas[i].InitAnswerData(_dontHaveColorWord,character);
                }

                if ( character == _targetAnswer[i])
                {
                    _answerDatas[i].InitAnswerData(_correctColorWord,character);
                }

                _answer += _answerAlphabetObject[i].GetAlphabet();
                
            }
            if (_answer == _targetAnswer)
            {
                CorrectAnswer();
                return;
            }
            for (var i = 0; i < _answerAlphabetObject.Length; i++)
            {
                _collectAnswerWords[_countCollectIndex].InitCollectAnswer(i,_answerDatas[i].color,_answerDatas[i].character);
                _answerAlphabetObject[i].IdleAlphabetObject();
            }
            _countCollectIndex++;
            if (_countCollectIndex >= _collectAnswerWords.Length)
            {
                GameOver();
            }

            SetColorKeyboard();
        }

        private void CorrectAnswer()
        {
            for (var i = 0; i < _answerAlphabetObject.Length; i++)
            {
                _answerAlphabetObject[i].InitAlphabet(_answerDatas[i].color, _answerDatas[i].character);
            }
            GameOver();
        }
        private void GameOver()
        {
            _countCollectIndex = 0;
            _answerText.text = _targetAnswer;
            _reStartPanel.SetActive(true);
        }
        private void Delete()
        {
            if (_countAnswerIndex <= 0)
            {
                return;
            }
            _countAnswerIndex--;
            _answerAlphabetObject[_countAnswerIndex].InitAlphabet(char.MinValue);
        }
        private void GenerateAnswer()
        {
            _targetAnswer = string.Empty;
            for (var i = 0; i < 5; i++)
            {
                int index = Random.Range(0,_alphabet.Length - 1);
                _targetAnswer += _alphabet[index];
                _alphabet.Remove(index);
            }
        }
        private void SetColorKeyboard()
        {
            for (var i = 0; i < _answerDatas.Length; i++)
            {
                KeyboardObject keyboardObject = _keyboardData[_answerDatas[i].character];
                keyboardObject.SetColorKeyboardObject(_answerDatas[i].color);
            }
        }
        private void ReStart_OnClick()
        {
            _answerText.text = string.Empty;
            for (var i = 0; i < _keyboardObjects.Length; i++)
            {
                _keyboardObjects[i].IdleKeyboard();
            }
            for (var i = 0; i < _collectAnswerWords.Length; i++)
            {
                _collectAnswerWords[i].IdleCollectAnswer();
            }
            for (var i = 0; i < _answerAlphabetObject.Length; i++)
            {
                _answerAlphabetObject[i].IdleAlphabetObject();
            }
            GenerateAnswer();
            _reStartPanel.SetActive(false);
        }
    }
}
