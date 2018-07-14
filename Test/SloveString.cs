using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;
using System.Threading;
using System.Data.OleDb;
using System.Data;
using Microsoft.Win32;
using System.Diagnostics;

namespace Test
{
    public class SloveString
    {
        /// <summary>
        /// ��Htmlת��ΪXML��ʽ
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlToXML(string str)
        {
            str = str.Replace(" ", @"\s+");
            str = str.Replace("\"", "&quot;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("&nbsp;", "&amp;nbsp;");
            str = str.Replace("\"", "\\\"");
            return str;
        }
        /// <summary>
        /// �ַ���ת��Ϊ html
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToHtml(string str)
        {
            str = str.Replace("&amp;", "&");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("''", "'");
            str = str.Replace("&quot;", "\"");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            return str;
        }
        /// <summary>
        /// ����URL��ȡ����ҳ������
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string GetHttpData(string sUrl)
        {
            if (String.IsNullOrEmpty(sUrl))
            {
                return null;
            }
            string html = null;
            HttpWebRequest myRequest = null;
            HttpWebResponse myResp = null;
            StreamReader myReader = null;

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    myRequest = (HttpWebRequest)WebRequest.Create(sUrl);
                    myRequest.Timeout = 60 * 1000;
                    myRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                    myRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.84 Safari/537.36";
                    //if (sUrl.ToLower().IndexOf("643651") > 0)
                    //{
                    //    string path = Config.PATH + "daili.txt";
                    //    ArrayList list = Fayi.General.Collection.IO.ReadAllData(path);

                    //string[] list = new string[] { "http://112.90.178.235:80", "http://211.151.186.228:80" };
                    //Random rd = new Random();

                    //string daili = list[rd.Next(2)];
                    ////ʹ�ô���
                    //WebProxy proxy = new WebProxy();
                    //proxy.Address = new Uri(daili);
                    //proxy.Credentials = new NetworkCredential();
                    //myRequest.Proxy = proxy;
                    //myRequest.UseDefaultCredentials = true;
                    //}
                    //��ȡ
                    myResp = (HttpWebResponse)myRequest.GetResponse();
                    myReader = new StreamReader(myResp.GetResponseStream(), Encoding.UTF8, true);
                    html = myReader.ReadToEnd();
                    if (html != "") break;
                }

                catch
                {
                    continue;
                }
                finally
                {
                    if (myReader != null)
                    {
                        myReader.Close();
                    }
                    if (myResp != null)
                    {
                        myResp.Close();
                    }

                }
            }
            return html;
        }
        public static string GetUTFHttpData(string sUrl)
        {
            if (String.IsNullOrEmpty(sUrl))
            {
                return null;
            }
            string html = null;
            HttpWebRequest myRequest = null;
            HttpWebResponse myResp = null;
            StreamReader myReader = null;

            for (int i = 0; i < 1; i++)
            {
                try
                {
                    myRequest = (HttpWebRequest)WebRequest.Create(sUrl);
                    myRequest.Timeout = 60 * 1000;
                    //string daili = "http://210.42.123.192:3128";
                    //WebProxy proxy = new WebProxy();
                    //proxy.Address = new Uri(daili);
                    //proxy.Credentials = new NetworkCredential();
                    //myRequest.Proxy = proxy;
                    //myRequest.UseDefaultCredentials = true;

                    //��ȡ
                    myResp = (HttpWebResponse)myRequest.GetResponse();
                    myReader = new StreamReader(myResp.GetResponseStream(), Encoding.Default, true);
                    html = myReader.ReadToEnd();
                    if (html != "") break;
                }
                catch
                {
                    continue;
                }
                finally
                {
                    if (myReader != null)
                    {
                        myReader.Close();
                    }
                    if (myResp != null)
                    {
                        myResp.Close();
                    }

                }
            }
            return html;
        }

        //ȥ��������
        /// <summary>
        /// ȥ��������
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StringDebarInterpunction(string s)
        {
            string[] temp = new string[] { " ", ",", "��", ".", "��", "��", "!", "��", "\"", "��", "?", "��", "��", "-", "��", "��", "��", "��", ":", "��", ";", "��", "'", "(", ")", "+", "_", "{", "}", "<", ">", "��", "~", "��", "��", "��", "#", "@", "$", "`", "^", "&", "*", "=", "|", "\\", "/", "%", "��", "��", "��", "[", "]", "��", "��" };
            for (int i = 0; i < temp.Length; i++)
            {
                s = s.Replace(temp[i], "");
            }
            return s;
        }
        //���ݹ���ƥ������ retrun List
        /// <summary>
        /// ���ݹ���ƥ������
        /// </summary>
        /// <param name="sStr">ҳ������</param>
        /// <param name="Patrn">����������ʽ��</param>
        /// <returns>����ƥ������</returns>
        public static ArrayList CutStr(string sStr, string Patrn)
        {
            ArrayList al = new ArrayList();
            try
            {
                Regex tmpreg = new Regex(Patrn, RegexOptions.Compiled);
                MatchCollection sMC = tmpreg.Matches(sStr);
                if (sMC.Count != 0)
                {
                    for (int i = 0; i < sMC.Count; i++)
                    {
                        al.Add(sMC[i].Groups[1].Value);
                    }
                }
            }
            catch
            {
            }
            return al;
        }
        //���ݹ���ƥ������ retrun string
        /// <summary>
        /// ���ݹ���ƥ������
        /// </summary>
        /// <param name="sStr">ҳ������</param>
        /// <param name="Patrn">����������ʽ��</param>
        /// <returns>����ƥ������</returns>
        public static string CutString(string sStr, string Patrn)
        {
            ArrayList al = new ArrayList();
            try
            {
                Regex tmpreg = new Regex(Patrn, RegexOptions.Compiled);
                MatchCollection sMC = tmpreg.Matches(sStr);
                if (sMC.Count != 0)
                {
                    for (int i = 0; i < sMC.Count; i++)
                    {
                        al.Add(sMC[i].Groups[1].Value);
                    }
                }
            }
            catch
            {
            }
            if (al.Count > 0)
                return al[0].ToString();
            else return "";
        }
        //�˷������ڽ�ȡURL��ID��ֻ������ID��ĩβ��URL
        /// <summary>
        /// �˷������ڽ�ȡURL��ID��ֻ������ID��ĩβ��URL
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>����ID</returns>
        public static string CutID(string url, string idRule)
        {
            string id = url.Replace(idRule, "");
            return id;
        }
        //��ʽ�����ӵ�ַ
        /// <summary>
        /// ��ʽ�����ӵ�ַ
        /// </summary>
        /// <param name="BaseUrl">����URL</param>
        /// <param name="theUrl">����URL</param>
        /// <returns>���ظ�ʽ��������URL</returns>
        public static string FormatUrl(string BaseUrl, string theUrl)
        {
            int pathLevel = 0;
            BaseUrl = BaseUrl.Substring(0, BaseUrl.LastIndexOf("/"));
            if (theUrl.StartsWith("./"))
            {
                theUrl = theUrl.Remove(0, 1);
                theUrl = BaseUrl + theUrl;
            }
            else if (theUrl.StartsWith("/"))
            {
                theUrl = BaseUrl + theUrl;
            }
            else if (theUrl.StartsWith("../"))
            {
                while (theUrl.StartsWith("../"))
                {
                    pathLevel = ++pathLevel;
                    theUrl = theUrl.Remove(0, 3);
                }
                for (int i = 0; i < pathLevel; i++)
                {
                    BaseUrl = BaseUrl.Substring(0, BaseUrl.LastIndexOf("/", BaseUrl.Length - 2));
                }
                theUrl = BaseUrl + "/" + theUrl;
            }
            if (!theUrl.StartsWith("http://") && !theUrl.StartsWith("https://"))
            {
                theUrl = BaseUrl + "/" + theUrl;
            }
            return theUrl;
        }
        /// <summary>
        /// ����XML���ƶ�ȡ��������
        /// </summary>
        /// <param name="xmlName">XML����</param>
        /// <param name="categoryName">�������</param>
        /// <returns>������������</returns>
        public static Dictionary<string, string> GetXMLInfo(string xmlName, string categoryName, string path)
        {
            XmlDocument document = new XmlDocument();
            document.Load(path);
            XmlNodeList nodeList = document.GetElementsByTagName("*");
            XmlElement element;
            XmlNamedNodeMap namedNodeMap;

            XmlAttribute attribute;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    element = (XmlElement)nodeList.Item(i);
                    namedNodeMap = element.Attributes;
                    if (namedNodeMap.Count > 0)
                    {
                        for (int j = 0; j < namedNodeMap.Count; j++)
                        {
                            attribute = (XmlAttribute)namedNodeMap.Item(j);
                            string attributeValue = attribute.Value;
                            if (attributeValue.Equals(categoryName))
                            {
                                if (element.ChildNodes[0].Value != null)
                                {
                                    try
                                    {
                                        //�ж�Key�Ƿ����
                                        if (dict.ContainsKey(element.Name))
                                        {
                                            dict[element.Name] = element.ChildNodes[0].Value;
                                        }
                                        else
                                        {
                                            dict.Add(element.Name, element.ChildNodes[0].Value);
                                        }
                                    }
                                    catch
                                    {

                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        if (element.ChildNodes[0].Value != null)
                        {
                            dict.Add(element.Name, element.ChildNodes[0].Value);
                        }
                    }

                }
            }
            catch
            {
            }
            return dict;

        }
        //��ͼƬ��ַ����ͼƬ�����ش���
        /// <summary>
        /// ��ͼƬ��ַ����ͼƬ�����ش���
        /// </summary>
        /// <param name="FileName">ͼƬ���ش��̵�ַ</param>
        /// <param name="Url">ͼƬ��ַ</param>
        /// <returns></returns>
        public static bool SavePhoto(string _Path, string FileName, string Url)
        {
            if (FileName == "" || FileName == null)
            {
                FileName = DateTime.Now.ToString("yyyymmddhhmmss") + ".gif";
            }
            if (!Directory.Exists(_Path))
            {
                Directory.CreateDirectory(_Path);
            }
            //if (Directory.GetFiles(_Path).Length > 3)
            //{
            //    Console.WriteLine("ͼƬ������" + Directory.GetFiles(_Path).Length);
            //    return true;
            //}


            FileName = Path.Combine(_Path, FileName);
            if (System.IO.File.Exists(FileName))
            {
                return true;
            }
            bool Value = false;
            WebResponse response = null;
            //Stream stream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Timeout = 65 * 1000;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31";
                //request.KeepAlive = true;
                request.Method = "GET";

                //string daili = "http://218.249.86.131:553";
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(daili);
                //proxy.Credentials = new NetworkCredential();
                //request.Proxy = proxy;
                //request.UseDefaultCredentials = true;

                response = request.GetResponse();
                if (response.ContentLength < 2048)
                {
                    return false;
                }

                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    //Value = SaveBinaryFile(response, FileName);
                    try
                    {
                        using (Stream inStream = response.GetResponseStream())
                        {
                            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(inStream);
                            bmp.Save(FileName);
                            bmp.Save(FileName);
                            Value = true;
                        }
                    }
                    catch
                    {
                        Value = false;
                    }
                }

            }
            catch (Exception err)
            {
                string aa = err.ToString();
            }
            return Value;
        }
        /// <summary>
        /// Save a binary file to disk.
        /// </summary>
        /// <param name="response">The response used to save the file</param>
        // ���������ļ����浽����
        /// <summary>
        /// ���������ļ����浽����
        /// </summary>
        /// <param name="response"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        private static bool SaveBinaryFile(WebResponse response, string FileName)
        {
            bool Value = true;
            byte[] buffer = new byte[1024];

            try
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                Stream outStream = System.IO.File.Create(FileName);
                Stream inStream = response.GetResponseStream();

                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                }
                while (l > 0);

                outStream.Close();
                inStream.Close();
            }
            catch
            {
                Value = false;
            }
            return Value;
        }
        //ȥ��HTML���
        /// <summary>
        /// ȥ��HTML���
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string ReplaceHtml(string body)
        {
            //body = body.ToLower();
            string temp = Regex.Replace(body, "<td.*?>", "");
            temp = Regex.Replace(temp, "</td>", "");
            temp = Regex.Replace(temp, "<table.*?>", "");
            temp = Regex.Replace(temp, "</table>", "");
            temp = Regex.Replace(temp, "</tr>", "");
            temp = Regex.Replace(temp, "<tr.*?>", "");
            temp = Regex.Replace(temp, "<span.*?>", "");
            temp = Regex.Replace(temp, "</span>", "");
            //temp = Regex.Replace(temp, "<P align=right>", "");
            //temp = Regex.Replace(temp, "<p align=left>", "");
            temp = Regex.Replace(temp, "</tbody>", "");
            temp = Regex.Replace(temp, "</temptagamigopcqh>", "");
            temp = Regex.Replace(temp, "<a.*?>", "");
            temp = Regex.Replace(temp, "<A.*?>", "");
            temp = Regex.Replace(temp, "</a>", "");
            temp = Regex.Replace(temp, "<font.*?>", "");
            temp = Regex.Replace(temp, "</font>", "");
            temp = Regex.Replace(temp, "<div.*?>", "");
            temp = Regex.Replace(temp, "</div>", "");
            temp = Regex.Replace(temp, "<DIV.*?>", "");
            temp = Regex.Replace(temp, "</DIV>", "");
            temp = Regex.Replace(temp, "alt=\".*?\"", "");
            temp = Regex.Replace(temp, "<center>", "");
            temp = Regex.Replace(temp, "</center>", "");
            temp = Regex.Replace(temp, "<input.*?>", "");
            temp = Regex.Replace(temp, "<form.*?>", "");
            temp = Regex.Replace(temp, "</form>", "");
            temp = Regex.Replace(temp, "<tbody>", "");
            temp = Regex.Replace(temp, "</tbody>", "");
            temp = Regex.Replace(temp, "<col.*?>", "");
            try
            {
                int t = temp.IndexOf("<script");
                if (t > -1)
                {
                    int t2 = temp.IndexOf("</script>");
                    string t3 = temp.Substring(t, t2 - t);
                    temp = temp.Replace(t3, "");
                }
            }
            catch { }
            temp = Regex.Replace(temp, "<script.*?>", "");
            temp = Regex.Replace(temp, "</script>", "");
            temp = Regex.Replace(temp, "onclick=\".*?\"", "");
            temp = Regex.Replace(temp, "style=\".*?\"", "");
            temp = Regex.Replace(temp, "<p align=right>.*?</p>", "");
            temp = Regex.Replace(temp, "<!--.*?>", "");
            temp = Regex.Replace(temp, @"\[.*?\]", "");
            temp = Regex.Replace(temp, "<hr.*?>", "");
            temp = Regex.Replace(temp, "<FONT.*?>", "");
            temp = Regex.Replace(temp, "</FONT>", "");
            temp = Regex.Replace(temp, "<base.*?>", "");
            temp = Regex.Replace(temp, "\" target=_blank>", "");
            temp = Regex.Replace(temp, "<style.*?>.*?</style>", "");
            temp = Regex.Replace(temp, "<head>", "");
            temp = Regex.Replace(temp, "</head>", "");
            temp = Regex.Replace(temp, "<body>", "");
            temp = Regex.Replace(temp, "</body>", "");
            temp = Regex.Replace(temp, "<meta.*?>", "");
            return temp;
        }

        public static ArrayList ReadKeyWords(string path)
        {
            string temp = null;
            ArrayList list = new ArrayList();
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    temp = sr.ReadLine();
                    list.Add(temp);
                    while (temp != null)
                    {
                        temp = sr.ReadLine();
                        if (temp != null && temp != "")
                            list.Add(temp);
                    }
                }
            }
            catch
            {

            }
            return list;
        }
        //ɾ��ArrayList�е��ظ�����
        /// <summary>
        /// ɾ��ArrayList�е��ظ�����
        /// </summary>
        /// <param name="arrayList"></param>
        /// <returns></returns>
        public static ArrayList ArrayListToRepeat(ArrayList arrayList)
        {
            for (int i = 0; i < arrayList.Count; i++)
            {
                for (int j = i + 1; j < arrayList.Count; j++)
                {
                    if (arrayList[i].Equals(arrayList[j]))
                    {
                        arrayList.RemoveAt(j);
                        if (i > 0)
                        {
                            i--;
                        }
                    }
                }
            }
            return arrayList;
        }
        /// �漴��ĸ
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateRandom(int Length)
        {
            char[] constant = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant.Length);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(26)]);
            }
            return newRandom.ToString();
        }
        //�ɼ�ͼƬ ����ͼƬToBase64String��ʽ��WebClient��
        /// <summary>
        /// �ɼ�ͼƬ ����ͼƬToBase64String��ʽ��WebClient��
        /// </summary>
        /// <param name="url">ͼ��url</param>
        /// <returns>����ͼƬToBase64String��ʽ</returns>
        public static string DownImageToString(string url)
        {
            byte[] streamByte;//Ҫ�洢��ͼƬ������
            string imageFile = "";
            using (WebClient cilent = new WebClient())
            {
                try
                {
                    streamByte = cilent.DownloadData(url);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                    if (image.Width >= 300)
                    {
                        imageFile = Convert.ToBase64String(streamByte);
                    }
                    else
                    {
                        Console.WriteLine("ͼƬ̫С�������洢  width:" + image.Width + "  height:" + image.Height);
                        Console.WriteLine("url:" + url);
                    }
                }
                catch { }
            }
            return imageFile;
        }
        //�ɼ�ͼƬ ����ͼƬToBase64String��ʽ��WebResponse��
        /// <summary>
        /// �ɼ�ͼƬ ����ͼƬToBase64String��ʽ��WebResponse��
        /// </summary>
        /// <param name="url">ͼ��url</param>
        /// <param name="proxyServer">����</param>
        /// <returns>����ͼƬToBase64String��ʽ</returns>
        public static string DownImageToString(string url, string proxyServer)
        {
            WebResponse rsp = null;
            byte[] retBytes = null;
            string imageFile = "";
            System.Drawing.Image image = null;
            try
            {
                Uri uri = new Uri(url);
                WebRequest req = WebRequest.Create(uri);
                HttpWebRequest reqs = (HttpWebRequest)WebRequest.Create(url);
                //req.Method = "POST";
                reqs.KeepAlive = true;
                reqs.ContentType = "application/x-www-form-urlencoded";
                reqs.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                reqs.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.2; zh-CN; rv:1.9.2.8) Gecko/20100722 Firefox/3.6.8";

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                string strResult = reader.ReadToEnd();

                rsp = req.GetResponse();
                if (!rsp.ContentType.ToLower().StartsWith("text/"))
                {
                    Stream stream = rsp.GetResponseStream();
                    if (!string.IsNullOrEmpty(proxyServer))
                    {
                        req.Proxy = new WebProxy(proxyServer);
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int b;
                        while ((b = stream.ReadByte()) != -1)
                        {
                            ms.WriteByte((byte)b);
                        }
                        image = System.Drawing.Image.FromStream(ms);
                        retBytes = ms.ToArray();
                    }
                    if (image.Width > 300)
                    {
                        imageFile = Convert.ToBase64String(retBytes);
                    }
                    else
                    {
                        Console.WriteLine("ͼƬ̫С�������洢  width:" + image.Width + "  height:" + image.Height);
                        Console.WriteLine("url:" + url);
                    }
                }
                else
                { Console.WriteLine("��ͼ���ʽ"); }
            }
            catch (Exception ex)
            {
                retBytes = null; Console.WriteLine("ͼ��ɼ�����" + ex.ToString());
            }
            finally
            {
                if (rsp != null)
                {
                    rsp.Close();
                    image.Dispose();
                }
            }
            return imageFile;
        }
        /// <summary> 
        /// ȥ���ַ����е����� 
        /// </summary> 
        /// <param name="key"></param> 
        /// <returns></returns> 
        public static string RemoveNumber(string key)
        {
            return Regex.Replace(key, @"\d", "");
        }
        /// <summary> 
        /// ȥ���ַ����еķ����� 
        /// </summary> 
        /// <param name="key"></param> 
        /// <returns></returns> 
        public static string RemoveNotNumber(string key)
        {
            return Regex.Replace(key, @"[^\d]*", "");
        }
        //����ͼƬ
        public static bool DownLoadImage(string Path, string fileName, string Url)
        {
            using (WebClient cilent = new WebClient())
            {
                try
                {
                    if (!Directory.Exists(Path))//�ж�·���Ƿ����
                    {
                        Directory.CreateDirectory(Path);
                    }
                    if (fileName == "")
                    {
                        fileName = DateTime.Now.Ticks.ToString() + ".jpg";
                    }
                    Path = Path + fileName;
                    if (System.IO.File.Exists(Path))//�ж��ļ��Ƿ����
                    {
                        return true;
                    }

                    byte[] streamByte;
                    streamByte = cilent.DownloadData(Url);
                    FileStream fs = new FileStream(Path, FileMode.CreateNew);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(streamByte);
                    bw.Close(); //�رն�������д����
                    fs.Close(); //�ر��ļ���
                    return true;
                }
                catch { }
            }
            return false;
        }

        public static DataTable GetExcelSheet(string FileName, int index)
        {
            DataTable datatable = null;
            try
            {
                //���ӵ�Excel�� 
                string connExecel = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
                //string connExecel = Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";  //Office 07���°汾
                OleDbConnection Conn = new OleDbConnection(connExecel);
                Conn.Open();
                //��ȡSheet�����֡� 
                DataTable schemaTable = Conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                //schemaTable.Rows.CountֵΪSheet������ 
                OleDbDataAdapter thisAdapter = new OleDbDataAdapter("SELECT   *   FROM   [" + schemaTable.Rows[index]["table_name"] + "] ", connExecel);
                DataSet set = new DataSet();
                thisAdapter.Fill(set, "ExcelInfo");
                datatable = set.Tables[0];
                Conn.Close();
            }
            catch
            {
            }
            return datatable;
        }

        public static byte[] GetPictureData(string imagepath)
        {
            /**/
            ////����ͼƬ�ļ���·��ʹ���ļ����򿪣�������Ϊbyte[]     
            FileStream fs = new FileStream(imagepath, FileMode.Open);//�������������ط���   
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }
        public static bool IsPath(string path)
        {
            if (System.IO.File.Exists(path))//�ж��Ƿ����
                return true;
            else
                return false;
        }

        #region RaR �ļ�����
        /// <summary>
        /// �Ƿ�װ��Winrar
        /// </summary>
        /// <returns></returns>
        public static bool Exists()
        {
            RegistryKey the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
            return !string.IsNullOrEmpty(the_Reg.GetValue("").ToString());
        }
        /// <summary>
        /// �����Rar
        /// </summary>
        /// <param name="patch"></param>
        /// <param name="rarPatch"></param>
        /// <param name="rarName"></param>
        public static void CompressRAR(string patch, string rarPatch, string rarName)
        {
            string the_rar;
            RegistryKey the_Reg;
            object the_Obj;
            string the_Info;
            ProcessStartInfo the_StartInfo;
            Process the_Process;
            try
            {
                the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                the_Obj = the_Reg.GetValue("");
                the_rar = the_Obj.ToString();
                the_Reg.Close();
                the_rar = the_rar.Substring(1, the_rar.Length - 7);
                Directory.CreateDirectory(patch);
                //�������
                //the_Info = " a    " + rarName + " " + @"C:Test?70821.txt"; //�ļ�ѹ��
                the_Info = " a    " + rarName + " " + patch + " -r"; ;
                the_StartInfo = new ProcessStartInfo();
                the_StartInfo.FileName = the_rar;
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //����ļ����Ŀ¼
                the_StartInfo.WorkingDirectory = rarPatch;
                the_Process = new Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
                the_Process.WaitForExit();
                the_Process.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ��ѹ
        /// </summary>
        /// <param name="unRarPatch"></param>
        /// <param name="rarPatch"></param>
        /// <param name="rarName"></param>
        /// <returns></returns>
        public static string unCompressRAR(string unRarPatch, string rarPatch, string rarName)
        {
            string the_rar;
            RegistryKey the_Reg;
            object the_Obj;
            string the_Info;
            try
            {
                the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                the_Obj = the_Reg.GetValue("");
                the_rar = the_Obj.ToString();
                the_Reg.Close();
                //the_rar = the_rar.Substring(1, the_rar.Length - 7);

                if (Directory.Exists(unRarPatch) == false)
                {
                    Directory.CreateDirectory(unRarPatch);
                }
                the_Info = "x " + rarName + " " + unRarPatch + " -y";

                ProcessStartInfo the_StartInfo = new ProcessStartInfo();
                the_StartInfo.FileName = the_rar;
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                the_StartInfo.WorkingDirectory = rarPatch;//��ȡѹ����·��

                Process the_Process = new Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
                the_Process.WaitForExit();
                the_Process.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRarPatch;
        }
        #endregion
    }
}
