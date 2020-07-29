using Zenject;

namespace SA.TenkyuClone
{
    public class GamePlayMonoInstaller : MonoInstaller
    {
        #region Init

        public override void InstallBindings()
        {
            InstallManagers();
            InstallSignalGame();
        }

        #endregion


        #region Managers

        private void InstallManagers()
        {

        }

        #endregion


        #region Signals

        private void InstallSignalGame()
        {
            SignalBusInstaller.Install(Container);

            //Player
            Container.DeclareSignal<SignalGame.CreateBall>();
            Container.DeclareSignal<SignalGame.PlayerRotation>();

            //UI
            Container.DeclareSignal<SignalGame.OnClickResetButton>();
            Container.DeclareSignal<SignalGame.OnClickExitButtton>();
            Container.DeclareSignal<SignalGame.OnClickPointer>();
            Container.DeclareSignal<SignalGame.OnDragPointer>();

        }

        #endregion
    }
}