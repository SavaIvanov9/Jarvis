﻿using System;
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
        
        private readonly IInteractor _interactor;
        private readonly IDecisionTaker _decisionTaker;
        //private readonly IDataBase data;
        private readonly VoiceController _voiceController;
        private CoomandContainer _coomandContainer = new CoomandContainer();
        public string commandLine = "";
        private bool _isAlive = true;

        private JarvisEngine(IInteractor interactor, IDecisionTaker decisionTaker)
        {
            this._interactor = interactor;
            this._decisionTaker = decisionTaker;
            this._voiceController = new VoiceController(interactor, _coomandContainer);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static JarvisEngine Instance(IInteractor interactor, IDecisionTaker decisionTaker)
        {
            if (interactor == null)
            {
                throw new ArgumentNullException($"Interactor module cannot be null.");
            }

            if (decisionTaker == null)
            {
                throw new ArgumentNullException($"Decision Taker module cannot be null.");
            }

            return new JarvisEngine(interactor, decisionTaker);
        }

        public void Start()
        {
            //Console.Title = "Jarvis";

            _interactor.SendOutput(" >Jarvis: Hi, I am Jarvis");
            _voiceController.Speak("Hi, I am Jarvis");
            
            _voiceController.StartListening();
            //StayAlive();
            
            commandLine = _interactor.RecieveInput(_coomandContainer);
            
            while (_isAlive)
            {
                //_voiceController.StopListening();
                try
                {
                    var commandSegments = _interactor.ParseInput(commandLine);
                    _isAlive = CommandProcessor.Instance.ProcessCommand(commandSegments.Item1, commandSegments.Item2, _interactor);
                }
                catch (Exception ex)
                {
                    _interactor.SendOutput(ex.ToString());
                }
                finally
                {
                    //_voiceController.StartListening();
                    commandLine = _interactor.RecieveInput(_coomandContainer);
                }
            }

            
            _interactor.SendOutput("See ya ;)");
        }

        private void StayAlive()
        {
            while (true)
            {
                
            }
        }
    }
}
