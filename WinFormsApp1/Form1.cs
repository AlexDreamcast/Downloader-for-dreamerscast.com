using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private string tempDir = Path.Combine(Path.GetTempPath(), "yt-dlp-temp");
        private string ffmpegPath;
        private string ytdlpPath;
        private string outputDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public Form1()
        {
            InitializeComponent();
            lblOutputDir.Text = $"Output: {outputDir}";
            ExtractResources();
        }

        private void ExtractResources()
        {
            Directory.CreateDirectory(tempDir);

            string[] resourceFiles = {
                "yt-dlp.exe", "ffmpeg.exe",
                "avcodec-62.dll", "avdevice-62.dll", "avfilter-11.dll", "avformat-62.dll",
                "avutil-60.dll", "swresample-6.dll", "swscale-9.dll"
            };

            foreach (string fileName in resourceFiles)
            {
                using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"WinFormsApp1.{fileName}");
                string outPath = Path.Combine(tempDir, fileName);

                using FileStream fileStream = new FileStream(outPath, FileMode.Create, FileAccess.Write);
                stream?.CopyTo(fileStream);
            }

            ytdlpPath = Path.Combine(tempDir, "yt-dlp.exe");
            ffmpegPath = Path.Combine(tempDir, "ffmpeg.exe");
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            string mpdUrl = txtMpdUrl.Text.Trim();
            if (string.IsNullOrEmpty(mpdUrl))
            {
                MessageBox.Show("Please enter a valid MPD URL.");
                return;
            }

            // Define paths for temporary files
            string videoPath = Path.Combine(tempDir, "video.mp4");
            string audioPath = Path.Combine(tempDir, "audio.m4a");
            string audioTempPath = Path.Combine(tempDir, "audio.temp.m4a"); // Added for completeness if yt-dlp creates it

            // Generate unique output filename based on current time to prevent overwriting previous downloads
            string uniqueFileName = $"output_{DateTime.Now:yyyyMMdd_HHmmss}.mp4";
            string outputPath = Path.Combine(outputDir, uniqueFileName);

            txtOutput.Clear();
            lblProgress.Text = "Progress: Clearing old files...";

            try
            {
                // Clean up previous temporary files before starting a new download
                if (File.Exists(videoPath)) File.Delete(videoPath);
                if (File.Exists(audioPath)) File.Delete(audioPath);
                if (File.Exists(audioTempPath)) File.Delete(audioTempPath); // Delete if it exists

                lblProgress.Text = "Progress: Downloading video...";
                await RunProcessAsync(ytdlpPath, $"-f bestvideo -o \"{videoPath}\" \"{mpdUrl}\"");

                lblProgress.Text = "Progress: Downloading audio...";
                await RunProcessAsync(ytdlpPath, $"-f bestaudio -o \"{audioPath}\" \"{mpdUrl}\"");

                lblProgress.Text = "Progress: Merging...";
                string args = $"-i \"{videoPath}\" -i \"{audioPath}\" -c copy \"{outputPath}\"";
                await RunProcessAsync(ffmpegPath, args);

                lblProgress.Text = "Progress: Cleaning temporary files...";
                // After successful merge, delete the temporary audio and video files
                if (File.Exists(videoPath)) File.Delete(videoPath);
                if (File.Exists(audioPath)) File.Delete(audioPath);
                if (File.Exists(audioTempPath)) File.Delete(audioTempPath); // Delete if it exists

                lblProgress.Text = "Progress: Done.";
                MessageBox.Show("Download and merge complete! File saved as: " + outputPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                lblProgress.Text = "Progress: Error.";
            }
        }

        private async Task RunProcessAsync(string exePath, string arguments)
        {
            if (!File.Exists(exePath))
                throw new FileNotFoundException($"Не найден исполняемый файл: {exePath}");

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = arguments,
                WorkingDirectory = tempDir,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using Process proc = new Process { StartInfo = psi };
            StringBuilder output = new StringBuilder();

            proc.OutputDataReceived += (s, e) => { if (e.Data != null) AppendOutput(e.Data); };
            proc.ErrorDataReceived += (s, e) => { if (e.Data != null) AppendOutput(e.Data); };

            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();

            await proc.WaitForExitAsync();
        }

        private void AppendOutput(string text)
        {
            if (txtOutput.InvokeRequired)
            {
                txtOutput.Invoke(new Action<string>(AppendOutput), text);
            }
            else
            {
                txtOutput.AppendText(text + Environment.NewLine);
            }
        }

        private void btnSelectOutput_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                outputDir = dlg.SelectedPath;
                lblOutputDir.Text = $"Output: {outputDir}";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // This will delete the entire temp directory and its contents
                if (Directory.Exists(tempDir))
                    Directory.Delete(tempDir, true);
            }
            catch (Exception ex)
            {
                // Handle potential errors during directory deletion (e.g., file in use)
                Console.WriteLine($"Error deleting temp directory on closing: {ex.Message}");
                // You might want to log this error or inform the user, but for temp files, it's often ignorable.
            }
        }
    }
}