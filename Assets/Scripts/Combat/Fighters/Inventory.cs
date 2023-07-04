[System.Serializable] public class Item
{
    public string name;
    // public Fighter target;
    public int effect;

    public Item(string name, int effect)
    {
        this.name = name;
        this.effect = effect;
    }

}