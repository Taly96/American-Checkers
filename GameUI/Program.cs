using System.Windows.Forms;

namespace GameUI
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        // [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            CheckersUI checkersGame = new CheckersUI();

            checkersGame.RunGame();
        }
    }
}
