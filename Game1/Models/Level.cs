using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using Game1.Interfaces;
using Game1.Core;

using RogueSharp.Random;
using RogueSharp;
using RogueSharp.MapCreation;

namespace Game1.Models
{
    public class Level : ILevel
    {
        #region --- private fields ---
        private IMap _dungeonMap;

        private List<Enemie> _enemies;
        private List<IItem> _item;
        #endregion

        #region --- Properties ---
        public IEnumerable<DrawCell> GetMapToDraw { get; private set; }
        #endregion

        #region --- constructors ---
        public Level(IMapCreationStrategy<Map> mapCreationStrategy)
        {
            _dungeonMap = Map.Create(mapCreationStrategy);

            _enemies = new List<Enemie>()
            {
                new Enemie(GetRandomWalkableCell(), "g", 4){Color = Color.GreenYellow}
            };
            _item = new List<IItem>()
            {
                new Weapon(GetRandomWalkableCell()){Name = "Eskalibur", Description = "Bardzo wazny miecz z legendy o krolu Arturze."}
            };
        }
        #endregion

        #region --- public methods ---
        public void ItemDroped(IItem item)
        {
            _item.Add(item);
        }

        public IItem TakeItem(int x, int y)
        {
            IItem item = null;
            if (_item.Any(i => i.X == x && i.Y == y))
            {
                item = _item.FirstOrDefault(i => i.X == x && i.Y == y);
                _item.Remove(item);
            }
            return item;
        }

        public void FirstView(IActor player)
        {
            ComputeFov(player.X, player.Y, player.RangeFov);
            UpdateMapToDraw(player);
        }

        public ICell GetRandomWalkableCell(Random random = null)
        {
            if (random == null) random = new Random();

            ICell cell = new Cell(-1, -1, false, false, false);
            while (!cell.IsWalkable)
            {
                cell = _dungeonMap.GetCell(random.Next(0, _dungeonMap.Width - 1), random.Next(0, _dungeonMap.Height - 1));
            }

            return cell;
        }

        public void SetWalkable(int x, int y, bool isWalkable)
        {
            ICell cell = _dungeonMap.GetCell(x, y);
            _dungeonMap.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
            _dungeonMap.Save();
        }

        public bool SetActorPosition(IActor actor, int x, int y)
        {
            if (_dungeonMap.GetCell(x, y).IsWalkable)
            {
                SetWalkable(actor.X, actor.Y, true);
                actor.X = x;
                actor.Y = y;
                SetWalkable(x, y, false);
                return true;
            }

            return false;
        }

        public void ComputeFov(int centreX, int centreY, int range)
        {
            var ListFov = _dungeonMap.ComputeFov(centreX, centreY, range, true);

            foreach (ICell cell in ListFov)
            {
                if (!cell.IsExplored)
                {
                    _dungeonMap.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                    _dungeonMap.Save();
                }
            }
        }

        public void UpdateMapToDraw(IActor player)
        {
            GetMapToDraw = GetCellToDraw(player.X, player.Y);
        }

        private IEnumerable<DrawCell> GetCellToDraw(int centreX, int centreY, int radius = 12)
        {
            List<DrawCell> MapView = new List<DrawCell>();
            foreach (var cell in _dungeonMap.GetAllCells().Where(c => c.IsExplored && (c.X >= centreX - radius && c.X <= centreX + radius) && (c.Y >= centreY - radius && c.Y <= centreY + radius)))
            {
                DrawCell dc = new DrawCell()
                {
                    Ox = (radius + 1) - (centreX - cell.X),
                    Oy = (radius + 1) - (centreY - cell.Y),
                    ParentCell = cell,
                    Glyph = cell.IsWalkable ? "." : "#",
                    Color = cell.IsInFov ? Color.White : Color.DarkGray,
                    Type = cell.IsWalkable ? TypeObject.None : TypeObject.Wall
                };

                if (_item.Any(c => c.X == cell.X && c.Y == cell.Y))
                {
                    IItem item = _item.Single(c => c.X == cell.X && c.Y == cell.Y);
                    dc.Glyph = item.Glyph;
                    dc.Type = TypeObject.Item;
                    dc.Color = item.Color;
                }

                if (_enemies.Any(c => c.X == cell.X && c.Y == cell.Y))
                {
                    Enemie enemie = _enemies.Single(c => c.X == cell.X && c.Y == cell.Y);
                    dc.Glyph = enemie.Glyph;
                    dc.Type = TypeObject.Npc;
                    dc.Color = enemie.Color;
                }

                MapView.Add(dc);

            }
            return MapView;
        }

        public void NPCTurn(IActor player)
        {
            foreach (Enemie enemie in _enemies)
            {
                FieldOfView monsterFov = new FieldOfView(_dungeonMap);
                monsterFov.ComputeFov(enemie.X, enemie.Y, enemie.RangeFov, true);

                if (monsterFov.IsInFov(player.X, player.Y))
                {
                    SetWalkable(player.X, player.Y, true);
                    SetWalkable(enemie.X, enemie.Y, true);

                    ICell cellPlayer = _dungeonMap.GetCell(player.X, player.Y);
                    ICell cellEnemie = _dungeonMap.GetCell(enemie.X, enemie.Y);

                    PathFinder pathFinder = new PathFinder(_dungeonMap);
                    Path path = pathFinder.TryFindShortestPath(cellEnemie, cellPlayer);

                    SetWalkable(player.X, player.Y, false);
                    SetWalkable(enemie.X, enemie.Y, false);

                    if (path != null)
                    {
                        ICell cell = path.TryStepForward();
                        SetActorPosition(enemie, cell.X, cell.Y);
                    }
                }
                else
                {
                    //Czy się poruszy w losowe miejsce?
                    if (Roll.Dice.D100() < 50)
                    {
                        DotNetRandom dot = new DotNetRandom();

                        List<ICell> cells = _dungeonMap.GetCellsInSquare(enemie.X, enemie.Y, 1).Where(c => c.IsWalkable).ToList();
                        ICell cell = cells[(dot.Next(0, cells.Count() - 1))];
                        if (cell != null) SetActorPosition(enemie, cell.X, cell.Y);
                    }
                }
            }
        }
        #endregion
    }
}
