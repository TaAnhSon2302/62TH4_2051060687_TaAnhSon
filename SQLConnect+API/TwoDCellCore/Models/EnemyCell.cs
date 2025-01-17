﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TwoDCellCore.Models;

[Table("enemy_cells")]
[Index("AbilityId", Name = "IX_enemy_cells_AbilityID")]
[Index("FactionId", Name = "IX_enemy_cells_factionID")]
public partial class EnemyCell
{
    [Key]
    [Column("EnemyID")]
    [StringLength(10)]
    public string EnemyId { get; set; } = null!;

    [StringLength(50)]
    public string EnemyName { get; set; } = null!;

    [Column("HP")]
    public int Hp { get; set; }

    [Column("MP")]
    public int Mp { get; set; }

    [StringLength(10)]
    public string CellProtection { get; set; } = null!;

    public int Armor { get; set; }

    public double MoveSpeed { get; set; }

    [Column("factionID")]
    [StringLength(10)]
    public string FactionId { get; set; } = null!;

    [Column("AbilityID")]
    [StringLength(10)]
    public string? AbilityId { get; set; }

    [StringLength(10)]
    public string ShieldType { get; set; } = null!;

    public int Shield { get; set; }

    [StringLength(10)]
    public string Equipment { get; set; } = null!;

    public int? BodyDamage { get; set; }
    public int? XpObs { get; set; }

    [JsonIgnore]
    [ForeignKey("AbilityId")]
    [InverseProperty("EnemyCells")]
    public virtual MutationAbility? Ability { get; set; }

    [JsonIgnore]
    [ForeignKey("FactionId")]
    [InverseProperty("EnemyCells")]
    public virtual CellFaction Faction { get; set; } = null!;
}
