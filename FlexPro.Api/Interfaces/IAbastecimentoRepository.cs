using FlexPro.Api.Models;

namespace FlexPro.Api.Interfaces
{
    public interface IAbastecimentoRepository
    {
        Task<string> CalculateGeneralFuelSupply(DateTime date);
        Task<string> CalculateSetorFuelSupply(DateTime date);
        Task<string> CalculateIndividualFuelSupply(DateTime date);
        Task<string> CalculateFuelSupply(IList<Abastecimento> fuelSupplyCurrentMonth, IList<Abastecimento> fuelSupplyLastMonth, string type);
        Task AddFuelSupply(Abastecimento fuelSupply);
        Task RemoveFuelSupply(int fuelSupplyId);
        Task AddRangeFuelSupply(IList<Abastecimento> fuelSupplies);
        Task ExportData(IList<Abastecimento> fuelSupplies);
        Task ImportData();
        Task<IEnumerable<Abastecimento>> GetFuelSupply();
        Task<IEnumerable<Abastecimento>> CollectFuelSupplyData(IFormFile file);
    }
}
