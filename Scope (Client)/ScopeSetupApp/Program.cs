using System;
using System.Windows.Forms;

namespace ScopeApp
{
	internal static class Program
	{
		public static MainForm.MainForm MainFormWin;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				MainFormWin = new MainForm.MainForm(args);

				Application.Run(MainFormWin);
			}
			catch (Exception exception)
			{

			}

		}
	}
}
