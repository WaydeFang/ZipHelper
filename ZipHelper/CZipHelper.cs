using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;

namespace ZipHelper
{
    public class ZipReportEventArgs : EventArgs
    {
    }
    public class ZipReportProgressEventArgs : ZipReportEventArgs
    {
        public string zipname;
        public string filename;
        public int perc;
        public long speed;
        public long resttime;
    }

    public class ZipReportErrorEventArgs : ZipReportEventArgs
    {
        public string strError;
    }

    public class ZipReportFinishEventArgs : ZipReportEventArgs
    {
        public string strResult;
    }

    
    public class CZipHelper
    {
        private event EventHandler<ZipReportEventArgs> m_ZipReportEvent;

        private CancellationTokenSource m_cancelTokenSource;


        private long GetTotalSize(string srcPath)
        {
            long size = 0;
            ZipEntry theEntry = null;

            using (ZipInputStream zipInStream = new ZipInputStream(File.OpenRead(srcPath)))
            {
                while ((theEntry = zipInStream.GetNextEntry()) != null)
                    size += theEntry.Size;

                zipInStream.Close();
            }
            return size;
        }

        private void GetDecompressSpeed(object sender, System.Timers.ElapsedEventArgs e, long received, ref long time, ref long speed)
        {
            time++;
            speed = received / time;
            
        }

        public void DoCancle()
        {
            m_cancelTokenSource.Cancel();
        }

        private void DoCompress(string srcPath, ZipOutputStream outputStream,string extraDir = "")
        {
            string[] filenames = new String[] { };

            if (Directory.Exists(srcPath))
            {
                filenames = Directory.GetFileSystemEntries(srcPath);
            }
            else
            {
                filenames = new String[] { srcPath };
            }
            
            foreach (string file in filenames)
            {
                if (Directory.Exists(file))
                {
                    if (String.IsNullOrEmpty(extraDir))
                    {
                        PathHelper.RemoveBackslash(ref srcPath);
                        extraDir = Path.GetDirectoryName(srcPath);
                        PathHelper.InculdeBackslash(ref extraDir);
                    }
                    DoCompress(file, outputStream, extraDir);  //递归压缩子文件夹
                }
                else
                {
                    if (String.IsNullOrEmpty(extraDir))
                    {
                        extraDir = Path.GetDirectoryName(srcPath);
                        PathHelper.InculdeBackslash(ref extraDir);
                    }
                    using (FileStream fstream = File.OpenRead(file))
                    {
                        byte[] buffer = new byte[2 * 1024];
                        string test = file.Replace(extraDir, "");
                        ZipEntry entry = new ZipEntry(file.Replace(extraDir, ""));     
                        entry.DateTime = DateTime.Now;
                        outputStream.PutNextEntry(entry);

                        int sourceBytes = 0;
                        do
                        {
                            sourceBytes = fstream.Read(buffer, 0, buffer.Length);
                            outputStream.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);

                        fstream.Close();
                    }
                }


            }
        }

        public void Compress(string srcPath, string tarPath, int level = 6)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(tarPath));

            using (ZipOutputStream outputStream = new ZipOutputStream(File.Create(tarPath)))
            {
                outputStream.SetLevel(level);
                DoCompress(srcPath, outputStream);
                outputStream.Finish();
                outputStream.Close();
            }

        }

        public void SetDecompressEvent(EventHandler<ZipReportEventArgs> eventhandler)
        {
            m_ZipReportEvent += eventhandler;
        }

        private void ReportExceptionError(Exception ex)
        {
            ZipReportErrorEventArgs arg = new ZipReportErrorEventArgs();
            arg.strError = string.Format("Error: {0}", ex.Message);
            if (m_ZipReportEvent != null)
                m_ZipReportEvent(this, arg);
        }

        private void ReportFinished(string srcPath)
        {
            ZipReportFinishEventArgs arg = new ZipReportFinishEventArgs();
            arg.strResult = srcPath;
            if (m_ZipReportEvent != null)
                m_ZipReportEvent(this, arg);
        }

        private void ReportProgress(int perc, string srcPath ,string filename, long speed, long totalSize, long receivedSize)
        {
            ZipReportProgressEventArgs arg = new ZipReportProgressEventArgs();
            arg.perc = perc;
            arg.zipname = srcPath;
            arg.filename = filename;
            arg.speed = speed;
            if (speed > 0)
                arg.resttime = Convert.ToInt32(Math.Ceiling(((totalSize - receivedSize) * 1.0) / speed));
            else
                arg.resttime = 0;

            if (m_ZipReportEvent != null)
                m_ZipReportEvent(this, arg);
        }

        public void Decompress(string srcPath, string targetPath, bool isDeleteSrc = false)
        {
            long totalSize = GetTotalSize(srcPath);     //bit
            long receivedSize = 0;  //bit
            long time = 0;  //second
            long speed = 0; //bit/s
            byte[] data = new byte[2048]; //readbuf
            int size = 0; //read size

            PathHelper.InculdeBackslash(ref targetPath);
            PathHelper.CreateDirifnotExist(targetPath); //生成解压目录   

            System.Timers.Timer m_speedTimer = new System.Timers.Timer(Convert.ToDouble(2000));
            m_speedTimer.Elapsed += new System.Timers.ElapsedEventHandler((s, e) => GetDecompressSpeed(s, e, receivedSize, ref time, ref speed));
            m_speedTimer.AutoReset = true;
            
            try
            {
                using (ZipInputStream zipInStream = new ZipInputStream(File.OpenRead(srcPath)))
                {
                    m_cancelTokenSource = new CancellationTokenSource();
                    ZipEntry theEntry = null;
                    while ((theEntry = zipInStream.GetNextEntry()) != null && (!m_cancelTokenSource.IsCancellationRequested))
                    {
                        string EntryPath = targetPath + theEntry.Name;
                        
                        if (theEntry.IsDirectory)
                        {
                            PathHelper.CreateDirifnotExist(EntryPath);
                        }
                        else
                        {
                            if (theEntry.Name != String.Empty)
                            {
                                PathHelper.CreateDirifnotExist(Path.GetDirectoryName(EntryPath));

                                using (FileStream streamWriter = File.Create(EntryPath))
                                {
                                    while (!m_cancelTokenSource.IsCancellationRequested)
                                    {
                                        if(!m_speedTimer.Enabled)
                                            m_speedTimer.Enabled = true;

                                        size = zipInStream.Read(data, 0, data.Length);
                                        if (size <= 0)
                                            break;
                                        else
                                        {
                                            receivedSize += size;
                                            int perc = Convert.ToInt32(Math.Floor((receivedSize * 100.00) / totalSize));
                                            if (perc == 100)
                                            {
                                                int s = 9 + 9;
                                            }
                                            ReportProgress(perc, srcPath, theEntry.Name, speed, totalSize, receivedSize);
                                        }

                                        streamWriter.Write(data, 0, size);
                                    }
                                    streamWriter.Close();
                                }
                            }
                        }
                    }
                    zipInStream.Close();
                    m_speedTimer.Enabled = false;
                    m_speedTimer.Close();
                }
            }
            catch (Exception ex)
            {
                ReportExceptionError(ex);
                return;
            }

            if (!m_cancelTokenSource.IsCancellationRequested)
            {
                ReportFinished(srcPath);

                if (isDeleteSrc)
                    File.Delete(srcPath);
            }

        }
    }
}
