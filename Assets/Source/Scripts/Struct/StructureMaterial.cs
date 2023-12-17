namespace BuilderStory
{
    public class StructureMaterial
    {
        public bool IsPlaced { get; private set; }

        public BuildMaterial Material { get; private set; }

        public StructureMaterial(BuildMaterial material)
        {
            IsPlaced = false;
            Material = material;
        }

        public void Place()
        {
            IsPlaced = true;
        }
    }
}