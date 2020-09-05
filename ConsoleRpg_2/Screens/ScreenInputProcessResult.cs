using ConsoleRpg_2.Engine;

namespace ConsoleRpg_2.Screens
{
    public class ScreenInputProcessResult
    {
        public bool RerenderFlag { get; set; }
        public bool UpdateFlag { get; set; }
        public GameState? SwitchState { get; set; }
    }
}