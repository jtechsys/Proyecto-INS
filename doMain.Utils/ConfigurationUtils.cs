using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace doMain.Utils
{
    public class ConfigurationUtils : AppSettingsReader
    {

       
        static string globalfile = "global.config";
        static string cryptofile = "crypto.config";
       


        public static List<KeyValuePair<string,object>> ListCryptoValueText(string likekey)
        {
            var list = new List<KeyValuePair<string, object>>();

            if (!FileUtils.Exist(cryptofile))
                FileUtils.CreateFile(cryptofile);

            string info = FileUtils.ReadText(cryptofile);
            StringBuilder sb = new StringBuilder(info);

            string[] lines = sb.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);


            string keyformat = string.Format("*{0}", likekey);

            for (int i = 0; i < lines.Length; i++)
            {
               

                    if (lines[i].Trim() == "")
                        continue;

                    string lineitem = CryptoUtils.Decrypt(lines[i]);

                    if (lineitem.Contains(keyformat))
                    {
                        //lines[i] = keyformat + value;
                        //exist = true;
                        string splitstring = CharacterUtils.Sombrero.ToString();
                        //var item = new KeyValuePair<string, object>();
                        string[] resp = lineitem.Split(new string[] { splitstring }, StringSplitOptions.None);
                        if (resp.Length == 2)
                        {
                          
                            list.Add( new KeyValuePair<string, object>(resp[0], resp[1]));
                        }
                    }               
                }

            return list;
        }

        public static string GetCryptoValueText(string key)
        {
                if (!FileUtils.Exist(cryptofile))               
                FileUtils.CreateFile(cryptofile);

                    string info = FileUtils.ReadText(cryptofile);
                    StringBuilder sb = new StringBuilder(info);

                    string[] lines = sb.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);


                    string keyformat = string.Format("*{0}{1}", key, CharacterUtils.Sombrero);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Trim() == "")
                            continue;

                        string lineitem = CryptoUtils.Decrypt(lines[i]);

                        if (lineitem.Contains(keyformat))
                        {

                            return lineitem.Split(new string[] { keyformat }, StringSplitOptions.None)[1];
                        }
                    }

                return null;
        }

        public static string GetCryptoValueWebText(string key)
        {            
            if (!FileUtils.ExistAppSetting(cryptofile))
                FileUtils.CreateFile(cryptofile);

            string info = FileUtils.ReadText(cryptofile);
            StringBuilder sb = new StringBuilder(info);

            string[] lines = sb.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);


            string keyformat = string.Format("*{0}{1}", key, CharacterUtils.Sombrero);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim() == "")
                    continue;

                string lineitem = CryptoUtils.Decrypt(lines[i]);

                if (lineitem.Contains(keyformat))
                {

                    return lineitem.Split(new string[] { keyformat }, StringSplitOptions.None)[1];
                }
            }

            return null;
        }

        public static void SetCryptoValueText(string key, string value)
        {

            if (!FileUtils.Exist(cryptofile))
                FileUtils.CreateFile(cryptofile);

            string info = FileUtils.ReadText(cryptofile);
            StringBuilder sb = new StringBuilder(info);

            string[] lines = sb.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            bool exist = false;
            string keyformat = string.Format("*{0}{1}", key, CharacterUtils.Sombrero);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                    continue;

                string lineitem = CryptoUtils.Decrypt(lines[i]);
                if (lineitem.Contains(keyformat))
                {
                    lines[i] = CryptoUtils.Encrypt(keyformat + value);
                    exist = true;
                }
            }


            StringBuilder newsb = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "")
                    newsb.AppendLine(lines[i]);

            }

            if (!exist)
            {
                newsb.AppendLine(CryptoUtils.Encrypt(keyformat + value));
            }

            FileUtils.WriteText(cryptofile, newsb.ToString());

        }


        public static List<KeyValuePair<string,object>> ListValueText(string likekey)
        {
            var list = new List<KeyValuePair<string, object>>();

            if (!FileUtils.Exist(globalfile))
                FileUtils.CreateFile(globalfile);

            string info = FileUtils.ReadText(globalfile);
            StringBuilder sb = new StringBuilder(info);

            string[] lines = sb.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);


            string keyformat = string.Format("*{0}", likekey);

            for (int i = 0; i < lines.Length; i++)
            {
              

                    if (lines[i].Trim() == "")
                        continue;

                    string lineitem = lines[i];

                    if (lineitem.Contains(keyformat))
                    {
                        //lines[i] = keyformat + value;
                        //exist = true;
                        string splitstring = CharacterUtils.Sombrero.ToString();
                        //var item = new KeyValue();
                        string[] resp = lineitem.Split(new string[] { splitstring }, StringSplitOptions.None);
                        if (resp.Length == 2)
                        {
                            //item.Key = ;
                            //item.Value = resp[1];
                            list.Add(new KeyValuePair<string, object>(resp[0], resp[1]));
                        }

                    }

                
            }


            return list;
        }

        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetAppSettingsCrypto(string key)
        {
            return CryptoUtils.Decrypt(ConfigurationManager.AppSettings[key]);
        }

        public static string GetValueText(string key)
        {
            return GetValueText(key, globalfile);
        }

        

        public static string GetValueText(string key,string fileconfig)
        {

            
                if (!FileUtils.Exist(fileconfig))
                    FileUtils.CreateFile(fileconfig);

                string info = FileUtils.ReadText(fileconfig);
                StringBuilder sb = new StringBuilder(info);

                string[] lines = sb.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);


                string keyformat = string.Format("*{0}{1}", key, CharacterUtils.Sombrero);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Trim() == "")
                        continue;

                    string lineitem = lines[i];

                    if (lineitem.Contains(keyformat))
                    {

                        return lineitem.Split(new string[] { keyformat }, StringSplitOptions.None)[1];
                    }
                }
           

            return "";
        }

        public static void SetValueText(string key, string value)
        {
            SetValueText(key, value, globalfile);
        }

        public static T GetValueObject<T>(string key, string fileconfig)
        {
            string value = GetValueText(key, fileconfig);
            return SerializeUtils.JsonDeserialize<T>(value);
        }

        public static void SetValueObject<T>(string key, T obj, string fileconfig)
        {
            string value = SerializeUtils.JsonSerializer(obj);
            SetValueText(key, value, fileconfig);
        }

        public static void SetValueText(string key, string value, string fileconfig)
        {

            if (!FileUtils.Exist(fileconfig))
                FileUtils.CreateFile(fileconfig);

            string info = FileUtils.ReadText(fileconfig);
            StringBuilder sb = new StringBuilder(info);

            string[] lines = sb.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            bool exist = false;
            string keyformat = string.Format("*{0}{1}", key, CharacterUtils.Sombrero);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                    continue;

                string lineitem = lines[i];
                if (lineitem.Contains(keyformat))
                {
                    lines[i] = keyformat + value;
                    exist = true;
                }
            }


            StringBuilder newsb = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "")
                    newsb.AppendLine(lines[i]);

            }

            if (!exist)
            {
                newsb.AppendLine(keyformat + value);
            }

            FileUtils.WriteText(fileconfig, newsb.ToString());

        }



       


    }
}
