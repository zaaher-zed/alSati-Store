
using System.Windows.Forms;

namespace alSati_store
{
    public class AppContext : ApplicationContext
    {
        public AppContext()
        {
            AuthForm authForm = new AuthForm(this);

            MainForm = authForm;

            authForm.FormClosed += (s, e) =>
            {
                if (MainForm == authForm)
                    ExitThread();
            };

            authForm.Show();
        }

        public void ShowMainForm(
            int userId,
            string username,
            string displayName,
            string role)
        {
            Form previousForm = MainForm;

            MainForm mainForm = new MainForm(
                userId,
                username,
                displayName,
                role,
                this);

            MainForm = mainForm;

            mainForm.FormClosed += (s, e) =>
            {
                if (MainForm == mainForm)
                    ExitThread();
            };

            mainForm.Show();

            previousForm.Close();
        }

        public void ShowAuthForm()
        {
            Form previousForm = MainForm;

            AuthForm authForm = new AuthForm(this);

            MainForm = authForm;

            authForm.FormClosed += (s, e) =>
            {
                if (MainForm == authForm)
                    ExitThread();
            };

            authForm.Show();

            previousForm.Close();
        }
    }
}
