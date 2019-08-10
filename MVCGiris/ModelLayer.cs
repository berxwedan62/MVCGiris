using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCGiris
{

    public abstract class ModelLayer
    {
        public Dictionary<string, object> dictList = new Dictionary<string, object>();
        public int Id { get; set; }
        public string Active { get; set; }
        public string Date { get; set; }

    }

    interface IDesigner
    {
        void Add();
    }

    public class Information:ModelLayer,IDesigner
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }

        public Information(Dictionary<string, object> dictList0)
        {
            Add();
        }

        public void Add()
        {
            //using (AdoHelper adoHelper=new AdoHelper())
            //{
            //    dictList.Clear();
            //    string sorgu = "insert into users (img,baslik,icerik,kategori,aktif,tarih,userId) Values (@img,@baslik,@icerik,@kategori,@aktif,@tarih,@userId)";
            //    dictList.Add("@img", Image);
            //    dictList.Add("@baslik", Title);
            //    dictList.Add("@icerik", Content);
            //    dictList.Add("@kategori", Category);
            //    dictList.Add("@aktif", "0");
            //    dictList.Add("@tarih", DateTime.Now);
            //    dictList.Add("@userId", "0");
            //    adoHelper.ExecNonQueryProc(sorgu, dictList);

            //}
        }
    }

    public class Message: ModelLayer
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string SendId { get; set; }
        public string IncomingId { get; set; }
        public string Read { get; set; }


    }

    public class Users: ModelLayer
    {
        public string UserName { get; set; }
        public string UserPw { get; set; }
        public string Email { get; set; }
    }
    

}