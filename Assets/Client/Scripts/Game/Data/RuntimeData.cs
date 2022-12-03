using System.Collections.Generic;

namespace Game
{
    [System.Serializable]
    public class RuntimeData
    {
        public bool IsNewGame;
        public string PreviousScene;
        public bool IsEnd;

        public string PickedItem;
        public List<string> DeliveredItems = new List<string>();
    }
}