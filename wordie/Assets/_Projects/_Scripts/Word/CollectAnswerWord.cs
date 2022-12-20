using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wordle.Word
{
    public class CollectAnswerWord : MonoBehaviour
    {
        [SerializeField] private AlphabetObject[] _alphabetObjects;
        
        public void InitCollectAnswer(int index, Color color,char character)
        {
            _alphabetObjects[index].SetFontSize(72);
            _alphabetObjects[index].InitAlphabet(color,character);
        }
        public void IdleCollectAnswer()
        {
            for (var i = 0; i < _alphabetObjects.Length; i++)
            {
                _alphabetObjects[i].IdleAlphabetObject();
            }
        }
    }
}
