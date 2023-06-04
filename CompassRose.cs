using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.Common;
using Vintagestory.GameContent;
using VSEssentials.Systems;

namespace CompassRose
{
    public class CompassRose : ModSystem
    {
        ICoreClientAPI capi;

        public override bool ShouldLoad(EnumAppSide forSide)
        {
            return forSide == EnumAppSide.Client;
        }
        
        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);
            capi = api;
            
            var worldMapManager =  api.ModLoader.GetModSystem<WorldMapManager>();
            worldMapManager.RegisterMapLayer<CompassRoseMapLayer>("compassrose");
        }
    }
}