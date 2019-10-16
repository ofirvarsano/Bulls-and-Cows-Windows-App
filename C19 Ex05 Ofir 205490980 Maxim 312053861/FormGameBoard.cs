namespace C19_Ex05_Ofir_205490980_Maxim_312053861
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;

    public delegate void EndLevel();

    public partial class FormGameBoard : Form
    {
        private const int k_Space = 10;
        private LogicManagement<Color> m_Managment = new LogicManagement<Color>();
        private GridSquares m_SecretSequence;
        private BoardLine[] m_ArrayOfLevels;
        private int m_CurrentLevel = 0;
        private Size m_GuessButtonSize = new Size(50, 50);
        private Color m_DefaultColor = Button.DefaultBackColor;

        public FormGameBoard(int i_TotalLevelsAmount)
        {
            m_Managment.InitializNumberOfLevels(i_TotalLevelsAmount);
            m_Managment.Shuffle(FormColorSelection.ColorKit);
            m_ArrayOfLevels = new BoardLine[i_TotalLevelsAmount];
            initializeComponent();
            m_ArrayOfLevels[0].SetEnable = true;
        }

        private void closeLevel()
        {
            int bulls = 0;
            int cows = 0;

            m_Managment.GetBullsAndCows(m_ArrayOfLevels[m_CurrentLevel].m_ChosenColors, out bulls, out cows);
            foreach (Button button in m_ArrayOfLevels[m_CurrentLevel].GetResultButtons.MatrixOfButtons)
            {
                if (bulls > 0)
                {
                    button.BackColor = Color.Black;
                    bulls--;
                }
                else if (cows > 0)
                {
                    button.BackColor = Color.Yellow;
                    cows--;
                }
            }

            if (m_Managment.IsGameOver)
            {
                endTheGame();
            }
            else
            {
                m_CurrentLevel++;
                m_ArrayOfLevels[m_CurrentLevel].SetEnable = true;
            }
        }

        private void endTheGame()
        {
            int idx = 0;
            DialogResult result;

            foreach (Button button in m_SecretSequence.MatrixOfButtons)
            {
                button.BackColor = m_Managment.GetCombination[idx];
                idx++;
            }

            if (m_Managment.IsWin)
            {
                result = endGameYesNoInit(string.Format("YOU WIN!!!! {0}Would yot like to restart Game?", Environment.NewLine), "WINNER");
            }
            else
            {
                result = endGameYesNoInit(string.Format("GAME OVER,{0}Would yot like to restart Game?", Environment.NewLine), "WINNER");
            }

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Yes;
            }
            else if (result == DialogResult.No)
            {
                this.DialogResult = DialogResult.No;
                MessageBox.Show(
                           "BYE BYE",
                           "Bulls And Cows",
                            MessageBoxButtons.OK);
            }
        }

        private DialogResult endGameYesNoInit(string i_Output, string i_Titel)
        {
            return MessageBox.Show(
                           i_Output,
                           i_Titel,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Exclamation);
        }

        private void initializeComponent()
        {
            Point prevLocation = new Point(this.Location.X + k_Space, this.Location.Y + k_Space);

            this.Text = "Bulls And Cows!";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("David", 12, FontStyle.Bold);
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            m_SecretSequence = new GridSquares(1, LogicManagement<Color>.k_LengthOfTheCombination, m_GuessButtonSize, k_Space, Color.Black, false);
            m_SecretSequence.Location = new Point(prevLocation.X, prevLocation.Y);
            m_SecretSequence.Size = new Size(LogicManagement<Color>.k_LengthOfTheCombination * (m_GuessButtonSize.Width + k_Space), m_GuessButtonSize.Height);
            this.Controls.Add(m_SecretSequence);

            prevLocation.Y += m_SecretSequence.Height + (k_Space * 2);

            for (int i = 0; i < m_ArrayOfLevels.Length; i++)
            {
                m_ArrayOfLevels[i] = new BoardLine(m_GuessButtonSize, m_DefaultColor, k_Space, closeLevel);
                m_ArrayOfLevels[i].Location = new Point(prevLocation.X, prevLocation.Y);
                prevLocation.Y += m_ArrayOfLevels[i].Height + k_Space;
                this.Controls.Add(m_ArrayOfLevels[i]);
            }

            this.Size = new Size(m_ArrayOfLevels[0].Width + (3 * k_Space), prevLocation.Y + (4 * k_Space));
        }

        private void FormGameBoard_Load(object sender, EventArgs e)
        {
        }
    }
}
