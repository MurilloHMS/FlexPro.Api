using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;
public class Email : Entity
{
    public string Address { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;

}
