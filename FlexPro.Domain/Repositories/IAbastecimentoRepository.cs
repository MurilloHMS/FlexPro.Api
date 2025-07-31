using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories;

public interface IAbastecimentoRepository
{
    Task AddFuelSupply(Abastecimento fuelSupply);
    Task RemoveFuelSupply(int fuelSupplyId);
    Task AddRangeFuelSupply(List<Abastecimento> fuelSupplies);
    Task<List<Abastecimento>> GetFuelSupply();
    Task<List<Abastecimento>> GetFuelSupplyByDate(DateTime initial, DateTime finish);
}