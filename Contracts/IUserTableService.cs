//Contracts/IUserTable.cs

public interface IUserTableService
{
    Task<ServiceResult> GetCityList();
    Task<ServiceResult> GetDistrictList();
}