using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GowWebSite.Models
{
    public class CoresModel
    {
        public Dictionary<string, Item> Items = new Dictionary<string, Item>();

        public CoresModel(GowEntities db, bool isCore)
        {
            var cores = db.Cores.Where(m => 1 == 1);
            int coreCount = 0;
            foreach (var core in cores)
            {
                Item item = new Item();
                item.Name = core.GearName;
                item.Levels = new Dictionary<string, LevelDetail>();
                foreach (var lvl in cores.Where(m => m.GearName == core.GearName && m.GearSlot == core.GearSlot))
                {
                    //Here we have all the entries for that GearName and GearSlot
                    LevelDetail level = new LevelDetail();
                    level.TroopAttackLow = lvl.TroopAttackLow;
                    level.TroopAttackHigh = lvl.TroopAttackHigh;
                    level.TroopDefenceLow = lvl.TroopDefenceLow;
                    level.TroopDefenceHigh = lvl.TroopDefenceHigh;
                    level.TroopHealthLow = lvl.TroopHealthLow;
                    level.TroopHealthHigh = lvl.TroopHealthHigh;
                    level.StrategicTroopAttackLow = lvl.StrategicTroopAttackLow;
                    level.StrategicTroopAttackHigh = lvl.StrategicTroopAttackHigh;
                    level.StrategicTroopDefenceLow = lvl.StrategicTroopDefenceLow;
                    level.StrategicTroopDefenceHigh = lvl.StrategicTroopDefenceHigh;
                    level.StrategicTroopHealthLow = lvl.StrategicTroopHealthLow;
                    level.StrategicTroopHealthHigh = lvl.StrategicTroopHealthHigh;
                    level.EnemyAttackDebuffLow = lvl.EnemyAttackDebuffLow;
                    level.EnemyAttackDebuffHigh = lvl.EnemyAttackDebuffHigh;
                    level.EnemyStrategicAttackDebuffLow = lvl.EnemyStrategicAttackDebuffLow;
                    level.EnemyStrategicAttackDebuffHigh = lvl.EnemyStrategicAttackDebuffHigh;
                    level.EnemyDefenceDebuffLow = lvl.EnemyDefenceDebuffLow;
                    level.EnemyDefenceDebuffHigh = lvl.EnemyDefenceDebuffHigh;
                    level.EnemyStrategicDefenceDebuffLow = lvl.EnemyStrategicDefenceDebuffLow;
                    level.EnemyStrategicDefenceDebuffHigh = lvl.EnemyStrategicDefenceDebuffHigh;
                    level.EnemyHealthDebuffLow = lvl.EnemyHealthDebuffLow;
                    level.EnemyHealthDebuffHigh = lvl.EnemyHealthDebuffHigh;
                    level.EnemyStrategicHealthDebuffLow = lvl.EnemyStrategicHealthDebuffLow;
                    level.EnemyStrategicHealthDebuffHigh = lvl.EnemyStrategicHealthDebuffHigh;
                    level.MarchSpeedLow = lvl.MarchSpeedLow;
                    level.MarchSpeedHigh = lvl.MarchSpeedHigh;
                    level.InfantryAttackLow = lvl.InfantryAttackLow;
                    level.InfantryAttackHigh = lvl.InfantryAttackHigh;
                    level.InfantryDefenceLow = lvl.InfantryDefenceLow;
                    level.InfantryDefenceHigh = lvl.InfantryDefenceHigh;
                    level.StrategicInfantryAttackLow = lvl.StrategicInfantryAttackLow;
                    level.StrategicInfantryAttackHigh = lvl.StrategicInfantryAttackHigh;
                    level.StrategicInfantryDefenceLow = lvl.StrategicInfantryDefenceLow;
                    level.StrategicInfantryDefenceHigh = lvl.StrategicInfantryDefenceHigh;
                    level.InfantryHealthLow = lvl.InfantryHealthLow;
                    level.InfantryHealthHigh = lvl.InfantryHealthHigh;
                    level.StrategicInfantryHealthLow = lvl.StrategicInfantryHealthLow;
                    level.StrategicInfantryHealthHigh = lvl.StrategicInfantryHealthHigh;
                    level.EnemyInfantryAttackDebuffLow = lvl.EnemyInfantryAttackDebuffLow;
                    level.EnemyInfantryAttackDebuffHigh = lvl.EnemyInfantryAttackDebuffHigh;
                    level.StratEnemyInfantryAttackDebuffLow = lvl.StratEnemyInfantryAttackDebuffLow;
                    level.StratEnemyInfantryAttackDebuffHigh = lvl.StratEnemyInfantryAttackDebuffHigh;
                    level.EnemyInfantryDefenseDebuffLow = lvl.EnemyInfantryDefenceDebuffLow;
                    level.EnemyInfantryDefenseDebuffHigh = lvl.EnemyInfantryDefenceDebuffHigh;
                    level.EnemyStrategicInfantryDefenseDebuffLow = lvl.EnemyStrategicInfantryDefenceDebuffLow;
                    level.EnemyStrategicInfantryDefenseDebuffHigh = lvl.EnemyStrategicInfantryDefenceDebuffHigh;
                    level.EnemyStrategicInfantryHealthDebuffLow = lvl.EnemyStrategicInfantryHealthDebuffLow;
                    level.EnemyStrategicInfantryHealthDebuffHigh = lvl.EnemyStrategicInfantryHealthDebuffHigh;
                    level.EnemyInfantryHealthDebuffLow = lvl.EnemyInfantryHealthDebuffLow;
                    level.EnemyInfantryHealthDebuffHigh = lvl.EnemyInfantryHealthDebuffHigh;
                    level.RangedAttackLow = lvl.RangedAttackLow;
                    level.RangedAttackHigh = lvl.RangedAttackHigh;
                    level.RangedDefenceLow = lvl.RangedDefenceLow;
                    level.RangedDefenceHigh = lvl.RangedDefenceHigh;
                    level.StrategicRangedAttackLow = lvl.StrategicRangedAttackLow;
                    level.StrategicRangedAttackHigh = lvl.StrategicRangedAttackHigh;
                    level.StrategicRangedDefenceLow = lvl.StrategicRangedDefenceLow;
                    level.StrategicRangedDefenceHigh = lvl.StrategicRangedDefenceHigh;
                    level.RangedHealthLow = lvl.RangedHealthLow;
                    level.RangedHealthHigh = lvl.RangedHealthHigh;
                    level.StrategicRangedHealthLow = lvl.StrategicRangedHealthLow;
                    level.StrategicRangedHealthHigh = lvl.StrategicRangedHealthHigh;
                    level.EnemyRangedAttackDebuffLow = lvl.EnemyRangedAttackDebuffLow;
                    level.EnemyRangedAttackDebuffHigh = lvl.EnemyRangedAttackDebuffHigh;
                    level.EnemyStrategicRangedAttackDebuffLow = lvl.EnemyStrategicRangedAttackDebuffLow;
                    level.EnemyStrategicRangedAttackDebuffHigh = lvl.EnemyStrategicRangedAttackDebuffHigh;
                    level.EnemyRangedDefenceDebuffLow = lvl.EnemyRangedDefenceDebuffLow;
                    level.EnemyRangedDefenceDebuffHigh = lvl.EnemyRangedDefenceDebuffHigh;
                    level.EnemyStrategicRangedDefenceDebuffLow = lvl.EnemyStrategicRangedDefenceDebuffLow;
                    level.EnemyStrategicRangedDefenceDebuffHigh = lvl.EnemyStrategicRangedDefenceDebuffHigh;
                    level.EnemyRangedHealthDebuffLow = lvl.EnemyRangedHealthDebuffLow;
                    level.EnemyRangedHealthDebuffHigh = lvl.EnemyRangedHealthDebuffHigh;
                    level.EnemyStrategicRangedHealthDebuffLow = lvl.EnemyStrategicRangedHealthDebuffLow;
                    level.EnemyStrategicRangedHealthDebuffHigh = lvl.EnemyStrategicRangedHealthDebuffHigh;
                    level.CavalryAttackLow = lvl.CavalryAttackLow;
                    level.CavalryAttackHigh = lvl.CavalryAttackHigh;
                    level.CavalryDefenceLow = lvl.CavalryDefenceLow;
                    level.CavalryDefenceHigh = lvl.CavalryDefenceHigh;
                    level.StrategicCavalryAttackLow = lvl.StrategicCavalryAttackLow;
                    level.StrategicCavalryAttackHigh = lvl.StrategicCavalryAttackHigh;
                    level.StrategicCavalryDefenceLow = lvl.StrategicCavalryDefenceLow;
                    level.StrategicCavalryDefenceHigh = lvl.StrategicCavalryDefenceHigh;
                    level.CavalryHealthLow = lvl.CavalryHealthLow;
                    level.CavalryHealthHigh = lvl.CavalryHealthHigh;
                    level.StrategicCavalryHealthLow = lvl.StrategicCavalryHealthLow;
                    level.StrategicCavalryHealthHigh = lvl.StrategicCavalryHealthHigh;
                    level.EnemyCavalryAttackDebuffLow = lvl.EnemyCavalryAttackDebuffLow;
                    level.EnemyCavalryAttackDebuffHigh = lvl.EnemyCavalryAttackDebuffHigh;
                    level.StrategicEnemyCavalryAttackDebuffLow = lvl.StrategicEnemyCavalryAttackDebuffLow;
                    level.StrategicEnemyCavalryAttackDebuffHigh = lvl.StrategicEnemyCavalryAttackDebuffHigh;
                    level.EnemyCavalryDefenceDebuffLow = lvl.EnemyCavalryDefenceDebuffLow;
                    level.EnemyCavalryDefenceDebuffHigh = lvl.EnemyCavalryDefenceDebuffHigh;
                    level.StrategicEnemyCavalryDefenceDebuffLow = lvl.StrategicEnemyCavalryDefenceDebuffLow;
                    level.StrategicEnemyCavalryDefenceDebuffHigh = lvl.StrategicEnemyCavalryDefenceDebuffHigh;
                    level.EnemyCavalryHealthDebuffLow = lvl.EnemyCavalryHealthDebuffLow;
                    level.EnemyCavalryHealthDebuffHigh = lvl.EnemyCavalryHealthDebuffHigh;
                    level.StrategicEnemyCavalryHealthDebuffLow = lvl.StrategicEnemyCavalryHealthDebuffLow;
                    level.StrategicEnemyCavalryHealthDebuffHigh = lvl.StrategicEnemyCavalryHealthDebuffHigh;
                    level.SiegeAttackLow = lvl.SiegeAttackLow;
                    level.SiegeAttackHigh = lvl.SiegeAttackHigh;
                    level.SiegeDefenceLow = lvl.SiegeDefenceLow;
                    level.SiegeDefenceHigh = lvl.SiegeDefenceHigh;
                    level.TrapDefenseLow = lvl.TrapDefenseLow;
                    level.TrapDefenseHigh = lvl.TrapDefenseHigh;
                    level.StrategicTrapDefenseLow = lvl.StrategicTrapDefenseLow;
                    level.StrategicTrapDefenseHigh = lvl.StrategicTrapDefenseHigh;
                    level.TroopTrainingSpeedLow = lvl.TroopTrainingSpeedLow;
                    level.TroopTrainingSpeedHigh = lvl.TroopTrainingSpeedHigh;
                    item.Levels.Add(lvl.GearLevel.ToString(), level);
                }

                Items.Add(coreCount.ToString(), item);
                coreCount++;
            }

        }

        public CoresModel(GowEntities db)
        {
            var pieces = db.Pieces.GroupBy(m => m.PieceName).Select(grp=>grp.FirstOrDefault());
            int pieceCount = 0;
            foreach (var piece in pieces)
            {
                Item item = new Item();
                item.Name = piece.PieceName;
                item.Levels = new Dictionary<string, LevelDetail>();
                foreach (var lvl in pieces.Where(m => m.PieceName == piece.PieceName))
                {
                    //Here we have all the entries for that GearName and GearSlot
                    LevelDetail level = new LevelDetail();
                    level.TroopAttackLow = lvl.TroopAttackLow;
                    level.TroopAttackHigh = lvl.TroopAttackHigh;
                    level.TroopDefenceLow = lvl.TroopDefenceLow;
                    level.TroopDefenceHigh = lvl.TroopDefenceHigh;
                    level.TroopHealthLow = lvl.TroopHealthLow;
                    level.TroopHealthHigh = lvl.TroopHealthHigh;
                    level.StrategicTroopAttackLow = lvl.StrategicTroopAttackLow;
                    level.StrategicTroopAttackHigh = lvl.StrategicTroopAttackHigh;
                    level.StrategicTroopDefenceLow = lvl.StrategicTroopDefenceLow;
                    level.StrategicTroopDefenceHigh = lvl.StrategicTroopDefenceHigh;
                    level.StrategicTroopHealthLow = lvl.StrategicTroopHealthLow;
                    level.StrategicTroopHealthHigh = lvl.StrategicTroopHealthHigh;
                    level.EnemyAttackDebuffLow = lvl.EnemyAttackDebuffLow;
                    level.EnemyAttackDebuffHigh = lvl.EnemyAttackDebuffHigh;
                    level.EnemyStrategicAttackDebuffLow = lvl.EnemyStrategicAttackDebuffLow;
                    level.EnemyStrategicAttackDebuffHigh = lvl.EnemyStrategicAttackDebuffHigh;
                    level.EnemyDefenceDebuffLow = lvl.EnemyDefenceDebuffLow;
                    level.EnemyDefenceDebuffHigh = lvl.EnemyDefenceDebuffHigh;
                    level.EnemyStrategicDefenceDebuffLow = lvl.EnemyStrategicDefenceDebuffLow;
                    level.EnemyStrategicDefenceDebuffHigh = lvl.EnemyStrategicDefenceDebuffHigh;
                    level.EnemyHealthDebuffLow = lvl.EnemyHealthDebuffLow;
                    level.EnemyHealthDebuffHigh = lvl.EnemyHealthDebuffHigh;
                    level.EnemyStrategicHealthDebuffLow = lvl.EnemyStrategicHealthDebuffLow;
                    level.EnemyStrategicHealthDebuffHigh = lvl.EnemyStrategicHealthDebuffHigh;
                    level.MarchSpeedLow = lvl.MarchSpeedLow;
                    level.MarchSpeedHigh = lvl.MarchSpeedHigh;
                    level.InfantryAttackLow = lvl.InfantryAttackLow;
                    level.InfantryAttackHigh = lvl.InfantryAttackHigh;
                    level.InfantryDefenceLow = lvl.InfantryDefenceLow;
                    level.InfantryDefenceHigh = lvl.InfantryDefenceHigh;
                    level.StrategicInfantryAttackLow = lvl.StrategicInfantryAttackLow;
                    level.StrategicInfantryAttackHigh = lvl.StrategicInfantryAttackHigh;
                    level.StrategicInfantryDefenceLow = lvl.StrategicInfantryDefenceLow;
                    level.StrategicInfantryDefenceHigh = lvl.StrategicInfantryDefenceHigh;
                    level.InfantryHealthLow = lvl.InfantryHealthLow;
                    level.InfantryHealthHigh = lvl.InfantryHealthHigh;
                    level.StrategicInfantryHealthLow = lvl.StrategicInfantryHealthLow;
                    level.StrategicInfantryHealthHigh = lvl.StrategicInfantryHealthHigh;
                    level.EnemyInfantryAttackDebuffLow = lvl.EnemyInfantryAttackDebuffLow;
                    level.EnemyInfantryAttackDebuffHigh = lvl.EnemyInfantryAttackDebuffHigh;
                    level.StratEnemyInfantryAttackDebuffLow = lvl.StratEnemyInfantryAttackDebuffLow;
                    level.StratEnemyInfantryAttackDebuffHigh = lvl.StratEnemyInfantryAttackDebuffHigh;
                    level.EnemyInfantryDefenseDebuffLow = lvl.EnemyInfantryDefenceDebuffLow;
                    level.EnemyInfantryDefenseDebuffHigh = lvl.EnemyInfantryDefenceDebuffHigh;
                    level.EnemyStrategicInfantryDefenseDebuffLow = lvl.EnemyStrategicInfantryDefenceDebuffLow;
                    level.EnemyStrategicInfantryDefenseDebuffHigh = lvl.EnemyStrategicInfantryDefenceDebuffHigh;
                    level.EnemyStrategicInfantryHealthDebuffLow = lvl.EnemyStrategicInfantryHealthDebuffLow;
                    level.EnemyStrategicInfantryHealthDebuffHigh = lvl.EnemyStrategicInfantryHealthDebuffHigh;
                    level.EnemyInfantryHealthDebuffLow = lvl.EnemyInfantryHealthDebuffLow;
                    level.EnemyInfantryHealthDebuffHigh = lvl.EnemyInfantryHealthDebuffHigh;
                    level.RangedAttackLow = lvl.RangedAttackLow;
                    level.RangedAttackHigh = lvl.RangedAttackHigh;
                    level.RangedDefenceLow = lvl.RangedDefenceLow;
                    level.RangedDefenceHigh = lvl.RangedDefenceHigh;
                    level.StrategicRangedAttackLow = lvl.StrategicRangedAttackLow;
                    level.StrategicRangedAttackHigh = lvl.StrategicRangedAttackHigh;
                    level.StrategicRangedDefenceLow = lvl.StrategicRangedDefenceLow;
                    level.StrategicRangedDefenceHigh = lvl.StrategicRangedDefenceHigh;
                    level.RangedHealthLow = lvl.RangedHealthLow;
                    level.RangedHealthHigh = lvl.RangedHealthHigh;
                    level.StrategicRangedHealthLow = lvl.StrategicRangedHealthLow;
                    level.StrategicRangedHealthHigh = lvl.StrategicRangedHealthHigh;
                    level.EnemyRangedAttackDebuffLow = lvl.EnemyRangedAttackDebuffLow;
                    level.EnemyRangedAttackDebuffHigh = lvl.EnemyRangedAttackDebuffHigh;
                    level.EnemyStrategicRangedAttackDebuffLow = lvl.EnemyStrategicRangedAttackDebuffLow;
                    level.EnemyStrategicRangedAttackDebuffHigh = lvl.EnemyStrategicRangedAttackDebuffHigh;
                    level.EnemyRangedDefenceDebuffLow = lvl.EnemyRangedDefenceDebuffLow;
                    level.EnemyRangedDefenceDebuffHigh = lvl.EnemyRangedDefenceDebuffHigh;
                    level.EnemyStrategicRangedDefenceDebuffLow = lvl.EnemyStrategicRangedDefenceDebuffLow;
                    level.EnemyStrategicRangedDefenceDebuffHigh = lvl.EnemyStrategicRangedDefenceDebuffHigh;
                    level.EnemyRangedHealthDebuffLow = lvl.EnemyRangedHealthDebuffLow;
                    level.EnemyRangedHealthDebuffHigh = lvl.EnemyRangedHealthDebuffHigh;
                    level.EnemyStrategicRangedHealthDebuffLow = lvl.EnemyStrategicRangedHealthDebuffLow;
                    level.EnemyStrategicRangedHealthDebuffHigh = lvl.EnemyStrategicRangedHealthDebuffHigh;
                    level.CavalryAttackLow = lvl.CavalryAttackLow;
                    level.CavalryAttackHigh = lvl.CavalryAttackHigh;
                    level.CavalryDefenceLow = lvl.CavalryDefenceLow;
                    level.CavalryDefenceHigh = lvl.CavalryDefenceHigh;
                    level.StrategicCavalryAttackLow = lvl.StrategicCavalryAttackLow;
                    level.StrategicCavalryAttackHigh = lvl.StrategicCavalryAttackHigh;
                    level.StrategicCavalryDefenceLow = lvl.StrategicCavalryDefenceLow;
                    level.StrategicCavalryDefenceHigh = lvl.StrategicCavalryDefenceHigh;
                    level.CavalryHealthLow = lvl.CavalryHealthLow;
                    level.CavalryHealthHigh = lvl.CavalryHealthHigh;
                    level.StrategicCavalryHealthLow = lvl.StrategicCavalryHealthLow;
                    level.StrategicCavalryHealthHigh = lvl.StrategicCavalryHealthHigh;
                    level.EnemyCavalryAttackDebuffLow = lvl.EnemyCavalryAttackDebuffLow;
                    level.EnemyCavalryAttackDebuffHigh = lvl.EnemyCavalryAttackDebuffHigh;
                    level.StrategicEnemyCavalryAttackDebuffLow = lvl.StrategicEnemyCavalryAttackDebuffLow;
                    level.StrategicEnemyCavalryAttackDebuffHigh = lvl.StrategicEnemyCavalryAttackDebuffHigh;
                    level.EnemyCavalryDefenceDebuffLow = lvl.EnemyCavalryDefenceDebuffLow;
                    level.EnemyCavalryDefenceDebuffHigh = lvl.EnemyCavalryDefenceDebuffHigh;
                    level.StrategicEnemyCavalryDefenceDebuffLow = lvl.StrategicEnemyCavalryDefenceDebuffLow;
                    level.StrategicEnemyCavalryDefenceDebuffHigh = lvl.StrategicEnemyCavalryDefenceDebuffHigh;
                    level.EnemyCavalryHealthDebuffLow = lvl.EnemyCavalryHealthDebuffLow;
                    level.EnemyCavalryHealthDebuffHigh = lvl.EnemyCavalryHealthDebuffHigh;
                    level.StrategicEnemyCavalryHealthDebuffLow = lvl.StrategicEnemyCavalryHealthDebuffLow;
                    level.StrategicEnemyCavalryHealthDebuffHigh = lvl.StrategicEnemyCavalryHealthDebuffHigh;
                    level.SiegeAttackLow = lvl.SiegeAttackLow;
                    level.SiegeAttackHigh = lvl.SiegeAttackHigh;
                    level.SiegeDefenceLow = lvl.SiegeDefenceLow;
                    level.SiegeDefenceHigh = lvl.SiegeDefenceHigh;
                    level.TrapDefenseLow = lvl.TrapDefenceLow;
                    level.TrapDefenseHigh = lvl.TrapDefenceHigh;
                    level.StrategicTrapDefenseLow = lvl.StrategicTrapDefenceLow;
                    level.StrategicTrapDefenseHigh = lvl.StrategicTrapDefenceHigh;
                    level.TroopTrainingSpeedLow = lvl.TroopTrainingSpeedLow;
                    level.TroopTrainingSpeedHigh = lvl.TroopTrainingSpeedHigh;

                    level.EnemySiegeAttackDebuffLow = lvl.EnemySiegeAttackDebuffLow;
                    level.EnemySiegeAttackDebuffHigh = lvl.EnemySiegeAttackDebuffHigh;
                    level.StrategicTrapAttackLow = lvl.StrategicTrapAttackLow;
                    level.StrategicTrapAttackHigh = lvl.StrategicTrapAttackHigh;
                    level.TrapAttackLow = lvl.TrapAttackLow;
                    level.TrapAttackHigh = lvl.TrapAttackHigh;
                    level.HeroCriticalLow = lvl.HeroCriticalLow;
                    level.HeroCriticalHigh = lvl.HeroCriticalHigh;
                    level.MonsterDebuffLow = lvl.MonsterDebuffLow;
                    level.MonsterDebuffHigh = lvl.MonsterDebuffHigh;
                    item.Levels.Add(lvl.PieceLevel.ToString(), level);
                }

                Items.Add(pieceCount.ToString(), item);
                pieceCount++;
            }

        }
    
        
    }

    public class Filters
    {
        public Filters()
        {

        }

        public bool Overall = true;
        public bool Infantry = true;
        public bool Ranged = true;
        public bool Cavalry = true;
        public bool Other = true;
        public bool Attack = true;
        public bool Defence = true;
        public bool Health = true;
        public bool AttackDebuff = true;
        public bool DefenceDebuff = true;
        public bool HealthDebuff = true;
    }

    public class SortPiece
    {
        public long PieceID { get; set; }
        public string PieceName { get; set; }
        public decimal SortValue { get; set; }
    }

    public class SortCore
    {
        public long GearID { get; set; }
        public string GearName { get; set; }
        public string GearSlot { get; set; }
        public decimal SortValue { get; set; }
    }

    public class Item
    {
        public string Name;
        public Dictionary<string, LevelDetail> Levels;
    }

    public class LevelDetail
    {
        public decimal TroopAttackLow;
        public decimal TroopAttackHigh;
        public decimal TroopDefenceLow;
        public decimal TroopDefenceHigh;
        public decimal TroopHealthLow;
        public decimal TroopHealthHigh;
        public decimal StrategicTroopAttackLow;
        public decimal StrategicTroopAttackHigh;
        public decimal StrategicTroopDefenceLow;
        public decimal StrategicTroopDefenceHigh;
        public decimal StrategicTroopHealthLow;
        public decimal StrategicTroopHealthHigh;
        public decimal EnemyAttackDebuffLow;
        public decimal EnemyAttackDebuffHigh;
        public decimal EnemyStrategicAttackDebuffLow;
        public decimal EnemyStrategicAttackDebuffHigh;
        public decimal EnemyDefenceDebuffLow;
        public decimal EnemyDefenceDebuffHigh;
        public decimal EnemyStrategicDefenceDebuffLow;
        public decimal EnemyStrategicDefenceDebuffHigh;
        public decimal EnemyHealthDebuffLow;
        public decimal EnemyHealthDebuffHigh;
        public decimal EnemyStrategicHealthDebuffLow;
        public decimal EnemyStrategicHealthDebuffHigh;
        public decimal MarchSpeedLow;
        public decimal MarchSpeedHigh;
        public decimal InfantryAttackLow;
        public decimal InfantryAttackHigh;
        public decimal InfantryDefenceLow;
        public decimal InfantryDefenceHigh;
        public decimal StrategicInfantryAttackLow;
        public decimal StrategicInfantryAttackHigh;
        public decimal StrategicInfantryDefenceLow;
        public decimal StrategicInfantryDefenceHigh;
        public decimal InfantryHealthLow;
        public decimal InfantryHealthHigh;
        public decimal StrategicInfantryHealthLow;
        public decimal StrategicInfantryHealthHigh;
        public decimal EnemyInfantryAttackDebuffLow;
        public decimal EnemyInfantryAttackDebuffHigh;
        public decimal StratEnemyInfantryAttackDebuffLow;
        public decimal StratEnemyInfantryAttackDebuffHigh;
        public decimal EnemyInfantryDefenseDebuffLow;
        public decimal EnemyInfantryDefenseDebuffHigh;
        public decimal EnemyStrategicInfantryDefenseDebuffLow;
        public decimal EnemyStrategicInfantryDefenseDebuffHigh;
        public decimal EnemyStrategicInfantryHealthDebuffLow;
        public decimal EnemyStrategicInfantryHealthDebuffHigh;
        public decimal EnemyInfantryHealthDebuffLow;
        public decimal EnemyInfantryHealthDebuffHigh;
        public decimal RangedAttackLow;
        public decimal RangedAttackHigh;
        public decimal RangedDefenceLow;
        public decimal RangedDefenceHigh;
        public decimal StrategicRangedAttackLow;
        public decimal StrategicRangedAttackHigh;
        public decimal StrategicRangedDefenceLow;
        public decimal StrategicRangedDefenceHigh;
        public decimal RangedHealthLow;
        public decimal RangedHealthHigh;
        public decimal StrategicRangedHealthLow;
        public decimal StrategicRangedHealthHigh;
        public decimal EnemyRangedAttackDebuffLow;
        public decimal EnemyRangedAttackDebuffHigh;
        public decimal EnemyStrategicRangedAttackDebuffLow;
        public decimal EnemyStrategicRangedAttackDebuffHigh;
        public decimal EnemyRangedDefenceDebuffLow;
        public decimal EnemyRangedDefenceDebuffHigh;
        public decimal EnemyStrategicRangedDefenceDebuffLow;
        public decimal EnemyStrategicRangedDefenceDebuffHigh;
        public decimal EnemyRangedHealthDebuffLow;
        public decimal EnemyRangedHealthDebuffHigh;
        public decimal EnemyStrategicRangedHealthDebuffLow;
        public decimal EnemyStrategicRangedHealthDebuffHigh;
        public decimal CavalryAttackLow;
        public decimal CavalryAttackHigh;
        public decimal CavalryDefenceLow;
        public decimal CavalryDefenceHigh;
        public decimal StrategicCavalryAttackLow;
        public decimal StrategicCavalryAttackHigh;
        public decimal StrategicCavalryDefenceLow;
        public decimal StrategicCavalryDefenceHigh;
        public decimal CavalryHealthLow;
        public decimal CavalryHealthHigh;
        public decimal StrategicCavalryHealthLow;
        public decimal StrategicCavalryHealthHigh;
        public decimal EnemyCavalryAttackDebuffLow;
        public decimal EnemyCavalryAttackDebuffHigh;
        public decimal StrategicEnemyCavalryAttackDebuffLow;
        public decimal StrategicEnemyCavalryAttackDebuffHigh;
        public decimal EnemyCavalryDefenceDebuffLow;
        public decimal EnemyCavalryDefenceDebuffHigh;
        public decimal StrategicEnemyCavalryDefenceDebuffLow;
        public decimal StrategicEnemyCavalryDefenceDebuffHigh;
        public decimal EnemyCavalryHealthDebuffLow;
        public decimal EnemyCavalryHealthDebuffHigh;
        public decimal StrategicEnemyCavalryHealthDebuffLow;
        public decimal StrategicEnemyCavalryHealthDebuffHigh;
        public decimal SiegeAttackLow;
        public decimal SiegeAttackHigh;
        public decimal SiegeDefenceLow;
        public decimal SiegeDefenceHigh;
        public decimal TrapDefenseLow;
        public decimal TrapDefenseHigh;
        public decimal StrategicTrapDefenseLow;
        public decimal StrategicTrapDefenseHigh;
        public decimal TroopTrainingSpeedLow;
        public decimal TroopTrainingSpeedHigh;
        public decimal EnemySiegeAttackDebuffLow;
        public decimal EnemySiegeAttackDebuffHigh;
        public decimal StrategicTrapAttackLow;
        public decimal StrategicTrapAttackHigh;
        public decimal TrapAttackLow;
        public decimal TrapAttackHigh;
        public decimal HeroCriticalLow;
        public decimal HeroCriticalHigh;
        public decimal MonsterDebuffLow;
        public decimal MonsterDebuffHigh;
    }
}