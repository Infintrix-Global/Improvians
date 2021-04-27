using Evo.Admin.BAL_Classes;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

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

        public long GetDataInsertORUpdate(string v, NameValueCollection nv1)
        {
            throw new NotImplementedException();
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
                objGeneral.AddParameterWithValueToSQLCommand("@NavisionCustomerID", objEmployee.NavisionCustomerID);
                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_AddEmployee");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int InsertFertilzerMaster(FertilizerMasters obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@Name", obj.FertilizerName);
                objGeneral.AddParameterWithValueToSQLCommand("@IsActive", obj.IsActive);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("AddFertilzerMaster");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }
        public int InsertPlantProductionProfile(ProfilePlanner obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@Code", obj.code);
                objGeneral.AddParameterWithValueToSQLCommand("@Crop", obj.crop);
                objGeneral.AddParameterWithValueToSQLCommand("@traycode", obj.traycode.ToString());
                objGeneral.AddParameterWithValueToSQLCommand("@activitycode", obj.activitycode);
                objGeneral.AddParameterWithValueToSQLCommand("@dateshift", obj.dateshift);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_AddPlantProductionProfile");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }


        public int InsertPlantProductionAddNewProfile(ProfilePlanner obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@Code", obj.code);
                objGeneral.AddParameterWithValueToSQLCommand("@Crop", obj.crop);
                objGeneral.AddParameterWithValueToSQLCommand("@traycode", obj.traycode.ToString());
                objGeneral.AddParameterWithValueToSQLCommand("@dateshift", obj.dateshift);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_AddPlantProductionProfileAddNewRows");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int UpdatePlantProductionAddNewProfile(ProfilePlanner obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@Code", obj.code);
                objGeneral.AddParameterWithValueToSQLCommand("@Crop", obj.crop);
                objGeneral.AddParameterWithValueToSQLCommand("@traycode", obj.traycode.ToString());
                objGeneral.AddParameterWithValueToSQLCommand("@dateshift", obj.dateshift);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_UpdatePlantProductionProfileAddNewRows");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }


        public int InsertChemicalMaster(ChemicalMasters obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@Name", obj.ChemicalName);
                objGeneral.AddParameterWithValueToSQLCommand("@IsActive", obj.IsActive);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("AddChemicalMaster");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int InsertTaskTypeMaster(TaskTypeMasters obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@Name", obj.TaskType);
                objGeneral.AddParameterWithValueToSQLCommand("@IsActive", obj.IsActive);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("AddTaskTypeMaster");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }
        public int InsertCropHealthMaster(CropHealthMasters obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@ProblemType", obj.TypeOfProblem);
                objGeneral.AddParameterWithValueToSQLCommand("@ProblemCause", obj.CauseOfProblem);
                objGeneral.AddParameterWithValueToSQLCommand("@IsActive", obj.IsActive);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("AddCropHealthMaster");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }


        public int UpdateDateShift(ProfilePlanner obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@pid", obj.pid);
                objGeneral.AddParameterWithValueToSQLCommand("@dateshift", obj.dateshift);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("UpdateDateShift");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int DeletePlantProductionProfile(int id)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@pid", id);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("DeletePlantProductionProfile");
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
                objGeneral.AddParameterWithValueToSQLCommand("@NavisionCustomerID", objEmployee.NavisionCustomerID);
                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_UpdateEmployee");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int UpdateFertilizer(FertilizerMasters obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@id", obj.id);
                objGeneral.AddParameterWithValueToSQLCommand("@fertilizerName", obj.FertilizerName);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("UpdateFertilizer");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }
        public int AddCustomerSalesMapping(int CustomerID,int SalesID)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@CustomerID", CustomerID);
                objGeneral.AddParameterWithValueToSQLCommand("@SalesID", SalesID);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("AddCustomerSalesMapping");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int UpdateChemical(ChemicalMasters obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@id", obj.id);
                objGeneral.AddParameterWithValueToSQLCommand("@chemicalName", obj.ChemicalName);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("UpdateChemical");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int UpdateTaskType(TaskTypeMasters obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@id", obj.id);
                objGeneral.AddParameterWithValueToSQLCommand("@tasktype", obj.TaskType);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("UpdateTaskType");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }
        public int UpdateCropHealth(CropHealthMasters obj)
        {
            int _isInserted = -1;
            try
            {
                General objGeneral = new General();

                objGeneral.AddParameterWithValueToSQLCommand("@id", obj.id);
                objGeneral.AddParameterWithValueToSQLCommand("@ProblemType", obj.TypeOfProblem);
                objGeneral.AddParameterWithValueToSQLCommand("@ProblemCause", obj.CauseOfProblem);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("UpdateCropHealth");
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

        public DataTable GetAllFertilizerList()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 20);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public DataTable GetAllChemicalList()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 21);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }



        public DataTable GetAllTaskTypeList()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 22);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public DataTable GetAllCropHealthList()
        {
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 23);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public DataTable GetPlanProductionProfile(string code, string crop, string activitycode, string traycode)
        {
            DataTable dt = new DataTable();
            try
            {

                General objGeneral = new General();
                string strQuery = string.Empty;
                strQuery = "SELECT pid,code, traycode, crop, activitycode, dateshift FROM gti_jobs_prodprofile WHERE 1=1 ";
                if (code != "0")
                {
                    strQuery += " AND [code]= " + "'" + code + "'";
                }
                if (crop != "0")
                {
                    strQuery += " And [crop]= " + "'" + crop + "'";
                }
                if (activitycode != "0")
                {
                    strQuery += " And [activitycode]= " + "'" + activitycode + "'";
                }
                if (traycode != "0")
                {
                    strQuery += " And [traycode]= " + "'" + traycode + "'";

                }
                //objGeneral.AddParameterWithValueToSQLCommand("@Crop", code);
                //objGeneral.AddParameterWithValueToSQLCommand("@trayCode", traycode);
                //objGeneral.AddParameterWithValueToSQLCommand("@activityCode", activitycode);
                //ds = objGeneral.GetDatasetByCommand_SP("GetPlanProductProfileDetail");
                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }


        public DataTable GetPlanProductionCrop(string crop)
        {
            DataTable dt = new DataTable();
            try
            {

                General objGeneral = new General();
                string strQuery = string.Empty;
                strQuery = "SELECT code, traycode, crop FROM gti_jobs_prodprofile WHERE 1=1";

                if (crop != "0")
                {
                    strQuery += " And [crop]= " + "'" + crop + "'";
                }

                strQuery += " Group by code, traycode, crop";

                dt = objGeneral.GetDatasetByCommand(strQuery);
            }
            catch (Exception ex)
            {
            }
            return dt;
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
        public DataTable GETCrop(string Code)
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@code", Code);
                ds = objGeneral.GetDatasetByCommand_SP("GetCropByCode");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public DataTable GETCrop()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 17);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public DataTable GetPlantProductionConfiguration()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 1);
                ds = objGeneral.GetDatasetByCommand_SP("GetPlantProductionConfiguration");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }
        public DataTable GetPlantProductionCrop()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 0);
                ds = objGeneral.GetDatasetByCommand_SP("GetPlantProductionConfiguration");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }
        public int UpdatePlantProductionConfiguration(int Germination1, int Germination2, int Germination3)
        {
            int _isUpdated = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@Germination1", Germination1);
                objGeneral.AddParameterWithValueToSQLCommand("@Germination2", Germination2);
                objGeneral.AddParameterWithValueToSQLCommand("@Germination3", Germination3);
                _isUpdated = objGeneral.GetExecuteNonQueryByCommand_SP("UpdatePlantProductionConfiguration");
            }
            catch (Exception ex)
            {
            }
            return _isUpdated;

        }
        public int UpdatePlantProductionPlantReadyConfiguration(int PlantReady)
        {
            int _isUpdated = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@PlantReady", PlantReady);
                _isUpdated = objGeneral.GetExecuteNonQueryByCommand_SP("UpdatePlantProductionPlantReadyConfiguration");
            }
            catch (Exception ex)
            {
            }
            return _isUpdated;

        }
        public int AddPlantProductionCrop(string Crop, int Germination1, int Germination2, int Germination3)
        {
            int _isUpdated = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@Crop", Crop);
                objGeneral.AddParameterWithValueToSQLCommand("@Germination1", Germination1);
                objGeneral.AddParameterWithValueToSQLCommand("@Germination2", Germination2);
                objGeneral.AddParameterWithValueToSQLCommand("@Germination3", Germination3);
                _isUpdated = objGeneral.GetExecuteNonQueryByCommand_SP("AddPlantProductionCrop");
            }
            catch (Exception ex)
            {
            }
            return _isUpdated;

        }

        public int AddPlantProductionCropPlantReady(string Crop, int PlantReady)
        {
            int _isUpdated = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@Crop", Crop);
                objGeneral.AddParameterWithValueToSQLCommand("@PlantReady", PlantReady);
                _isUpdated = objGeneral.GetExecuteNonQueryByCommand_SP("AddPlantProductionCropPlantReady");
            }
            catch (Exception ex)
            {
            }
            return _isUpdated;

        }
        public DataTable GETCode()
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@mode", 18);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }
        public DataTable GETActivityCode(string code)
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@crop", code);
                ds = objGeneral.GetDatasetByCommand_SP("GetActivityCodeByCrop");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }
        public DataTable GETTrayCode(string code, string crop)
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@crop", crop);
                objGeneral.AddParameterWithValueToSQLCommand("@code", code);
                ds = objGeneral.GetDatasetByCommand_SP("GetTrayCodeByActivityCode");
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

        public int RemoveFertilizer(int employeeID)
        {
            int _isDeleted = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@id", employeeID);
                _isDeleted = objGeneral.GetExecuteScalarByCommand_SP("DeleteFertilizer");
            }
            catch (Exception ex)
            {

            }
            return _isDeleted;
        }

        public int RemoveChemical(int employeeID)
        {
            int _isDeleted = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@id", employeeID);
                _isDeleted = objGeneral.GetExecuteScalarByCommand_SP("DeleteChemical");
            }
            catch (Exception ex)
            {

            }
            return _isDeleted;
        }

        public int RemoveCropHealth(int eid)
        {
            int _isDeleted = -1;
            try
            {
                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@id", eid);
                _isDeleted = objGeneral.GetExecuteScalarByCommand_SP("DeleteCropHealth");
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

        public DataTable GETPlantReadyShiftDate(string GCode)
        {
            try
            {

                General objGeneral = new General();
                objGeneral.AddParameterWithValueToSQLCommand("@GCode", GCode);

                ds = objGeneral.GetDatasetByCommand_SP("spGetDateAdminShift");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
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
    public string NavisionCustomerID { get; set; }

}

public class ProfilePlanner
{
    public int pid { get; set; }
    public string code { get; set; }
    public string crop { get; set; }
    public string activitycode { get; set; }
    public string traycode { get; set; }
    public int dateshift { get; set; }


}

public class FertilizerMasters
{
    public int id { get; set; }
    public string FertilizerName { get; set; }
    public string FertilizerCode { get; set; }
    public bool IsActive { get; set; }

}

public class ChemicalMasters
{
    public int id { get; set; }
    public string ChemicalName { get; set; }
    public bool IsActive { get; set; }

}

public class TaskTypeMasters
{
    public int id { get; set; }
    public string TaskType { get; set; }
    public bool IsActive { get; set; }

}

public class CropHealthMasters
{
    public int id { get; set; }
    public string TypeOfProblem { get; set; }
    public string CauseOfProblem { get; set; }
    public bool IsActive { get; set; }
}