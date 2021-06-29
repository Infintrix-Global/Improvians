using Evo.Admin.BAL_Classes;
using System;
using System.Web.UI;
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


        public int InsertEmployee(Employee objEmployee)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();
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

        internal int InsertNotificationPreference(NotificationPreferenceMaster obj)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@ViaApp", obj.IsApp);
                objGeneral.AddParameterWithValueToSQLCommand("@ViaEmail", obj.IsEmail);
                objGeneral.AddParameterWithValueToSQLCommand("@TaskName", obj.Task);
                objGeneral.AddParameterWithValueToSQLCommand("@UserName", obj.User);
                objGeneral.AddParameterWithValueToSQLCommand("@LoginID", 5);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_AddNotificationPreference");
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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();
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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

                objGeneral.AddParameterWithValueToSQLCommand("@Name", obj.TaskType);
                objGeneral.AddParameterWithValueToSQLCommand("@IsActive", obj.IsActive);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("AddTaskTypeMaster");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }
        public int InsertTaskTypeWorkHoursMaster(TaskTypeWorkHoursMasters obj)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();

                objGeneral.AddParameterWithValueToSQLCommand("@TaskType", obj.TaskType);
                objGeneral.AddParameterWithValueToSQLCommand("@TaskHours", obj.TaskHours);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("AddTaskTypeWorkHoursMaster");
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

                objGeneral.ClearParameters();
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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

                objGeneral.AddParameterWithValueToSQLCommand("@id", obj.id);
                objGeneral.AddParameterWithValueToSQLCommand("@fertilizerName", obj.FertilizerName);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("UpdateFertilizer");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }
        public int AddCustomerSalesMapping(int CustomerID, int SalesID)
        {
            int _isInserted = -1;
            try
            {

                objGeneral.ClearParameters();
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

                objGeneral.ClearParameters();
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
                objGeneral.ClearParameters();

                objGeneral.AddParameterWithValueToSQLCommand("@id", obj.id);
                objGeneral.AddParameterWithValueToSQLCommand("@tasktype", obj.TaskType);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("UpdateTaskType");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }
        public int UpdateTaskTypeWorkHoursMaster(TaskTypeWorkHoursMasters obj)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@ID", obj.id);
                objGeneral.AddParameterWithValueToSQLCommand("@TaskType", obj.TaskType);
                objGeneral.AddParameterWithValueToSQLCommand("@TaskHours", obj.TaskHours);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("UpdateTaskTypeeWorkHours");
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
                objGeneral.ClearParameters();

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

                objGeneral.ClearParameters();
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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();


                objGeneral.AddParameterWithValueToSQLCommand("@mode", 22);
                ds = objGeneral.GetDatasetByCommand_SP("GET_Common");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }
        public DataTable GetAllTaskTypeWorkHourList()
        {
            try
            {
                objGeneral.ClearParameters();


                objGeneral.AddParameterWithValueToSQLCommand("@mode", 24);
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
                objGeneral.ClearParameters();
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

                objGeneral.ClearParameters();
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

                objGeneral.ClearParameters();
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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();
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
                objGeneral.ClearParameters();
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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

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
                objGeneral.ClearParameters();

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

                objGeneral.ClearParameters();
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

                objGeneral.ClearParameters();
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

                objGeneral.ClearParameters();
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
                objGeneral.ClearParameters();
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
                objGeneral.ClearParameters();
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
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@id", employeeID);
                _isDeleted = objGeneral.GetExecuteScalarByCommand_SP("DeleteChemical");
            }
            catch (Exception ex)
            {

            }
            return _isDeleted;
        }


        public int RemoveTaskTypeMaster(int ID)
        {
            int _isDeleted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@id", ID);
                _isDeleted = objGeneral.GetExecuteScalarByCommand_SP("DeleteTaskTypeMaster");
            }
            catch (Exception ex)
            {

            }
            return _isDeleted;
        }





        public int RemoveTaskTypeWorkHours(int employeeID)
        {
            int _isDeleted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@id", employeeID);
                _isDeleted = objGeneral.GetExecuteScalarByCommand_SP("DeleteTaskTypeWorkHours");
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
                objGeneral.ClearParameters();

                objGeneral.AddParameterWithValueToSQLCommand("@EmployeeID", employeeID);
                objGeneral.AddParameterWithValueToSQLCommand("@FacilityID", FacilityID);
                _isInserted = objGeneral.GetExecuteNonQueryByCommand_SP("SP_AddEmployeeFacilityNew");
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

                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@GCode", GCode);

                ds = objGeneral.GetDatasetByCommand_SP("spGetDateAdminShift");
            }
            catch (Exception ex)
            {
            }
            return ds.Tables[0];
        }

        public int UpdateHelpContact(string Name, string Email, String Phone, String Photo)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@Name", Name);
                objGeneral.AddParameterWithValueToSQLCommand("@Email", Email);
                objGeneral.AddParameterWithValueToSQLCommand("@Phone", Phone);
                objGeneral.AddParameterWithValueToSQLCommand("@Photo", Photo);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_UpdateHelpContact");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int InsertHelpFAQ(string Title, string Description)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@Title", Title);
                objGeneral.AddParameterWithValueToSQLCommand("@Description", Description);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_AddHelpFAQ");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int UpdateHelpFAQ(int ID, string Title, string Description)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@ID", ID);
                objGeneral.AddParameterWithValueToSQLCommand("@Title", Title);
                objGeneral.AddParameterWithValueToSQLCommand("@Description", Description);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_UpdateHelpFAQ");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int RemoveHelpFAQ(int ID)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@ID", ID);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_DeleteHelpFAQ");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int InsertHelpDocument(string Title, string DocumentLink, string VideoLink)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@Title", Title);
                objGeneral.AddParameterWithValueToSQLCommand("@DocumentLink", DocumentLink);
                objGeneral.AddParameterWithValueToSQLCommand("@VideoLink", VideoLink);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_AddHelpDocument");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int UpdateHelpDocument(int ID, string Title, string DocumentLink, string VideoLink)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@ID", ID);
                objGeneral.AddParameterWithValueToSQLCommand("@Title", Title);
                objGeneral.AddParameterWithValueToSQLCommand("@DocumentLink", DocumentLink);
                objGeneral.AddParameterWithValueToSQLCommand("@VideoLink", VideoLink);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_UpdateHelpDocument");
            }
            catch (Exception ex)
            {

            }
            return _isInserted;
        }

        public int RemoveHelpDocument(int ID)
        {
            int _isInserted = -1;
            try
            {
                objGeneral.ClearParameters();
                objGeneral.AddParameterWithValueToSQLCommand("@ID", ID);

                _isInserted = objGeneral.GetExecuteScalarByCommand_SP("SP_DeleteHelpDocument");
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

public class TaskTypeWorkHoursMasters
{
    public int id { get; set; }
    public string TaskType { get; set; }
    public int TaskHours { get; set; }
    public bool IsActive { get; set; }

}

public class CropHealthMasters
{
    public int id { get; set; }
    public string TypeOfProblem { get; set; }
    public string CauseOfProblem { get; set; }
    public bool IsActive { get; set; }
}

public class NotificationPreferenceMaster
{
    public int id { get; set; }
    public string Task { get; set; }
    public string User { get; set; }
    public bool IsApp { get; set; }
    public bool IsEmail { get; set; }
}