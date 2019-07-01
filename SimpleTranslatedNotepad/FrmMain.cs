using System;
using System.IO;
using System.Windows.Forms;
using EasyI18n;
using EasyI18n.Locales;

namespace SimpleTranslatedNotepad
{
    public partial class FrmMain : Form
    {
        private string m_FileName;

        public FrmMain()
        {
            InitializeComponent();

            I18n.LoadLocales($@"{Environment.CurrentDirectory}\Translations");
            I18n.DefaultLocale = Locale.en_GB;
            I18n.OnDefaultLanguageChanged += (sender, e) => TranslateUI();

            cmbLanguage.SelectedIndex = 0;

            btnNewFile.Click += BtnNewFileOnClick;
            btnOpenFile.Click += BtnOpenFileOnClick;
            btnSaveFile.Click += BtnSaveFileOnClick;
            btnExit.Click += BtnExitOnClick;
            btnSelectAll.Click += BtnSelectAllOnClick;
            btnClear.Click += BtnClearOnClick;
            cmbLanguage.SelectedIndexChanged += CmbLanguageOnSelectedIndexChanged;

            TranslateUI();
        }

        /// <summary>
        ///     File Name
        /// </summary>
        private string FileName
        {
            get => m_FileName;
            set
            {
                m_FileName = value;
                Text = I18n.Get("form_title_editing", value);
            }
        }

        /// <summary>
        ///     Translate UI
        /// </summary>
        private void TranslateUI()
        {
            Text = I18n.Get("form_title_idle");
            btnFile.Text = I18n.Get("menu_file");
            btnNewFile.Text = I18n.Get("menu_new");
            btnOpenFile.Text = I18n.Get("menu_open");
            btnSaveFile.Text = I18n.Get("menu_save");
            btnExit.Text = I18n.Get("menu_exit");
            btnEdit.Text = I18n.Get("menu_edit");
            btnSelectAll.Text = I18n.Get("menu_select_all");
            btnClear.Text = I18n.Get("menu_clear");
            btnLanguage.Text = I18n.Get("menu_lang");
        }

        private void BtnNewFileOnClick(object sender, EventArgs e)
        {
            FileName = "New File";
            txtContent.Clear();
        }

        private void BtnOpenFileOnClick(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog();
            if(diag.ShowDialog() == DialogResult.OK)
            {
                var file = new FileInfo(diag.FileName);
                if(file.Exists && (file.FullName.EndsWith(".txt") || file.FullName.EndsWith(".log")))
                {
                    txtContent.Text = File.ReadAllText(file.FullName);
                    FileName = file.Name;
                }
            }
        }

        private void BtnSaveFileOnClick(object sender, EventArgs e)
        {
            var diag = new SaveFileDialog();
            if(diag.ShowDialog() == DialogResult.OK)
                File.WriteAllText(diag.FileName, txtContent.Text);
        }

        private void BtnExitOnClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSelectAllOnClick(object sender, EventArgs e)
        {
            txtContent.SelectAll();
        }

        private void BtnClearOnClick(object sender, EventArgs e)
        {
            txtContent.Clear();
        }

        private void CmbLanguageOnSelectedIndexChanged(object sender, EventArgs e)
        {
            I18n.DefaultLocale = LocaleHelper.GetLocaleFromString(cmbLanguage.Text);
        }
    }
}