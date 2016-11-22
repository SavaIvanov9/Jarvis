using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Jarvis.Logic.CommandControl;
using Jarvis.Logic.Core.Interfaces.Decisions;
using Jarvis.Logic.Interaction;
using Jarvis.Logic.Interaction.Interfaces;

namespace Jarvis.Logic.Core
{
    public class JarvisEngine
    {

        private readonly IInteractor _interactor = new ConsoleInteractor();
        private readonly IDecisionTaker _decisionTaker;
        private readonly InteractorManager _interactorManager;
        //private readonly IDataBase data;
        private readonly VoiceInteractor _voiceController;
        public string commandLine = "";
        private bool _isAlive = true;
        

        private JarvisEngine(InteractorManager manager)
        {
            this._interactorManager = manager;
            //this._interactor = interactor;
            //this._decisionTaker = decisionTaker;
            this._voiceController = new VoiceInteractor();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static JarvisEngine Instance(InteractorManager manager)
        {
            if (manager.Interactors.Count == 0)
            {
                throw new InvalidEnumArgumentException($"Interactors cannot be 0!");
            }

            return new JarvisEngine(manager);
        }

        public void Start()
        {

            try
            {
                //Console.Title = "Jarvis";

                //_interactor.SendOutput(" >Jarvis: Hi, I am Jarvis");
                //_voiceController.Speak("Hi, I am Jarvis");

                CommandProcessor.Instance.Start(_interactor);
                StartInteractors();

                StayAlive(_interactor);
                //while (_isAlive)
                //{
                //    //_voiceController.StopListening();
                //    try
                //    {
                //        //var commandSegments = _interactor.ParseInput(commandLine);
                //        CommandProcessor.Instance.Start(commandLine, _interactor);
                //    }
                //    catch (Exception ex)
                //    {
                //        _interactor.SendOutput(ex.ToString());
                //    }
                //    finally
                //    {
                //        //_voiceController.StartListening();
                //        commandLine = _interactor.RecieveInput();
                //    }
                //}
            }
            catch (Exception ex)
            {
                _interactor.SendOutput(ex.ToString());
            }
        }

        private void StartInteractors()
        {
            foreach (var interactor in _interactorManager.Interactors)
            {
                interactor.Start();
            }
        }

        private void StayAlive(IInteractor interactor)
        {
            while (true)
            {
                interactor.RecieveInput();
            }
        }
    }
}
