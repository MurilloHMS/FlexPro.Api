using FlexPro.Api.Models;

namespace FlexPro.Api.Interfaces
{
    public interface IAbastecimentoRepository
    {
        Task AddFuelSupply(Abastecimento fuelSupply);
        Task RemoveFuelSupply(int fuelSupplyId);
        Task AddRangeFuelSupply(List<Abastecimento> fuelSupplies);
        Task ExportData(List<Abastecimento> fuelSupplies);
        Task ImportData();
        Task<List<Abastecimento>> GetFuelSupply();
        Task<List<Abastecimento>> GetFuelSupplyByDate(DateTime initial, DateTime finish);
    }
}
