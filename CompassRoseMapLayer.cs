using System.Collections.Generic;
using Cairo;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.Client.NoObf;
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
        private int _color = 0x7FFF00FF;

        private LoadedTexture _compassRoseTextureDialog;
        private LoadedTexture _compassRoseTextureHud;

        private float x, y, w, h, z;


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

            _compassRoseTextureDialog = _capi.Gui.LoadSvgWithPadding(new AssetLocation("compassrose", "textures/compassrose.svg"), 200, 200, 20, null);
        }

        public override void OnMapOpenedClient()
        {
            if (!_mapOpenEventRegistered)
            {
                _mapOpenEventRegistered = true;

                // int num = (int) GuiElement.scaled(32.0);

                // ImageSurface surface = new ImageSurface((Format) 0, num, num);
                // Context cr = new Context((Surface) surface);

                _worldMapManager.worldMapDlg.OnOpened += OnMapDialogOpen;
                OnMapDialogOpen();
            }
        }

        private void OnMapDialogOpen()
        {
            switch (_worldMapManager.worldMapDlg.DialogType)
            {
                default:
                case EnumDialogType.Dialog:
                    _color = 0x7FFFFFFF;
                    break;
                case EnumDialogType.HUD:
                    _color = 0x7FFF00FF;
                    break;
            }

            x = (float)_worldMapManager.worldMapDlg.SingleComposer.Bounds.renderX;
            y = (float)_worldMapManager.worldMapDlg.SingleComposer.Bounds.renderY;
            w = (float)_worldMapManager.worldMapDlg.SingleComposer.Bounds.OuterWidth;
            h = (float)_worldMapManager.worldMapDlg.SingleComposer.Bounds.OuterHeight;
            z = (float)_worldMapManager.worldMapDlg.SingleComposer.zDepth+1;
        }

        public override void Render(GuiElementMap map, float dt)
        {
            // if (map.)
            // _capi.Gui.Text.DrawTextLine();
            // _capi.Gui.Text.DrawTextLine("Test", map.Bounds.renderX, map.Bounds.renderY, );

            _capi.Render.PushScissor(null);
            _capi.Render.RenderRectangle(x, y, z, 100, 100, _color);
            _capi.Render.Render2DTexture(
                _compassRoseTextureDialog.TextureId,
                x,
                y,
                100,
                100,
                z);
            _capi.Render.PopScissor();

            // _capi.Render.RenderTexture();
            // capi.Render.Render2DTexture(
            //     this.colorTexture.TextureId,
            //     (int)(map.Bounds.renderX + viewPos.X),
            //     (int)(map.Bounds.renderY + viewPos.Y),
            //     (int)(this.colorTexture.Width * map.ZoomLevel),
            //     (int)(this.colorTexture.Height * map.ZoomLevel),
            //     50);
        }

        public override void Dispose()
        {
            _compassRoseTextureDialog?.Dispose();
            _compassRoseTextureDialog = (LoadedTexture)null;
            _compassRoseTextureHud?.Dispose();
            _compassRoseTextureHud = (LoadedTexture)null;
        }
    }

}