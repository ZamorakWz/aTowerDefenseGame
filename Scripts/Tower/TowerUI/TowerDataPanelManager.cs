public class TowerDataPanelManager
{
    private TowerDataUI currentOpenPanel;

    public void ShowTowerPanel(TowerDataUI towerDataUI)
    {
        if (currentOpenPanel != null && currentOpenPanel != towerDataUI)
        {
            currentOpenPanel.HidePanel();
        }

        towerDataUI.ShowPanel();
        currentOpenPanel = towerDataUI;
    }

    public void CloseAllPanels()
    {
        if (currentOpenPanel != null)
        {
            currentOpenPanel.HidePanel();
            currentOpenPanel = null;
        }
    }
}