namespace Jarvis.FaceDetection.Recognition
{
    public struct RecognizeResult
    {
        private string _name;
        private double _distance;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public double Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                _distance = value;
            }
        }
    }
}
