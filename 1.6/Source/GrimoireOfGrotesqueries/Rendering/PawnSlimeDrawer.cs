using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace GrimoireOfGrotesqueries.Rendering;

public class PawnSlimeDrawer(Pawn pawn) : PawnOverlayDrawer(pawn)
{

    public bool _coveredInSlime = false;

    public bool CoveredInSlime
    {
        get => _coveredInSlime;
        set
        {
            if(value == _coveredInSlime) return;
            needRecache = true;
            _coveredInSlime = value;
        }
    }

    public bool needRecache = false;

    public const float TextureScaleFactor = 2.8f;
    public const float TextureTiles = 1.4f;
    public const float TextureOffsetVecMagnitude = 2f;

    private static readonly string[] SlimeTexturePaths =
    [
        "Things/Pawn/Overlays/GOG_Slime/GOG_SlimeOverlayA",
        "Things/Pawn/Overlays/GOG_Slime/GOG_SlimeOverlayB",
        "Things/Pawn/Overlays/GOG_Slime/GOG_SlimeOverlayC",
        "Things/Pawn/Overlays/GOG_Slime/GOG_SlimeOverlayD"
    ];

    protected override void WriteCache(
        CacheKey key,
        PawnDrawParms parms,
        List<DrawCall> writeTarget)
    {
        Rot4 pawnRot = key.pawnRot;
        Mesh bodyMesh = key.bodyMesh;
        OverlayLayer layer = key.layer;
        Graphic graphic = layer == OverlayLayer.Body ? pawn.Drawer.renderer.BodyGraphic : pawn.Drawer.renderer.HeadGraphic;
        Rand.PushState(pawn.thingIDNumber * (int) (layer + 1));
        try
        {
            int num1 = !graphic.EastFlipped || !(pawnRot == Rot4.East) ? (!graphic.WestFlipped ? 0 : (pawnRot == Rot4.West ? 1 : 0)) : 1;
            int index = (Rand.Range(0, SlimeTexturePaths.Length) + pawnRot.AsInt) % SlimeTexturePaths.Length;
            Material material1 = MaterialPool.MatFrom(SlimeTexturePaths[index], ShaderDatabase.FirefoamOverlay, Color.white);
            Mesh mesh = num1 != 0 ? MeshPool.GridPlaneFlip(Vector2.one * 0.25f) : MeshPool.GridPlane(Vector2.one * 0.25f);
            Vector3 size = bodyMesh.bounds.size;
            float num2 = size.magnitude * 2.8f;
            Material material2 = MaterialPool.MatFrom(new MaterialRequest()
            {
                maskTex = (Texture2D) graphic.MatAt(pawnRot).mainTexture, mainTex = material1.mainTexture, color = material1.color, shader = material1.shader
            });
            Vector3 vector3_1 = Rand.InsideUnitCircleVec3 * 2f;
            Vector3 vector3_2 = mesh.bounds.size * num2;
            Vector4 vector4_1 = new(vector3_2.x / size.x, vector3_2.z / size.z);
            Vector4 vector4_2 = new(vector3_1.x, vector3_1.z);
            Vector4 vector4_3 = new(1.4f, 1.4f, 1f, 1f);
            writeTarget.Add(new DrawCall()
            {
                overlayMat = material2,
                matrix = Matrix4x4.Scale(Vector3.one * num2),
                overlayMesh = mesh,
                displayOverApparel = true,
                maskTexScale = vector4_1,
                mainTexScale = vector4_3,
                mainTexOffset = vector4_2
            });
        }
        finally
        {
            Rand.PopState();
        }
    }
}
