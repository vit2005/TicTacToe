
namespace DesktopClient.ViewModel
{
    public class BattleViewModel
    {
        public string Button00Content { get; set; }
        public string Button01Content { get; set; }
        public string Button02Content { get; set; }
        public string Button10Content { get; set; }
        public string Button11Content { get; set; }
        public string Button12Content { get; set; }
        public string Button20Content { get; set; }
        public string Button21Content { get; set; }
        public string Button22Content { get; set; }

        //commands
        private bool CanMove()
        {
            return true;
        }
    }
}
