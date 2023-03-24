using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WD_UFT_Selenium_Auto.Library.BaseLibrary
{
    public static class Base_File
    {
        /// <summary>
        /// Copy a file from Input folder to Output folder based on same Case Name.
        /// e.g. CopyInputFileToOutputFolder("620542", pfdtut.bkp)
        /// when a file in case subfolder and need rename and paste to different subfolder.
        /// e.g. CopyInputFileToOutputFolder("620542", @"AITraining\AIMB.txt", @"AIMB\Test.log)
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="fileName"></param>
        /// <param name="newFileName"></param>
        /// <returns></returns>
        public static string CopyInputFileToOutputFolder(string caseName, string fileName, string newFileName = null)
        {
            string inputFilePath = Base_Directory.GenerateInputFileDir(caseName, fileName);
            string outputFileFolder = Base_Directory.GenerateOutputFileDir(caseName, "");
            string outputFilePath = string.Empty;
            if (newFileName == null)
            {
                outputFilePath = Base_Directory.GenerateOutputFileDir(caseName, fileName);
            }
            else
            {
                outputFilePath = Base_Directory.GenerateOutputFileDir(caseName, newFileName);
                fileName = newFileName;

            }

            if (outputFilePath.Contains('\\'))
            {
                string[] subfolders = fileName.Split('\\');
                for (int i = 0; i < subfolders.Count() - 1; ++i)
                {
                    outputFileFolder = Path.Combine(outputFileFolder, subfolders[i]);
                    if (!Directory.Exists(outputFileFolder))
                    {
                        Directory.CreateDirectory(outputFileFolder);
                    }
                }
            }

            if (File.Exists(outputFilePath))
                File.Delete(outputFilePath);
            File.Copy(inputFilePath, outputFilePath);
            File.SetAttributes(outputFilePath, FileAttributes.Normal);
            return outputFilePath;

        }
        /// <summary>
        /// Copy a file from source subfolder in Input Folder to a specificed target subfolder in Output.
        /// e.g. Base_File.CopyInputFileToOutputFolder(@"AITraining", @"620542\AITraining", "Model.txt", "Test.log");
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="targetFolder"></param>
        /// <param name="fileName"></param>
        /// <param name="newFileName"></param>
        /// <returns></returns>
        public static string CopyInputFileToOutputFolder(string sourceFolder, string targetFolder, string fileName, string newFileName)
        {
            string inputFilePath = Base_Directory.GenerateInputFileDir(sourceFolder, fileName);
            string outputFileFolder = Base_Directory.GenerateOutputFileDir(targetFolder, "");
            string outputFilePath = string.Empty;
            if (newFileName == null)
            {
                outputFilePath = Base_Directory.GenerateOutputFileDir(targetFolder, fileName);
            }
            else
            {
                outputFilePath = Base_Directory.GenerateOutputFileDir(targetFolder, newFileName);
                fileName = newFileName;

            }

            if (outputFilePath.Contains('\\'))
            {
                string[] subfolders = fileName.Split('\\');
                for (int i = 0; i < subfolders.Count() - 1; ++i)
                {
                    outputFileFolder = Path.Combine(outputFileFolder, subfolders[i]);
                    if (!Directory.Exists(outputFileFolder))
                    {
                        Directory.CreateDirectory(outputFileFolder);
                    }
                }
            }

            if (File.Exists(outputFilePath))
                File.Delete(outputFilePath);
            File.Copy(inputFilePath, outputFilePath);
            File.SetAttributes(outputFilePath, FileAttributes.Normal);
            return outputFilePath;

        }
        public static string CopyExamplesFileToOutputFolder(string casename, string fileName)
        {
            string inputFilePath = Path.Combine(Base_Directory.APlusExampleDir, fileName);
            string outputFilePath = string.Empty;
            if (fileName.Contains('\\'))
            {
                fileName = fileName.Split('\\')[fileName.Split('\\').Count() - 1];
            }
            outputFilePath = Base_Directory.GenerateOutputFileDir(casename, fileName);
            File.Delete(outputFilePath);
            File.Copy(inputFilePath, outputFilePath);
            return outputFilePath;
        }
        public static string CopyFile(string fileSourceDir, string fileTargerDir, bool IsOverwrite)
        {
            int lastIndex = fileTargerDir.LastIndexOf("\\");
            string pFilePath = fileTargerDir.Substring(0, lastIndex);
            string pFileName = fileTargerDir.Substring(lastIndex + 1);
            string targetFilePath = string.Empty;

            if (pFilePath.Contains('\\'))
            {
                string[] subfolders = pFilePath.Split('\\');
                targetFilePath = subfolders[0];
                for (int i = 1; i < subfolders.Count(); ++i)
                {
                    targetFilePath = targetFilePath + "\\" + subfolders[i];
                    if (!Directory.Exists(targetFilePath))
                    {
                        Directory.CreateDirectory(targetFilePath);
                    }
                }
                targetFilePath = Path.Combine(targetFilePath, pFileName);
            }
            else
            {
                targetFilePath = fileTargerDir;
            }

            File.Copy(fileSourceDir, targetFilePath, IsOverwrite);
            return targetFilePath;
        }
        public static bool VerifyFileContents(string targetFile, string textContents)
        {
            if (!File.Exists(targetFile))
            {
                throw new FileNotFoundException("Failed to find target file:" + targetFile);
            }

            StreamReader targetFileStream = new StreamReader(targetFile, Encoding.Default);
            string targetFilesStreamReadline = null;
            targetFilesStreamReadline = targetFileStream.ReadToEnd();
            targetFileStream.Close();

            if (targetFilesStreamReadline.Contains(textContents))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static void GenerateTextFile(string fileDirectory, string textcontents)
        {
            File.WriteAllText(fileDirectory, textcontents);
            Base_Assert.IsTrue(File.Exists(fileDirectory));
        }
        public static void ClearTempAndBackupFiles(string casePath)
        {
            var f = new FileInfo(casePath);
            var d = f.Directory;
            var filesToDelete = d.EnumerateFiles().Where(x => x.Name.StartsWith("_") || x.Name.StartsWith("$")).ToList();

            filesToDelete.ForEach(x =>
            {
                try
                {
                    x.Delete();
                }
                catch (Exception e)
                {
                    Base_logger.Warning("Failed to delete file: \"" + x.FullName + "\".\r\n\tException: " + e.Message);
                }
            });
        }
        public static void ClearTempFiles(string casePath, bool bForce = false)
        {
            var f = new FileInfo(casePath);
            var d = f.Directory;
            var filesToDelete = d.EnumerateFiles().Where(x => x.Name.StartsWith("_")).ToList();
            filesToDelete.ForEach(x =>
            {
                try
                {
                    x.Delete();
                }
                catch (Exception e)
                {
                    Base_logger.Warning("Failed to delete file: \"" + x.FullName + "\".\r\n\tException: " + e.Message);
                }
            });

            filesToDelete = d.EnumerateFiles().Where(x => x.Name.StartsWith("_")).ToList();
            if (bForce && filesToDelete.Count > 0)
            {
                System.Threading.Thread.Sleep(3000); //Sleep 3 sec. and delete again
                filesToDelete.ForEach(x =>
                {
                    try
                    {
                        x.Delete();
                    }
                    catch (Exception)
                    {
                        Base_logger.Error("Failed to delete file: \"" + x.FullName + "\".");
                    }
                });

                if (d.EnumerateFiles().Where(x => x.Name.StartsWith("_")).ToList().Count() > 0)
                    throw new Exception("Failed to delete temporary files from " + d.FullName);
            }
        }
        public static void CreateFolder(string folderPath)
        {
            DeleteFolder(folderPath);
            try
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message);
            }
        }
        public static void DeleteFolder(string folderPath)
        {
            try
            {
                if (System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.Delete(folderPath, true);
                }
            }
            catch (Exception e)
            {
                Base_logger.Exception(e);
            }
        }
        public static void ClearFolder(string folderPath)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                FileInfo[] fileInfos = directoryInfo.GetFiles();
                foreach (var fileInfo in fileInfos)
                    Base_File.DeleteFile(fileInfo.FullName);
                DirectoryInfo[] subdir = directoryInfo.GetDirectories();
                foreach (var directory in subdir)
                    Base_File.DeleteFolder(directoryInfo.FullName);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message);
            }
        }
        public static void RemoveFolderReadOnly(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return;
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                File.SetAttributes(fileInfo.FullName, FileAttributes.Normal);
            }
        }
        public static void CleanWorkFolder(string workFolder, bool recursive = true)
        {
            if (Directory.Exists(workFolder))
            {
                string[] files = Directory.GetFileSystemEntries(workFolder);

                foreach (string file in files)
                {
                    if (Directory.Exists(file))
                    {
                        if (recursive)
                        {
                            CleanWorkFolder(file);
                            try
                            {
                                Directory.Delete(file);
                            }
                            catch (Exception e)
                            {
                                Base_logger.Warning(file + "was not deleted");
                                Base_logger.Warning(e.Message);
                            }
                        }
                    }
                    else if (File.Exists(file))
                    {
                        try
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            fileInfo.Attributes = FileAttributes.Normal;

                            fileInfo.Delete();
                        }
                        catch
                        {
                            Base_logger.Warning(file + " was not deleted!");
                        }
                    }
                }


            }
        }
        public static string DesktopPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
        }
        public static string ReadFile(string path)
        {
            string fileContent = null;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    fileContent = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message);
            }

            return fileContent;
        }
        public static void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.SetAttributes(filename, FileAttributes.Normal);
                File.Delete(filename);
            }
        }
        public static void CopyFile(string sourcefile, string targetfile)
        {
            if (!File.Exists(sourcefile))
                throw new FileNotFoundException("cannot find source file: " + sourcefile);

            if (!sourcefile.Equals(targetfile, StringComparison.CurrentCultureIgnoreCase))
            {
                File.Copy(sourcefile, targetfile, true);
                File.SetAttributes(targetfile, FileAttributes.Normal);
            }
        }
        public static void CopyFolder(string sourcefolder, string targetfolder)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(sourcefolder);

            if (!Directory.Exists(sourcefolder))
                throw new DirectoryNotFoundException("cannot find the folder: " + sourcefolder);

            if (!Directory.Exists(targetfolder))
                Directory.CreateDirectory(targetfolder);

            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo file in fileInfos)
            {
                CopyFile(file.FullName, Path.Combine(targetfolder, file.Name));
            }

            DirectoryInfo[] subDirs = directoryInfo.GetDirectories();
            foreach (DirectoryInfo subDir in subDirs)
            {
                CopyFolder(subDir.FullName, Path.Combine(targetfolder, subDir.Name));
            }

        }
        public static void RemoveFileReadOnlyInFolder(string folderPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                File.SetAttributes(fileInfo.FullName, FileAttributes.Normal);
            }
        }
        public static void CheckFilesInFolder(string folderPath, IEnumerable<string> baseFiles)
        {
            DirectoryInfo tempFolder = new DirectoryInfo(folderPath);
            List<string> tempFileNames = new List<string>();
            foreach (FileInfo file in tempFolder.GetFiles())
            {
                tempFileNames.Add(file.Name);
            }
            foreach (string fileName in baseFiles)
            {
                Base_Assert.IsTrue(tempFileNames.Contains(fileName), string.Format("folder doesn't contain file {0}", fileName));
            }
        }

        public static Base_ExamplesPath ExamplesPath = new Base_ExamplesPath();

        
    }

    public class Base_ExamplesPath
    {
        public static string _EsterificationHybridModel_APWZ = @"Hybrid Models\Esterification\Esterification Hybrid Model.apwz";
        public static string _pfdtut_BKP = @"Bulk Chemicals\pfdtut.bkp";
        public static string _BatchopAspirin_APWZ = @"Batch Modeling\Batch Reaction and Crystallization\Batchop Aspirin.apwz";
        public static string _Bioethanolviafermentation_BKP = @"Batch Modeling\Bioethanol via fermentation\Bioethanol via fermentation.bkp";
        public static string _ELECNRTLRateBasedAMPModel_BKP = @"Carbon Capture\Amines ELECNRTL\ELECNRTL_Rate_Based_AMP_Model.bkp";
        public static string _BDOviafermentation_Bkp = @"Biofuel and Biochemicals\BDO via fermentation\BDO via fermentation.bkp";

        public string EsterificationHybridModel_APWZ => Path.Combine(Base_Directory.APlusExampleDir, _EsterificationHybridModel_APWZ);
        public string Pfdtut_BKP => Path.Combine(Base_Directory.APlusExampleDir, _pfdtut_BKP);
        public string BatchopAspirin_APWZ => Path.Combine(Base_Directory.APlusExampleDir, _BatchopAspirin_APWZ);
        public string Bioethanolviafermentation_BKP => Path.Combine(Base_Directory.APlusExampleDir, _Bioethanolviafermentation_BKP);
        public string ELECNRTLRateBasedAMPModel_BKP => Path.Combine(Base_Directory.APlusExampleDir, _ELECNRTLRateBasedAMPModel_BKP);
        public string BDOviafermentation_Bkp => Path.Combine(Base_Directory.APlusExampleDir, _BDOviafermentation_Bkp);


    }
}
