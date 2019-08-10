using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCGiris
{
    public class AdoHelper : IDisposable
    {
        protected string _connString = null;
        protected SqlConnection _conn = null;

        public AdoHelper()
        {
            Connect();
        }

        protected void Connect()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString());
            _conn.Open();
        }

        //public SqlCommand DeleteCommand(string qry, int id)
        //{
        //    SqlCommand cmd = new SqlCommand(qry, _conn);
        //    cmd.Parameters.AddWithValue("@id", id);
        //    return cmd;

        //}

        public object GetDefault<T>()
        {
            var asd = default(T) as string;

            return default(T);
        }

        public object ExecNonQueryProc(string proc, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(proc, args))
            {
                return cmd.ExecuteNonQuery();
            }
            var a = new DataSet();

            a.Tables.Add(new DataTable());

            var b = a.Tables[0];
        }

        public SqlCommand CreateCommand(string qry, params object[] args)
        {
            SqlCommand cmd = new SqlCommand(qry, _conn);
            int count = ((System.Collections.Generic.Dictionary<string, object>)args[0]).Count;
            int i = 0;
            foreach (Dictionary<string, object> item in args)
            {
                foreach (var item1 in item)
                {
                    if (i < count)
                    {
                        SqlParameter parm = new SqlParameter();
                        parm.ParameterName = item1.Key;
                        parm.Value = item1.Value;
                        cmd.Parameters.Add(parm);
                        i++;
                    }
                }


            }


            return cmd;
        }

        public SqlDataReader ExecDataReaderProc(string proc, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(proc, args))
            {
                return cmd.ExecuteReader();
            }
        }

        //public 


        //public (int toplam, int carpim) ToplamCarpim(int x, int y, int z)
        //{
        //    var t = x + y + z;
        //    var c = x * y * z;
        //    return (t, c);
        //}

        public void Dispose()
        {
            GC.SuppressFinalize(this);

        }
    }
}