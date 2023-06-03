using Vintagestory.API.Client;
using Vintagestory.API.Common;

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
        }
    }
}