namespace Jarvis.FaceDetection.Recognition
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Windows.Forms;

    using Emgu.CV;
    using Emgu.CV.Face;
    using Emgu.CV.Structure;

    public class Recognizer : IDisposable
    {
        public static readonly string TrainedFacesPath = Path.Combine(Application.StartupPath, "TrainedFaces\\");
        private LBPHFaceRecognizer _recognizer = null;

        private List<Image<Gray, byte>> _faces = new List<Image<Gray, byte>>();
        private List<string> _names = new List<string>();

        public Recognizer()
        {
            IsTrained = Train(TrainedFacesPath);
        }

        public bool IsTrained { get; private set; }

        public RecognizeResult Recognize(Image<Gray, Byte> image)
        {
            RecognizeResult result = new RecognizeResult();

            if (!IsTrained)
            {
                return result;
            }

            FaceRecognizer.PredictionResult predictionResult = _recognizer.Predict(image);

            if (predictionResult.Label == -1)
            {
                result.Name = "unknown";
                return result;
            }

            result.Name = _names[predictionResult.Label];
            result.Distance = predictionResult.Distance;

            return result;
        }

        public bool Retrain()
        {
            return IsTrained = Train(TrainedFacesPath);
        }

        public void Save(string filename)
        {
            _recognizer.Save(filename);
            string path = Path.GetDirectoryName(filename);
            FileStream labels = File.OpenWrite(Path.Combine(path, "Labels.xml"));
            using (XmlWriter writer = XmlWriter.Create(labels))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("labels");

                for (int i = 0; i < _names.Count; ++i)
                {
                    writer.WriteStartElement("label");
                    writer.WriteElementString("name", _names[i]);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            labels.Close();
        }

        public void Load(string filename)
        {
            _recognizer.Load(filename);


            string path = Path.GetDirectoryName(filename);
            FileStream labels = File.OpenRead(Path.Combine(path, "Labels.xml"));
            _names.Clear();

            using (XmlReader reader = XmlTextReader.Create(labels))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "name":
                                _names.Add(reader.GetAttribute("name"));
                                break;
                        }
                    }
                }
            }

            labels.Close();

            IsTrained = true;
        }

        public void Dispose()
        {
            _recognizer.Dispose();
            _faces = null;
            _names = null;

            GC.Collect();
        }

        private bool Train(string folder)
        {
            string facesPath = Path.Combine(folder, "faces.xml");
            if (!File.Exists(facesPath))
            {
                return false;
            }

            try
            {
                _names.Clear();
                _faces.Clear();
                List<int> tmp = new List<int>();
                FileStream facesInfo = File.OpenRead(facesPath);

                using (XmlReader reader = XmlTextReader.Create(facesInfo))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "name":
                                    if (reader.Read())
                                    {
                                        tmp.Add(_names.Count);
                                        _names.Add(reader.Value.Trim());
                                    }
                                    break;
                                case "file":
                                    if (reader.Read())
                                    {
                                        _faces.Add(new Image<Gray, byte>(Path.Combine(Application.StartupPath,
                                            "TrainedFaces",
                                            reader.Value.Trim())));
                                    }
                                    break;
                            }
                        }
                    }
                }

                facesInfo.Close();

                if (_faces.Count == 0)
                {
                    return false;
                }

                _recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
                _recognizer.Train(_faces.ToArray(), tmp.ToArray());

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}