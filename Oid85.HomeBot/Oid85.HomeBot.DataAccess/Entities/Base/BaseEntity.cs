using System.ComponentModel.DataAnnotations.Schema;

namespace Oid85.HomeBot.DataAccess.Entities.Base;

public class BaseEntity
{
    [Column("id")]
    public Guid Id { get; set; }
}