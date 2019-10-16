namespace C19_Ex05_Ofir_205490980_Maxim_312053861
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LogicManagement<T>
    {
        public const int k_MinimumGuesses = 4;
        public const int k_MaximumGuesses = 10;
        public const int k_LengthOfTheCombination = 4;
        private readonly List<T> m_SecretSequence = new List<T>();
        private int m_CurrentGuess = 0;
        private int m_TotalGuessesToPlay = k_MinimumGuesses;
        private eGameStatus m_CurrentGameStatus = eGameStatus.On;
        private eGameResult m_IsWin = eGameResult.Lose;

        private enum eGameResult
        {
            Win,
            Lose
        }

        public enum eGameStatus
        {
            On,
            Off
        }

        public bool IsGameOver
        {
            get { return m_CurrentGameStatus == eGameStatus.Off; }
        }

        public bool IsWin
        {
            get { return m_IsWin == eGameResult.Win; }
        }

        public List<T> GetCombination
        {
            get { return m_SecretSequence; }
        }

        public int TotalGuessesToPlay
        {
            get { return m_TotalGuessesToPlay; }
        }

        public bool InitializNumberOfLevels(int i_InputToCheck)
        {
            bool resultOfTest = false;

            if ((i_InputToCheck >= k_MinimumGuesses) && (i_InputToCheck <= k_MaximumGuesses))
            {
                resultOfTest = true;
                m_TotalGuessesToPlay = i_InputToCheck;
            }

            return resultOfTest;
        }

        public void Shuffle(List<T> i_Collection)
        {
            Random random = new Random();

            List<T> list = i_Collection.ToList();
            for (int i = 0; i < k_LengthOfTheCombination; ++i)
            {
                int indexRand = random.Next(list.Count);
                m_SecretSequence.Add(list[indexRand]);
                list.RemoveAt(indexRand);
            }
        }

        public void GetBullsAndCows(List<T> i_Pins, out int o_Bulls, out int o_Cows)
        {
            o_Bulls = 0;
            o_Cows = 0;

            for (int i = 0; i < k_LengthOfTheCombination; i++)
            {
                if (List<T>.Equals(i_Pins[i], m_SecretSequence[i]))
                {
                    o_Bulls++;
                }
                else if (m_SecretSequence.Contains(i_Pins[i]))
                {
                    o_Cows++;
                }
            }

            m_CurrentGuess++;
            if (m_CurrentGuess == m_TotalGuessesToPlay)
            {
                m_CurrentGameStatus = eGameStatus.Off;
            }

            if (o_Bulls == k_LengthOfTheCombination)
            {
                m_CurrentGameStatus = eGameStatus.Off;
                m_IsWin = eGameResult.Win;
            }
        }
    }
}
