using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;
//using DataAccess.SqlServer;
using DataAccess.MySql;
using Dapper;
using MySql.Data.MySqlClient;
using Qiniu.Conf;
using Qiniu.RS;
using Qiniu.IO;
using Qiniu.FileOp;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Drawing;
using Cache.RadisCache;
using System.Data.OleDb;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Diagnostics;
using ImageProcessor.Imaging.Formats;
using ImageProcessor;
using System.Threading;
using System.Text.RegularExpressions;
using iTextSharp.text;
using System.Collections;
using System.Net.Http;

namespace Test
{
    public enum DBName { SiteDB }
    public class Program
    {

        static void Main(string[] args)
        {
            #region 经典递归
            //经典递归
            //var recursive_list = new List<int>();
            //for (int i = 1; i < 30; i++)
            //{
            //    recursive_list.Add(GetRecursive(i));
            //}
            #endregion

            #region 快速排序 递归
            //var func = new Func<IList<int>, IList<int>>((arr) =>
            //{
            //    return arr;
            //});
            ////快速排序 递归
            //var list = new List<int> { 5, 9, 7, 3, 99, 11, 4, 58, 24, 88, 42 };
            //var t = QuickSort(list);
            //Console.WriteLine(string.Join(",", t.ToArray()));
            #endregion

            #region skip/take 分页排序
            /*
             * skip take 获取数据
             * skip 跳过n条之后数据
             * take 获取当前连续n条数据
             */
            //var list = new List<int>();
            //for (int i = 0; i < 30; i++)
            //{
            //    list.Add(i + 1);
            //}
            //Console.WriteLine(string.Join(",", list.Take(10).ToArray()));
            //Console.WriteLine(string.Join(",", list.Skip(10).Take(10).ToArray()));
            //Console.WriteLine(string.Join(",", list.Skip(20).Take(10).ToArray()));
            //Console.WriteLine(string.Join(",", list.Skip(20).Take(10).OrderBy(x=>x).ToArray()));
            //Console.WriteLine(string.Join(",", list.Skip(20).Take(10).OrderByDescending(x => x).ToArray()));
            #endregion

            Console.WriteLine("END");
            
            #region linq join
            //List<Student> student = new List<Student>() { 
            //    new Student(){ID=1,Name="张6", Age=20, Gender=1, GradeID=1},
            //    new Student(){ID=2,Name="张5", Age=16, Gender=0, GradeID=5},
            //    new Student(){ID=3,Name="张6", Age=12, Gender=0, GradeID=2},
            //    new Student(){ID=4,Name="张8", Age=18, Gender=0, GradeID=4},
            //    new Student(){ID=5,Name="张4", Age=21, Gender=1, GradeID=2},
            //    new Student(){ID=6,Name="张2", Age=10, Gender=0, GradeID=3},
            //    new Student(){ID=7,Name="张1", Age=13, Gender=1, GradeID=2},
            //    new Student(){ID=8,Name="张11", Age=15, Gender=1, GradeID=2},
            //    new Student(){ID=9,Name="张12", Age=11, Gender=1, GradeID=2},
            //    new Student(){ID=10,Name="张13", Age=17, Gender=1, GradeID=2},
            //    new Student(){ID=11,Name="张21", Age=13, Gender=1, GradeID=2},
            //    new Student(){ID=12,Name="张9", Age=16, Gender=1, GradeID=6},
                
            //    new Student(){ID=11,Name="张21", Age=13, Gender=1, GradeID=7},
            //    new Student(){ID=12,Name="张9", Age=16, Gender=1, GradeID=8},
            //};
            //List<Grade> grade = new List<Grade>() { 
            //    new Grade(){ID=1, Name="1",  Floor=1},
            //    new Grade(){ID=2, Name="2",  Floor=2},
            //    new Grade(){ID=3, Name="3",  Floor=3},
            //    new Grade(){ID=4, Name="4",  Floor=4},
            //    new Grade(){ID=5, Name="5",  Floor=5},
            //    new Grade(){ID=6, Name="6",  Floor=6},
            //    new Grade(){ID=10, Name="10",  Floor=10},
            //};
            //var result = (from s in student
            //              join g in grade on s.GradeID equals g.ID
            //              where s.GradeID == 2 && s.Age > 10 && s.Age < 18
            //              orderby s.ID ascending
            //              //group s by new { s.Gender} into g
            //              select new
            //              {
            //                  SID = s.ID,
            //                  s.Name,
            //                  s.Age,
            //                  s.Gender,
            //                  GID = g.ID,
            //                  GradeName = g.Name,
            //                  g.Floor
            //              });
            //var left = from s in student
            //           join g in grade on s.GradeID equals g.ID into t
            //           from g in t.DefaultIfEmpty()
            //           select new
            //           {
            //               SID = s.ID,
            //               s.Name,
            //               s.Age,
            //               s.Gender,
            //               GID = g!=null? g.ID:0,
            //               GradeName =g!=null?g.Name:string.Empty,
            //               Floor = g!=null? g.Floor:0
            //           };
            //foreach (var item in left)
            //{
            //    Console.WriteLine(string.Format("ID:{0} - Name:{1} - Age:{2} - Gender:{3} - GradeName:{4} - Floor:{5} - GID:{6}", item.SID, item.Name, item.Age, item.Gender, item.GradeName, item.Floor,item.GID));
            //}

            //var right = from g in grade
            //            join s in student on g.ID equals s.GradeID into t
            //           from s in t.DefaultIfEmpty()
            //           select new
            //           {
            //               ID = s != null ? s.ID : 0,
            //               Name = s!=null?s.Name:string.Empty,
            //               GID = g != null ? g.ID : 0,
            //               GradeName = g != null ? g.Name : string.Empty,
            //               Floor = g != null ? g.Floor : 0
            //           };
            //foreach (var item in right)
            //{
            //    Console.WriteLine(string.Format("ID:{0} - Name:{1} - GradeName:{2} - Floor:{3} - GID:{4}", item.ID, item.Name, item.GradeName, item.Floor, item.GID));
            //}

            //int pageSize = 3, pageIndex = 1;
            //var result1 = result.Skip((pageIndex-1)*pageSize).Take(pageSize);
            //pageIndex = 2;
            //var result2 = result.Skip((pageIndex-1)*pageSize).Take(pageSize);

            //foreach (var item in result)
            //{
            //    Console.WriteLine(string.Format("ID:{0} - Name:{1} - Age:{2} - Gender:{3} - GradeName:{4} - Floor:{5}",item.SID, item.Name, item.Age, item.Gender, item.GradeName, item.Floor));
            //}
            //Console.WriteLine("result1--");
            //foreach (var item in result1)
            //{
            //    Console.WriteLine(string.Format("ID:{0} - Name:{1} - Age:{2} - Gender:{3} - GradeName:{4} - Floor:{5}", item.SID, item.Name, item.Age, item.Gender, item.GradeName, item.Floor));
            //}
            //Console.WriteLine("result2--");
            //foreach (var item in result2)
            //{
            //    Console.WriteLine(string.Format("ID:{0} - Name:{1} - Age:{2} - Gender:{3} - GradeName:{4} - Floor:{5}", item.SID, item.Name, item.Age, item.Gender, item.GradeName, item.Floor));
            //}
            #endregion

            #region Qiniu
            //Qiniu.Conf.Config.ACCESS_KEY = "2U2RcNJt3BfD7HDGfFGLoRY4s3UdF8x******";
            //Qiniu.Conf.Config.SECRET_KEY = "Z1_jfUyy3ema1Qvu5ihjjTanASGEMX********";

            //string inputPath = @"F:\movie-image\process\",
            //    baseLog = System.AppDomain.CurrentDomain.BaseDirectory + "log_qiniu.txt";

            //var files = System.IO.Directory.GetFiles(inputPath);

            //foreach (var item in files)
            //{
            //    //string fileName = item.Substring(item.LastIndexOf("\\") + 1);             
            //    Action<string> putFile = (url) =>
            //    {
            //        var fileName = url.Substring(url.LastIndexOf("\\") + 1);
            //        if (!fileName.Contains("-")) return;
            //        var saveKey = "image/movie/" + fileName;
            //        var policy = new PutPolicy("myweb0802", 10);
            //        string upToken = policy.Token();
            //        PutExtra extra = new PutExtra();
            //        var result = new IOClient().PutFile(upToken, saveKey, url, extra);
            //        Console.WriteLine(result.OK);
            //        if (!result.OK)
            //        {
            //            Console.WriteLine(result.Exception.Message);
            //        }
            //        else
            //        {
            //            var temp = fileName.Split('-');
            //            var sql = string.Format("INSERT INTO `movie_db`.`movie_images` (`name`,`createdAt`,`old_id`,`domain`)VALUES('{0}','{1}','{2}','{3}') ;", saveKey, DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), temp[1], temp[0]);
            //            var data_result = new DBClient().Execute(sql, "movie_db");
            //        }
            //    };
            //    putFile.BeginInvoke(item, null, null);
            //    Thread.Sleep(300);
            //}
            #endregion
            Console.ReadKey();

            #region Regex AESEncrypt userAgent
            //var id = "eee123456789";
            //Regex reg = new Regex(@"^\d+$");
            //if (reg.Match(id).Success)
            //{
            //    Console.WriteLine("ok");
            //}
            //id = "gdgd123456789DDD";
            //if (reg.Match(id).Success)
            //{
            //    Console.WriteLine("ok");
            //}
            //id = "10056";
            //if (reg.Match(id).Success)
            //{
            //    Console.WriteLine("ok");
            //}


            //var temp = Test.AESHelper.AESEncrypt("13800138000", "auto@life");
            //var temp2 = Test.AESHelper.AESDecrypt(temp, "auto@life");

            //Console.WriteLine(temp + "--" + temp2);

           

            //var userAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
            ////"Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1";
            //Regex regex = new Regex("Android|iPhone|iPod|mobile|ucweb|Windows Phone|SymbianOS", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //if (regex.IsMatch(userAgent))
            //{
            //    Console.WriteLine("/");
            //}
            //else
            //{
            //    Console.WriteLine("http:/www.auto.life/");
            //}
            #endregion

            #region redis action
            //var redis = new Radis(1);
            //redis.Set<string>("redis", "this is string");
            //var value = redis.Get<string>("redis");
            //Console.WriteLine(value);
            #endregion

            #region mysql movie 图片下载
            //DataAccess.MySql.DBClient dbclient = new DBClient();
            //var data = dbclient.GetData("SELECT mt.Id,mt.Gid,mt.Title FROM movie_2tu mt", "mysql");
            //string basePath = @"E:\movie-image\2tu", outPath = @"E:\movie-image\2tu-thumb";

            //foreach (DataRow item in data.Rows)
            //{
            //    var id = item["id"].ToString();
            //    var title = item["title"].ToString();
            //    var oldid = item["gid"].ToString();

            //    string titles = string.Empty;
            //    if (title.Contains("/"))
            //    {
            //        titles = title.Substring(0, title.IndexOf("/"));
            //    }
            //    else if (title.Contains("1280"))
            //    {
            //        titles=title.Substring(0, title.IndexOf("1280"));
            //    }
            //    else if (title.Contains("DVD"))
            //    {
            //        titles = title.Substring(0, title.IndexOf("DVD"));
            //    }
            //    else
            //    {
            //        titles = rec(title);
            //        if (titles.IndexOf("-") > -1)
            //        {
            //            titles = titles.Substring(titles.IndexOf("-") + 1);
            //        }
            //        else if (titles.IndexOf("：") > -1)
            //        {
            //            titles = titles.Substring(titles.IndexOf("：") + 1);
            //        }
            //    }

            //    if (titles.IndexOf("-") > -1)
            //    {
            //        titles = titles.Substring(titles.IndexOf("-") + 1);
            //    }
            //    if (titles.IndexOf("：") > -1)
            //    {
            //        titles = titles.Substring(titles.IndexOf("：") + 1);
            //    }

            //    titles = SloveString.StringDebarInterpunction(titles).Replace("电影", "").Trim();
            //    Console.WriteLine(id);
            //    Console.WriteLine(title);
            //    Console.WriteLine(titles);

            //    var oids = System.IO.File.ReadAllLines(System.IO.Path.Combine(basePath, "id.txt")).ToList();
            //    if (oids.Contains(oldid))
            //    {
            //        continue;
            //    }

            //    Action<string, string, string> getImage = (_title, olid, bpath) =>
            //    {
            //        var _url = "http://baike.baidu.com/api/openapi/BaikeLemmaCardApi?scope=103&format=json&appid=379020&bk_key=" + _title + "&bk_length=600";
            //        var html = SloveString.GetHttpData(_url);
            //        if (html.Contains("html"))
            //        {
            //            Thread.Sleep(30 * 1000);
            //            html = SloveString.GetHttpData(_url);
            //        }
            //        try
            //        {
            //            var json = JObject.Parse(html);
            //            JToken jt = null;
            //            if (json.TryGetValue("image", out jt))
            //            {
            //                if (SloveString.SavePhoto(bpath, olid + ".jpg", jt.ToString()))
            //                {
            //                    Console.WriteLine("ok:" + oldid);
            //                    WriteText(System.IO.Path.Combine(basePath, "id.txt"), oldid);
            //                }
            //            }
            //            else
            //            {
            //                WriteText(System.IO.Path.Combine(basePath, "error.txt"), oldid);
            //                Console.WriteLine("error");
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            WriteText(System.IO.Path.Combine(basePath, "baikeNO.txt"), oldid);
            //            Console.WriteLine(ex.Message);
            //        }
            //    };
            //    var result = getImage.BeginInvoke(titles, oldid, basePath, null, null);
            //    //result.AsyncWaitHandle.WaitOne();
            //    Thread.Sleep(300);
            //}

            //var array = Directory.GetFiles(@"E:\movie-image\2tu");
            //var outPath = @"E:\movie-image\2tu-thumb";
            //Size size = new Size(300, 450);
            //ISupportedImageFormat format = new JpegFormat { Quality = 85 };
            //ImageProcessor.Imaging.ResizeLayer resize = new ImageProcessor.Imaging.ResizeLayer(size, ImageProcessor.Imaging.ResizeMode.Max);

            //foreach (var item in array)
            //{
            //    var fileName = item.Substring(item.LastIndexOf("\\") + 1);
            //    Console.WriteLine(fileName);
            //    using (FileStream fs = new FileStream(item, FileMode.Open, FileAccess.Read, FileShare.Read))
            //    {
            //        if (fs.Length == 0) continue;
            //        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            //        {
            //            imageFactory.Load(fs)
            //            .Resize(resize)
            //            .Format(format)
            //            .Save(System.IO.Path.Combine(outPath, fileName));
            //        }
            //    }
            //}
            #endregion

            #region Test 图片切割压缩 ImageProcessor
            //var c = Int64.Parse("10000000").ToString("N");
            //decimal a = 10000.00M;
            //Console.WriteLine(a.ToString("F4"));
            //var c1 = SloveString.GetHttpData("http://www.xiamp4.com/Html/GP24390.html");
            //var c2 = SloveString.GetHttpData("http://www.xiamp4.com/inc/ajax.asp?id=24390&action=videoscore&timestamp=" + GetRandomTimeSpan());

            //var document = SloveString.GetHttpData("http://localhost:8983/solr/core/select?indent=on&q=*:*&wt=json");
            //var obj = JObject.Parse(document);
            //var response = obj["response"];
            //var numFound = response["numFound"].Value<int>();
            //var start = response["start"].Value<int>();
            //var docs = response["docs"].Value<JArray>();
            //Console.WriteLine(docs.Count);
            //foreach (var item in docs)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.WriteLine(str);
            //string[] fruits = { "apple", "passionfruit", "banana", "mango", 
            //          "orange", "blueberry", "grape", "strawberry" };

            //IEnumerable<string> query =
            //    fruits.TakeWhile((fruit, index) => fruit.Length >= index);

            //foreach (string fruit in query)
            //{
            //    Console.WriteLine(fruit);
            //}

            //var data = System.IO.File.ReadAllText(@"F:\workspace\AutoTime\trunk\Web\Auto.Life.A2\virtual\article.txt");
            //JArray _data = new JArray();
            //try
            //{
            //    _data = JArray.Parse(data);
            //}
            //catch { }
            //var data1 = _data.Where(target => target["name"].Value<string>() == "车评").FirstOrDefault();
            //var data2 = _data.Where(target => target["name"].Value<string>() == "改装").FirstOrDefault();

            //foreach (var item in data1["data"])
            //{
            //    Console.WriteLine(item["thumbnail"]);
            //}

            //var data3 = _data.Select(e => e["data"]);
            //Console.WriteLine(data3.Count());
            //foreach (var item in data3)
            //{
            //    foreach (var it in item)
            //    {
            //        Console.WriteLine(it["id"]);
            //    }
            //}

            //var url = "http://baike.baidu.com/api/openapi/BaikeLemmaCardApi?scope=103&format=json&appid=379020&bk_key=复活&bk_length=600";
            //var html=SloveString.GetHttpData(url);

            //SloveString.SavePhoto(@"E:\", "1.jpg", "http://imgsrc.baidu.com/baike/pic/item/1e30e924b899a901bedb4d9c15950a7b0308f5aa.jpg");

            //Console.WriteLine("");
            //var str = string.Join(',', c);
            //Console.WriteLine(str);

            //var list = new List<string>();
            //foreach (var item in System.IO.Directory.GetDirectories(@"E:\fblife\fblife-2\images\logo-bak"))
            //{
            //    var f = System.IO.Directory.GetFiles(item);

            //    foreach (var row in f)
            //    {
            //        if (row.Contains("._logo.png")) continue;
            //        string savePath = @"E:\fblife\logo\" + row.Substring(row.LastIndexOf("logo\\") + 5);
            //        var folder = savePath.Substring(0, savePath.LastIndexOf("\\") + 1);
            //        if (!System.IO.Directory.Exists(folder))
            //            System.IO.Directory.CreateDirectory(folder);
            //        byte[] photoBytes = File.ReadAllBytes(row);
            //        // Format is automatically detected though can be changed.
            //        ISupportedImageFormat format = new JpegFormat { Quality = 85 };
            //        Size size = new Size(250, 250);
            //        using (FileStream fs = new FileStream("",FileMode.Open,FileAccess.Read,FileShare.Read))
            //        {
            //            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            //            {
            //                // Load, resize, set the format and quality and save an image.
            //                imageFactory.Load(fs)
            //                    .Format(format)
            //                    .Resize(new ImageProcessor.Imaging.ResizeLayer(size, ImageProcessor.Imaging.ResizeMode.BoxPad))
            //                    .BackgroundColor(Color.White)
            //                    .Save(savePath.Replace(".png",".jpg"));
            //            }
            //        }
            //    }
            //}

            //string file = @"E:\bak\1920x1080a.jpg";
            //byte[] photoBytes = File.ReadAllBytes(file);
            //// Format is automatically detected though can be changed.
            //ISupportedImageFormat format = new JpegFormat { Quality = 85 };
            //Size size = new Size(640, 400);
            //using (FileStream inStream = new FileStream(inputDir, FileMode.Open, FileAccess.Read, FileShare.Read))
            //{
            //    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            //    {
            //        // Load, resize, set the format and quality and save an image.
            //        imageFactory.Load(inStream)
            //                    //.Resize(size)
            //                    .Format(format);
            //        imageFactory.Resize(new ImageProcessor.Imaging.ResizeLayer(size, ImageProcessor.Imaging.ResizeMode.Crop)).Watermark(new ImageProcessor.Imaging.TextLayer() { Opacity = 100, FontColor = System.Drawing.Color.DarkBlue, FontSize = 30, DropShadow = true, Text = "三角梨免费水印制作",
            //            Position=new Point(300,300)});
            //        //.Save(outStream);
            //        imageFactory.Save(@"E:\bak\n\1.jpg");
            //    }
            //}
            #endregion

            #region mysql脚本导入
            //var filepath = @"E:\backup.sql";
            //using (var fs = new FileStream(filepath, FileMode.Open))
            //{
            //    using (var sr = new StreamReader(fs, Encoding.Default))
            //    {
            //        while (!sr.EndOfStream)
            //        {
            //            string sline = sr.ReadLine();
            //            if (sline.Length > 0)
            //            {
            //                var array = sline.Split(new string[] { "),(" }, StringSplitOptions.RemoveEmptyEntries);
            //                var i = 0;
            //                StringBuilder sb = new StringBuilder();
            //                foreach (var item in array)
            //                {
            //                    if (i > 900) //大于1000无法执行insert
            //                    {
            //                        i = 0;
            //                        var result = new DBClient().Execute(sb.ToString(), "Log");
            //                        sb = new StringBuilder();

            //                        Console.WriteLine("****** insert *****");
            //                    }

            //                    if (!item.Contains("INSERT INTO Log VALUES ("))
            //                    {
            //                        sb.Append("INSERT INTO Log VALUES (" + item + ");\n");
            //                    }
            //                    else
            //                    {
            //                        sb.Append(item + ");\n");
            //                    }
            //                    i++;
            //                }
            //            }
            //            Console.WriteLine("****** next line *****");
            //        }
            //    }
            //}
            #endregion

            #region 多线程插入数据测试
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("insert into Article_Tags(");			
            //strSql.Append("Name,AliaName,AddDate,UpdateDate,Status");
            //strSql.Append(") values (");
            //strSql.Append("@Name,@AliaName,@AddDate,@UpdateDate,@Status");            
            //strSql.Append(") ");
            ////var result = new DBClient().Execute(strSql.ToString(), "sqlserver", new { Name = "test", AliaName = "test", AddDate = DateTime.Now, UpdateDate = DateTime.Now, Status = 1 }); ;

            //var _dic = new Dictionary<string, string>();
            //for (int i = 0; i < 100; i++)
            //{
            //    Action<string> action = (string id) =>
            //    {
            //        Stopwatch sw = new Stopwatch();
            //        sw.Start();
            //        var _result = new DBClient().Execute(strSql.ToString(), "sqlserver", new { Name = "test" + id, AliaName = "test" + id, AddDate = DateTime.Now, UpdateDate = DateTime.Now, Status = 1 }); ;
            //        sw.Stop();
            //        _dic.Add(id, "消耗：" + sw.ElapsedMilliseconds.ToString());
            //        Console.WriteLine(_result + " --- " + id);
            //    };
            //    action.BeginInvoke(i.ToString(), null, null);
            //}
            //var idx = 0;
            //while (true)
            //{
            //    if (idx > 1 * 60)
            //    {
            //        var t = _dic.OrderBy(e => int.Parse(e.Key));
            //        foreach (var item in t)
            //        {
            //            WriteText(@"E:/2.txt", item.Key + " -- " + item.Value);
            //        }
            //        //foreach (var item in _dic)
            //        //{
            //        //    WriteText(@"E:/1.txt", item.Key + " -- " + item.Value);
            //        //}
            //        break;
            //    }
            //    if (_dic.Count == 100)
            //    {
            //        foreach (var item in _dic)
            //        {
            //            WriteText(@"E:/2.txt", item.Key + " -- " + item.Value);
            //        }
            //        break;
            //    }
            //    Console.WriteLine(idx);
            //    System.Threading.Thread.Sleep(1000);
            //    idx++;
            //}
            #endregion

            #region 多线程

            //var html = SloveString.GetHttpData("http://list.jd.com/list.html?cat=9987,653,655");
            //var list = SloveString.CutStr(html, "<div class=\"p-img\" >\\s+<a target=\"_blank\" href=\"([\\S\\s][^<>]*?)\" >");
            //var tasks = new List<Task>();

            //foreach (var item in list)
            //{
            //    var id = item.ToString().Substring(item.ToString().LastIndexOf("/") + 1).Replace(".html","");
            //    var url = "http://p.3.cn/prices/mgets?type=1&area=1_2901_4135_0.138059946&skuIds=J_" + id;
            //    Action<object> action = (object uri) =>
            //    {
            //        var _thtml = SloveString.GetHttpData(uri.ToString());
            //        WriteText(@"E:\jd.txt", DateTime.Now.ToString() + "----" + _thtml);
            //        Console.WriteLine(_thtml + "---" + Task.CurrentId);
            //    };
            //    tasks.Add(Task.Factory.StartNew(action, url));
            //    //var taskStatus = action.BeginInvoke(url, null, null);

            //    //while (!taskStatus.IsCompleted)
            //    //{
            //    //    Thread.Sleep(500);
            //    //    Console.WriteLine("异步线程还没完成，主线程干其他事!");
            //    //}
            //}
            //var alltask = Task.WhenAll(tasks);
            //alltask.Wait();


            //string[] filenames = { "chapter1.txt", "chapter2.txt", "chapter3.txt", "chapter4.txt", "chapter5.txt" };
            //string pattern = @"\b\w+\b";
            //var tasks = new List<Task>();
            //int totalWords = 0;

            //// Determine the number of words in each file.
            //foreach (var filename in filenames)
            //{
            //    tasks.Add(Task.Factory.StartNew(fn =>
            //    {
            //        if (!File.Exists(fn.ToString()))
            //            throw new FileNotFoundException("{0} does not exist.", filename);

            //        StreamReader sr = new StreamReader(fn.ToString());
            //        String content = sr.ReadToEnd();
            //        sr.Close();
            //        int words = Regex.Matches(content, pattern).Count;
            //        Interlocked.Add(ref totalWords, words);
            //        Console.WriteLine("{0,-25} {1,6:N0} words", fn, words);
            //    }, filename));
            //}


            //var finalTask = Task.Factory.ContinueWhenAll(tasks.ToArray(), wordCountTasks =>
            //{
            //    int nSuccessfulTasks = 0;
            //    int nFailed = 0;
            //    int nFileNotFound = 0;
            //    foreach (var t in wordCountTasks)
            //    {
            //        if (t.Status == TaskStatus.RanToCompletion)
            //            nSuccessfulTasks++;

            //        if (t.Status == TaskStatus.Faulted)
            //        {
            //            nFailed++;
            //            t.Exception.Handle((e) =>
            //            {
            //                if (e is FileNotFoundException)
            //                    nFileNotFound++;
            //                return true;
            //            });
            //        }
            //    }
            //    Console.WriteLine("\n{0,-25} {1,6} total words\n", String.Format("{0} files", nSuccessfulTasks), totalWords);
            //    if (nFailed > 0)
            //    {
            //        Console.WriteLine("{0} tasks failed for the following reasons:", nFailed);
            //        Console.WriteLine("   File not found:    {0}", nFileNotFound);
            //        if (nFailed != nFileNotFound)
            //            Console.WriteLine("   Other:          {0}", nFailed - nFileNotFound);
            //    }
            //});
            //finalTask.Wait();

            //异步
            //var i = 0;
            //while (true)
            //{
            //    if (i > 100)
            //        break;
            //    Action<string> action = (string uri) =>
            //    {
            //        Stopwatch t = new Stopwatch();
            //        t.Start();
            //        var _thtml = SloveString.GetHttpData(uri);
            //        t.Stop();
            //        WriteText(@"E:\Action.txt", DateTime.Now.ToString() + "--" + i + "--" + _thtml.Length + "--毫秒：" + t.ElapsedMilliseconds);
            //    };
            //    action.BeginInvoke("http://www.cnblogs.com/x-xk/archive/2012/12/11/2804563.html", null, null);
            //    i++;
            //}

            ////多线程
            //List<Task> tasks2 = new List<Task>();
            //i = 0;
            //while (true)
            //{
            //    if (i > 100)
            //        break;
            //    //CancellationTokenSource cts = new CancellationTokenSource();
            //    tasks2.Add(Task.Factory.StartNew((idx) =>
            //    {
            //        Stopwatch t = new Stopwatch();
            //        t.Start();
            //        var _thtml = SloveString.GetHttpData("http://www.cnblogs.com/x-xk/archive/2012/12/11/2804563.html");
            //        t.Stop();
            //        WriteText(@"E:\Task.txt", DateTime.Now.ToString() + "--" + idx + "--" + _thtml.Length + "--毫秒：" + t.ElapsedMilliseconds);
            //    },i.ToString()));
            //    i++;
            //}
            //var result = Task.Factory.ContinueWhenAll(tasks2.ToArray(), wordCountTasks =>
            //{
            //    int nSuccessfulTasks = 0;
            //    int nFailed = 0;
            //    foreach (var t in wordCountTasks)
            //    {
            //        if (t.Status == TaskStatus.RanToCompletion)
            //            nSuccessfulTasks++;

            //        if (t.Status == TaskStatus.Faulted)
            //        {
            //            nFailed++;
            //            t.Exception.Handle((e) =>
            //            {
            //                Console.WriteLine(e.Message);
            //                return true;
            //            });
            //        }
            //    }
            //    Console.WriteLine("\n{0} nSuccessfulTasks\n", String.Format("{0} files", nSuccessfulTasks));
            //    if (nFailed > 0)
            //    {
            //        Console.WriteLine("{0} tasks failed for the following reasons:", nFailed);
            //    }
            //});
            //result.Wait();

            //var url = "http://www.cnblogs.com/x-xk/archive/2012/12/11/2804563.html";
            //var result = Parallel.For(0, 100, (x, y) =>
            //{
            //    Stopwatch t = new Stopwatch();
            //    t.Start();
            //    var _thtml = SloveString.GetHttpData(url);
            //    t.Stop();
            //    WriteText(@"E:\Parallel.txt", DateTime.Now.ToString() + "--" + x + "--" + _thtml.Length + "--毫秒：" + t.ElapsedMilliseconds);
            //    //return t.ElapsedMilliseconds;
            //});
            //Console.WriteLine(result.IsCompleted);

            #endregion

            #region SQLite
            //var db = DataAccess.SQLite.DBClient.GetTable("select * from view_Ad");
            //var dic = new Dictionary<string, object>()
            //    {
            //        {"Title","测试"},
            //        {"Href",""},
            //        {"TypeID",1},
            //        {"Sort",1},
            //        {"Images",""},
            //        {"SkuID",10000},
            //        {"Price",10.28},
            //        {"Platform","PC"},
            //        {"CreateDate",DateTime.Now},
            //        {"UpdateDate",DateTime.Now},
            //        {"User","111"},
            //    };

            //int id = DataAccess.SQLite.DBClient.Add("CMS",dic);

            //var result2 = DataAccess.SQLite.DBClient.GetTableColumns("view_Ad");

            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(result2);
            //var dic = new Dictionary<string, object>(){
            //     {"CreateDate",DateTime.Now},
            //     {"UpdateDate",DateTime.Now},
            //};
            //var dic_where = new Dictionary<string, object>(){
            //     {"Id",1}
            //};
            //DataAccess.SQLite.DBClient.Update("AD", dic, dic_where);

            //var dic = new Dictionary<string, object>(){
            //    {"Title","测试2"},
            //    {"Href","http://www.auto.life/"},
            //    {"TypeID",1},
            //    {"Images",""},
            //    {"CreateDate",DateTime.Now},
            //    {"UpdateDate",DateTime.Now},
            //    {"Users","测试2"},
            //};
            //var result = DataAccess.SQLite.DBClient.Add("AD", dic);
            #endregion

            #region  pdf 读取
            //System.Text.StringBuilder text = new StringBuilder();
            //using (PdfReader pdf = new PdfReader(@"E:\17.pdf"))
            //{
            //    for (int i = 1; i <= pdf.NumberOfPages; i++)
            //    {
            //        var txt = PdfTextExtractor.GetTextFromPage(pdf, i);
            //        var id = SloveString.CutString(txt, "Online Account Id #:([\\S\\s]*?)\n");
            //        var price = SloveString.CutString(txt, "Total Credit :([\\S\\s]*?)\n");
            //        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(price))
            //        {
            //            Console.WriteLine("");
            //        }
            //        text.AppendLine(i+":"+id + ":" + price);
            //        Console.WriteLine("page:" + i + " -- " + id + ":" + price);
            //    }
            //}
            //System.IO.File.AppendAllText(@"E:\2.txt", text.ToString());
            //int i = 0;
            #endregion

            #region 合并多个pdf
            //var files = System.IO.Directory.GetFiles(@"E:\test\0410");
            //var outMergeFile = @"E:\test\0410\merge.pdf";

            //Document document = new Document();
            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
            //document.Open();
            //PdfContentByte cb = writer.DirectContent;
            //PdfImportedPage newPage;
            //foreach (var item in files)
            //{
            //    PdfReader reader = new PdfReader(item);
            //    int iPageNum = reader.NumberOfPages;
            //    for (int j = 1; j <= iPageNum; j++)
            //    {
            //        document.NewPage();
            //        newPage = writer.GetImportedPage(reader, j);
            //        cb.AddTemplate(newPage, 0, 0);
            //    }
            //}
            //document.Close();



            //var _files = new RecursionFile(@"E:\test\0410\Invocie");
            //var files = _files.Files_Array;
            //string outMergeFile = @"E:\test\0410\newpdf.pdf";
            //using (Document document = new Document())
            //{
            //    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));

            //    document.Open();

            //    PdfContentByte cb = writer.DirectContent;
            //    PdfImportedPage newPage;
            //    foreach (var item in files)
            //    {
            //        PdfReader reader = new PdfReader(item);
            //        int iPageNum = reader.NumberOfPages;
            //        for (int j = 1; j <= iPageNum; j++)
            //        {
            //            document.NewPage();
            //            newPage = writer.GetImportedPage(reader, j);
            //            cb.AddTemplate(newPage, 0, 0);
            //        }
            //    }
            //}
            //Console.Read();
            #endregion

            #region dictonary 序列化
            //var dic = new Dictionary<string, string>()
            //{
            //    { "9ace34ec-15ab-42bc-bc31-23cc7f", "/special/lc71" },
            //    { "eb5d553d-ba3f-4ef3-8d95-82ef05", "/special/c1794" },
            //    { "8ad7b815-4582-416c-bb72-0fc8f6", "/special/amarok2013" },
            //    { "0aa4a51c-9a2c-457a-bbf0-b74252", "/special/landroverdefender2015" },
            //    { "b93a2dc0-182e-48b0-8474-404abb", "/special/f150" },
            //    {"2be8645e-bec1-4caf-b6e2-142c67","/Special/fordraptor2015"},
            //};

            //var dics = dic.Select(item => new { item.Key, Value = dic[item.Key] });
            //string dic_text = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dics);
            #endregion

            #region 筛选测试
            //var text = File.ReadAllText(@"D:\workspace\svn\Auto.Life\virtual\data\listData\special.txt");
            //JObject jo = JObject.Parse(JObject.Parse(text).ToString());

            //int pageSize = 10, PageIndex = 1;
            //string PriceRange = "";
            //string CarType = "";
            //string OrderBy = "Price";
            //string Sort = "Desc";

            //decimal minAmount = 0, maxAmount = 0;
            //try
            //{
            //    if (!string.IsNullOrWhiteSpace(PriceRange))
            //    {
            //        var range = PriceRange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable().Select(e => decimal.Parse(e)).ToArray();
            //        minAmount = range[0];
            //        maxAmount = range[1];
            //    }
            //}
            //catch { }
            ////.Where(e => e["GuidePrice"].Value<decimal>() > 0 && e["GuidePrice"].Value<decimal>() < 45).ToArray();
            ////var data3 = data2.Where(e => e["CarCategory"].Value<string>() == "皮卡").ToArray();

            //IEnumerable<JToken> data = jo.SelectToken("CarLists");
            ////排序
            //if (!string.IsNullOrWhiteSpace(OrderBy))
            //{
            //    if (OrderBy == "Price")
            //    {
            //        if (Sort == "ASC")
            //        {
            //            data = data.OrderBy(e => e["GuidePrice"].Value<decimal>());
            //        }
            //        else
            //        {
            //            data = data.OrderByDescending(e => e["GuidePrice"].Value<decimal>());
            //        }
            //    }
            //    //默认排序
            //}

            ////筛选价格
            //if (minAmount >= 0 && maxAmount > 0)
            //{
            //    data = data.Where(e => e["GuidePrice"].Value<decimal>() > minAmount && e["GuidePrice"].Value<decimal>() < maxAmount);
            //}//级别筛选
            //if (!string.IsNullOrWhiteSpace(CarType))
            //{
            //    data = data.Where(e => e["CarCategory"].Value<string>() == CarType);
            //}
            ////总页数
            //int pageTotal = data.Count() % pageSize > 0 ? (data.Count() % pageSize + 1) : data.Count() / pageSize;

            ////分页
            //data = data.Where((item, idx) => idx >= ((PageIndex - 1) * pageSize) && idx < ((pageSize * PageIndex)));

            //foreach (var item in data.ToArray())
            //{
            //    Console.WriteLine(item["CarGuid"] + ":" + item["GuidePrice"]);
            //}
            #endregion

            #region 图片切割压缩
            //var outPath = @"E:\images-test\1\1.jpg";
            //Size size = new Size(640, 0);
            //ISupportedImageFormat format = new JpegFormat { Quality = 85 };
            //ImageProcessor.Imaging.ResizeLayer resize = new ImageProcessor.Imaging.ResizeLayer(size, ImageProcessor.Imaging.ResizeMode.Max);

            //var fileName = @"E:\images-test\57906074N8c9c510f.jpg";
            //Console.WriteLine(fileName);
            //using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            //{
            //    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            //    {
            //        imageFactory.Load(fs)
            //        .Resize(resize)
            //        .Format(format)
            //        .Save(System.IO.Path.Combine(outPath));
            //    }
            //}

            //ThumbsImage thumbs = new ThumbsImage() { Quality = 100 };
            //thumbs.CutForCustom(@"D:\3.jpg", @"D:\360Downloads\3_Cut.jpg", 1280, 600);
            //thumbs.GetPicThumbnail(@"D:\3.jpg", @"D:\360Downloads\2_Crop.jpg", 100);

            //using (System.Drawing.Image initImage = System.Drawing.Image.FromFile(@"D:\3.jpg"))
            //using (var resized = ImageHelper.CustomImage(initImage, 1280, 600))
            //{
            //    ImageHelper.SaveJpeg(@"D:\360Downloads\CustomImage.jpg", resized, 80);
            //}
            //using (System.Drawing.Image initImage2 = System.Drawing.Image.FromFile(@"D:\1.jpg"))
            //using (var resized = ImageHelper.CustomImage(initImage2, 640, 427))
            //{
            //    ImageHelper.SaveJpeg(@"D:\360Downloads\1.jpg", resized, 90);
            //}
            #endregion

            #region 控制台日志写入文件
            //FileStream fs = new FileStream(@"C:\Users\Administrator\Desktop\xinqiu\css\login.css", FileMode.Append);
            ////StreamWriterWithTimestamp sw = new StreamWriterWithTimestamp(fs);
            //StreamWriter sw = new StreamWriter(fs);
            //sw.AutoFlush = true;
            //Console.SetOut(sw);
            //Console.SetError(sw);
            //Console.WriteLine("ererererere1212");
            #endregion

            #region Qiniu
            //Qiniu.Conf.Config.ACCESS_KEY = "2U2RcNJt3BfD7HDGfFGLoRY4s3UdF8x******";
            //Qiniu.Conf.Config.SECRET_KEY = "Z1_jfUyy3ema1Qvu5ihjjTanASGEMX********";


            //var array = Directory.GetFiles(@"E:\movie-image\ygdy8-new");
            //var outPath = @"E:\movie-image\ygdy8-thumb-new";
            //Size size = new Size(640, 450);
            //ISupportedImageFormat format = new JpegFormat { Quality = 85 };
            //ImageProcessor.Imaging.ResizeLayer resize = new ImageProcessor.Imaging.ResizeLayer(size, ImageProcessor.Imaging.ResizeMode.Max);

            //foreach (var item in array)
            //{
            //    var fileName = item.Substring(item.LastIndexOf("\\") + 1);
            //    Console.WriteLine(fileName);
            //    using (FileStream fs = new FileStream(item, FileMode.Open, FileAccess.Read, FileShare.Read))
            //    {
            //        if (fs.Length == 0) continue;
            //        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            //        {
            //            imageFactory.Load(fs)
            //            .Resize(resize)
            //            .Format(format)
            //            .Save(System.IO.Path.Combine(outPath, fileName));
            //        }
            //    }
            //}

            //array = Directory.GetFiles(@"E:\movie-image\ygdy8-thumb-new");
            //foreach (var item in array)
            //{
            //    int id = 0;
            //    if (!int.TryParse(item.Substring(item.LastIndexOf("\\") + 1).Replace(".jpg", ""), out id))
            //    {
            //        Console.WriteLine("error");
            //    }
            //    Console.WriteLine(id);
            //    //int id = int.Parse(item.Substring(item.LastIndexOf("\\") + 1).Replace(".jpg", ""));
            //    if (id < 50000) continue;
            //    Console.WriteLine(item);
            //    Action<string> putFile = (url) =>
            //    {
            //        var fileName = url.Substring(url.LastIndexOf("\\") + 1);
            //        var saveKey = "image/movie/ygdy8-" + fileName;
            //        var policy = new PutPolicy("myweb0802", 10);
            //        string upToken = policy.Token();
            //        PutExtra extra = new PutExtra();
            //        var result = new IOClient().PutFile(upToken, saveKey, url, extra);
            //        Console.WriteLine(result.OK);
            //        if (!result.OK)
            //        {
            //            Console.WriteLine(result.Exception.Message);
            //        }
            //    };
            //    putFile.BeginInvoke(item, null, null);
            //    Thread.Sleep(300);
            //}

            //Console.WriteLine("ok");
            //Thread.Sleep(1000 * 60);

            //var policy = new PutPolicy("xmall", 3600);
            //System.Console.WriteLine(policy);
            //string upToken = policy.Token();
            //IOClient target = new IOClient();
            //PutExtra extra = new PutExtra();
            //PutRet ret = target.PutFileWithoutKey(upToken, @"C:\Users\Public\Pictures\Sample Pictures\1.jpg", extra);
            //Console.WriteLine(ret.Response.ToString());

            //ImageView imageView = new ImageView { Mode = 0, Width = 1200, Height = 800, Quality = 90, Format = "jpg" };
            //string viewUrl = imageView.MakeRequest("http://lmall.qiniudn.com/FjBCDRqa-yvLYDNYElaa9ENaWc4X");
            //viewUrl = GetPolicy.MakeRequest(viewUrl);
            //Console.WriteLine("ImageViewURL:" + viewUrl);

            
            #endregion

            #region 压缩图片ImageProcessor
            //string inputPath = @"F:\movie-image\original\",
            //    outPath = @"F:\movie-image\process\",
            //    baseLog = System.AppDomain.CurrentDomain.BaseDirectory + "log.txt";

            //var files = System.IO.Directory.GetFiles(inputPath);

            //string file = @"E:\bak\1920x1080a.jpg";
            //byte[] photoBytes = File.ReadAllBytes(file);

            //foreach (var item in files)
            //{
            //    string fileName = item.Substring(item.LastIndexOf("\\") + 1),
            //        newFileName = System.IO.Path.Combine(outPath, fileName);
            //    try
            //    {
            //        using (FileStream inStream = new FileStream(item, FileMode.Open, FileAccess.Read, FileShare.Read))
            //        {
            //            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            //            {
            //                imageFactory.Load(inStream).
            //                    Resize(new ImageProcessor.Imaging.ResizeLayer(new Size(300, 420), ImageProcessor.Imaging.ResizeMode.Max))
            //                    .Format(new JpegFormat { Quality = 85 })
            //                    .Save(newFileName);
            //                Console.WriteLine("OK：" + fileName);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        using (FileStream fs = new FileStream(baseLog, FileMode.Append, FileAccess.Write, FileShare.Write))
            //        {
            //            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
            //            {
            //                sw.WriteLine(item);
            //            }
            //        }
            //    }
            //}
            #endregion

            #region Test DB Connection
            //DBClient db = new DBClient();
            //Execute("INSERT INTO `carattr` VALUES (@ID, @Key, @Value, @Sort, @Group, @ProductID);", new { Id = 0, Key = model.Key, Value = model.Value, Group = model.Group.ID, ProductId = model.ProductID, Sort=0 });
            //var e = db.Execute("insert into carattr(`Id`,`key`,`Value`,`Sort`,`Group`,`ProductId`) VALUES(@Id,@key,@Value,@Sort,@Group,@ProductId)", "mysql.dbmodel", new { Id = 0, key = "测试Key", Value = "测试Value", Sort = 0, Group = 1, ProductId = 1 });

            //var e2 = db.Execute("delete from carattr where id=@id", "mysql.dbmodel", new { id = 24 });

            //Func<MySqlConnection, IEnumerable<CarAttr>> result = (e) =>
            //{
            //    return e.Query<CarAttr, CarAttrGroup, CarAttr>("select a.ID,a.`Key`,a.`Value`,a.ProductID,a.Sort,b.`ID`,b.`Name`,b.`Sort` from carattr a LEFT JOIN carattrgroup b ON a.`Group`=b.ID  order by b.Sort,a.Sort"
            //       , (attr, group) => { attr.Group = group; return attr; }
            //       , null, null, true, "Id", null, null).ToList<CarAttr>();
            //};

            //var e3 = db.Query<CarAttr>("select * from carattr", "mysql.dbmodel", result);

            //var key = DataAccess.Base.GetAppSetting<string>("DataBase");
            //var key1 = DataAccess.Base.GetAppSetting<int>("int");
            //var key2 = DataAccess.Base.GetAppSetting<float>("float");

            //var model = new CarPropertyValue()
            //{
            //    CateDate = DateTime.Now.ToString(),
            //    CreateUser = "",
            //    ModelId = 1,
            //    PropertyID = 32,
            //    Remark = "111",
            //    MoreNum = 1,
            //    Value = "222",
            //};
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update car_propertyvalue set ");

            //strSql.Append(" ID = @ID , ");
            //strSql.Append(" ModelId = @ModelId , ");
            //strSql.Append(" PropertyID = @PropertyID , ");
            //strSql.Append(" MoreNum = @MoreNum , ");
            //strSql.Append(" Value = @Value , ");
            //strSql.Append(" Remark = @Remark , ");
            //strSql.Append(" CateDate = @CateDate , ");
            //strSql.Append(" CreateUser = @CreateUser  ");
            //strSql.Append(" where ID=@ID  ");
            //var result = new DataAccess.MySql.DBClient().Execute(strSql.ToString(), "mysql", model);


            //CarProperty model = new CarProperty() { CNName = "测试", ENName = "test", Sort = 1 };
            //foreach (System.Reflection.PropertyInfo p in model.GetType().GetProperties())
            //{
            //    Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(model));
            //}

            //string strWhere = "";
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select ID, OptionsType, PropertyCategoryID, MoreNum, CNName, ENName, Sort, CreateDate, CreateUser2 ");
            //strSql.Append(" FROM Car_Property ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //var result = new DataAccess.MySql.DBClient().Query<CarProperty>(strSql.ToString(), "mysql", new CarProperty());
            #endregion

            #region cookie
            //HttpCookie cookie;
            //if (app.Context.Request.Cookies.Get("test") != null)
            //{
            //    cookie = app.Context.Request.Cookies.Get("test");
            //    cookie.Expires = DateTime.Now.AddDays(-1);
            //    cookie.Value = "";
            //    cookie.Domain = "auto.life";
            //    cookie.Path = "/";
            //}
            //else
            //{
            //    cookie = new HttpCookie("test", "99999999")
            //    {
            //        Domain = "auto.life",
            //        Path = "/",
            //        Expires = DateTime.Now.AddDays(1),
            //    };
            //}
            //app.Context.Response.Cookies.Add(cookie);
            #endregion

            //System.Threading.Thread.Sleep(1000 * 120);
            Console.ReadKey();
        }

        static List<int> QuickSort(List<int> arr)
        {
            if (arr.Count() <= 1) return arr;

            var mid = Math.Floor((double)(arr.Count() / 2));
            int val = arr[Math.Sign(mid)];
            arr.RemoveAt(Math.Sign(mid));

            var left = new List<int>();
            var right = new List<int>();

            for (int i = 0; i < arr.Count(); i++)
            {
                if (arr[i] < val)
                {
                    left.Add(arr[i]);
                }
                else
                {
                    right.Add(arr[i]);
                }
            }
            return QuickSort(left).Concat(new List<int>() { val }).ToList<int>().Concat(QuickSort(right)).ToList<int>();
            //var _arr = new List<int>();
            //_arr.AddRange(QuickSort(left));
            //_arr.AddRange(new List<int>() { val });
            //_arr.AddRange(QuickSort(right));
            //return _arr;
        }

        //public void test(int i)
        //{
        //    Lock(this)
        //    {
        //      if(i>0)
        //        {
        //        i--;
        //        }
        //        test(i);
        //    }
        //}
        static string GetFileMd5(string path)
        {
            try
            {
                FileStream file = new FileStream(path, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
                //throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        private static void DownDomain(string basePath, ArrayList link)
        {
            Console.WriteLine(basePath);
            foreach (var item in link)
            {
                var it = item.ToString().ToLower();
                var _uri = it.IndexOf("http://") == -1 ? "http://m.auto.life" + item : it;
                if (it.Contains(".ico") || it.Contains(".png") || it.Contains("javascript:") || it.Contains("tel:")) continue;
                var paths = it.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (paths.Count() == 0) continue;
                var _html = SloveString.GetHttpData(_uri);
                if (paths.Count() == 1)
                {
                   
                    if (_uri.Contains(".html"))
                    {
                        if (!System.IO.File.Exists(basePath))
                        {
                            var fileName = _uri.Substring(_uri.Trim('/').LastIndexOf("/")).Trim('/');
                            System.IO.File.WriteAllText(System.IO.Path.Combine(basePath, fileName), _html);
                        }
                    }
                    else
                    {
                        
                        var fileName = _uri.Substring(_uri.Trim('/').LastIndexOf("/")).Trim('/');
                        System.IO.File.WriteAllText(System.IO.Path.Combine(basePath,fileName), _html);
                    }
                    
                }
                else
                {
                    var file = paths[paths.Count() - 1];
                    paths.RemoveAt(paths.Count() - 1);
                    var p = string.Join(@"\", paths.ToArray()).Replace("http:\\", "");
                    var _path = System.IO.Path.Combine(basePath, p);

                    if (!System.IO.Directory.Exists(_path))
                    {
                        System.IO.Directory.CreateDirectory(_path);
                    }
                    var filePath = System.IO.Path.Combine(_path, file);
                    if (filePath.Contains("?"))
                    {
                        filePath = filePath.Substring(0, filePath.IndexOf("?"));
                    }
                    if (!System.IO.File.Exists(filePath))
                    {
                        System.IO.File.WriteAllText(filePath, _html);
                    }
                    else
                    {
                        continue;
                    }

                    if (!it.Contains(".css") && !it.Contains(".js"))
                    {
                        var _link = SloveString.CutStr(_html, "href=\"([\\S\\s][^<>]*?)\"");
                        DownDomain(basePath, _link);
                    }
                }
            }
        }

        static string rec(string v)
        {
            var k = new string[] { "1024高清", "迅雷下载", "悬疑", "惊悚", "恐怖", "剧情", "喜剧", "爱情", "家庭", "奇幻", "犯罪", "科幻", "动作", "纪录", "古装", "历史", "传记", "同性", "冒险", "战争", "儿童", "灾难", "动画", "音乐", "青春", "魔幻", "运动", "情色", "丧尸", "励志", "武侠", "传纪", "歌舞", "穿越", "西部", "短片", "贺岁", "军事", "伦理", "文艺", "微电影", "罪案", "现实", "电影" };
             v = v.IndexOf("：") > -1 ? v.Split(new string[]{"："},StringSplitOptions.RemoveEmptyEntries)[1] : v;

             foreach (var item in k)
             {
                 v = v.Replace(item, "");
             }
            return v;
        }
        static string GetRandomTimeSpan()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        static string ReaderText(string path)
        {
            string text = string.Empty;
            try
            {
                using (FileStream fsRead = new System.IO.FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader sr = new StreamReader(fsRead, System.Text.Encoding.UTF8))
                    {
                        text = sr.ReadToEnd();
                    }
                }
                return text;
            }
            catch { }
            return text;
        }
        static void WriteText(string path, string text)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(text);
                    }
                }
            }
            catch { }
        }
        private static string ChangeDataToD(string strData)
        {
            string temp = strData;
            try
            {
                if (strData.Contains("e"))
                {
                    temp = Convert.ToDecimal(Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float)).ToString();
                }
            }
            catch { }
            
            return temp;
        }
        static DataTable GetExcelSheet(string FileName, int index)
        {
            DataTable datatable = null;
            try
            {
                //连接到Excel； 
                string connExecel = "Provider=Microsoft.ACE.OLEDB.12 .0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
                //string connExecel = Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";  //Office 07以下版本
                OleDbConnection Conn = new OleDbConnection(connExecel);
                Conn.Open();
                //获取Sheet的名字。 
                DataTable schemaTable = Conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                //schemaTable.Rows.Count值为Sheet的总数 
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

        /**
        * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
         * @return 时间戳
        */
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public Task<string> GetHtmlAsync()
        {
            var client = new HttpClient();
            return client.GetStringAsync("http://www.dotnetfoundation.org");
        }

        public async Task<string> GetFirstCharactersCountAsync()
        {
            // Execution is synchronous here
            var client = new HttpClient();

            // Execution of GetFirstCharactersCountAsync() is yielded to the caller here
            // GetStringAsync returns a Task<string>, which is *awaited*
            var page = await client.GetStringAsync("http://www.dotnetfoundation.org");

            // Execution resumes when the client.GetStringAsync task completes,
            // becoming synchronous again.

            return page;
        }

        public static int GetRecursive(int Num)
        {
            if (Num <= 1) return Num;
            int result = GetRecursive(Num - 1) + GetRecursive(Num - 2);
            return result;
        }

    }

    public class Student {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public int GradeID { get; set; }
    }
    public class Grade {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
    }


    public class StreamWriterWithTimestamp : StreamWriter
    {
        public StreamWriterWithTimestamp(Stream stream)
            : base(stream)
        {

        }

        private string GetTimestamp()
        {
            return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] ";
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(GetTimestamp() + value);
        }

        public override void Write(string value)
        {
            base.Write(GetTimestamp() + value);
        }

        public string Name;
    }


    //递归取路径
    /// <summary>
    /// 递归取路径
    /// </summary>
    public class RecursionFile
    {
        public RecursionFile(string strBaseDir)
        {
            this.StrBaseDir = strBaseDir;
            this.Files_Array = new List<string>();
            this.GetAllDir(strBaseDir);
        }

        public List<string> Files_Array { get; set; }
        public string StrBaseDir { get; set; }
        //循环目录递归上传
        private void GetAllDir(string strBaseDir)
        {
            DirectoryInfo[] array = new DirectoryInfo(strBaseDir).GetDirectories();
            foreach (var item in array)
            {
                var files = Directory.GetFiles(item.FullName);
                if (files.Length > 0)
                {
                    this.Files_Array.AddRange(files);
                }
                this.GetAllDir(item.FullName);
            }
        }
    }

}

    
