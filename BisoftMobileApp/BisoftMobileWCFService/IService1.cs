using BisoftMobileWCFService.Classes;
using BisoftMobileWCFService.Classes.CarPreSales;
using BisoftMobileWCFService.Classes.InternalControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BisoftMobileWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        // TODO: Add your service operations here

        #region Login
        [OperationContract]
        EmployeeData LoginEmployee(string username, string password, string ucid);

        #endregion

        #region Internal Controls

        [OperationContract]
        List<InternalControlOfficeData> GetOfficeInternalControls(string username, string password, string ucid, int officeId);

        [OperationContract]
        List<InternalControlOfficeResultsData> GetOfficeInternalControlsGoalVsResult(string username, string password, string ucid, int companyid);

        [OperationContract]
        InternalControlOfficeData GetOfficeInternalControl(string username, string password, string ucid, int officeId, int officecontrolId);
        [OperationContract]
        InternalControlOfficeLogData GetOfficeInternalControlLog(string username, string password, string ucid, int controlLogId);
        [OperationContract]
        InternalControlOfficeData GetOfficeInternalControlLogs(string username, string password, string ucid, int officecontrolId);

        [OperationContract]
        string InsertOfficeInternalControlPerformControl(string username, string password, string ucid, InternalControlOfficeLogData officeLog);
        [OperationContract]
        string UpdateOfficeInternalControlLog(string username, string password, string ucid, InternalControlOfficeLogData officeLog);
        [OperationContract]
        string DeleteOfficeInternalControlPerformControl(string username, string password, string ucid, int logId);
        [OperationContract]
        List<ICErrorCodeMainGroupData> GetICErrorCodes(string username, string password, string ucid, int companyId);

        #endregion

        #region Company Offices Employees

        [OperationContract]
        List<OfficeData> GetOfficesByCompanyId(string username, string password, string ucid, int companyId);

        [OperationContract]
        List<OfficeData> GetOfficesAndEmployeesByCompanyId(string username, string password, string ucid, int companyId);

        #endregion

        #region Car Pre sales
        [OperationContract]
        List<CarPreSalesMaintenanceData> GetCarPreSalesmaintenanceByOffice(string username, string password, string ucid, int officeId, int? employeeId);

        [OperationContract]
        List<CarPreSalesLogData> GetCarPreSalesMaintenanceLog(string username, string password, string ucid, int carpresalesid);

        [OperationContract]
        CarPreSalesMaintenaceBegData GetCarPreSalesMaintenanceBeg(string username, string password, string ucid, int begid);

        [OperationContract]
        CarPreSalesMaintenaceLagerData GetCarPreSalesMaintenanceLager(string username, string password, string ucid, int lagerid);

        [OperationContract]
        string InsertCarPreSalesmaintenanceBeg(string username, string password, string ucid, CarPreSalesMaintenaceBegData begdata);

        [OperationContract]
        string InsertCarPreSalesmaintenanceLager(string username, string password, string ucid, CarPreSalesMaintenaceLagerData lagerdata);

        [OperationContract]
        string InsertCarPreSalesmaintenanceStandard(string username, string password, string ucid, CarPreSalesMaintenaceStandardData standarddata);
        #endregion

        #region Vehice Brand
        [OperationContract]
        List<VehicleBrandData> GetCompanyVehicleBrands(string username, string password, string ucid, int companyId);

        #endregion

        #region Quality Report

        [OperationContract]
        WCFReturnResultCls InsertQualityReport(string username, string password, string ucid, QualityReportData data);

        #endregion
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.

}
