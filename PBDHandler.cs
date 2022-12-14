using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using SSXMultiTool.Utilities;
using SSXMultiTool.FileHandlers;

namespace SSXMultiTool.FileHandlers.LevelFiles.TrickyPS2
{
    public class PBDHandler
    {
        public byte[] MagicBytes;
        public int NumPlayerStarts;
        public int NumPatches;
        public int NumInstances;
        public int NumParticleInstances;
        public int NumMaterials;
        public int NumMaterialBlocks;
        public int NumLights;
        public int NumSplines;
        public int NumSplineSegments;
        public int NumTextureFlipbooks;
        public int NumModels;
        public int NumParticleModel;
        public int NumTextures;
        public int NumCameras;
        public int LightMapSize;

        public int PlayerStartOffset;
        public int PatchOffset;
        public int InstanceOffset;
        public int ParticleInstancesOffset;
        public int MaterialOffset;
        public int MaterialBlocksOffset;
        public int LightsOffset;
        public int SplineOffset;
        public int SplineSegmentOffset;
        public int TextureFlipbookOffset;
        public int ModelPointerOffset;
        public int ModelsOffset;
        public int ParticleModelPointerOffset;
        public int ParticleModelsOffset;
        public int CameraPointerOffset;
        public int CamerasOffset;
        public int HashOffset;
        public int MeshDataOffset;

        public List<Patch> Patches;
        public List<Spline> splines;
        public List<SplinesSegments> splinesSegments;
        public List<Instance> Instances;
        public List<ParticleInstance> particleInstances;
        public List<Light> lights;

        public void LoadPBD(string path)
        {
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                MagicBytes = StreamUtil.ReadBytes(stream, 4);
                NumPlayerStarts = StreamUtil.ReadInt32(stream); //NA
                NumPatches = StreamUtil.ReadInt32(stream); //Done
                NumInstances = StreamUtil.ReadInt32(stream); //Done
                NumParticleInstances = StreamUtil.ReadInt32(stream); //Done
                NumMaterials = StreamUtil.ReadInt32(stream); //Done
                NumMaterialBlocks = StreamUtil.ReadInt32(stream); //Done
                NumLights = StreamUtil.ReadInt32(stream); //Done
                NumSplines = StreamUtil.ReadInt32(stream); //Done
                NumSplineSegments = StreamUtil.ReadInt32(stream); //Done
                NumTextureFlipbooks = StreamUtil.ReadInt32(stream); //Done
                NumModels = StreamUtil.ReadInt32(stream); //Done
                NumParticleModel = StreamUtil.ReadInt32(stream); //Done
                NumTextures = StreamUtil.ReadInt32(stream); //Done
                NumCameras = StreamUtil.ReadInt32(stream); //Used in SSXFE MAP
                LightMapSize = StreamUtil.ReadInt32(stream); //Always blank?

                PlayerStartOffset = StreamUtil.ReadInt32(stream); //NA
                PatchOffset = StreamUtil.ReadInt32(stream); //Done
                InstanceOffset = StreamUtil.ReadInt32(stream); //Done
                ParticleInstancesOffset = StreamUtil.ReadInt32(stream); //Done
                MaterialOffset = StreamUtil.ReadInt32(stream); //Done
                MaterialBlocksOffset = StreamUtil.ReadInt32(stream); //Done
                LightsOffset = StreamUtil.ReadInt32(stream); //Done 
                SplineOffset = StreamUtil.ReadInt32(stream); //Done
                SplineSegmentOffset = StreamUtil.ReadInt32(stream); //Done
                TextureFlipbookOffset = StreamUtil.ReadInt32(stream); //Done
                ModelPointerOffset = StreamUtil.ReadInt32(stream); //Done
                ModelsOffset = StreamUtil.ReadInt32(stream); //Sort of Loading
                ParticleModelPointerOffset = StreamUtil.ReadInt32(stream); //Done
                ParticleModelsOffset = StreamUtil.ReadInt32(stream); //Sort of Loading
                CameraPointerOffset = StreamUtil.ReadInt32(stream); //Done
                CamerasOffset = StreamUtil.ReadInt32(stream); //Unknown
                HashOffset = StreamUtil.ReadInt32(stream);
                MeshDataOffset = StreamUtil.ReadInt32(stream); //Loading

                //Patch Loading
                stream.Position = PatchOffset;
                Patches = new List<Patch>();
                for (int i = 0; i < NumPatches; i++)
                {
                    Patch patch = new Patch();

                    patch.LightMapPoint = StreamUtil.ReadVector4(stream);

                    patch.UVPoint1 = StreamUtil.ReadVector4(stream);
                    patch.UVPoint2 = StreamUtil.ReadVector4(stream);
                    patch.UVPoint3 = StreamUtil.ReadVector4(stream);
                    patch.UVPoint4 = StreamUtil.ReadVector4(stream);

                    patch.R4C4 = StreamUtil.ReadVector4(stream);
                    patch.R4C3 = StreamUtil.ReadVector4(stream);
                    patch.R4C2 = StreamUtil.ReadVector4(stream);
                    patch.R4C1 = StreamUtil.ReadVector4(stream);
                    patch.R3C4 = StreamUtil.ReadVector4(stream);
                    patch.R3C3 = StreamUtil.ReadVector4(stream);
                    patch.R3C2 = StreamUtil.ReadVector4(stream);
                    patch.R3C1 = StreamUtil.ReadVector4(stream);
                    patch.R2C4 = StreamUtil.ReadVector4(stream);
                    patch.R2C3 = StreamUtil.ReadVector4(stream);
                    patch.R2C2 = StreamUtil.ReadVector4(stream);
                    patch.R2C1 = StreamUtil.ReadVector4(stream);
                    patch.R1C4 = StreamUtil.ReadVector4(stream);
                    patch.R1C3 = StreamUtil.ReadVector4(stream);
                    patch.R1C2 = StreamUtil.ReadVector4(stream);
                    patch.R1C1 = StreamUtil.ReadVector4(stream);

                    patch.LowestXYZ = StreamUtil.ReadVector3(stream);
                    patch.HighestXYZ = StreamUtil.ReadVector3(stream);

                    patch.Point1 = StreamUtil.ReadVector4(stream);
                    patch.Point2 = StreamUtil.ReadVector4(stream);
                    patch.Point3 = StreamUtil.ReadVector4(stream);
                    patch.Point4 = StreamUtil.ReadVector4(stream);

                    patch.PatchStyle = StreamUtil.ReadInt32(stream);
                    patch.Unknown2 = StreamUtil.ReadInt32(stream);
                    patch.TextureAssigment = StreamUtil.ReadInt16(stream);

                    patch.LightmapID = StreamUtil.ReadInt16(stream);

                    patch.Unknown4 = StreamUtil.ReadInt32(stream);
                    patch.Unknown5 = StreamUtil.ReadInt32(stream);
                    patch.Unknown6 = StreamUtil.ReadInt32(stream);
                    Patches.Add(patch);
                }

                stream.Position = InstanceOffset;
                Instances = new List<Instance>();
                for (int i = 0; i < NumInstances; i++)
                {
                    var TempInstance = new Instance();
                    TempInstance.MatrixCol1 = StreamUtil.ReadVector4(stream);
                    TempInstance.MatrixCol2 = StreamUtil.ReadVector4(stream);
                    TempInstance.MatrixCol3 = StreamUtil.ReadVector4(stream);
                    TempInstance.InstancePosition = StreamUtil.ReadVector4(stream);
                    TempInstance.Unknown5 = StreamUtil.ReadVector4(stream);
                    TempInstance.Unknown6 = StreamUtil.ReadVector4(stream);
                    TempInstance.Unknown7 = StreamUtil.ReadVector4(stream);
                    TempInstance.Unknown8 = StreamUtil.ReadVector4(stream);
                    TempInstance.Unknown9 = StreamUtil.ReadVector4(stream);
                    TempInstance.Unknown10 = StreamUtil.ReadVector4(stream);
                    TempInstance.Unknown11 = StreamUtil.ReadVector4(stream);
                    TempInstance.RGBA = StreamUtil.ReadVector4(stream);
                    TempInstance.ModelID = StreamUtil.ReadInt32(stream);
                    TempInstance.PrevInstance = StreamUtil.ReadInt32(stream);
                    TempInstance.NextInstance = StreamUtil.ReadInt32(stream);

                    TempInstance.LowestXYZ = StreamUtil.ReadVector3(stream);
                    TempInstance.HighestXYZ = StreamUtil.ReadVector3(stream);

                    TempInstance.UnknownInt26 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt27 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt28 = StreamUtil.ReadInt32(stream);
                    TempInstance.ModelID2 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt30 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt31 = StreamUtil.ReadInt32(stream);
                    TempInstance.UnknownInt32 = StreamUtil.ReadInt32(stream);
                    Instances.Add(TempInstance);
                }

                stream.Position = ParticleInstancesOffset;
                particleInstances = new List<ParticleInstance>();
                for (int i = 0; i < NumParticleInstances; i++)
                {
                    ParticleInstance TempParticle = new ParticleInstance();
                    TempParticle.MatrixCol1 = StreamUtil.ReadVector4(stream);
                    TempParticle.MatrixCol2 = StreamUtil.ReadVector4(stream);
                    TempParticle.MatrixCol3 = StreamUtil.ReadVector4(stream);
                    TempParticle.ParticleInstancePosition = StreamUtil.ReadVector4(stream);
                    TempParticle.UnknownInt1 = StreamUtil.ReadInt32(stream);
                    TempParticle.LowestXYZ = StreamUtil.ReadVector3(stream);
                    TempParticle.HighestXYZ = StreamUtil.ReadVector3(stream);
                    TempParticle.UnknownInt8 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt9 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt10 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt11 = StreamUtil.ReadInt32(stream);
                    TempParticle.UnknownInt12 = StreamUtil.ReadInt32(stream);
                    particleInstances.Add(TempParticle);
                }

                stream.Position = LightsOffset;
                lights = new List<Light>();
                for (int i = 0; i < NumLights; i++)
                {
                    var TempLights = new Light();
                    TempLights.Type = StreamUtil.ReadInt32(stream);
                    TempLights.spriteRes = StreamUtil.ReadInt32(stream);
                    TempLights.UnknownFloat1 = StreamUtil.ReadFloat(stream);
                    TempLights.UnknownInt1 = StreamUtil.ReadInt32(stream);
                    TempLights.Colour = StreamUtil.ReadVector3(stream);
                    TempLights.Direction = StreamUtil.ReadVector3(stream);
                    TempLights.Postion = StreamUtil.ReadVector3(stream);
                    TempLights.LowestXYZ = StreamUtil.ReadVector3(stream);
                    TempLights.HighestXYZ = StreamUtil.ReadVector3(stream);
                    TempLights.UnknownFloat2 = StreamUtil.ReadInt32(stream);
                    TempLights.UnknownInt2 = StreamUtil.ReadInt32(stream);
                    TempLights.UnknownFloat3 = StreamUtil.ReadInt32(stream);
                    TempLights.UnknownInt3 = StreamUtil.ReadInt32(stream);
                    lights.Add(TempLights);
                }

                //Spline Data
                stream.Position = SplineOffset;
                splines = new List<Spline>();
                for (int i = 0; i < NumSplines; i++)
                {
                    Spline spline = new Spline();
                    spline.LowestXYZ = StreamUtil.ReadVector3(stream);
                    spline.HighestXYZ = StreamUtil.ReadVector3(stream);
                    spline.Unknown1 = StreamUtil.ReadInt32(stream);
                    spline.SplineSegmentCount = StreamUtil.ReadInt32(stream);
                    spline.SplineSegmentPosition = StreamUtil.ReadInt32(stream);
                    spline.Unknown2 = StreamUtil.ReadInt32(stream);
                    splines.Add(spline);
                }

                //Spline Segments
                stream.Position = SplineSegmentOffset;
                splinesSegments = new List<SplinesSegments>();
                for (int i = 0; i < NumSplineSegments; i++)
                {
                    SplinesSegments splinesSegment = new SplinesSegments();

                    splinesSegment.Point4 = StreamUtil.ReadVector4(stream);
                    splinesSegment.Point3 = StreamUtil.ReadVector4(stream);
                    splinesSegment.Point2 = StreamUtil.ReadVector4(stream);
                    splinesSegment.ControlPoint = StreamUtil.ReadVector4(stream);

                    splinesSegment.ScalingPoint = StreamUtil.ReadVector4(stream);

                    splinesSegment.PreviousSegment = StreamUtil.ReadInt32(stream);
                    splinesSegment.NextSegment = StreamUtil.ReadInt32(stream);
                    splinesSegment.SplineParent = StreamUtil.ReadInt32(stream);

                    splinesSegment.LowestXYZ = StreamUtil.ReadVector3(stream);
                    splinesSegment.HighestXYZ = StreamUtil.ReadVector3(stream);

                    splinesSegment.SegmentDisatnce = StreamUtil.ReadFloat(stream);
                    splinesSegment.PreviousSegmentsDistance = StreamUtil.ReadFloat(stream);
                    splinesSegment.Unknown32 = StreamUtil.ReadInt32(stream);
                    splinesSegments.Add(splinesSegment);
                }
            }
        }
    }

    public struct Spline
    {
        public Vector3 LowestXYZ;
        public Vector3 HighestXYZ;
        public int Unknown1;
        public int SplineSegmentCount;
        public int SplineSegmentPosition;
        public int Unknown2;
    }

    public struct SplinesSegments
    {
        public Vector4 Point4;
        public Vector4 Point3;
        public Vector4 Point2;
        public Vector4 ControlPoint;
        public Vector4 ScalingPoint; //Not really sure about that
        public int PreviousSegment;
        public int NextSegment;
        public int SplineParent;
        public Vector3 LowestXYZ;
        public Vector3 HighestXYZ;
        public float SegmentDisatnce;
        public float PreviousSegmentsDistance;
        public int Unknown32;
    }

    public struct Instance
    {
        public Vector4 MatrixCol1;
        public Vector4 MatrixCol2;
        public Vector4 MatrixCol3;
        public Vector4 InstancePosition;
        public Vector4 Unknown5;
        public Vector4 Unknown6;
        public Vector4 Unknown7;
        public Vector4 Unknown8;
        public Vector4 Unknown9;
        public Vector4 Unknown10;
        public Vector4 Unknown11;
        public Vector4 RGBA;
        public int ModelID;
        public int PrevInstance;
        public int NextInstance;

        public Vector3 LowestXYZ;
        public Vector3 HighestXYZ;

        public int UnknownInt26;
        public int UnknownInt27;
        public int UnknownInt28;
        public int ModelID2;
        public int UnknownInt30;
        public int UnknownInt31;
        public int UnknownInt32;

        public int LTGState;
    }

    public struct ParticleInstance
    {
        public Vector4 MatrixCol1;
        public Vector4 MatrixCol2;
        public Vector4 MatrixCol3;
        public Vector4 ParticleInstancePosition;
        public int UnknownInt1;
        public Vector3 LowestXYZ;
        public Vector3 HighestXYZ;
        public int UnknownInt8;
        public int UnknownInt9;
        public int UnknownInt10;
        public int UnknownInt11;
        public int UnknownInt12;
    }

    public struct Light
    {
        public int Type;
        public int spriteRes;
        public float UnknownFloat1;
        public int UnknownInt1;
        public Vector3 Colour;
        public Vector3 Direction;
        public Vector3 Postion;
        public Vector3 LowestXYZ;
        public Vector3 HighestXYZ;
        public float UnknownFloat2;
        public int UnknownInt2;
        public float UnknownFloat3;
        public int UnknownInt3;
    }


    public struct Patch
    {
        public Vector4 LightMapPoint;

        public Vector4 UVPoint1;
        public Vector4 UVPoint2;
        public Vector4 UVPoint3;
        public Vector4 UVPoint4;

        public Vector4 R4C4;
        public Vector4 R4C3;
        public Vector4 R4C2;
        public Vector4 R4C1;
        public Vector4 R3C4;
        public Vector4 R3C3;
        public Vector4 R3C2;
        public Vector4 R3C1;
        public Vector4 R2C4;
        public Vector4 R2C3;
        public Vector4 R2C2;
        public Vector4 R2C1;
        public Vector4 R1C4;
        public Vector4 R1C3;
        public Vector4 R1C2;
        public Vector4 R1C1;

        public Vector3 LowestXYZ;
        public Vector3 HighestXYZ;

        public Vector4 Point1;
        public Vector4 Point2;
        public Vector4 Point3;
        public Vector4 Point4;

        //0 - Reset
        //1 - Standard Snow
        //2 - Standard Off Track?
        //3 - Powered Snow
        //4 - Slow Powered Snow
        //5 - Ice Standard
        //6 - Bounce/Unskiiable //
        //7 - Ice/Water No Trail
        //8 - Glidy(Lots Of snow particels) //
        //9 - Rock 
        //10 - Wall
        //11 - No Trail, Ice Crunch Sound Effect//
        //12 - No Sound, No Trail, Small particle Wake//
        //13 - Off Track Metal (Slidly Slow, Metal Grinding sounds, Sparks)
        //14 - Speed, Grinding Sound//
        //15 - Standard?//
        //16 - Standard Sand
        //17 - ?//
        //18 - Show Off Ramp/Metal
        public int PatchStyle; //Type

        public int Unknown2; // Some Kind of material Assignment Or Lighting
        public int TextureAssigment; // Texture Assigment 
        public int LightmapID;
        public int Unknown4; //Negative one
        public int Unknown5; //Same
        public int Unknown6; //Same
    }
}
