using System.Net;
using System.IO.Compression;
using System.Diagnostics;

namespace DepotGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appdata = Path.Combine(dir, "DepotGUI");
            if (!Directory.Exists(appdata))
            {
                DialogResult result = MessageBox.Show("In order for this software to function it must download depot downloader. Would you like to connect to the internet and download it now?", "DepotGUI", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Directory.CreateDirectory(appdata);
                    getdepotdownloader();
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.InitialDirectory = "C:\\";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = dialog.SelectedPath;
            }
        }

        private void getdepotdownloader()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appdata = Path.Combine(dir, "DepotGUI");
            string down = "https://github.com/SteamRE/DepotDownloader/releases/download/DepotDownloader_3.1.0/DepotDownloader-framework.zip";
            string path = appdata;
            string file = "DepotDownloader-framework.zip";
            string full = Path.Combine(path, file);
            using (WebClient webclient = new WebClient())
            {
                webclient.DownloadFileAsync(new Uri(down), full);
                webclient.DownloadFileCompleted += (s, ev) =>
                {
                    string zip = full;
                    string extract = appdata;
                    if (!Directory.Exists(extract))
                    {
                        Directory.CreateDirectory(extract);
                    }
                    try
                    {
                        ZipFile.ExtractToDirectory(zip, extract);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occured extracting the depot downloader: {ex.Message}");
                    }
                };
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appdata = Path.Combine(dir, "DepotGUI\\DepotDownloader.exe");
            string user = textBox1.Text;
            string pass = textBox2.Text;
            string app = textBox3.Text;
            string depot = textBox4.Text;
            string manifest = textBox5.Text;
            string direc = textBox6.Text;
            Process process = new Process();
            process.StartInfo.FileName = appdata;
            process.StartInfo.Arguments = $"-app {app} -depot {depot} -manifest {manifest} -username {user} -password {pass} -dir {direc}";
            process.Start();
            process.WaitForExit();
        }
    }
}
