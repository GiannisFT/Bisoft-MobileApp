using BisoftMobileWCFService.Classes;
using BisoftMobileWCFService.Classes.CarPreSales;
using BisoftMobileWCFService.Classes.InternalControl;
using BisoftMobileWCFService.HelpClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BisoftMobileWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        #region Internal control

        #region Get

        public InternalControlOfficeData GetOfficeInternalControl(string username, string password, string ucid, int officeId, int officecontrolId)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            InternalControlOfficeData data = null;
            InternalControlOffice contoff;
            InternalControlOfficeRowData row;

            Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
            List<InternalControlOfficeRowData> rows = new List<InternalControlOfficeRowData>();
            if (employee != null)
            {
                contoff = db.InternalControlOffices.Where(o => o.Deleted == false && o.OfficeId == officeId && o.Id == officecontrolId).FirstOrDefault();

                if (contoff != null)
                {
                    data = new InternalControlOfficeData();

                    data.Id = contoff.Id;
                    data.Name = contoff.Name;
                    data.OfficeId = contoff.OfficeId;
                    if (contoff.HasVehicleBrand == true)
                        data.HasVehicleBrand = true;
                    else
                        data.HasVehicleBrand = false;

                    if (contoff.UseEmployee == true)
                        data.HasEmployee = true;
                    else
                        data.HasEmployee = false;
                    if (contoff.InternalControlOfficeRows != null && contoff.InternalControlOfficeRows.Count > 0)
                    {
                        foreach (InternalControlOfficeRow _r in contoff.InternalControlOfficeRows.Where(r => r.Deleted == false))
                        {
                            row = new InternalControlOfficeRowData();
                            row.Id = _r.Id;
                            row.RowText = _r.RowText;
                            if (_r.InternalControlOfficeSubGroup != null)
                                row.Group = _r.InternalControlOfficeSubGroup.Name;
                            rows.Add(row);
                        }

                        data.ControlRows = rows;

                    }
                }

                return data;
            }
            else
                return null;
        }

        public InternalControlOfficeLogData GetOfficeInternalControlLog(string username, string password, string ucid, int controlLogId)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            InternalControlOfficeLogData logdata = null;
            InternalControlOfficeData contdata;
            InternalControlOfficeLogRowData rowdata;
            InternalControlOfficeLog offlog;
            EmployeeData empdata;
            VehicleBrandData branddata;
            ICErrorCodeData errorcodedata;
            ICOLogRowFileData filedata;

            Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
            List<InternalControlOfficeRowData> rows = new List<InternalControlOfficeRowData>();

            if (employee != null)
            {
                offlog = db.InternalControlOfficeLogs.Where(o => o.Deleted == false && o.Id == controlLogId && o.InternalControlOffice.Office.Company.Ucid.Equals(ucid)).FirstOrDefault();

                if (offlog != null)
                {
                    logdata = new InternalControlOfficeLogData();
                    logdata.AoNr = offlog.AoNr;
                    logdata.Created = offlog.Created;
                    if (offlog != null)
                    {
                        empdata = new EmployeeData();
                        empdata.Id = offlog.Employee.Id;
                        empdata.FName = offlog.Employee.FName;
                        empdata.LName = offlog.Employee.LName;
                        logdata.Employee = empdata;
                    }

                    logdata.Id = offlog.Id;
                    if (offlog.InternalControlOffice != null)
                    {
                        contdata = new InternalControlOfficeData();
                        if (offlog.InternalControlOffice.UseEmployee == true)
                            contdata.HasEmployee = true;
                        else
                            contdata.HasEmployee = false;
                        if (offlog.InternalControlOffice.HasVehicleBrand == true)
                            contdata.HasVehicleBrand = true;
                        else
                            contdata.HasVehicleBrand = false;

                        contdata.Id = offlog.InternalControlOffice.Id;
                        contdata.Name = offlog.InternalControlOffice.Name;

                        logdata.InternalControlOffice = contdata;
                    }

                    if (offlog.IsPartSaved == true)
                        logdata.IsPartSaved = true;
                    else
                        logdata.IsPartSaved = false;

                    logdata.RegNr = offlog.RegNr;

                    if (offlog.VehicleBrand != null)
                    {
                        branddata = new VehicleBrandData();
                        branddata.Code = offlog.VehicleBrand.VehicleBrandCode;
                        branddata.Id = offlog.VehicleBrand.Id;
                        branddata.Name = offlog.VehicleBrand.VehicleBrandName;

                        logdata.VehicleBrand = branddata;
                    }

                    logdata.Rows = new List<InternalControlOfficeLogRowData>();

                    if (offlog.InternalControlOfficeLogRows != null && offlog.InternalControlOfficeLogRows.Count > 0)
                    {
                        foreach (InternalControlOfficeLogRow row in offlog.InternalControlOfficeLogRows)
                        {
                            if (row.Deleted == false)
                            {
                                rowdata = new InternalControlOfficeLogRowData();
                                rowdata.Comment = row.Comment;
                                rowdata.Deleted = false;
                                rowdata.Id = row.Id;
                                rowdata.InternalControlOfficeRowId = row.InternalControlOfficeRowId;
                                rowdata.IsEA = row.IsEa;
                                rowdata.IsNo = row.IsNo;
                                rowdata.IsYes = row.IsYes;
                                rowdata.Text = row.Text;
                                if (row.InternalControlOfficeRow != null)
                                {
                                    if (row.InternalControlOfficeRow.InternalControlOfficeSubGroup != null)
                                        rowdata.Group = row.InternalControlOfficeRow.InternalControlOfficeSubGroup.Name;

                                    rowdata.HelpText = row.InternalControlOfficeRow.Description;
                                }

                                rowdata.ErrorCodes = new List<ICErrorCodeData>();
                                if (row.InternalControlOfficeLogRowCodes != null && row.InternalControlOfficeLogRowCodes.Count > 0)
                                {
                                    foreach (ICErrorCode code in row.InternalControlOfficeLogRowCodes.Select(iu => iu.ICErrorCode))
                                    {
                                        errorcodedata = new ICErrorCodeData();
                                        errorcodedata.Code = code.Code;
                                        errorcodedata.Id = code.Id;
                                        errorcodedata.Group = new ICErrorCodeGroupData
                                        {
                                            Id = code.ICErrorCodeGroup.Id,
                                            Name = code.ICErrorCodeGroup.Name,
                                            MainGroup = new ICErrorCodeMainGroupData
                                            {
                                                Id = code.ICErrorCodeGroup.ICErrorCodeMainGroupId,
                                                Name = code.ICErrorCodeGroup.ICErrorCodeMainGroup.Name
                                            }

                                        };

                                        rowdata.ErrorCodes.Add(errorcodedata);

                                    }


                                }

                                rowdata.ImageFiles = new List<ICOLogRowFileData>();

                                if (row.InternalControlOfficeLogRowFiles != null && row.InternalControlOfficeLogRowFiles.Count > 0)
                                {
                                    foreach (InternalControlOfficeLogRowFile file in row.InternalControlOfficeLogRowFiles)
                                    {
                                        filedata = new ICOLogRowFileData();
                                        filedata.FileName = file.FileName;
                                        filedata.FilePath = file.FilePath;
                                        filedata.Id = file.Id;

                                        rowdata.ImageFiles.Add(filedata);
                                    }
                                }
                                logdata.Rows.Add(rowdata);

                            }
                        }


                    }
                }


            }
            return logdata;
        }
        public List<InternalControlOfficeData> GetOfficeInternalControls(string username, string password, string ucid, int officeId)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            InternalControlOfficeData data;
            InternalControlBrandGoalData goalData;
            Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
            List<InternalControlOfficeData> returnList = new List<InternalControlOfficeData>();
            if (employee != null)
            {
                List<InternalControlOffice> off = new List<InternalControlOffice>(db.InternalControlOffices.Where(o => o.Deleted == false && o.OfficeId == officeId));

                foreach (InternalControlOffice cont in off)
                {
                    data = new InternalControlOfficeData();
                    data.AmountBudget = cont.AmountBudget;
                    data.AmountUnit = cont.AmountUnit;
                    data.Id = cont.Id;
                    data.Name = cont.Name;
                    data.OfficeId = cont.OfficeId;

                    if (cont.InternalControlOfficeVehicleBrandGoals != null && cont.InternalControlOfficeVehicleBrandGoals.Count > 0)
                    {
                        data.Goals = new List<InternalControlBrandGoalData>();
                        foreach (InternalControlOfficeVehicleBrandGoal goal in cont.InternalControlOfficeVehicleBrandGoals)
                        {
                            goalData = new InternalControlBrandGoalData();
                            if (goal.VehicleBrand != null)
                                goalData.BrandName = goal.VehicleBrand.VehicleBrandName;

                            if (goal.GoalUnit.Equals("Månad"))
                            {
                                goalData.MonthGoal = goal.GoalNr;
                                goalData.YearGoal = goal.GoalNr * DateTime.Now.Month;
                                if (cont.InternalControlOfficeLogs != null && cont.InternalControlOfficeLogs.Count > 0)
                                {
                                    goalData.MonthResult = cont.InternalControlOfficeLogs.Where(lo => lo.Created.Year == DateTime.Now.Year && lo.Created.Month == DateTime.Now.Month && lo.Deleted == false && (lo.VehicleBrandId != null && lo.VehicleBrandId == goal.VehicelBrandId)).Count();
                                    goalData.YearResult = cont.InternalControlOfficeLogs.Where(lo => lo.Created.Year == DateTime.Now.Year && lo.Deleted == false && (lo.VehicleBrandId != null && lo.VehicleBrandId == goal.VehicelBrandId)).Count();
                                }
                                else
                                {
                                    goalData.MonthResult = 0;
                                    goalData.YearResult = 0;
                                }

                            }
                            else if (goal.GoalUnit.Equals("År"))
                            {
                                goalData.MonthGoal = goal.GoalNr / 12;
                                goalData.YearGoal = goal.GoalNr / DateTime.Now.Month;

                                if (cont.InternalControlOfficeLogs != null && cont.InternalControlOfficeLogs.Count > 0)
                                {
                                    goalData.MonthResult = cont.InternalControlOfficeLogs.Where(lo => lo.Created.Year == DateTime.Now.Year && lo.Created.Month == DateTime.Now.Month && lo.Deleted == false && (lo.VehicleBrandId != null && lo.VehicleBrandId == goal.VehicelBrandId)).Count();
                                    goalData.YearResult = cont.InternalControlOfficeLogs.Where(lo => lo.Created.Year == DateTime.Now.Year && lo.Deleted == false && (lo.VehicleBrandId != null && lo.VehicleBrandId == goal.VehicelBrandId)).Count();
                                }
                                else
                                {
                                    goalData.MonthResult = 0;
                                    goalData.YearResult = 0;
                                }
                            }

                            data.Goals.Add(goalData);
                        }
                    }

                    if (cont.InternalControlOfficeLogs != null && cont.InternalControlOfficeLogs.Count > 0)
                    {
                        data.PerformedThisMonth = cont.InternalControlOfficeLogs.Where(lo => lo.Created.Year == DateTime.Now.Year && lo.Created.Month == DateTime.Now.Month && lo.Deleted == false).Count();
                        data.PerformedThisYear = cont.InternalControlOfficeLogs.Where(lo => lo.Created.Year == DateTime.Now.Year && lo.Deleted == false).Count();
                    }


                    returnList.Add(data);
                }

                return returnList;
            }
            else
                return null;
        }

        public List<InternalControlOfficeResultsData> GetOfficeInternalControlsGoalVsResult(string username, string password, string ucid, int companyid)
        {

            KvalPortDbEntities db = new KvalPortDbEntities();
            InternalControlOfficeResultsData data;
            OfficeData officedata;
            InternalControlOfficeData contData;
            ObservableCollection<InternalControlOffice> offcontrols;
            Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
            List<InternalControlOfficeResultsData> returnList = new List<InternalControlOfficeResultsData>();
            
            if (employee != null)
            {
                ObservableCollection<Office> offices = new ObservableCollection<Office>(db.Offices.Where(off => off.CompanyId == companyid && off.Status == true).OrderBy(o => o.Name));

                if(offices != null && offices.Count > 0)
                {
                    foreach(Office of in offices)
                    {
                        data = new InternalControlOfficeResultsData();
                        officedata = new OfficeData();
                        officedata.Id = of.Id;
                        officedata.Name = of.Name;
                        data.OfficeData = officedata;

                        offcontrols = new ObservableCollection<InternalControlOffice>(of.InternalControlOffices.Where(off => off.Deleted == false));

                        data.Controls = new List<InternalControlOfficeData>();
                        foreach (InternalControlOffice cont in offcontrols)
                        {
                            contData = new InternalControlOfficeData();
                            contData.Name = cont.Name;
                            data.Controls.Add(contData);
                        }

                        returnList.Add(data);

                    }
                }
            }

            return returnList;
        }

        public InternalControlOfficeData GetOfficeInternalControlLogs(string username, string password, string ucid, int officecontrolId)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            InternalControlOfficeData data;
            Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
            InternalControlOfficeData controlData = new InternalControlOfficeData();
            InternalControlOfficeLogData logData;
            EmployeeData empData;
            VehicleBrandData brandData;
            if (employee != null)
            {
                InternalControlOffice off = db.InternalControlOffices.Where(o => o.Deleted == false && o.Id == officecontrolId && o.Office.Company.Ucid.Equals(ucid)).FirstOrDefault();

                if (off != null)
                {
                    controlData.Id = off.Id;
                    controlData.Name = off.Name;
                    controlData.Logs = new List<InternalControlOfficeLogData>();
                    foreach (InternalControlOfficeLog log in off.InternalControlOfficeLogs.Where(ol => ol.Deleted == false))
                    {

                        logData = new InternalControlOfficeLogData();
                        logData.Id = log.Id;
                        logData.InternalControlOfficeId = off.Id;
                        if (log.IsPartSaved == true)
                            logData.IsPartSaved = true;
                        else
                            logData.IsPartSaved = false;
                        logData.AoNr = log.AoNr;
                        logData.Created = log.Created;
                        logData.CreatedById = log.CreatedBy;
                        empData = new EmployeeData();
                        empData.Id = log.Employee.Id;
                        empData.FName = log.Employee.FName;
                        empData.LName = log.Employee.LName;
                        logData.Employee = empData;
                        logData.RegNr = log.RegNr;
                        if (log.VehicleBrand != null)
                        {
                            logData.VehicleBrandId = log.VehicleBrandId;
                            brandData = new VehicleBrandData();
                            brandData.Id = log.VehicleBrand.Id;
                            brandData.Name = log.VehicleBrand.VehicleBrandName;
                            brandData.Code = log.VehicleBrand.VehicleBrandCode;
                            logData.VehicleBrand = brandData;

                        }
                        controlData.Logs.Add(logData);

                    }





                }
            }

            return controlData;
        }

        public List<ICErrorCodeMainGroupData> GetICErrorCodes(string username, string password, string ucid, int companyId)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            ICErrorCodeData cdata;
            ICErrorCodeGroupData gdata;
            ICErrorCodeMainGroupData mdata;
            List<ICErrorCodeGroupData> tempgroup;
            List<ICErrorCodeData> codetemp;

            Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
            List<ICErrorCodeMainGroupData> returnList = new List<ICErrorCodeMainGroupData>();
            if (employee != null)
            {
                var gr = db.ICErrorCodeMainGroups.Where(mg => mg.CompanyId == companyId && mg.Company.Ucid.EndsWith(ucid));

                foreach (ICErrorCodeMainGroup mg in gr)
                {
                    mdata = new ICErrorCodeMainGroupData();
                    mdata.Id = mg.Id;
                    mdata.Name = mg.Name;
                    returnList.Add(mdata);

                    tempgroup = new List<ICErrorCodeGroupData>();
                    foreach (ICErrorCodeGroup eg in mg.ICErrorCodeGroups)
                    {
                        gdata = new ICErrorCodeGroupData();
                        gdata.Id = eg.Id;
                        gdata.Name = eg.Name;
                        tempgroup.Add(gdata);

                        codetemp = new List<ICErrorCodeData>();
                        foreach (ICErrorCode ec in eg.ICErrorCodes)
                        {
                            cdata = new ICErrorCodeData();
                            cdata.Id = ec.Id;
                            cdata.Code = ec.Code;
                            codetemp.Add(cdata);
                        }
                        gdata.Codes = codetemp;
                    }

                    mdata.Groups = tempgroup;

                }
            }

            return returnList;
        }

        #endregion

        #region Insert

        public string InsertOfficeInternalControlPerformControl(string username, string password, string ucid, InternalControlOfficeLogData officeLog)
        {
            try
            {
                KvalPortDbEntities db = new KvalPortDbEntities();
                Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
                InternalControlOfficeLogRowFile file;
                QualityReportData qreport;
                InternalControlOfficeLogRow row;
                InternalControlOfficeRow offrow;
                InternalControlOfficeLogRowCode code;
                InternalControlOffice contoff;
                WCFReturnResultCls returnResult;
                ICErrorCode iccode;
                if (employee != null)
                {
                    if (officeLog != null && officeLog.InternalControlOfficeId > 0)
                    {
                        contoff = db.InternalControlOffices.Where(ico => ico.Id == officeLog.InternalControlOfficeId && ico.Deleted == false && ico.Office.Company.Ucid.Equals(ucid)).FirstOrDefault();

                        if (contoff != null)
                        {
                            InternalControlOfficeLog log = new InternalControlOfficeLog();
                            log.AoNr = officeLog.AoNr;
                            log.Created = officeLog.Created;
                            log.CreatedBy = officeLog.CreatedById;
                            log.CreatedOn = officeLog.CreatedOnId;
                            log.Deleted = false;
                            log.InternalControlOfficeId = officeLog.InternalControlOfficeId;
                            log.IsPartSaved = officeLog.IsPartSaved;
                            log.RegNr = officeLog.RegNr;
                            log.VehicleBrandId = officeLog.VehicleBrandId;

                            db.InternalControlOfficeLogs.Add(log);

                            if (officeLog.Rows != null && officeLog.Rows.Count > 0)
                            {


                                foreach (InternalControlOfficeLogRowData data in officeLog.Rows.ToList())
                                {
                                    if (data.InternalControlOfficeRowId != null)
                                        offrow = db.InternalControlOfficeRows.Where(or => or.Id == data.InternalControlOfficeRowId).FirstOrDefault();
                                    else
                                        offrow = null;
                                    row = new InternalControlOfficeLogRow();
                                    row.Comment = data.Comment;
                                    row.Deleted = false;
                                    row.InternalControlOfficeLog = log;
                                    row.InternalControlOfficeRowId = data.InternalControlOfficeRowId;
                                    row.IsEa = data.IsEA;
                                    row.IsNo = data.IsNo;
                                    row.IsYes = data.IsYes;
                                    row.Text = data.Text;




                                    if (data.ErrorCodes != null && data.ErrorCodes.Count > 0)
                                    {
                                        foreach (ICErrorCodeData codedata in data.ErrorCodes.ToList())
                                        {
                                            code = new InternalControlOfficeLogRowCode();
                                            code.Deleted = false;
                                            code.ICErrorCodeId = codedata.Id;
                                            code.InternalControlOfficeLogRow = row;

                                            db.InternalControlOfficeLogRowCodes.Add(code);
                                        }
                                    }

                                    if (data.ImageFiles != null && data.ImageFiles.Count > 0)
                                    {
                                        foreach (ICOLogRowFileData filedata in data.ImageFiles.ToList())
                                        {
                                            file = new InternalControlOfficeLogRowFile();
                                            file.Deleted = false;
                                            file.FileName = filedata.FileName;
                                            file.FilePath = filedata.FilePath;
                                            file.InternalControlOfficeLogRow = row;

                                            db.InternalControlOfficeLogRowFiles.Add(file);
                                        }
                                    }

                                    if (officeLog.IsPartSaved != true && row.IsNo == true && offrow != null && offrow.OfficeDepartmentTaskId != null)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        sb.AppendLine("Kontroll: " + contoff.Name);
                                        if (officeLog.VehicleBrandId != null)
                                        {
                                            VehicleBrand brand = db.VehicleBrands.Where(vb => vb.Id == officeLog.VehicleBrandId).FirstOrDefault();

                                            if (brand != null)
                                                sb.AppendLine("Tillverkare: " + brand.VehicleBrandName);
                                        }
                                        sb.AppendLine("");
                                        sb.AppendLine(row.Text + ": Nej");
                                        sb.AppendLine("");

                                        if (data.ErrorCodes != null && data.ErrorCodes.Count > 0)
                                            foreach (ICErrorCodeData qrecodedata in data.ErrorCodes.ToList())
                                            {
                                                iccode = db.ICErrorCodes.Where(ico => ico.Id == qrecodedata.Id).FirstOrDefault();
                                                if (iccode != null)
                                                    sb.AppendLine(iccode.ICErrorCodeGroup.ICErrorCodeMainGroup.Name + "/" + iccode.ICErrorCodeGroup.Name + "/" + iccode.Code);
                                                // sb.AppendLine(qrecodedata.Code);
                                            }




                                        if (!string.IsNullOrWhiteSpace(row.Comment))
                                        {
                                            sb.AppendLine("");
                                            sb.AppendLine(row.Comment);
                                        }

                                        qreport = new QualityReportData();
                                        qreport.Created = DateTime.Now;
                                        qreport.CreatedBy = officeLog.CreatedById;
                                        qreport.Deleted = false;

                                        if (data.ImageFiles != null && data.ImageFiles.Count > 0)
                                        {
                                            qreport.QRAttachedFileData = new List<QRAttachedFileData>();
                                            QRAttachedFileData afile;

                                            foreach (ICOLogRowFileData lrf in data.ImageFiles.ToList())
                                            {
                                                afile = new QRAttachedFileData();
                                                afile.FileName = lrf.FileName;
                                                afile.FilePath = lrf.FilePath;
                                                qreport.QRAttachedFileData.Add(afile);
                                            }
                                        }

                                        qreport.Description = sb.ToString();
                                        qreport.OfficeDepartmentTaskId = (int)offrow.OfficeDepartmentTaskId;
                                        qreport.OfficeId = contoff.OfficeId;
                                        qreport.QRReportResponsibleId = 1;
                                        qreport.RegNr = officeLog.RegNr;
                                        qreport.Status = "Skapad";
                                        qreport.StatusId = 1;
                                        returnResult = InsertQualityReport(username, password, ucid, qreport);

                                        if (returnResult.Id > 0)
                                            row.QRReportId = returnResult.Id;

                                    }

                                    db.InternalControlOfficeLogRows.Add(row);
                                }
                            }

                            db.SaveChanges();

                            return "Klart: Kontrollen sparad";
                        }
                        else
                            return "Error: Hittar inte kontrollen";
                    }
                    else
                        return "Error: Saknar controlId";
                }
                else
                    return "Error: Fel inloggningsuppgifter";


            }
            catch (Exception e)
            {
                return "Error: " + e.Message;
            }
        }

        #endregion

        #region Update

        public string UpdateOfficeInternalControlLog(string username, string password, string ucid, InternalControlOfficeLogData officeLog)
        {
            try
            {
                KvalPortDbEntities db = new KvalPortDbEntities();
                Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
                InternalControlOfficeLogRowFile newfile;
                InternalControlOfficeLogRow row;
                InternalControlOfficeLogRowCode code;
                InternalControlOfficeLog log;
                WCFReturnResultCls returnResult;
                QualityReportData qreport;
                ICErrorCode iccode;
                if (employee != null)
                {
                    if (officeLog != null && officeLog.Id != null)
                    {
                        log = db.InternalControlOfficeLogs.Where(ico => ico.Id == officeLog.Id && ico.Deleted == false && ico.InternalControlOffice.Office.Company.Ucid.Equals(ucid)).FirstOrDefault();

                        if (log != null)
                        {
                            log.AoNr = officeLog.AoNr;
                            log.Created = officeLog.Created;
                            log.CreatedBy = officeLog.CreatedById;
                            log.CreatedOn = officeLog.CreatedOnId;
                            log.Deleted = false;
                            // log.InternalControlOfficeId = officeLog.InternalControlOfficeId;
                            log.IsPartSaved = officeLog.IsPartSaved;
                            log.RegNr = officeLog.RegNr;
                            log.VehicleBrandId = officeLog.VehicleBrandId;

                            //db.InternalControlOfficeLogs.Add(log);

                            if (officeLog.Rows != null && officeLog.Rows.Count > 0)
                            {
                                foreach (InternalControlOfficeLogRowData data in officeLog.Rows.ToList())
                                {
                                    row = log.InternalControlOfficeLogRows.Where(ri => ri.Id == data.Id).FirstOrDefault();
                                    if (row != null)
                                    {
                                        row.Comment = data.Comment;
                                        //row.Deleted = false;
                                        row.IsEa = data.IsEA;
                                        row.IsNo = data.IsNo;
                                        row.IsYes = data.IsYes;
                                        row.Text = data.Text;

                                        if (row.InternalControlOfficeLogRowCodes != null && row.InternalControlOfficeLogRowCodes.Count > 0)
                                        {
                                            foreach (InternalControlOfficeLogRowCode rowcode in row.InternalControlOfficeLogRowCodes.ToList())
                                            {
                                                if (data.ErrorCodes == null || data.ErrorCodes.Count < 1)
                                                    db.InternalControlOfficeLogRowCodes.Remove(rowcode);
                                                else if (!data.ErrorCodes.Select(rec => rec.Id).Contains(rowcode.Id))
                                                    db.InternalControlOfficeLogRowCodes.Remove(rowcode);
                                            }
                                        }

                                        if (data.ErrorCodes != null && data.ErrorCodes.Count > 0)
                                        {
                                            foreach (ICErrorCodeData codedata in data.ErrorCodes.ToList())
                                            {
                                                if (!row.InternalControlOfficeLogRowCodes.Select(eec => eec.ICErrorCodeId).Contains(codedata.Id))
                                                {
                                                    code = new InternalControlOfficeLogRowCode();
                                                    code.Deleted = false;
                                                    code.ICErrorCodeId = codedata.Id;
                                                    code.InternalControlOfficeLogRow = row;

                                                    db.InternalControlOfficeLogRowCodes.Add(code);
                                                }
                                            }
                                        }

                                        if (row.InternalControlOfficeLogRowFiles != null && row.InternalControlOfficeLogRowFiles.Count > 0)
                                        {
                                            foreach (InternalControlOfficeLogRowFile file in row.InternalControlOfficeLogRowFiles.ToList())
                                            {
                                                if (data.ImageFiles == null || data.ImageFiles.Count < 1)
                                                    db.InternalControlOfficeLogRowFiles.Remove(file);
                                                else if (!data.ImageFiles.Where(ui => ui.Id != null).Select(imf => imf.Id).Contains(file.Id))
                                                    db.InternalControlOfficeLogRowFiles.Remove(file);
                                            }
                                        }

                                        if (data.ImageFiles != null && data.ImageFiles.Count > 0)
                                        {
                                            foreach (ICOLogRowFileData filedata in data.ImageFiles.ToList())
                                            {
                                                if (filedata.Id == null)
                                                {
                                                    newfile = new InternalControlOfficeLogRowFile();
                                                    newfile.Deleted = false;
                                                    newfile.FileName = filedata.FileName;
                                                    newfile.FilePath = filedata.FilePath;
                                                    newfile.InternalControlOfficeLogRow = row;

                                                    db.InternalControlOfficeLogRowFiles.Add(newfile);
                                                }
                                            }
                                        }

                                        if (log.IsPartSaved != true && row.IsNo == true && row.InternalControlOfficeRow != null && row.InternalControlOfficeRow.OfficeDepartmentTaskId != null)
                                        {
                                            StringBuilder sb = new StringBuilder();
                                            sb.AppendLine("Kontroll: " + log.InternalControlOffice.Name);
                                            if (officeLog.VehicleBrandId != null)
                                            {
                                                VehicleBrand brand = db.VehicleBrands.Where(vb => vb.Id == log.VehicleBrandId).FirstOrDefault();

                                                if (brand != null)
                                                    sb.AppendLine("Tillverkare: " + brand.VehicleBrandName);
                                            }
                                            sb.AppendLine("");
                                            sb.AppendLine(row.Text + ": Nej");
                                            sb.AppendLine("");

                                            if (data.ErrorCodes != null && data.ErrorCodes.Count > 0)
                                                foreach (ICErrorCodeData qrecodedata in data.ErrorCodes.ToList())
                                                {
                                                    iccode = db.ICErrorCodes.Where(ico => ico.Id == qrecodedata.Id).FirstOrDefault();
                                                    if (iccode != null)
                                                        sb.AppendLine(iccode.ICErrorCodeGroup.ICErrorCodeMainGroup.Name + "/" + iccode.ICErrorCodeGroup.Name + "/" + iccode.Code);
                                                    // sb.AppendLine(qrecodedata.Code);
                                                }




                                            if (!string.IsNullOrWhiteSpace(row.Comment))
                                            {
                                                sb.AppendLine("");
                                                sb.AppendLine(row.Comment);
                                            }

                                            qreport = new QualityReportData();
                                            qreport.Created = DateTime.Now;
                                            qreport.CreatedBy = officeLog.CreatedById;
                                            qreport.Deleted = false;

                                            if (data.ImageFiles != null && data.ImageFiles.Count > 0)
                                            {
                                                qreport.QRAttachedFileData = new List<QRAttachedFileData>();
                                                QRAttachedFileData afile;

                                                foreach (ICOLogRowFileData lrf in data.ImageFiles.ToList())
                                                {
                                                    afile = new QRAttachedFileData();
                                                    afile.FileName = lrf.FileName;
                                                    afile.FilePath = lrf.FilePath;
                                                    qreport.QRAttachedFileData.Add(afile);
                                                }
                                            }

                                            qreport.Description = sb.ToString();
                                            qreport.OfficeDepartmentTaskId = (int)row.InternalControlOfficeRow.OfficeDepartmentTaskId;
                                            qreport.OfficeId = log.InternalControlOffice.OfficeId;
                                            qreport.QRReportResponsibleId = 1;
                                            qreport.RegNr = log.RegNr;
                                            qreport.Status = "Skapad";
                                            qreport.StatusId = 1;
                                            returnResult = InsertQualityReport(username, password, ucid, qreport);

                                            if (returnResult.Id > 0)
                                                row.QRReportId = returnResult.Id;

                                        }
                                    }
                                }
                            }

                            db.SaveChanges();

                            return "Klart: Kontrollen sparad";
                        }
                        else
                            return "Error: Hittar inte kontroll-loggen";
                    }
                    else
                        return "Error: Saknar controlId";
                }
                else
                    return "Error: Fel inloggningsuppgifter";


            }
            catch (Exception e)
            {
                return "Error: " + e.Message;
            }
        }

        #endregion

        #region Delete
        public string DeleteOfficeInternalControlPerformControl(string username, string password, string ucid, int logId)
        {

            try
            {
                KvalPortDbEntities db = new KvalPortDbEntities();
                Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
                
                if (employee != null)
                {
                    if (logId > 0)
                    {
                        InternalControlOfficeLog log = db.InternalControlOfficeLogs.Where(ol => ol.Id == logId && ol.InternalControlOffice.Office.Company.Ucid.Equals(ucid)).FirstOrDefault();

                        if (log != null)
                        {
                            if (log.InternalControlOfficeLogRows != null && log.InternalControlOfficeLogRows.Count > 0)
                            {
                                foreach (InternalControlOfficeLogRow datarow in log.InternalControlOfficeLogRows)
                                {
                                    datarow.Deleted = true;
                                }
                            }

                            log.Deleted = true;

                            db.SaveChanges();

                            return "Klart: Kontrollen raderad";
                        }
                        else
                            return "Error: Hittar inte loggen";

                    }
                    else
                        return "Error: Id saknas";
                }
                else
                    return "Error: Fel inloggningsuppgifter";
            }
            catch(Exception e)
            {
                return "Error: " + e.Message;
            }

        }

        #endregion

        #endregion

        #region CarPre Sales

        public List<CarPreSalesMaintenanceData> GetCarPreSalesmaintenanceByOffice(string username, string password, string ucid, int officeId, int? employeeId)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            CarPreSalesMaintenanceData data;
            CarPreSaleFlowGroupData fgroup;
            EmployeeData emp;
            Employee employee = db.Employees.Where(empd => empd.Office.Company.Ucid.Equals(ucid) && empd.Username.ToLower().Equals(username.ToLower()) && empd.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
            List<CarPreSalesMaintenanceData> returnList = new List<CarPreSalesMaintenanceData>();
            if (employee != null)
            {
                List<CarPreSale> cps = new List<CarPreSale>(db.CarPreSales.Where(o => o.Finished == null && o.OfficeId == officeId && o.RecievedDate != null && o.MaintenanceResponsibleId != null));

                if (employeeId != null && employeeId > 0)
                    cps = new List<CarPreSale>(cps.Where(o => o.MaintenanceResponsibleId != null && o.MaintenanceResponsibleId == (int)employeeId));

                foreach (CarPreSale cont in cps)
                {
                    fgroup = new CarPreSaleFlowGroupData();
                    data = new CarPreSalesMaintenanceData();
                    emp = new EmployeeData();

                    if (cont.CarPreSaleFlowGroup != null) {
                        fgroup.Color = cont.CarPreSaleFlowGroup.Color;
                        fgroup.Id = cont.CarPreSaleFlowGroup.Id;
                        fgroup.Name = cont.CarPreSaleFlowGroup.Name;
                        data.CarPreSaleFlowGroupData = fgroup;
                    }

                    if (cont.CarPreSalesMaintenanceFormId != null)
                        data.MaintenanceFormId = (int)cont.CarPreSalesMaintenanceFormId;
                    else
                        data.MaintenanceFormId = 1;

                    data.Id = cont.Id;

                    data.MaintenanceNext = cont.Next;

                    if (cont.OfficeKeyCabinet != null)
                        data.KeyCabinet = cont.OfficeKeyCabinet.Name;

                    if(cont.Employee3 != null)
                    {
                        emp.Id = cont.Employee3.Id;
                        emp.FName = cont.Employee3.FName;
                        emp.LName = cont.Employee3.LName;

                        data.MaintenanceResponsible = emp;
                    }

                    if (cont.OfficeParking != null)
                        data.Parking = cont.OfficeParking.Name;

                    if(cont.VehicleCompany != null)
                    {
                        data.RegNr = cont.VehicleCompany.RegNr;
                        data.VehicelModel = cont.VehicleCompany.Model;

                        if(cont.VehicleCompany.VehicleBrand != null)
                        {
                            data.VehicleBrandId = cont.VehicleCompany.VehicleBrand.Id;
                            data.VehicleBrandName = cont.VehicleCompany.VehicleBrand.VehicleBrandName;

                        }

                            
                    }
                    

                    returnList.Add(data);
                }

                return returnList;
            }
            else
                return null;
        }

        public List<CarPreSalesLogData> GetCarPreSalesMaintenanceLog(string username, string password, string ucid, int carpresalesid)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            EmployeeData emp;
            CarPreSalesLogData logData;
            Employee employee = db.Employees.Where(empd => empd.Office.Company.Ucid.Equals(ucid) && empd.Username.ToLower().Equals(username.ToLower()) && empd.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
            List<CarPreSalesLogData> returnList = new List<CarPreSalesLogData>();
            if (employee != null)
            {
                List<CarPreSaleLog> cps = new List<CarPreSaleLog>(db.CarPreSaleLogs.Where(o => o.CarPreSaleId == carpresalesid && o.IsMaintenance == true && o.CarPreSale.Office.Company.Ucid.Equals(ucid)));

                foreach(CarPreSaleLog log in cps)
                {
                    logData = new CarPreSalesLogData();
                    logData.CarPreSaleMaintenanceBegId = log.CarPreSaleMaintenanceBegId;
                    logData.CarPreSaleMaintenanceLagerId = log.CarPreSaleMaintenanceLagerId;
                    logData.CarPreSalesId = log.CarPreSaleId;
                    logData.Created = log.Created;

                    if(log.Employee != null)
                    {
                        emp = new EmployeeData();
                        emp.Id = log.Employee.Id;
                        emp.FName = log.Employee.FName;
                        emp.LName = log.Employee.LName;

                        logData.CreatedBy = emp;
                    }

                    logData.Description = log.Description;
                    
                    if(log.CarPreSaleLogDocuments.Count > 0)
                    {
                        logData.DocName = log.CarPreSaleLogDocuments.First().Name;
                        logData.DocPath = log.CarPreSaleLogDocuments.First().DocPath;
                    }

                    logData.Header = log.Header;
                    logData.Id = log.Id;

                    if (log.IsMaintenance == true)
                        logData.IsMaintenance = true;
                    else
                        logData.IsMaintenance = false;

                    returnList.Add(logData);


                }
            }

            return returnList;
        }

        public CarPreSalesMaintenaceBegData GetCarPreSalesMaintenanceBeg(string username, string password, string ucid, int begid)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            EmployeeData emp;
            CarPreSalesMaintenaceBegData begData = new CarPreSalesMaintenaceBegData();
            Employee employee = db.Employees.Where(empd => empd.Office.Company.Ucid.Equals(ucid) && empd.Username.ToLower().Equals(username.ToLower()) && empd.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();

            if (employee != null)
            {
                CarPreSalesMaintenanceBeg beg = db.CarPreSalesMaintenanceBegs.Where(cpb => cpb.CarPreSaleLogs.FirstOrDefault().CarPreSale.Office.Company.Ucid.Equals(ucid) && cpb.Id == begid).FirstOrDefault();

                if (beg != null)
                {
                    begData.BatteriCheckInfo = beg.BatteriCheckInfo;
                    begData.BatteriCheckOK = beg.BatteriCheckOK;

                    begData.BrakeCheckInfo = beg.BrakeCheckInfo;
                    begData.BrakeCheckOK = beg.BrakeCheckOK;

                    begData.CleanCheckInfo = beg.CleanCheckInfo;
                    begData.CleanCheckOK = beg.CleanCheckOK;

                    begData.GearboxCheckInfo = beg.GearboxCheckInfo;
                    begData.GearboxCheckOK = beg.GearboxCheckOK;

                    begData.TireCheckOK = beg.TireCheckOK;
                    begData.TireCheckInfo = beg.TireCheckInfo;

                    if (beg.CarPreSaleLogs != null && beg.CarPreSaleLogs.Count > 0)
                    {
                        CarPreSaleLog log = beg.CarPreSaleLogs.FirstOrDefault();

                        if (log != null)
                        {
                            begData.PerformedDate = log.Created;

                            if (log.Employee != null)
                            {
                                emp = new EmployeeData();
                                emp.Id = log.Employee.Id;
                                emp.FName = log.Employee.FName;
                                emp.LName = log.Employee.LName;

                                begData.PerformedByEmployee = emp;
                            }
                        }
                    }
                }

            }

            return begData;
        }

        public CarPreSalesMaintenaceLagerData GetCarPreSalesMaintenanceLager(string username, string password, string ucid, int lagerid)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            EmployeeData emp;
            CarPreSalesMaintenaceLagerData lagerData = new CarPreSalesMaintenaceLagerData();
            Employee employee = db.Employees.Where(empd => empd.Office.Company.Ucid.Equals(ucid) && empd.Username.ToLower().Equals(username.ToLower()) && empd.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();

            if (employee != null)
            {
                CarPreSalesMaintenanceLager lager = db.CarPreSalesMaintenanceLagers.Where(cpb => cpb.CarPreSaleLogs.FirstOrDefault().CarPreSale.Office.Company.Ucid.Equals(ucid) && cpb.Id == lagerid).FirstOrDefault();

                if (lager != null)
                {
                    lagerData.BatteriCheckInfo = lager.BatteriCheckInfo;
                    lagerData.BatteriCheckOK = lager.BatteriCheckOK;

                    lagerData.BrakeCheckInfo = lager.BrakeCheckInfo;
                    lagerData.BrakeCheckOK = lager.BrakeCheckOK;

                    lagerData.TireCheckInfo = lager.TireCheckInfo;
                    lagerData.TireCheckOK = lager.TireCheckOK;

                    lagerData.HighVoltCheckInfo = lager.HighVoltCheckInfo;
                    lagerData.HighVoltCheckOK = lager.HighVoltCheckOK;


                    if (lager.CarPreSaleLogs != null && lager.CarPreSaleLogs.Count > 0)
                    {
                        CarPreSaleLog log = lager.CarPreSaleLogs.FirstOrDefault();

                        if (log != null)
                        {
                            lagerData.PerformedDate = log.Created;

                            if (log.Employee != null)
                            {
                                emp = new EmployeeData();
                                emp.Id = log.Employee.Id;
                                emp.FName = log.Employee.FName;
                                emp.LName = log.Employee.LName;

                                lagerData.PerformedByEmployee = emp;
                            }
                        }
                    }
                }

            }

            return lagerData;
        }
        public string InsertCarPreSalesmaintenanceBeg(string username, string password, string ucid, CarPreSalesMaintenaceBegData begdata)
        {
            string returnMessage = "";

            try
            {
                KvalPortDbEntities db = new KvalPortDbEntities();
                Employee employee = db.Employees.Where(empd => empd.Office.Company.Ucid.Equals(ucid) && empd.Username.ToLower().Equals(username.ToLower()) && empd.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
                CarPreSale car;
                if (employee != null)
                {
                    if (begdata != null && begdata.CarPreSalesId > 0)
                    {
                        car = db.CarPreSales.Where(cps => cps.Id == begdata.CarPreSalesId && cps.Office.Company.Ucid == ucid && cps.Finished == null).FirstOrDefault();

                        if (car != null)
                        {
                            CarPreSaleLog log = new CarPreSaleLog();
                            log.CarPreSale = car;
                            log.Created = begdata.PerformedDate;
                            log.CreatedBy = begdata.PerformedById;
                            log.Header = "Underhåll utfört";
                            log.IsMaintenance = true;
                            db.CarPreSaleLogs.Add(log);

                            CarPreSalesMaintenanceBeg beglog = new CarPreSalesMaintenanceBeg();
                            beglog.BatteriCheckInfo = begdata.BatteriCheckInfo;
                            beglog.BatteriCheckOK = begdata.BatteriCheckOK;
                            beglog.BrakeCheckInfo = begdata.BrakeCheckInfo;
                            beglog.BrakeCheckOK = begdata.BrakeCheckOK;
                            beglog.CleanCheckInfo = begdata.CleanCheckInfo;
                            beglog.CleanCheckOK = begdata.CleanCheckOK;
                            beglog.GearboxCheckInfo = begdata.GearboxCheckInfo;
                            beglog.GearboxCheckOK = begdata.GearboxCheckOK;
                            beglog.TireCheckInfo = begdata.TireCheckInfo;
                            beglog.TireCheckOK = begdata.TireCheckOK;

                            log.CarPreSalesMaintenanceBeg = beglog;

                            db.CarPreSalesMaintenanceBegs.Add(beglog);

                            if(car.IntervalNr != null && !string.IsNullOrEmpty(car.IntervalUnit))
                            {
                                DateTime next = TimeDateFunctions.GetNextDate(begdata.PerformedDate, car.IntervalUnit, (int)car.IntervalNr);

                                if (car.Next == null || car.Next < next)
                                    car.Next = next;
                            }

                            db.SaveChanges();

                            return "Klart: Underhållet är sparat";
                        }
                        else
                            return "Error: Hittar inte fordon";
                    }else
                        return "Error: Saknar CarPreSalesId";
                }
                else
                    return "Error: Fel inloggningsuppgifter";

                

            }
            catch(Exception e)
            {
                returnMessage = "Error: " + e.Message;
                return returnMessage;
            }

            
        }

        public string InsertCarPreSalesmaintenanceLager(string username, string password, string ucid, CarPreSalesMaintenaceLagerData lagerdata)
        {

            try
            {
                KvalPortDbEntities db = new KvalPortDbEntities();
                Employee employee = db.Employees.Where(empd => empd.Office.Company.Ucid.Equals(ucid) && empd.Username.ToLower().Equals(username.ToLower()) && empd.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
                CarPreSale car;
                if (employee != null)
                {
                    if (lagerdata != null && lagerdata.CarPreSalesId > 0)
                    {
                        car = db.CarPreSales.Where(cps => cps.Id == lagerdata.CarPreSalesId && cps.Office.Company.Ucid == ucid && cps.Finished == null).FirstOrDefault();

                        if (car != null)
                        {
                            CarPreSaleLog log = new CarPreSaleLog();
                            log.CarPreSale = car;
                            log.Created = lagerdata.PerformedDate;
                            log.CreatedBy = lagerdata.PerformedById;
                            log.Header = "Underhåll utfört";
                            log.IsMaintenance = true;
                            db.CarPreSaleLogs.Add(log);

                            CarPreSalesMaintenanceLager lagerlog = new CarPreSalesMaintenanceLager();
                            lagerlog.BatteriCheckInfo = lagerdata.BatteriCheckInfo;
                            lagerlog.BatteriCheckOK = lagerdata.BatteriCheckOK;
                            lagerlog.BrakeCheckInfo = lagerdata.BrakeCheckInfo;
                            lagerlog.BrakeCheckOK = lagerdata.BrakeCheckOK;
                            lagerlog.TireCheckInfo = lagerdata.TireCheckInfo;
                            lagerlog.TireCheckOK = lagerdata.TireCheckOK;
                            lagerlog.HighVoltCheckInfo = lagerdata.HighVoltCheckInfo;
                            lagerdata.HighVoltCheckOK = lagerdata.HighVoltCheckOK;

                            log.CarPreSalesMaintenanceLager = lagerlog;

                            db.CarPreSalesMaintenanceLagers.Add(lagerlog);

                            if (car.IntervalNr != null && !string.IsNullOrEmpty(car.IntervalUnit))
                            {
                                DateTime next = TimeDateFunctions.GetNextDate(lagerdata.PerformedDate, car.IntervalUnit, (int)car.IntervalNr);

                                if (car.Next == null || car.Next < next)
                                    car.Next = next;
                            }

                            db.SaveChanges();

                            return "Klart: Underhållet är sparat";
                        }
                        else
                            return "Error: Hittar inte fordon";
                    }
                    else
                        return "Error: Saknar CarPreSalesId";
                }
                else
                    return "Error: Fel inloggningsuppgifter";
            }
            catch (Exception e)
            {
                return "Error: " + e.Message;
            }
        }

        public string InsertCarPreSalesmaintenanceStandard(string username, string password, string ucid, CarPreSalesMaintenaceStandardData standarddata)
        {
            try
            {
                KvalPortDbEntities db = new KvalPortDbEntities();
                Employee employee = db.Employees.Where(empd => empd.Office.Company.Ucid.Equals(ucid) && empd.Username.ToLower().Equals(username.ToLower()) && empd.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
                CarPreSale car;
                if (employee != null)
                {
                    if (standarddata != null && standarddata.CarPreSalesId > 0)
                    {
                        car = db.CarPreSales.Where(cps => cps.Id == standarddata.CarPreSalesId && cps.Office.Company.Ucid == ucid && cps.Finished == null).FirstOrDefault();

                        if (car != null)
                        {
                            CarPreSaleLog log = new CarPreSaleLog();
                            log.CarPreSale = car;
                            log.Created = standarddata.PerformedDate;
                            log.CreatedBy = standarddata.PerformedById;
                            log.Header = "Underhåll utfört";
                            log.IsMaintenance = true;
                            db.CarPreSaleLogs.Add(log);

                            if (!string.IsNullOrWhiteSpace(standarddata.DocPath))
                            {
                                CarPreSaleLogDocument doc = new CarPreSaleLogDocument();
                                doc.CarPreSaleLog = log;
                                doc.DocPath = standarddata.DocPath;
                                doc.Name = "Underhållsdokument";
                                db.CarPreSaleLogDocuments.Add(doc);
                            }

                            if (car.IntervalNr != null && !string.IsNullOrEmpty(car.IntervalUnit))
                            {
                                DateTime next = TimeDateFunctions.GetNextDate(standarddata.PerformedDate, car.IntervalUnit, (int)car.IntervalNr);

                                if (car.Next == null || car.Next < next)
                                    car.Next = next;
                            }

                            db.SaveChanges();

                            return "Klart: Underhållet är sparat";
                        }
                        else
                            return "Error: Hittar inte fordon";
                    }
                    else
                        return "Error: Saknar CarPreSalesId";
                }
                else
                    return "Error: Fel inloggningsuppgifter";
            }
            catch (Exception e)
            {
                return "Error: " + e.Message;
            }
        }
                        


        #endregion

        #region Company Office Employees

        public List<OfficeData> GetOfficesByCompanyId(string username, string password, string ucid, int companyId)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            OfficeData data;

            Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
            List<OfficeData> returnList = new List<OfficeData>();

            if (employee != null)
            {
                List<Office> off = new List<Office>(db.Offices.Where(o => o.Status == true && o.CompanyId == companyId));

                foreach(Office office in off)
                {
                    data = new OfficeData();
                    data.Id = office.Id;
                    data.Name = office.Name;

                    returnList.Add(data);
                }

                return returnList;
            }
            else
                return null;
        }

        public List<OfficeData> GetOfficesAndEmployeesByCompanyId(string username, string password, string ucid, int companyId)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            OfficeData data;

            Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
            List<OfficeData> returnList = new List<OfficeData>();
            List<EmployeeData> temp;
            EmployeeData empData;

            if (employee != null)
            {
                List<Office> off = new List<Office>(db.Offices.Include("Employees").Where(o => o.Status == true && o.CompanyId == companyId));

                foreach (Office office in off)
                {
                    data = new OfficeData();
                    data.Id = office.Id;
                    data.Name = office.Name;

                    if(office.Employees != null && office.Employees.Count > 0)
                    {
                        temp = new List<EmployeeData>();

                        foreach (Employee emp in office.Employees)
                        {
                            empData = new EmployeeData();
                            empData.FName = emp.FName;
                            empData.LName = emp.LName;
                            empData.Id = emp.Id;
                            empData.OfficeId = emp.OfficeId;

                            temp.Add(empData);
                        }

                        data.Employees = temp;
                    }
                    returnList.Add(data);
                }
            }

            return returnList;
        }


        #endregion

        #region Login

        public EmployeeData LoginEmployee(string username, string password, string ucid)
        {
            EmployeeData empData = new EmployeeData();
            try
            {
                KvalPortDbEntities db = new KvalPortDbEntities();

                Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();

                if (employee != null)
                {
                    empData.Id = employee.Id;
                    empData.FName = employee.FName;
                    empData.LName = employee.LName;
                    empData.Message = "";
                    empData.OfficeId = employee.OfficeId;
                    empData.CompanyId = employee.Office.CompanyId;
                }
                else
                    empData.Message = "Employee missing";


            }
            catch (Exception e)
            {
                empData.Message = "Exception: " + e.Message;
            }

            return empData;

        }


        #endregion

        #region Vehicle Brands

        public List<VehicleBrandData> GetCompanyVehicleBrands(string username, string password, string ucid, int companyId)
        {
            KvalPortDbEntities db = new KvalPortDbEntities();
            List<VehicleBrandData> returnList = new List<VehicleBrandData>();
            VehicleBrandData cbrand;


            Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();

            if (employee != null)
            {
                var brands = db.CompanyVehicleBrands.Where(cvb => cvb.CompanyId == companyId && cvb.Company.Ucid.Equals(ucid)).Select(vb => vb.VehicleBrand);

                foreach(VehicleBrand brand in brands)
                {
                    cbrand = new VehicleBrandData();
                    cbrand.Code = brand.VehicleBrandCode;
                    cbrand.Id = brand.Id;
                    cbrand.Name = brand.VehicleBrandName;

                    returnList.Add(cbrand);

                }
            }

            return returnList;
        }

        #endregion
 
        #region Quality Report

        public WCFReturnResultCls InsertQualityReport(string username, string password, string ucid, QualityReportData data)
        {
            try
            {
                KvalPortDbEntities db = new KvalPortDbEntities();
                Employee employee = db.Employees.Where(emp => emp.Office.Company.Ucid.Equals(ucid) && emp.Username.ToLower().Equals(username.ToLower()) && emp.Password.ToLower().Equals(password.ToLower())).FirstOrDefault();
                QualityReport lastReport;
                WCFReturnResultCls returnResult = new WCFReturnResultCls();
                QualityReport report = new QualityReport();
                CQReportTaskRow taskrow;
                Office office = db.Offices.Where(off => off.Id == data.OfficeId && off.Company.Ucid.Equals(ucid)).FirstOrDefault();
                if (employee != null && office != null)
                {
                    lastReport = db.QualityReports.Where(qr => qr.Year == data.Created.Year && qr.OfficeId == data.OfficeId).OrderByDescending(qrd => qrd.Created).FirstOrDefault();

                    if (lastReport == null)
                        report.ReportNr = 1;
                    else
                        report.ReportNr = lastReport.ReportNr + 1;

                    report.AoNr = data.AoNr;
                    report.Created = data.Created;
                    report.CreatedBy = data.CreatedBy;
                    report.Deleted = false;
                    report.Description = data.Description;
                    report.OfficeId = data.OfficeId;

                    ObservableCollection<QRResponsible> SelectedOfficeQResponsibles = new ObservableCollection<QRResponsible>(db.QRResponsibles.Where(qr => qr.OfficeId == data.OfficeId));
                    OfficeDepartmentTask offtask = db.OfficeDepartmentTasks.Where(ta => ta.Id == data.OfficeDepartmentTaskId).FirstOrDefault();
                    QRResponsible qResp;

                    qResp = (from r in SelectedOfficeQResponsibles
                            where r.EmployeeId == offtask.OfficeDepartment.Employee.Id
                            select r).FirstOrDefault();

                    if (qResp == null)
                        qResp = SelectedOfficeQResponsibles.First();

                   

                    report.QRResponsibleId = qResp.Id;
                    report.RegNr = data.RegNr;
                    report.Status = "Skapad";
                    report.StatusId = 1;
                    report.Year = data.Created.Year;

                    db.QualityReports.Add(report);

                    if (data.QRAttachedFileData != null && data.QRAttachedFileData.Count > 0)
                    {
                        QRAttachedFile _file;

                        foreach (QRAttachedFileData file in data.QRAttachedFileData.ToList())
                        {
                            _file = new QRAttachedFile();
                            _file.FileName = file.FileName;
                            _file.FilePath = file.FilePath;

                            string[] names = _file.FilePath.Split('.');

                            if (names[1].ToLower().Equals("pdf"))
                                _file.Icon = "/Images/Icons/file-extension-pdf-icon24.png";
                            else if (names[1].ToLower().Equals("gif"))
                                _file.Icon = "/Images/Icons/file-extension-gif-icon24.png";
                            else if (names[1].ToLower().Equals("jpg"))
                                _file.Icon = "/Images/Icons/file-extension-jpeg-icon24.png";
                            else if (names[1].ToLower().Equals("doc") || names[1].ToLower().Equals("docx"))
                                _file.Icon = "/Images/Icons/Word-icon24.png";
                            else if (names[1].ToLower().Equals("png"))
                                _file.Icon = "/Images/Icons/file-extension-png-icon24.png";
                            else
                                _file.Icon = "/Images/Icons/Document-Blank-icon24.png";

                            _file.QualityReport = report;

                            db.QRAttachedFiles.Add(_file);
                        }

                    }


                    QRLog log = new QRLog();
                    log.Created = DateTime.Now;
                    log.CreatedBy = data.CreatedBy;
                    log.QualityReport = report;
                    log.LogTypeId = 1;
                    log.Deleted = false;
                    log.Description = "Rapport skapad";
                    log.Subject = "Rapport skapad";

                    db.QRLogs.Add(log);

                    taskrow = new CQReportTaskRow();
                    taskrow.OfficeDepartmentTaskId = data.OfficeDepartmentTaskId;
                    taskrow.QualityReport = report;

                    db.CQReportTaskRows.Add(taskrow);

                    ObservableCollection<QRStatusOffice> SelectedOfficeQReportStatus = new ObservableCollection<QRStatusOffice>(db.QRStatusOffices.Where(qr => qr.OfficeId == data.OfficeId && qr.Activated == true));

                    ObservableCollection<QRModuleOffice> SelectedOfficeQReportModules = new ObservableCollection<QRModuleOffice>(from qMod in db.QRModuleOffices
                                                                                                                                 where qMod.OfficeId == data.OfficeId && qMod.Activated == true
                                                                                                                                 select qMod);
                    ObservableCollection<QROwnMeasure> SelectedOfficeQReportMeasures = new ObservableCollection<QROwnMeasure>(from qMeas in db.QROwnMeasures
                                                                                                                              where qMeas.OfficeId == data.OfficeId && qMeas.Activated == true
                                                                                                                              select qMeas);

                    QRReportMeasure measureReport;

                    foreach (QRStatusOffice status in SelectedOfficeQReportStatus)
                    {
                        if (status.Activated == true && status.IsReportMeasure)
                        {
                            measureReport = new QRReportMeasure();
                            measureReport.QualityReport = report;
                            measureReport.IsBeforeControlStatus = false;
                            measureReport.IsOwnMeasure = false;
                            measureReport.IsModule = false;
                            measureReport.IsStatus = true;
                            measureReport.QRStatusId = status.QRStatusId;

                            if (status.ControlStatusId == 1)
                            {
                                measureReport.Activated = true;
                                measureReport.ControlStatusId = 1;

                                if (status.TimeUnit == "Dagar")
                                    measureReport.FinishBefore = report.Created.AddDays(Convert.ToInt32(status.TimeNr));
                                else if (status.TimeUnit == "Veckor")
                                    measureReport.FinishBefore = report.Created.AddDays(Convert.ToInt32(status.TimeNr) * 7);
                                else if (status.TimeUnit == "Månader")
                                    measureReport.FinishBefore = report.Created.AddMonths(Convert.ToInt32(status.TimeNr));

                            }
                            else if (status.ControlStatusId == 2)
                            {
                                measureReport.Activated = false;
                                measureReport.ControlStatusId = 2;
                                measureReport.FinishNr = status.TimeNr;
                                measureReport.FinishUnit = status.TimeUnit;
                            }

                            db.QRReportMeasures.Add(measureReport);
                        }
                    }

                    foreach (QRModuleOffice module in SelectedOfficeQReportModules)
                    {
                        if (module.Activated == true)
                        {
                            measureReport = new QRReportMeasure();
                            measureReport.QualityReport = report;
                            measureReport.IsBeforeControlStatus = false;
                            measureReport.IsOwnMeasure = false;
                            measureReport.IsModule = true;
                            measureReport.IsStatus = false;
                            measureReport.QRModuleId = module.QRModuleId;

                            if (module.ControlStatusId == 1)
                            {
                                measureReport.Activated = true;
                                measureReport.ControlStatusId = 1;

                                if (module.TimeUnit == "Dagar")
                                    measureReport.FinishBefore = report.Created.AddDays(Convert.ToInt32(module.TimerNr));
                                else if (module.TimeUnit == "Veckor")
                                    measureReport.FinishBefore = report.Created.AddDays(Convert.ToInt32(module.TimerNr) * 7);
                                else if (module.TimeUnit == "Månader")
                                    measureReport.FinishBefore = report.Created.AddMonths(Convert.ToInt32(module.TimerNr));
                            }
                            else if (module.ControlStatusId == 2)
                            {
                                measureReport.Activated = false;
                                measureReport.ControlStatusId = 2;
                                measureReport.FinishNr = module.TimerNr;
                                measureReport.FinishUnit = module.TimeUnit;
                            }
                            else if (module.ControlStatusId == 3 && module.IsBeforeControlStatus == true)
                            {
                                measureReport.Activated = true;
                                measureReport.ControlStatusId = 3;
                                measureReport.IsBeforeControlStatus = true;
                            }
                            else if (module.ControlStatusId == 3 && module.IsBeforeControlStatus != true)
                            {
                                measureReport.Activated = false;
                                measureReport.ControlStatusId = 3;
                                measureReport.FinishNr = module.TimerNr;
                                measureReport.FinishUnit = module.TimeUnit;
                            }

                            db.QRReportMeasures.Add(measureReport);
                        }
                    }

                    foreach (QROwnMeasure measure in SelectedOfficeQReportMeasures)
                    {
                        if (measure.Activated == true)
                        {
                            measureReport = new QRReportMeasure();
                            measureReport.QualityReport = report;
                            measureReport.IsBeforeControlStatus = false;
                            measureReport.IsOwnMeasure = true;
                            measureReport.IsModule = false;
                            measureReport.IsStatus = false;

                            measureReport.QROwnMeasureId = measure.Id;

                            if (measure.ControlStatusId == 1)
                            {
                                measureReport.Activated = true;
                                measureReport.ControlStatusId = 1;

                                if (measure.TimeUnit == "Dagar")
                                    measureReport.FinishBefore = report.Created.AddDays(Convert.ToInt32(measure.TimeNr));
                                else if (measure.TimeUnit == "Veckor")
                                    measureReport.FinishBefore = report.Created.AddDays(Convert.ToInt32(measure.TimeNr) * 7);
                                else if (measure.TimeUnit == "Månader")
                                    measureReport.FinishBefore = report.Created.AddMonths(Convert.ToInt32(measure.TimeNr));
                            }
                            else if (measure.ControlStatusId == 2)
                            {
                                measureReport.Activated = false;
                                measureReport.ControlStatusId = 2;
                                measureReport.FinishNr = measure.TimeNr;
                                measureReport.FinishUnit = measure.TimeUnit;
                            }
                            else if (measure.ControlStatusId == 3 && measure.IsBeforeControlStatus == true)
                            {
                                measureReport.Activated = true;
                                measureReport.ControlStatusId = 3;
                                measureReport.IsBeforeControlStatus = true;
                            }
                            else if (measure.ControlStatusId == 3 && measure.IsBeforeControlStatus != true)
                            {
                                measureReport.Activated = false;
                                measureReport.ControlStatusId = 3;
                                measureReport.FinishNr = measure.TimeNr;
                                measureReport.FinishUnit = measure.TimeUnit;
                            }

                            db.QRReportMeasures.Add(measureReport);
                        }
                    }

                    db.SaveChanges();

                    returnResult.Id = report.Id;
                    returnResult.Message = "Rapporten är sparad";

                    EmailSender sender = new EmailSender();
                    sender.SendNewQualityReportMail(report);

                    return returnResult;

                    


                }
                else
                    return new WCFReturnResultCls() { Id = 0, Message = "Fel inloggning" };

                
            }
            catch(Exception e)
            {
                return new WCFReturnResultCls() { Id = 0, Message = "Fel: " + e.Message };
            }
            
        }

      





        #endregion

        #region QRResponsible

        #endregion


    }
}
