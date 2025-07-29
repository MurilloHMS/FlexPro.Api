using System.ComponentModel.DataAnnotations.Schema;

namespace FlexPro.Domain.Abstractions;

public abstract class Entity
{
    public int Id { get; set; }
}