using SSXMultiTool.Utilities;
using SSXMultiTool.FileHandlers.LevelFiles.TrickyPS2;

namespace MiniLTGRebuilderForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LTGRebuild_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Map File (*.pbd)|*.pbd|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    Filter = "ltg File (*.ltg)|*.ltg|All files (*.*)|*.*",
                    FilterIndex = 1,
                    RestoreDirectory = false
                };
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    PBDHandler pBDHandler = new PBDHandler();
                    pBDHandler.LoadPBD(openFileDialog.FileName);

                    LTGHandler handler = new LTGHandler();
                    handler.LoadLTG(openFileDialog1.FileName);

                    for (int i = 0; i < pBDHandler.Instances.Count; i++)
                    {
                        var TempInstance = pBDHandler.Instances[i];
                        TempInstance.LTGState = handler.FindIfInstaneState(i);
                        pBDHandler.Instances[i] = TempInstance;
                    }

                    handler.RegenerateLTG(pBDHandler);
                    handler.SaveLTGFile(openFileDialog1.FileName);

                    MessageBox.Show("LTG File Rebuilt");
                }
            }
        }
    }
}