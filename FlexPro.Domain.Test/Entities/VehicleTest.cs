using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Test.Entities;

[TestClass]
public class VehicleTest
{
    [TestMethod]
    public void Should_Not_Fail_When_ToString_is_Correct()
    {
        var vehicle = new Veiculo
        {
            Nome = "Civic",
            Placa = "XYZ-1234",
            Marca = "Honda"
        };

        var result = vehicle.ToString();

        Assert.AreEqual("Civic, Honda - XYZ-1234", result);
    }

    [TestMethod]
    public void Should_Not_Fail_When_Vehicle_Has_Default_Values()
    {
        var vehicle = new Veiculo();

        Assert.AreEqual(string.Empty, vehicle.Nome);
        Assert.AreEqual(string.Empty, vehicle.Placa);
        Assert.AreEqual(string.Empty, vehicle.Marca);
        Assert.IsNull(vehicle.ConsumoRodoviarioAlcool);
        Assert.IsNull(vehicle.ConsumoRodoviarioGasolina);
        Assert.IsNull(vehicle.ConsumoUrbanoAlcool);
        Assert.IsNull(vehicle.ConsumoUrbanoGasolina);
    }

    [TestMethod]
    public void Should_Fail_When_Name_is_Null()
    {
        var vehicle = new Veiculo();
        vehicle.Nome = null;
    }
}