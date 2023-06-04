using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace CompassRose
{
    public class CompassRoseMapLayer : MapLayer
    {
        private ICoreClientAPI _capi;
        private WorldMapManager _worldMapManager;
        public override string Title => "CompassRoseOverlay";
        public override EnumMapAppSide DataSide => EnumMapAppSide.Client;
        private bool _mapOpenEventRegistered = false;
        private int _color = 0x7FFFFFFF;


        public CompassRoseMapLayer(ICoreAPI api, IWorldMapManager mapSink) : base(api, mapSink)
        {
            _capi = api as ICoreClientAPI;
        }

        public override void OnLoaded()
        {
            if (_capi == null)
                return;
            _capi.Logger.Debug("IT WORKS");
            _worldMapManager = api.ModLoader.GetModSystem<WorldMapManager>();
        }

        public override void OnMapOpenedClient()
        {
            if (!_mapOpenEventRegistered)
            {
                _mapOpenEventRegistered = true;
                _worldMapManager.worldMapDlg.OnOpened += OnMapDialogOpen;
            }
        }

        private void OnMapDialogOpen()
        {
            switch (_worldMapManager.worldMapDlg.DialogType)
            {
                case EnumDialogType.Dialog:
                    _color = 0x7FFFFFFF;
                    return;
                case EnumDialogType.HUD:
                    _color = 0x7FFF00FF;
                    return;
            }
        }

        public override void Render(GuiElementMap map, float dt)
        {
            // if (map.)
            // _capi.Gui.Text.DrawTextLine();
            // _capi.Gui.Text.DrawTextLine("Test", map.Bounds.renderX, map.Bounds.renderY, );
            _capi.Render.PushScissor(null);
            _capi.Render.RenderRectangle((int)map.Bounds.renderX-20, (int)map.Bounds.renderY-20, 1000, 50, 50, _color);
            _capi.Render.PopScissor();


            // capi.Render.Render2DTexture(
            //     this.colorTexture.TextureId,
            //     (int)(map.Bounds.renderX + viewPos.X),
            //     (int)(map.Bounds.renderY + viewPos.Y),
            //     (int)(this.colorTexture.Width * map.ZoomLevel),
            //     (int)(this.colorTexture.Height * map.ZoomLevel),
            //     50);
}
        }
    }