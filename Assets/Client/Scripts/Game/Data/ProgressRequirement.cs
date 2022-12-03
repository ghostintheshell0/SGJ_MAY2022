using LeopotamGroup.Globals;

namespace Game
{
    [System.Serializable]
    public class ProgressRequirement
    {
        public CrapData[] DeliveredItems;
        public int DeliveredItemsCount;

        public bool IsCompleted()
        {
            var data = Service<RuntimeData>.Get();

            foreach(var item in DeliveredItems)
            {
                if(!data.DeliveredItems.Contains(item.name))
                {
                    return false;
                }
            }

            return DeliveredItemsCount >= data.DeliveredItems.Count;
        }
    }
}