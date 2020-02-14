[System.Serializable]
public class SaveData
{

    public int playerCount;
    public int x;
    public int y;

    public SaveData (int p, int x, int y)
    {
        this.playerCount = p;
        this.x = x;
        this.y = y;
    }

}
