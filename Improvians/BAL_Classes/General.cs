using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Web.Hosting;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Mail;
using System.Web.Configuration;

namespace Evo.BAL_Classes
{
    public class General
    {
        private string mstr_ConnectionString;
        private SqlConnection mobj_SqlConnection;
        private SqlCommand mobj_SqlCommand;
        private int mint_CommandTimeout = 300000;

        public enum ExpectedType
        {

            StringType = 0,
            NumberType = 1,
            DateType = 2,
            BooleanType = 3,
            ImageType = 4
        }

        public General()
        {
            try
            {
                mstr_ConnectionString = ConfigurationManager.ConnectionStrings["Evo"].ToString();
                mobj_SqlConnection = new SqlConnection(mstr_ConnectionString);
                mobj_SqlCommand = new SqlCommand();
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.Connection = mobj_SqlConnection;
                //ParseConnectionString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error initializing data class." + Environment.NewLine + ex.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                //Clean Up Connection Object if (mobj_SqlConnection != null)
                {
                    if (mobj_SqlConnection.State != ConnectionState.Closed)
                    {
                        mobj_SqlConnection.Close();
                    }
                    mobj_SqlConnection.Dispose();
                }
                //Clean Up Command Object if (mobj_SqlCommand != null)
                {
                    mobj_SqlCommand.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error disposing data class." + Environment.NewLine + ex.Message);
            }
        }


        public void CloseConnection()
        {
            if (mobj_SqlConnection.State != ConnectionState.Closed) mobj_SqlConnection.Close();
        }

        #region "********* For SP **********"


        public string GetExecuteScalarByCommand(string Command)
        {
            object identity = 0;
            try
            {
                mobj_SqlCommand.CommandText = Command;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.CommandType = CommandType.Text;

                mobj_SqlConnection.Open();

                mobj_SqlCommand.Connection = mobj_SqlConnection;
                identity = mobj_SqlCommand.ExecuteScalar();
                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
            return identity.ToString();
        }

        public DataSet GetDatasetByCommand_SP(string Command)
        {
            try
            {
                mobj_SqlCommand.CommandText = Command;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.CommandType = CommandType.StoredProcedure;

                mobj_SqlConnection.Open();

                SqlDataAdapter adpt = new SqlDataAdapter(mobj_SqlCommand);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public int GetExecuteScalarByCommand_SP(string Command)
        {
            object identity = 0;
            try
            {
                mobj_SqlCommand.CommandText = Command;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.CommandType = CommandType.StoredProcedure;

                mobj_SqlConnection.Open();

                mobj_SqlCommand.Connection = mobj_SqlConnection;
                identity = mobj_SqlCommand.ExecuteScalar();
                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
            return Convert.ToInt32(identity);
        }


        public int GetExecuteNonQueryByCommand_SP(string Command)
        {
            int rowAffected = -1;
            try
            {
                mobj_SqlCommand.CommandText = Command;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.CommandType = CommandType.StoredProcedure;

                mobj_SqlConnection.Open();

                mobj_SqlCommand.Connection = mobj_SqlConnection;
                rowAffected = mobj_SqlCommand.ExecuteNonQuery();

                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                ErrorMessage(ex.StackTrace + "  " + ex.Message);
                throw ex;
            }
            return rowAffected;
        }

        #endregion


        #region "********* For Quries **********"

       

        public void GetExecuteNonQueryByCommand(string Command)
        {
            try
            {
                mobj_SqlCommand.CommandText = Command;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.CommandType = CommandType.Text;

                mobj_SqlConnection.Open();

                mobj_SqlCommand.Connection = mobj_SqlConnection;
                mobj_SqlCommand.ExecuteNonQuery();

                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }

        public DataTable GetDatasetByCommand(string Command)
        {
            try
            {
                mobj_SqlCommand.CommandText = Command;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.CommandType = CommandType.Text;

                mobj_SqlConnection.Open();

                SqlDataAdapter adpt = new SqlDataAdapter(mobj_SqlCommand);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

        }

        public DataTable GetDatasetByCommand_Paging(string Command, int StartIndex, int PageSize)
        {
            try
            {
                mobj_SqlCommand.CommandText = Command;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.CommandType = CommandType.Text;

                mobj_SqlConnection.Open();

                SqlDataAdapter adpt = new SqlDataAdapter(mobj_SqlCommand);
                DataSet ds = new DataSet();
                adpt.Fill(ds, StartIndex, PageSize, "Data");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }


        public SqlDataReader GetReaderBySQL(string strSQL)
        {
            mobj_SqlConnection.Open();
            try
            {
                SqlCommand myCommand = new SqlCommand(strSQL, mobj_SqlConnection);
                return myCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }

        public SqlDataReader GetReaderByCmd(string Command)
        {
            SqlDataReader objSqlDataReader = null;
            try
            {
                mobj_SqlCommand.CommandText = Command;
                mobj_SqlCommand.CommandType = CommandType.Text;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;

                mobj_SqlConnection.Open();
                mobj_SqlCommand.Connection = mobj_SqlConnection;

                objSqlDataReader = mobj_SqlCommand.ExecuteReader();
                return objSqlDataReader;
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }

        #endregion


        public void AddParameterWithValueToSQLCommand(string ParameterName, object Value)
        {
            try
            {
                mobj_SqlCommand.Parameters.AddWithValue(ParameterName, Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ClearParameters()
        {
            try
            {
                mobj_SqlCommand.Parameters.Clear();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static void ErrorMessage(string msg)
        {

            string ACPPath = HostingEnvironment.MapPath("~/log.txt"); //System.Configuration.ConfigurationManager.AppSettings["Log"];
            StreamWriter swExtLogFile = new StreamWriter(ACPPath, true);
            swExtLogFile.Write(Environment.NewLine);
            swExtLogFile.Write("*****Error message=****" + msg + " at " + DateTime.Now.ToString());
            swExtLogFile.Flush();
            swExtLogFile.Close();
        }



        public string SendNotification(string NotificationFormat)
        {
            FCMResponse response;
            AppSettingsReader settingsReader = new AppSettingsReader();
            string SERVER_API_KEY = (string)settingsReader.GetValue("SERVER_API_KEY", typeof(String));
            var SENDER_ID =  (string)settingsReader.GetValue("SENDER_ID", typeof(String));

            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";

            Byte[] byteArray = Encoding.UTF8.GetBytes(NotificationFormat);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
            tRequest.ContentLength = byteArray.Length;
            tRequest.ContentType = "application/json";
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String responseFromFirebaseServer = tReader.ReadToEnd();
                            response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(responseFromFirebaseServer);
                        }
                    }

                }
            }

            return response.ToString();
        }

        public string getExactPayload(string UserID, string Tokens, string Message, string Title, string typemsg)
        {
            string postData = "";
            postData = "{ \"userid\": \"" + UserID + "\",\"Message\": \"" + Message + "\",\"Title\": \"" + Title + "\",\"Type\": \"" + typemsg + "\"} ";
            //postData = "{\"collapse_key\":\"score_update\",\"time_to_live\":108,\"delay_while_idle\":true,\"priority\":\"high\",\"data\": { \"userid\": \"" + UserID + "\",\"Message\": \"" + Message + "\",\"Title\": \"" + Title + "\",\"Type\": \"" + typemsg + "\"}  ,\"registration_ids\":[\"" + Tokens + "\"] }";
            return postData;
        }

        public string SendMessage(int UserID, string Message, string Title, string TypeMsg)
        {
            BAL_Login _ballogin = new BAL_Login();
            string Token = _ballogin.GetFCMToken(UserID);
            var objNotification = new
            {
                to = Token,
                data = new
                {
                    postData = getExactPayload(UserID.ToString(), Token, Message, Title, TypeMsg)
                }

            };
            return SendNotification(Newtonsoft.Json.JsonConvert.SerializeObject(objNotification));
        }

        public class FCMResponse
        {
            public long multicast_id { get; set; }
            public int success { get; set; }
            public int failure { get; set; }
            public int canonical_ids { get; set; }
            public List<FCMResult> results { get; set; }
        }
        public class FCMResult
        {
            public string message_id { get; set; }
        }

        public void SendMail(string ToMail, string CCMail, string subject, string msg)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            string FromMail = WebConfigurationManager.AppSettings["FromEmail"];
            string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];
            smtpClient.Credentials = new System.Net.NetworkCredential(FromMail, FromEmailPassword);

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            mail.Subject = subject;
            mail.Body = msg;

            mail.From = new MailAddress(FromMail);
            mail.To.Add(new MailAddress(ToMail));
            if (!string.IsNullOrEmpty(CCMail))
            {
                mail.CC.Add(new MailAddress(CCMail));
            }

            //  Attachment atc = new Attachment(folderPath, "Uploded Picture");
            //   mail.Attachments.Add(atc);
            smtpClient.Send(mail);
        }

    }
}