using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public void UpgradeFuel()
    {
        UpgradeManager.instance.Upgrade("Fuel", 100); // 100$'a benzin kapasitesi artır
    }

    public void UpgradeSpeed()
    {
        UpgradeManager.instance.Upgrade("Speed", 150); // 150$'a hız artır
    }

    public void UpgradeSuspension()
    {
        UpgradeManager.instance.Upgrade("Suspension", 200); // 200$'a süspansiyon artır
    }
}