namespace C19_Ex05_Ofir_205490980_Maxim_312053861
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public partial class FormColorSelection : Form
    {
        private const int k_TotalColors = 8;
        private static List<Color> s_ColorKit = new List<Color>();
        private GridSquares m_UserSelctionColorKit;
        private Color m_ColorSlected = Button.DefaultBackColor;
        private Size m_ButtonSize;
        private int m_Space;

        public static List<Color> ColorKit
        {
            get { return s_ColorKit; }
        }

        public Color colorSelected
        {
            get { return m_ColorSlected; }
        }

        public enum eColorsPins
        {
            Purple = 0,
            Red,
            Green,
            LightBlue,
            Blue,
            Yellow,
            Maroon,
            White
        }

        static FormColorSelection()
        {
            setColorList();
        }

        public FormColorSelection(Size i_ButtonSize, int i_Space)
        {
            this.ControlBox = false;
            this.MinimizeBox = false;
            m_ButtonSize = i_ButtonSize;
            m_Space = i_Space;
            initializeComponent();
        }

        public void CloseDialog(object sender, EventArgs e)
        {
            this.Hide();
            Button button = sender as Button;
            m_ColorSlected = button.BackColor;
        }

        private static void setColorList()
        {
            eColorsPins color = new eColorsPins();
            color = eColorsPins.Purple;

            for (int i = 0; i < k_TotalColors; i++)
            {
                switch (color)
                {
                    case eColorsPins.Purple:
                        s_ColorKit.Add(Color.Purple);
                        break;

                    case eColorsPins.LightBlue:
                        s_ColorKit.Add(Color.LightBlue);
                        break;

                    case eColorsPins.Blue:
                        s_ColorKit.Add(Color.Blue);
                        break;

                    case eColorsPins.Green:
                        s_ColorKit.Add(Color.Green);
                        break;

                    case eColorsPins.Maroon:
                        s_ColorKit.Add(Color.Maroon);
                        break;

                    case eColorsPins.Red:
                        s_ColorKit.Add(Color.Red);
                        break;

                    case eColorsPins.White:
                        s_ColorKit.Add(Color.White);
                        break;

                    case eColorsPins.Yellow:
                        s_ColorKit.Add(Color.Yellow);
                        break;
                }

                color++;
            }
        }

        private void initializeComponent()
        {
            int i = 0;

            m_UserSelctionColorKit = new GridSquares(2, k_TotalColors / 2, m_ButtonSize, m_Space, default(Color), true);
            m_UserSelctionColorKit.Location = new Point(this.Location.X + m_Space, this.Location.Y + m_Space);
            this.Size = new Size(m_UserSelctionColorKit.Size.Width + (m_Space * 3), m_UserSelctionColorKit.Size.Height + (m_Space * 3));
            this.Controls.Add(m_UserSelctionColorKit);

            foreach (Button button in m_UserSelctionColorKit.MatrixOfButtons)
            {
                button.Click += new EventHandler(CloseDialog);
                button.BackColor = s_ColorKit[i];
                i++;
            }
        }

        private void FormColorSelection_Load(object sender, EventArgs e)
        {
        }
    }
}
