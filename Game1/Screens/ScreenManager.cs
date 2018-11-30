using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Screens
{
    public class ScreenManager
    {
        public MapScreen MapScreen { get; private set; }
        public InventoryScreen InventoryScreen { get; private set; }

        public ScreenManager(MapScreen mapScreen, InventoryScreen inventoryScreen)
        {
            MapScreen = mapScreen;
            InventoryScreen = inventoryScreen;
        }
    }
}
