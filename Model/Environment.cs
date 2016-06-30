namespace ModelNS
{
    public sealed class Environment
    {
        public double PathLossCoeficient { get; private set; }
        public double ReferenceDistance { get; private set; }
        public double ReferenceRSSIVal { get; private set; }

        public Environment() : this(1d, 1d, 100d) { }

        public Environment(double pathLossCoeficient) : this(pathLossCoeficient, 1d, 100d) { }

        public Environment(double referenceDistance, double referenceRSSIVal) : this(1d, referenceDistance, referenceRSSIVal) { }

        public Environment(double pathLossCoeficient, double referenceDistance, double referenceRSSIVal)
        {
            PathLossCoeficient = pathLossCoeficient;
            ReferenceDistance = referenceDistance;
            ReferenceRSSIVal = referenceRSSIVal;
        }
    }
}
