using SA.TestGame.Data;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SO_Installer", menuName = "Installers/SO_Installer")]
public class SO_Installer : ScriptableObjectInstaller<SO_Installer>
{
    #region Var

    [SerializeField] DataGame dataGame;

    #endregion


    public override void InstallBindings()
    {
        Container.BindInstances(dataGame);
    }
}