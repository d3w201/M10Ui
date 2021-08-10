using Controller;
using Script.Enumeral;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Controller.Game
{
    public class GameController : RootController
    {
        //Private-props
        private GameStatus _gameStatus;
        //Start
        private new void Start()
        {
            base.Start();
            Resume();
        }
        //Getter & Setter
        public void SetStatus(GameStatus status)
        {
            _gameStatus = status;
        }
        public GameStatus GetStatus()
        {
            return _gameStatus;
        }
        //Public-methods
        public void HandlePause(InputValue value)
        {
            if (_gameStatus.Equals(GameStatus.Pause))
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        //Private-methods
        private void Pause()
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0f;
            SetStatus(GameStatus.Pause);
        }
        private void Resume()
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
            SetStatus(GameStatus.Play);
        }
    }
}