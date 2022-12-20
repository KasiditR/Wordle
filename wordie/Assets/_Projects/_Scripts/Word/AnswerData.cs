using UnityEngine;

namespace Wordle.Manager
{
    public struct AnswerData
    {
        public Color color;
        public char character;

        public void InitAnswerData(Color color,char character)
        {
            this.color = color;
            this.character = character;
        }
    }
}
