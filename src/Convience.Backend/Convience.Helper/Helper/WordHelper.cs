using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Word;
using System.Configuration;
//using System.Web.Configuration;
using System.Web;
//using log4net;
using Document = Microsoft.Office.Interop.Word.Document;

namespace Convience.Helper
{ 
    public class WordHelper
    {
        string templatePath;
        string outputPath;
        public WordHelper()
        {
            string app = System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower();

            templatePath = @"D:\CF.EIP\src\Convience.Backend\Convience.Applications\Convience.ManagentApi\fileStore\Report\Template";

            //templatePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" +@"Report\Template";
            outputPath = @"D:\CF.EIP\src\Convience.Backend\Convience.Applications\Convience.ManagentApi\fileStore\Report\Output\";

            if (!templatePath[templatePath.Length - 1].Equals(@"\"))
            {
                templatePath = templatePath + @"\";
            }
            if (!outputPath[outputPath.Length - 1].Equals(@"\"))
            {
                outputPath = outputPath + @"\";
            }
        }

        /// <summary>
        /// Replace the parser tags in docx document
        /// </summary>
        /// <param name="oxp">OpenXmlPart object</param>
        /// <param name="dct">Dictionary contains parser tags to replace</param>
        private void parse(OpenXmlPart oxp, Dictionary<string, string> dct)
        {
            string xmlString = null;
            using (StreamReader sr = new StreamReader(oxp.GetStream()))
            {
                xmlString = sr.ReadToEnd();
            }
            foreach (string key in dct.Keys)
            {
                string value = dct[key];
                string encodedValue = System.Security.SecurityElement.Escape(value).Replace("\n", "</w:t><w:br/><w:t>");
                xmlString = xmlString.Replace("[$" + key + "$]", encodedValue);
            }
            using (StreamWriter sw = new StreamWriter(oxp.GetStream(FileMode.Create)))
            {
                sw.Write(xmlString);
            }
        }
        /// <summary>
        /// Parse template file and replace all parser tags, return the binary content of
        /// new docx file.
        /// </summary>
        /// <param name="templateFile">template file path</param>
        /// <param name="dct">a Dictionary containing parser tags and values</param>
        /// <returns></returns>
        public string MakeDocxFile(string template, string fileName, Dictionary<string, string> fields)
        {
            fileName = fileName + ".docx";
            string tempFile = Path.ChangeExtension(Path.GetTempFileName(), "docx");
            File.Copy(templatePath + template, tempFile);

            using (WordprocessingDocument wd = WordprocessingDocument.Open(tempFile, true))
            {
                //Replace document body
                parse(wd.MainDocumentPart, fields);
                foreach (HeaderPart hp in wd.MainDocumentPart.HeaderParts)
                {
                    parse(hp, fields);
                }
                foreach (FooterPart fp in wd.MainDocumentPart.FooterParts)
                {
                    parse(fp, fields);
                }
            }
            string outputFile = outputPath + Path.GetFileName(fileName);
            File.Copy(tempFile, outputFile, true);
            return outputFile;
        }
        public byte[] MakeDocxByte(string template, string fileName, Dictionary<string, string> fields)
        {
            string tempFile = MakeDocxFile(template, fileName, fields);
            byte[] buff = File.ReadAllBytes(tempFile);
            File.Delete(tempFile);
            return buff;
        }
        public string MakePdfFile(string wordFile,bool isDelete)
        {
            Microsoft.Office.Interop.Word.Application application = null;
            Microsoft.Office.Interop.Word.Document document = null;
            try
            {
                string pdfFile = Path.ChangeExtension(wordFile, "pdf");
                application = new Microsoft.Office.Interop.Word.Application();
                application.Visible = false;
                document = application.Documents.Open(wordFile);
                document.ExportAsFixedFormat(pdfFile, WdExportFormat.wdExportFormatPDF);
                return pdfFile;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                if (document != null)
                    document.Close();
                if (application != null)
                    application.Quit();
                if (File.Exists(wordFile) && isDelete)
                    File.Delete(wordFile);
            }
        }
        public string MakePdfFile(string template, string fileName, Dictionary<string, string> fields)
        {
            string temp = Path.ChangeExtension(fileName, "docx");
            string wordFile = string.Empty;
            Microsoft.Office.Interop.Word.Application application = null;
            Microsoft.Office.Interop.Word.Document document = null;
            try
            {
                wordFile = MakeDocxFile(template, temp, fields);
                string pdfFile = Path.ChangeExtension(wordFile, "pdf");
                application = new Microsoft.Office.Interop.Word.Application();
               application.Visible = false;
                document = application.Documents.Open(wordFile);
                document.ExportAsFixedFormat(pdfFile, WdExportFormat.wdExportFormatPDF);
                return pdfFile;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                if (document != null)
                    document.Close();
                if (application != null)
                    application.Quit();
                if (File.Exists(wordFile))
                    File.Delete(wordFile);
            }
        }

        public byte[] MakePdfByte(string template, string fileName, Dictionary<string, string> fields)
        {
            string tempFile = MakePdfFile(template, fileName, fields);
            byte[] buff = File.ReadAllBytes(tempFile);
            File.Delete(tempFile);
            return buff;
        }
    }




    public class WordDocumentMerger
    {
        private Application objApp = null;
        private Document objDocLast = null;
        private Document objDocBeforeLast = null;
        public WordDocumentMerger()
        {
            objApp = new Application();
        }
        #region 打開文件
        private void Open(string tempDoc)
        {
            object objTempDoc = tempDoc;
            object objMissing = System.Reflection.Missing.Value;

            objDocLast = objApp.Documents.Open(
                 ref objTempDoc,    //FileName
                 ref objMissing,   //ConfirmVersions
                 ref objMissing,   //ReadOnly
                 ref objMissing,   //AddToRecentFiles
                 ref objMissing,   //PasswordDocument
                 ref objMissing,   //PasswordTemplate
                 ref objMissing,   //Revert
                 ref objMissing,   //WritePasswordDocument
                 ref objMissing,   //WritePasswordTemplate
                 ref objMissing,   //Format
                 ref objMissing,   //Enconding
                 ref objMissing,   //Visible
                 ref objMissing,   //OpenAndRepair
                 ref objMissing,   //DocumentDirection
                 ref objMissing,   //NoEncodingDialog
                 ref objMissing    //XMLTransform
                 );
            objDocLast.Activate();
            objDocLast.SpellingChecked = false;//關閉Word的拼寫檢查
            objDocLast.ShowSpellingErrors = false;//關閉Word的拼寫錯誤提示 
        }
        #endregion

        #region 保存文件到輸出模板
        private void SaveAs(string outDoc)
        {
            object objMissing = System.Reflection.Missing.Value;
            object objOutDoc = outDoc;
            objDocLast.SaveAs(
              ref objOutDoc,      //FileName
              ref objMissing,     //FileFormat
              ref objMissing,     //LockComments
              ref objMissing,     //PassWord    
              ref objMissing,     //AddToRecentFiles
              ref objMissing,     //WritePassword
              ref objMissing,     //ReadOnlyRecommended
              ref objMissing,     //EmbedTrueTypeFonts
              ref objMissing,     //SaveNativePictureFormat
              ref objMissing,     //SaveFormsData
              ref objMissing,     //SaveAsAOCELetter,
              ref objMissing,     //Encoding
              ref objMissing,     //InsertLineBreaks
              ref objMissing,     //AllowSubstitutions
              ref objMissing,     //LineEnding
              ref objMissing      //AddBiDiMarks
              );
        }
        #endregion

        #region 循環合併多個文件（複製合併重複的文件）
        ///
        /// 循環合併多個文件（複製合併重複的文件）
        ///
        /// 模板文件
        /// 需要合併的文件
        /// 合併後的輸出文件
        public void CopyMerge(string tempDoc, string[] arrCopies, string outDoc)
        {
            object objMissing = Missing.Value;
            object objFalse = false;
            object objTarget = WdMergeTarget.wdMergeTargetSelected;
            object objUseFormatFrom = WdUseFormattingFrom.wdFormattingFromSelected;
            try
            {
                //打開模板文件
                Open(tempDoc);
                foreach (string strCopy in arrCopies)
                {
                    objDocLast.Merge(
                      strCopy,                //FileName   
                      ref objTarget,          //MergeTarget
                      ref objMissing,         //DetectFormatChanges
                      ref objUseFormatFrom,   //UseFormattingFrom
                      ref objMissing          //AddToRecentFiles
                      );
                    objDocBeforeLast = objDocLast;
                    objDocLast = objApp.ActiveDocument;
                    if (objDocBeforeLast != null)
                    {
                        objDocBeforeLast.Close(
                          ref objFalse,     //SaveChanges
                          ref objMissing,   //OriginalFormat
                          ref objMissing    //RouteDocument
                          );
                    }
                }
                //保存到輸出文件
                SaveAs(outDoc);
                foreach (Document objDocument in objApp.Documents)
                {
                    objDocument.Close(
                      ref objFalse,     //SaveChanges
                      ref objMissing,   //OriginalFormat
                      ref objMissing    //RouteDocument
                      );
                }
            }
            finally
            {
                objApp.Quit(
                  ref objMissing,     //SaveChanges
                  ref objMissing,     //OriginalFormat
                  ref objMissing      //RoutDocument
                  );
                objApp = null;
            }
        }
        ///
        /// 循環合併多個文件（複製合併重複的文件）
        ///
        /// 模板文件
        /// 需要合併的文件
        /// 合併後的輸出文件
        public void CopyMerge(string tempDoc, string strCopyFolder, string outDoc)
        {
            string[] arrFiles = Directory.GetFiles(strCopyFolder);
            CopyMerge(tempDoc, arrFiles, outDoc);
        }
        #endregion

        #region 循環合併多個文件（插入合併文件）
        ///
        /// 循環合併多個文件（插入合併文件）
        ///
        /// 模板文件
        /// 需要合併的文件
        /// 合併後的輸出文件
        public void InsertMerge(string tempDoc, string[] arrCopies, string outDoc)
        {
            object objMissing = Missing.Value;
            object objFalse = false;
            object confirmConversion = false;
            object link = false;
            object attachment = false;
            try
            {
                //打開模板文件
                Open(tempDoc);
                foreach (string strCopy in arrCopies)
                {
                    objApp.Selection.InsertFile(
                        strCopy,
                        ref objMissing,
                        ref confirmConversion,
                        ref link,
                        ref attachment
                        );
                }
                //保存到輸出文件
                SaveAs(outDoc);
                foreach (Document objDocument in objApp.Documents)
                {
                    objDocument.Close(
                      ref objFalse,     //SaveChanges
                      ref objMissing,   //OriginalFormat
                      ref objMissing    //RouteDocument
                      );
                }
            }
            finally
            {
                objApp.Quit(
                  ref objMissing,     //SaveChanges
                  ref objMissing,     //OriginalFormat
                  ref objMissing      //RoutDocument
                  );
                objApp = null;
            }
        }
        ///
        /// 循環合併多個文件（插入合併文件）
        ///
        /// 模板文件
        /// 需要合併的文件
        /// 合併後的輸出文件
        public void InsertMerge(string tempDoc, string strCopyFolder, string outDoc)
        {
            string[] arrFiles = Directory.GetFiles(strCopyFolder);
            InsertMerge(tempDoc, arrFiles, outDoc);
        }
        #endregion
    }
}
