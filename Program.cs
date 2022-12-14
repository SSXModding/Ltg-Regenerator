using SSXMultiTool.FileHandlers.LevelFiles.TrickyPS2;

namespace MiniLTGRebuilderForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            }
            else
            {
                List<string> ListArgs = args.ToList();
                ListArgs.Add("");
                ListArgs.Add("");
                ListArgs.Add("");
                ListArgs.Add("");
                ListArgs.Add("");
                if (File.Exists(ListArgs[0]))
                {
                    if (File.Exists(ListArgs[1]))
                    {
                        PBDHandler pBDHandler = new PBDHandler();
                        pBDHandler.LoadPBD(ListArgs[0]);

                        LTGHandler handler = new LTGHandler();
                        handler.LoadLTG(ListArgs[1]);

                        if (ListArgs[2].ToLower() != "all")
                        {
                            for (int i = 0; i < pBDHandler.Instances.Count; i++)
                            {
                                var TempInstance = pBDHandler.Instances[i];
                                TempInstance.LTGState = handler.FindIfInstaneState(i);
                                pBDHandler.Instances[i] = TempInstance;
                            }
                        }

                        handler.RegenerateLTG(pBDHandler);
                        handler.SaveLTGFile(ListArgs[1]);
                        if (ListArgs[3].ToLower() != "nc" && ListArgs[2].ToLower() != "nc")
                        {
                            MessageBox.Show("LTG File Rebuilt");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Output");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Input");
                }
            }
        }
    }
}