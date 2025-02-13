    using System;
using System.Windows.Forms;

namespace Saper.Forms
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public void ShowResult(string result)
        {
            MessageBox.Show(result, "Результат");
        }
    }
}