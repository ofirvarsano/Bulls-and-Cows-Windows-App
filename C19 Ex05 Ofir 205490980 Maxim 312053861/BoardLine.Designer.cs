namespace C19_Ex05_Ofir_205490980_Maxim_312053861
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Linq;

    public partial class BoardLine : Control
    {
        private readonly EndLevel r_OnEnd;
        private readonly int r_Space;
        private GridSquares m_GuessesButtons;
        private Button m_Submit;
        private GridSquares m_Result;
        private Color m_DefaultColor;
        private Size m_BaseSize;
        private bool m_IsEnable = false;
        private FormColorSelection m_ColorSelection;
        public List<Color> m_ChosenColors = new List<Color>();

        public GridSquares GetGuessesButtons
        {
            get { return m_GuessesButtons; }
        }

        public GridSquares GetResultButtons
        {
            get { return m_Result; }
        }

        public bool SetEnable
        {
            set
            {
                m_IsEnable = value;
                foreach (Button button in m_GuessesButtons.MatrixOfButtons)
                {
                    button.Enabled = m_IsEnable;
                    button.Click += new EventHandler(guessButton_Click);
                }
            }
        }

        public BoardLine(Size i_CustomSize, Color i_Color, int i_Space, EndLevel i_OnEnd)
        {
            m_BaseSize = i_CustomSize;
            m_DefaultColor = i_Color;
            r_Space = i_Space;
            r_OnEnd = i_OnEnd;
            initializeComponent();
            m_ColorSelection = new FormColorSelection(m_BaseSize, r_Space);
        }

        private void m_Submit_Click(object sender, EventArgs e)
        {
            m_GuessesButtons.SetEnable = false;
            Button sumbit = sender as Button;
            sumbit.Enabled = false;
            m_GuessesButtons.SetEnable = false;
            r_OnEnd.Invoke();
        }

        private void initializeComponent()
        {
            Point startLocation = new Point(this.Location.X, this.Location.Y);

            m_GuessesButtons = new GridSquares(1, LogicManagement<Color>.k_LengthOfTheCombination, m_BaseSize, r_Space, m_DefaultColor, false);
            m_GuessesButtons.Location = startLocation;
            m_GuessesButtons.Size = new Size(LogicManagement<Color>.k_LengthOfTheCombination * (m_BaseSize.Width + r_Space), m_BaseSize.Height);
            this.Controls.Add(m_GuessesButtons);

            startLocation.X += m_GuessesButtons.Width + (r_Space / 7);

            m_Submit = new Button();
            m_Submit.Text = "==>>";
            m_Submit.Size = new Size(m_BaseSize.Width, m_BaseSize.Height / 2);
            m_Submit.Location = new Point(startLocation.X, startLocation.Y + (m_BaseSize.Height / 3));
            m_Submit.Enabled = false;
            this.Controls.Add(m_Submit);

            startLocation.X += m_Submit.Width + (r_Space * 2);

            m_Result = new GridSquares(2, LogicManagement<Color>.k_LengthOfTheCombination / 2, new Size(m_BaseSize.Width / 3, m_BaseSize.Height / 4), r_Space, m_DefaultColor, false);
            m_Result.Location = new Point(startLocation.X, startLocation.Y + r_Space);
            m_Result.Size = new Size((LogicManagement<Color>.k_LengthOfTheCombination / 2) * ((m_BaseSize.Width / 3) + r_Space), m_BaseSize.Height);
            this.Controls.Add(m_Result);
            this.Size = new Size(startLocation.X + m_Result.Size.Width, m_BaseSize.Height);
        }

        private void allButtonsHaveBeenSelected(Button i_CurrentButton)
        {
            bool result = true;
            m_ChosenColors.Clear();
            List<Color> tempChosenColors = new List<Color>();

            foreach (Button button in m_GuessesButtons.MatrixOfButtons)
            {
                if (button.BackColor == m_DefaultColor)
                {
                    result = false;
                    break;
                }
            }

            if (result)
            {
                foreach (Button button in m_GuessesButtons.MatrixOfButtons)
                {
                    tempChosenColors.Add(button.BackColor);
                }

                if (tempChosenColors.Count != tempChosenColors.Distinct().Count())
                {
                    result = false;
                    MessageBox.Show(
                       string.Format("All colors must be distinct!"), "Wrong Pick Of Colors",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
            }

            if (result)
            {
                foreach (Button button in m_GuessesButtons.MatrixOfButtons)
                {
                    m_ChosenColors.Add(button.BackColor);
                }
                ///m_GuessesButtons.SetEnable = false;
                m_Submit.Enabled = true;
                m_Submit.Click += new EventHandler(m_Submit_Click);
            }
        }

        private void guessButton_Click(object sender, EventArgs e)
        {
            m_ColorSelection.ShowDialog();
            Button button = sender as Button;
            button.BackColor = m_ColorSelection.colorSelected;
            allButtonsHaveBeenSelected(button);
        }
    }
}
