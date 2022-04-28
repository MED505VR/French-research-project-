using System.IO;
using UnityEditor;
using UnityEngine;

namespace DataRecording
{
    public static class CsvPositionWriter
    {
        private static string reportDirectoryName = "Report";
        private static string reportFileName = GetDayStamp()+".csv";
        private static string reportSeparator = ",";
        private static string timeStampHeader = "time stamp";
        private static string[] reportHeaders = new string[18]
        {
            "Head X position",  //float or double
            "Head Y position",  //float or double
            "Head Z position",  //float or double
            "Head X rotation",  //float or double
            "Head Y rotation",  //float or double
            "Head Z rotation",  //float or double
            "Lhand X position",  //float or double
            "Lhand Y position",  //float or double
            "Lhand Z position",  //float or double
            "Lhand X rotation",  //float or double
            "Lhand Y rotation",  //float or double
            "Lhand Z rotation",  //float or double
            "Rhand X position",  //float or double                                                                                                                                                                                                                                                                            
            "Rhand Y position",  //float or double
            "Rhand Z position",  //float or double
            "Rhand X rotation",  //float or double
            "Rhand Y rotation",  //float or double
            "Rhand Z rotation",  //float or double
        };

        #region Interactions

        public static void AppendToReport(int num,string[] strings)
        {
            Debug.Log(GetDayStamp());
            VerifyDirectory();
            VerifyFile(num);
            using (StreamWriter sw = File.AppendText(GetFilePath(num)))
            {
                string finalString = "";
                finalString += reportSeparator + GetTimeStamp();
                for (int i = 0; i < strings.Length; i++)
                {
                    if (finalString != "")
                    {
                        finalString += reportSeparator;
                    }
                    finalString += strings[i];
                }
                
                sw.WriteLine(finalString);
            }
        }

        public static void CreateReport(int num)
        {
            VerifyDirectory();
            using (StreamWriter sw = File.CreateText(GetFilePath(num)))
            {
                string finalString = "";
                finalString += reportSeparator + timeStampHeader;
                for (int i = 0; i < reportHeaders.Length; i++)
                {
                    if (finalString != "")
                    {
                        finalString += reportSeparator;
                    }

                    finalString += reportHeaders[i];
                }
                sw.WriteLine(finalString);
            }
        }
        
        #endregion
        
        #region Operations

        static void VerifyDirectory()
        {
            string dir = GetDirectoryPath();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        static void VerifyFile(int playernum)
        {
            string file = GetFilePath(playernum);
            if (!File.Exists(file))
            {
                CreateReport(playernum);
            }
        }

        #endregion

        #region Queries

        static string GetDirectoryPath()
        {
            
            return Application.persistentDataPath + "/" + reportDirectoryName + "/" + GetDateStamp();
        }

        static string GetFilePath(int playerNum)
        {
            return GetDirectoryPath() + "/" + playerNum + reportFileName;
        }

        static string GetTimeStamp()
        {
            return System.DateTime.UtcNow.ToString();
        }

        static string GetDayStamp()
        {
            System.DateTime theTime = System.DateTime.Now;
            string time = "T" + theTime.Hour + "-" + theTime.Minute;
            return time;
        }
        
        static string GetDateStamp()
        {
            System.DateTime theTime = System.DateTime.Now;
            string date = theTime.Year + "-" + theTime.Month + "-" + theTime.Day;
            return date;
        }
        
        #endregion
    }
    
    
}
