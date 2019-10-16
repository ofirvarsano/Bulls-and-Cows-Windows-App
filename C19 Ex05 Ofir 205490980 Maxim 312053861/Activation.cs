namespace C19_Ex05_Ofir_205490980_Maxim_312053861
{
    using System.Windows.Forms;

    public class Activation
    {
        public static void Run()
        {
            DialogResult result = DialogResult.Yes;

            while (result == DialogResult.Yes)
            {
                FormGameLogin formGameSettings = new FormGameLogin();
                result = formGameSettings.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    FormGameBoard formGameBoard = new FormGameBoard(formGameSettings.GetNumberOfGuesses);
                    formGameBoard.ShowDialog();
                    result = formGameBoard.DialogResult;
                }
            }
        }
    }
}
