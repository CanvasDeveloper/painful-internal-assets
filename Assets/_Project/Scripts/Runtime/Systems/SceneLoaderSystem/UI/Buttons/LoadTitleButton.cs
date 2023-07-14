using PainfulSmile.Runtime.Systems.SceneLoaderSystem.Core;

namespace PainfulSmile.Runtime.Systems.SceneLoaderSystem.UI.Buttons
{
    public class LoadTitleButton : ButtonBase
    {
        protected override void ButtonAction()
        {
            SceneLoader.Instance.LoadTitleScene();
        }
    }
}