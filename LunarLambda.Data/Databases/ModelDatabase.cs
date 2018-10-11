using System.Collections.Generic;
using System.Drawing;

using OpenTK;

namespace LunarLambda.Data.Databases
{
    public class ModelData
    {
        public class EngineEmitterData
        {
            public Vector3 Position = new Vector3();
            public Color ExaustColor = Color.Wheat;
            public float ExauseScale = 1.0f;

            public EngineEmitterData(Vector3 pos, Color color, float scale)
            {
                Position = new Vector3(pos);
                ExauseScale = scale;
                ExaustColor = color;
            }
        }

        internal string Name = string.Empty;
        internal string MeshName = string.Empty;

        internal string TextureName = string.Empty;
        internal string SpecularTextureName = string.Empty;
        internal string IlluminationTextureName = string.Empty;

        internal string MaterialName = string.Empty;

        internal bool Loaded = false;

        internal Vector3 MeshOffset = new Vector3();
        internal float Scale = 1.0f;
        internal float Radius = 1.0f;
        internal Vector2 CollisionBox = new Vector2();

        internal List<Vector3> BeamPositions = new List<Vector3>();
        public Vector3[] Beams() { return BeamPositions.ToArray(); }

        internal List<Vector3> TubePositions = new List<Vector3>();
        public Vector3[] Tubes() { return TubePositions.ToArray(); }

        internal List<EngineEmitterData> EngineLocations = new List<EngineEmitterData>();
        public EngineEmitterData[] Engines() { return EngineLocations.ToArray(); }


        public ModelData SetName(string value) { Name = value; return this; }
        public ModelData SetMesh(string value) { MeshName = value; return this; }
        public ModelData SetTexture(string value) { TextureName = value; return this; }
        public ModelData SetSpecular(string value) { SpecularTextureName = value; return this; }
        public ModelData SetIllumination(string value) { IlluminationTextureName = value; return this; }
        public ModelData SetRenderOffset(Vector3 value) { MeshOffset = new Vector3(value); return this; }
        public ModelData SetRenderOffset(float x, float y, float z) { MeshOffset = new Vector3(x, y, z); return this; }
        public ModelData SetRenderOffset(double x, double y, double z) { MeshOffset = new Vector3((float)x, (float)y, (float)z); return this; }
        public ModelData SetScale(float value) { Scale = value; return this; }
        public ModelData SetRadius(float value) { Radius = value; return this; }
        public ModelData SetCollisionBox(Vector2 value) { CollisionBox = new Vector2(value.X,value.Y); return this; }
        public ModelData SetCollisionBox(float width, float height) { CollisionBox = new Vector2(width, height); return this; }

        public ModelData AddBeamPosition(Vector3 value) { BeamPositions.Add(value); return this; }
        public ModelData AddBeamPosition(float x, float y, float z) { BeamPositions.Add(new Vector3(x, y, z)); return this; }
        public ModelData AddBeamPosition(double x, double y, double z) { BeamPositions.Add(new Vector3((float)x, (float)y, (float)z)); return this; }

        public ModelData AddTubePosition(Vector3 value) { TubePositions.Add(value); return this; }
        public ModelData AddTubePosition(float x, float y, float z) { TubePositions.Add(new Vector3(x, y, z)); return this; }
        public ModelData AddTubePosition(double x, double y, double z) { TubePositions.Add(new Vector3((float)x, (float)y, (float)z)); return this; }


        public ModelData AddEngineEmitter(Vector3 pos, Color color, float scale = 1.0f) { EngineLocations.Add(new EngineEmitterData(pos, color, scale)); return this; }

        public ModelData AddEngineEmitter(float x, float y, float z, float r, float g, float b, float scale = 1.0f)
        {
            Vector3 pos = new Vector3(x, y, z);
            Color color = Color.FromArgb((int)(r * byte.MaxValue), (int)(g * byte.MaxValue),(int)(b * byte.MaxValue));

            EngineLocations.Add(new EngineEmitterData(pos, color, scale));
            return this;
        }

        public ModelData AddEngineEmitter(double x, double y, double z, double r, double g, double b, double scale = 1.0f)
        {
            Vector3 pos = new Vector3((float)x, (float)y, (float)z);
            Color color = Color.FromArgb((int)(r * byte.MaxValue), (int)(g * byte.MaxValue), (int)(b * byte.MaxValue));

            EngineLocations.Add(new EngineEmitterData(pos, color, (float)scale));
            return this;
        }
    }

    public static class ModelDatabase
    {
        internal static List<ModelData> Models = new List<ModelData>();

        public static ModelData AddModel(ModelData model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name))
                return null;

            Models.Add(model);
            return model;
        }

        public static ModelData AddModel(string name)
        {
            ModelData model = new ModelData();
            model.Name = name;
            return AddModel(model);
        }

        public static ModelData GetModel(string name)
        {
            return Models.Find((x) => x.Name == name);
        }
    }
}
