using Zenject;

namespace SA.TestGame.DI
{
    public class MenuMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBindings();
        }


        void SignalBindings()
        {
            SignalBusInstaller.Install(Container);

            //UI
            Container.DeclareSignal<SignalUI.OnClickStageButton>();
            Container.DeclareSignal<SignalUI.OnClickCloseStageButton>();
        }
    }
}