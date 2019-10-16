namespace C19_Ex05_Ofir_205490980_Maxim_312053861
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;

    public partial class GridSquares : UserControl
    {
        private readonly Size r_SizeButton;
        private readonly int r_Rows;
        private readonly int r_Cols;
        private readonly int r_Space;
        private Button[,] m_MatrixOfSquares;
        private Color m_DefaultColor;
        private bool m_EnableStatus = false;

        public bool SetEnable
        {
            set
            {
                m_EnableStatus = value;
                foreach (Button button in m_MatrixOfSquares)
                {
                    button.Enabled = m_EnableStatus;
                }
            }
        }

        public Button[,] MatrixOfButtons
        {
            get { return m_MatrixOfSquares; }
        }

        public GridSquares(int i_Rows, int i_Cols, Size i_SizeOfButton, int i_Space, Color i_Color, bool i_IsEnable)
        {
            Point prevLineLocation = this.Location;
            Point buttonLocation;

            r_SizeButton = i_SizeOfButton;
            r_Space = i_Space;
            r_Rows = i_Rows;
            r_Cols = i_Cols;
            m_DefaultColor = i_Color;
            m_MatrixOfSquares = new Button[r_Rows, r_Cols];
            m_EnableStatus = i_IsEnable;

            for (int i = 0; i < r_Rows; ++i)
            {
                buttonLocation = prevLineLocation;
                for (int j = 0; j < r_Cols; ++j)
                {
                    m_MatrixOfSquares[i, j] = new Button();
                    m_MatrixOfSquares[i, j].Enabled = i_IsEnable;
                    m_MatrixOfSquares[i, j].Size = r_SizeButton;
                    m_MatrixOfSquares[i, j].Location = buttonLocation;
                    m_MatrixOfSquares[i, j].BackColor = m_DefaultColor;
                    buttonLocation.X += r_Space + r_SizeButton.Width;
                    this.Controls.Add(m_MatrixOfSquares[i, j]);
                }

                prevLineLocation.Y += r_SizeButton.Height + r_Space;
            }

            this.Size = new Size((r_SizeButton.Height + r_Space) * r_Cols, (r_SizeButton.Width + r_Space) * r_Rows);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "GridSquares";
            this.Load += new System.EventHandler(this.GridSquares_Load);
            this.ResumeLayout(false);
        }

        private void GridSquares_Load(object sender, EventArgs e)
        {
        }
    }
}