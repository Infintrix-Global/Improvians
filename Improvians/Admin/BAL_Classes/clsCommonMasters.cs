using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

using System.Web.Mail;
using System.Web.UI.WebControls;
using Evo.Admin.BAL_Classes;
using MailMessage = System.Net.Mail.MailMessage;

namespace Evo.Admin
{
    public class clsCommonMasters
    {

        DataSet ds = new DataSet();
        General objGeneral = new General();
        BAL_Task objTask = new BAL_Task();
        //DataSet ds = new DataSet();
        private string strQuery = string.Empty;

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText.Replace(" ", "+"));
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public void SendMail(string Email, string Username, string Password)
        {
            try
            {
                // Gmail Address from where you send the mail
                var fromAddress = "igportalmail@gmail.com";//"infintrix.world@gmail.com";
                                                       // any address where the email will be sending
                                                       // var toAddress = "mehulrana1901@gmail.com,urvi.gandhi@infintrixglobal.com,nidhi.mehta@infintrixglobal.com,bhavin.gandhi@infintrixglobal.com,mehul.rana@infintrixglobal.com,naimisha.rohit@infintrixglobal.com";

                var toAddress = Email;

                //Password of your gmail address
                const string fromPassword = "admin@1234";
                // Passing the values and make a email formate to display
                string subject = "Your UserName and Password For IG-Portal";
                string body = "Dear ," + "\n";

                body += "Your UserName and passward For IG-Portal :" + "\n";
                body += "UserName : " + Username + " " + "\n\n";
                body += "Passward : " + Password + " " + "\n\n";
                body += "Thank you!" + "\n";
                body += "Warm Regards," + "\n";

                // smtp settings
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 50000;
                }
                // Passing values to smtp object
                smtp.Send(fromAddress, toAddress, subject, body);
            }
            catch (Exception ex)
            {
                General.ErrorMessage(ex.Message + ex.StackTrace);
            }
        }

       

        public void SendMailForgotPassword(string Email)
        {
            try
            {
                // Gmail Address from where you send the mail
                var fromAddress = "igportalmail@gmail.com";//"infintrix.world@gmail.com";
                                                       // any address where the email will be sending
                                                       // var toAddress = "mehulrana1901@gmail.com,urvi.gandhi@infintrixglobal.com,nidhi.mehta@infintrixglobal.com,bhavin.gandhi@infintrixglobal.com,mehul.rana@infintrixglobal.com,naimisha.rohit@infintrixglobal.com";

                var toAddress = Email;

                //Password of your gmail address
                const string fromPassword = "admin@1234";
                // Passing the values and make a email formate to display
                string subject = "OTP For IG-Portal";
                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

                string sRandomOTP = GenerateRandomOTP(6, saAllowedCharacters);
                string body = "Dear ," + "\n";

                body += "Your OTP for IG-Portal is  :" + sRandomOTP + "\n";

                body += "Thank you!" + "\n";
                body += "Warm Regards," + "\n";

                // smtp settings
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    
                    smtp.UseDefaultCredentials = false;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 50000;
                }
                // Passing values to smtp object
                smtp.Send(fromAddress, toAddress, subject, body);
                UpdateOTP(Email, sRandomOTP);
            }
            catch (Exception ex)
            {
                General.ErrorMessage(ex.Message + ex.StackTrace);
            }
        }

      
        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)

        {

            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }

        public DataTable CheckEmailExists(string email)
        {
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@Email", email);

                ds = objGeneral.GetDatasetByCommand_SP("SP_CheckEmaiLExists");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];

        }

        public DataTable CheckCurrentPassword(string mob, string password)
        {
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@Mobile", mob);
                objGeneral.AddParameterWithValueToSQLCommand("@Password", password);
                ds = objGeneral.GetDatasetByCommand_SP("SP_CheckCurrentPassword");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];

        }

        public int UpdatePassword(string email, string password)
        {
            int _isUpdated = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@Email", email);
                objGeneral.AddParameterWithValueToSQLCommand("@Password", password);
                _isUpdated = objGeneral.GetExecuteNonQueryByCommand_SP("SP_UpdatePasswordByEmail");
            }
            catch (Exception ex)
            {
            }
            return _isUpdated;

        }

        public int ChangePassword(string mobile, string password)
        {
            int _isUpdated = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@Mobile", mobile);
                objGeneral.AddParameterWithValueToSQLCommand("@Password", password);
                _isUpdated = objGeneral.GetExecuteNonQueryByCommand_SP("SP_UpdatePasswordByMobile");
            }
            catch (Exception ex)
            {

            }
            return _isUpdated;

        }

        public int UpdateOTP(string email, string otp)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@Email", email);
                objGeneral.AddParameterWithValueToSQLCommand("@OTP", otp);
                _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_UpdateOTP");
            }
            catch (Exception ex)
            {
            }
            return _isInserted;

        }

        public DataTable VerifyOTP(string email, string otp)
        {
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@Email", email);
                objGeneral.AddParameterWithValueToSQLCommand("@OTP", otp);
                ds = objGeneral.GetDatasetByCommand_SP("SP_VerifyOTP");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];

        }


        public int InsertEmployee(Employee objEmployee)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();
             
                objGeneral.AddParameterWithValueToSQLCommand("@Photo", objEmployee.Photo);
                objGeneral.AddParameterWithValueToSQLCommand("@Mobile", objEmployee.Mobile);
                objGeneral.AddParameterWithValueToSQLCommand("@Name", objEmployee.Name);
                objGeneral.AddParameterWithValueToSQLCommand("@Email", objEmployee.Email);
                objGeneral.AddParameterWithValueToSQLCommand("@Password", objEmployee.Password);
                objGeneral.AddParameterWithValueToSQLCommand("@EmployeeCode", objEmployee.EmployeeCode);
                objGeneral.AddParameterWithValueToSQLCommand("@Designation", objEmployee.Designation);
                objGeneral.AddParameterWithValueToSQLCommand("@Department", objEmployee.Department);
                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_AddEmployee");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int UpdateEmployee(Employee objEmployee)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@EmployeeID", objEmployee.EmployeeID);
                objGeneral.AddParameterWithValueToSQLCommand("@Photo", objEmployee.Photo);
                objGeneral.AddParameterWithValueToSQLCommand("@Mobile", objEmployee.Mobile);
                objGeneral.AddParameterWithValueToSQLCommand("@Name", objEmployee.Name);
                objGeneral.AddParameterWithValueToSQLCommand("@Email", objEmployee.Email);
                objGeneral.AddParameterWithValueToSQLCommand("@Password", objEmployee.Password);
                objGeneral.AddParameterWithValueToSQLCommand("@EmployeeCode", objEmployee.EmployeeCode);
                objGeneral.AddParameterWithValueToSQLCommand("@Designation", objEmployee.Designation);
                objGeneral.AddParameterWithValueToSQLCommand("@Department", objEmployee.Department);
                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_UpdateEmployee");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public DataTable GetAllEmployeeList()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 3);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public DataTable GetDepartmentMaster()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 1);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public DataTable GetRoleMaster()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 2);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public DataTable GetFacilityMaster()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 4);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }


        public int RemoveEmployee(int employeeID)
        {
            int _isDeleted = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@EmployeeID", employeeID);
                _isDeleted = objGeneral.GetExecuteScalarByCommand_SP("SP_RemoveEmployee");
            }
            catch (Exception ex)
            {

            }
            return _isDeleted;
        }

        public int AddEmployeeFacility(int employeeID, string FacilityID)
        {
            int _isInserted = -1;
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@EmployeeID", employeeID);
                objGeneral.AddParameterWithValueToSQLCommand("@FacilityID", FacilityID);
                _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_AddEmployeeFacility");
            }
            catch (Exception ex)
            {
            }
            return _isInserted;
        }
    }

}
[Serializable]


public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeCode { get; set; }

    public string Designation { get; set; }
    public string Mobile { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
 
    public string Password { get; set; }

    public string Department { get; set; }
  
    public string Photo { get; set; }
 
}




